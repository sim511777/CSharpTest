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
                    prbPercent.Value = i;
                    lblPercent.Text = $"{i}%";
                    lblLog.Text = "Downloading...";
                });
            }

            // 결과 보고
            this.Invoke((MethodInvoker)delegate () {
                lblLog.Text = (thCancelReq ? "Canceled." : "Completed.");
            });
        }

        // 다운로드 시작
        private void btnDownload_Click(object sender, EventArgs e) {
            if (th != null && th.ThreadState.HasFlag(ThreadState.Stopped) == false) {
                MessageBox.Show("Error - Already Downloading...");
                return;
            }

            th = new Thread(ThreadFunc);
            th.Start();
        }

        // 다운로드 취소
        private void btnCancel_Click(object sender, EventArgs e) {
            thCancelReq = true;
        }
    }
}
