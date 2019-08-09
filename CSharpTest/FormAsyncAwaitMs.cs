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
    public partial class FormAsyncAwaitMs : Form {
        public FormAsyncAwaitMs() {
            InitializeComponent();
        }

        async Task<int> UploadPicturesAsync(int totalCount, IProgress<int> progress, CancellationToken ct) {
            // 작업을 Task 로 만들어서 실행
            // 리턴값은 작업의 결과
            int processCount = await Task.Run<int>(() => {
                int tempCount = 0;
                
                // 작업 루프
                for (int i = 0; i < totalCount; i++) {
                    // 실제 작업
                    Thread.Sleep(30);
                    tempCount++;

                    // 취소 체크
                    ct.ThrowIfCancellationRequested();
                    
                    // 리포트
                    if (progress != null) {
                        progress.Report((tempCount * 100 / totalCount));
                    }
                }

                return tempCount;
            }, ct);

            // 작업의 결과 리턴
            return processCount;
        }

        void ReportProgress(int value) {
            // 진행상황 리포트
            this.pbrReport.Value = value;
            this.lblReport.Text = value + "%";
        }

        // 취소 토큰 소스
        CancellationTokenSource cts;


        private async void Start_Button_Click(object sender, EventArgs e) {
            // 진행 인디케이터
            var progressIndicator = new Progress<int>(ReportProgress);

            // 취소 토큰 소스
            cts = new CancellationTokenSource();

            try {
                int workNum = 100;
                // 비동기 실행
                int x = await UploadPicturesAsync(workNum, progressIndicator, cts.Token);
                // 대기화 결과 출력
                this.lblReport.Text = string.Format("{0}/{1} finished", x, workNum);
            } catch (OperationCanceledException ex) {
                // 취소 되었을때
                this.lblReport.Text = this.pbrReport.Value + "% : " + ex.Message;
            }
            cts = null;
        }

        private void Cancel_Button_Click(object sender, EventArgs e) {
            // 취소 신청
            if (this.cts != null)
                this.cts.Cancel();
        }
    }
}
