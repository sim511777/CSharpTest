using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.Reflection;
using System.Collections;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using System.Diagnostics;
using ShimLib;

namespace CSharpTest {
    public partial class FormMain : Form {
        public FormMain() {
            InitializeComponent();
            
            this.tbxCode.Text = Properties.Resources.Test;

            // 문법강조 : 넷중 어느것도 됨
            //this.tbxCode.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
            //this.tbxCode.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategyForFile(".cs");
            //this.tbxCode.Document.HighlightingStrategy = HighlightingManager.Manager.FindHighlighter("C#");
            this.tbxCode.Document.HighlightingStrategy = HighlightingManager.Manager.FindHighlighterForFile(".cs");

            // 함수목록 추가
            this.InitFunctionList();

            // 콘솔아웃 리다이렉션
            Console.SetOut(new TextBoxWriter(this.tbxConsole));
        }

        private void InitFunctionList() {
            var type = typeof(Test);
            MethodInfo[] mis = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
            this.lbxFunc.DisplayMember = "Name";
            this.lbxFunc.Items.AddRange(mis);
            if (this.lbxFunc.Items.Count > 0)
                this.lbxFunc.SelectedIndex = 0;
        }

        // 함수 실행
        private void RunMethod() {
            var sw = Stopwatch.StartNew();

            var method = grdParameter.Tag as MethodInfo;
            var prmNameList = method.GetParameters().Select(prm => prm.Name);
            if (method != null) {
                Console.WriteLine($"== {method.Name} ==");
                try {
                    var cs = this.grdParameter.SelectedObject as CustomClass;
                    var prms = cs.Cast<CustomProperty>().Select(prop => prop.Value).ToArray();
                    object r = method.Invoke(this, prms);
                    sw.Stop();
                    Console.WriteLine($"=> {sw.ElapsedMilliseconds}ms");
                } catch (TargetInvocationException ex) {
                    Console.WriteLine($"=> Fail: {ex.InnerException.Message}");
                } catch (Exception ex) {
                    Console.WriteLine($"=> Fail: {ex.Message}");
                }
                Console.WriteLine();
            }
        }

        private void lbxTest_SelectedIndexChanged(object sender, EventArgs e) {
            var mi = this.lbxFunc.SelectedItem as MethodInfo;
            var paramInfos = mi.GetParameters();
            CustomClass cs = new CustomClass();
            foreach (var pi in paramInfos) {
                string itemName = pi.Name;
                object itemValue = pi.HasDefaultValue ? pi.DefaultValue : Activator.CreateInstance(pi.ParameterType);
                Type itemType = pi.ParameterType;
                bool itemReadOnly = false;
                bool itemVisible = true;
                string itemDescription = pi.ParameterType.Name;
                string itemCategory = "Parameter";
                CustomProperty cp = new CustomProperty(itemName, itemValue, itemType, itemReadOnly, itemVisible, itemDescription, itemCategory);
                cs.Add(cp);
            }
            grdParameter.SelectedObject = cs;
            grdParameter.Refresh();
            grdParameter.Tag = mi;

            this.RunMethod();
        }

        private void grdParameter_PropertyValueChanged(object s, PropertyValueChangedEventArgs e) {
            this.RunMethod();
        }

        private void btnClear_Click(object sender, EventArgs e) {
            this.tbxConsole.Clear();
        }

        private void btnRunTest_Click(object sender, EventArgs e)
        {
            this.RunMethod();
        }

        List<int> matchIdx = new List<int>();
        int hilightIdx = -1;

        private void tbxSearch_TextChanged(object sender, EventArgs e) {
            matchIdx.Clear();
            hilightIdx = -1;

            string searchText = tbxSearch.Text.ToLower();
            if (searchText != string.Empty) {
                var items = lbxFunc.Items.Cast<MethodInfo>().Select((mi, idx) => new { methodName = mi.Name.ToLower(), methodIdx = idx });
                var searchIdxs = items.Where(item => item.methodName.Contains(searchText)).Select(item => item.methodIdx);
                matchIdx.AddRange(searchIdxs);
            }

            if (matchIdx.Count == 0) {
                lblHilightIndex.Text = "(0/0)";
            } else {
                int hiIdx = 0;
                SetHightlightIndex(hiIdx);
            }
            lbxFunc.Invalidate();
        }

        private void SetHightlightIndex(int hiIdx) {
            hilightIdx = matchIdx[hiIdx];
            lblHilightIndex.Text = $"({hiIdx + 1}/{matchIdx.Count})";
            lbxFunc.TopIndex = matchIdx[hiIdx];
        }

        private void btnSearch_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode != Keys.Enter)
                return;

            if (matchIdx.Count == 0)
                return;

            var matchIdxIdx = matchIdx.IndexOf(hilightIdx);
            int hiIdx = (matchIdxIdx + 1) % matchIdx.Count;
            SetHightlightIndex(hiIdx);
            lbxFunc.Invalidate();
        }

        private void lbxFunc_DrawItem(object sender, DrawItemEventArgs e) {
            var g = e.Graphics;

            Brush textBrush;
            if (e.State.HasFlag(DrawItemState.Selected)) {
                e.DrawBackground();
                textBrush = Brushes.White;
            } else {
                if (!matchIdx.Contains(e.Index)) {
                    e.DrawBackground();
                } else {
                    if (e.Index == hilightIdx)
                        g.FillRectangle(Brushes.Orange, e.Bounds);
                    else
                        g.FillRectangle(Brushes.Yellow, e.Bounds);
                }
                textBrush = new SolidBrush(e.ForeColor);
            }
            g.DrawString((lbxFunc.Items[e.Index] as MethodInfo).Name, e.Font, textBrush, e.Bounds, StringFormat.GenericDefault);
        }
    }
}
