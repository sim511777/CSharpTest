﻿using System;
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
                // Cancel 처리
                if (thCancelReq)
                    break;

                Thread.Sleep(50);
                i++;

                // 진행 보고
                this.Invoke((Action)delegate() {
                    ReportProgress(i);
                });
            }

            // 결과 보고
            this.Invoke((Action)delegate () {
                ReportComplete();
            });
        }

        // 진행 보고
        private void ReportProgress(int percent) {
            prbPercent.Value = percent;
            lblPercent.Text = $"{percent}%";
            lblLog.Text = "Downloading...";
        }

        // 결과 보고
        private void ReportComplete() {
            if (thCancelReq) {
                lblLog.Text = "Canceled.";
            } else {
                lblLog.Text = "Completed.";
            }
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
