using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpTest {
    public partial class FormAsyncAwait3 : Form {
        public FormAsyncAwait3() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            Run();
        }

        private async void Run() {
            // 동기
            //int sum = LongCalc(5);
            //this.label1.Text = sum.ToString();

            // 비동기
            var task1 = Task.Run(() => LongCalc(5));
            int sum = await task1;
            this.label1.Text = sum.ToString();
        }

        private int LongCalc(int n) {
            int result = 0;
            for (int i = 0; i < n; i++) {
                result++;
                Thread.Sleep(1000);
            }
            return result;
        }
    }
}
