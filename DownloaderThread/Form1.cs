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
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        Thread th = null;
        bool thCancelReq = false;

        private void ThreadFunc() {
            thCancelReq = false;
            int i = 0;
            while (i < 100) {
                // 작업 Cancel 처리
                if (thCancelReq) {
                    this.Invoke((Action)delegate() {
                        lblPercent.Text = $"{i}% Canceled.";
                    });
                    break;
                }

                Thread.Sleep(100);
                i++;

                // CrossThread UI Access
                this.Invoke((Action)delegate() {
                    prbPercent.Value = i;
                    lblPercent.Text = $"{i}% Downloading...";
                });
            }
        }

        private void StartDownload() {
            // 쓰레드가 null이 아니고 stop 상태가 아니라면 시작 할 수가 없다
            if (th != null && th.ThreadState.HasFlag(ThreadState.Stopped) == false) {
                MessageBox.Show("Error - Already Downloading...");
                return;
            }

            th = new Thread(ThreadFunc);
            th.Start();
        }

        private void CancelDownload() {
            // 쓰레드가 null이거나 Stop 상태라면 Cancel 할 수가 없다
            if (th == null || th.ThreadState.HasFlag(ThreadState.Stopped)) {
                MessageBox.Show("Error - No Downloading...");
                return;
            }

            thCancelReq = true;
        }

        private void btnDownload_Click(object sender, EventArgs e) {
            StartDownload();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            CancelDownload();
        }
    }
}
