﻿using System;
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
            var list = mis.Select((m, i) => Tuple.Create($"{i}_{m.Name}", m)).ToArray();
            this.lbxFunc.Items.AddRange(list);
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
            var tuple = this.lbxFunc.SelectedItem as Tuple<string, MethodInfo>;
            var mi = tuple.Item2;
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

        private void tbxSearch_TextChanged(object sender, EventArgs e) {
            string searchText = tbxSearch.Text;
            var items = lbxFunc.Items.Cast<Tuple<string, MethodInfo>>().Select((tuple, idx) => Tuple.Create(tuple.Item1, idx));
            var searchTuple = items.FirstOrDefault(tuple => tuple.Item1.ToLower().Contains(searchText.ToLower()));
            if (searchTuple != null)
                lbxFunc.TopIndex = searchTuple.Item2;
        }
    }
}
