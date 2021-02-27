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

namespace DownloaderThread {
    public partial class FormMainThread : Form {
        public FormMainThread() {
            InitializeComponent();
        }

        Thread th = null;
        bool thCancelReq = false;

        private void ThreadFunc() {
            thCancelReq = false;
            int i = 0;
            while (i < 100) {
                // Cancel 처리
                if (thCancelReq)
                    break;

                Thread.Sleep(50);
                i++;

                // 진행 보고
                this.Invoke((MethodInvoker)delegate() {
                    ReportProgress(i);
                });
            }

            // 결과 보고
            this.Invoke((MethodInvoker)delegate () {
                ReportComplete(thCancelReq ? "Canceled." : "Completed.");
            });
        }

        // 진행 보고
        private void ReportProgress(int percent) {
            prbPercent.Value = percent;
            lblPercent.Text = $"{percent}%";
            lblLog.Text = "Downloading...";
        }

        // 결과 보고
        private void ReportComplete(string msg) {
            lblLog.Text = msg;
        }

        private void btnDownload_Click(object sender, EventArgs e) {
            if (th != null && th.ThreadState.HasFlag(ThreadState.Stopped) == false) {
                MessageBox.Show("Error - Already Downloading...");
                return;
            }

            th = new Thread(ThreadFunc);
            th.Start();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            if (th == null || th.ThreadState.HasFlag(ThreadState.Stopped)) {
                MessageBox.Show("Error - No Downloading...");
                return;
            }

            thCancelReq = true;
        }
    }
}
