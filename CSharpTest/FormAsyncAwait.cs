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
    public partial class FormAsyncAwait : Form {
        public FormAsyncAwait () {
            InitializeComponent ();
        }

        // 최소 플래그
        bool stop = true;

        // 비동기 함수
        async private void DoAsync () {
            this.Text = "Started.";

            // 비동기 리포트 객체 생성 : 이 Action은 리포트 객체가 생성된 쓰레드에서 실행 됨
            IProgress<Action> progress = new Progress<Action> (action => action ());

            // 취소 플래그 초기화
            this.stop = false;

            int percent = 0;
            // Task 만들고 시작
            var task = Task.Run (() => {
                foreach (var i in Enumerable.Range (0, 100)) {
                    // 취소 체크
                    if (this.stop)
                        break;

                    // 진행 보고
                    progress.Report (() => {
                        percent = i + 1;
                        this.Text = $"{percent}% Running...";
                        this.progressBar1.Value = percent;
                    });

                    // do something
                    Thread.Sleep (30);
                }
            });

            // Task 완료를 기다리고 쓰레드를 돌려줌
            await task;

            // Task가 완료 후 실행 됨
            if (this.stop)
                this.Text = $"{percent}% Canceled.";
            else
                this.Text = $"{percent}% Finished.";

            // run 상태 리셋
            this.stop = true;
        }

        // 시작
        private void btnAsyncAwait_Click (object sender, EventArgs e) {
            if (this.stop)
                this.DoAsync ();
        }

        // 취소
        private void btnCancel_Click (object sender, EventArgs e) {
            this.stop = true;
        }
    }
}