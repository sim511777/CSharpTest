using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace DownloaderBackgroundWorker {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e) {
            int i = 0;
            while (i < 100) {
                // cancel 처리
                if (bgw.CancellationPending) {
                    e.Cancel = true;
                    break;
                }

                Thread.Sleep(50);
                i++;

                // 진행 보고
                bgw.ReportProgress(i);
            }
        }

        // 진행 보고
        private void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            prbPercent.Value = e.ProgressPercentage;
            lblPercent.Text = $"{e.ProgressPercentage}%";
            lblLog.Text = "Downloading...";
        }

        // 결과 보고
        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Cancelled) {
                lblLog.Text = "Canceled.";
            } else {
                lblLog.Text = "Completed.";
            }
        }

        private void btnDownload_Click(object sender, EventArgs e) {
            if (bgw.IsBusy) {
                MessageBox.Show("Error - Already Downloading...");
                return;
            }

            bgw.RunWorkerAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            if (!bgw.IsBusy) {
                MessageBox.Show("Error - No Downloading...");
                return;
            }

            bgw.CancelAsync();
        }
    }
}
