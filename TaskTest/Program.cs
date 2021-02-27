using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTest {
    class Program {
        public static int LongCalc() {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            for (var i = 0; i < 50; i++) {
                Console.WriteLine($"ThreadId = {threadId}, i={i}");
                Thread.Sleep(100);
            }
            return threadId;
        }

        static void Main(string[] args) {
            int taskNum = 10;
            
            // 동기호출
            //var results = new int[taskNum];
            //for (var i = 0; i < taskNum; i++) {
            //    results[i] = LongCalc();
            //}
            //for (var i = 0; i < taskNum; i++) {
            //    Console.WriteLine($"results[{i}]={results[i]}");
            //}

            // Task로 비동기 호출
            var tasks = new Task<int>[taskNum];
            for (var i = 0; i < taskNum; i++) {
                // ThreadPool에서 task 함수 () => LongCalc() 호출
                tasks[i] = Task.Run(() => LongCalc());
            }

            for (var i = 0; i < taskNum; i++) {
                // task 함수 완료까지 대기
                tasks[i].Wait();
            }

            for (var i = 0; i < taskNum; i++) {
                // taks 함수 결과 표시
                Console.WriteLine($"tasks[{i}.Result={tasks[i].Result}");
            }
        }
    }
}
