using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.IO.Compression;
using System.IO;
using System.ComponentModel;
using System.Threading;
using System.Net;

namespace CSharpTest {
    class Test {
        public static void HelloWorld() {
            Console.WriteLine("hello, world");
        }

        public static void Nothging() {
        }

        public static void BitCheck() {
            Console.WriteLine("IntPtr.Size: {0}", IntPtr.Size);
            Console.WriteLine("this process is {0}bit", IntPtr.Size * 8);
        }

        public static void MultiplyTable() {
            for (int i = 1; i <= 9; i++) {
                for (int j = 2; j <= 9; j++) {
                    Console.Write("{0}*{1}={2:00} ", j, i, j * i);
                }
                Console.WriteLine();
            }
        }

        public static void TestAdd(int a = 1, int b = 2) {
            Console.WriteLine("{0}+{1}={2}", a, b, a + b);
        }

        public static void CovarianceContravariance() {
            Console.WriteLine("covariance");
            Animal[] animals = { new Bird("bird"), new Bird("bird"), new Bird("bird") };
            animals.ToList().ForEach(animal => animal.Move());

            // error
            //Duck[] ducks = {new Bird(), new Bird(), new Bird()};

            Console.WriteLine("\r\ngeneric covariance");
            IEnumerable<Animal> animals2 = new List<Bird>() { new Bird("bird"), new Bird("bird"), new Bird("bird") };
            animals.ToList().ForEach(animal2 => animal2.Move());

            // error
            //IEnumerable<Duck> ducks = new List<Bird>(){new Bird(), new Bird(), new Bird()};

            Console.WriteLine("\r\ncontravariance");
            Action<Bird> aBird = (Bird bird) => bird.Fly();
            Action<Duck> aDuck = aBird;
            aDuck(new Duck("duck"));

            // error
            //Action<Animal> aAnim = aBird;

            Console.WriteLine("\r\ncovariance");
            Func<Bird> fBird = () => new Bird("bird");
            Func<Animal> fAnim = fBird;
            fAnim().Move();

            // error
            //Func<Duck> fDuck = fBird;
        }

        public static void LambdaClosure() {
            int a = 1;
            int b = 2;
            int c = 0;
            Action action = () => c = a + b;
            action();
            Console.WriteLine(c);
        }

        public static void SetTest() {
            //HashSet<int> set = new HashSet<int>();
            SortedSet<int> set = new SortedSet<int>();
            set.Add(1);
            set.Add(2);
            set.Add(2);
            Console.WriteLine(string.Join(", ", set.ToArray()));
            set.Remove(1);
            Console.WriteLine(string.Join(", ", set.ToArray()));
            set.Remove(1);
            Console.WriteLine(string.Join(", ", set.ToArray()));
        }

        public static void FinalizerTest() {
            SomeClass sc = new SomeClass();
        }

        public static void CollectGarbage() {
            GC.Collect();
        }

        public static void IDisposibleTest() {
            using (SomeClass sc = new SomeClass()) {

            }
        }

        public static void ParallelLinq() {
            Stopwatch stopwatch = new Stopwatch();
            IEnumerable<int> bigNumber = Enumerable.Range(1, 50000000);

            stopwatch.Restart();
            var result1 = bigNumber.Select(number => number / 2.1).ToArray();
            stopwatch.Stop();
            System.Console.WriteLine("Linq : " + stopwatch.ElapsedMilliseconds + "ms");

            stopwatch.Restart();
            var result2 = bigNumber.AsParallel().Select(number => number / 2.1).ToArray();
            stopwatch.Stop();
            System.Console.WriteLine("Parallel Linq : " + stopwatch.ElapsedMilliseconds + "ms");
        }

        public static void ListCapacity() {
            var list = new List<object>();
            int capacity = list.Capacity;
            Console.WriteLine($"count {list.Count} : capacity : {capacity}");
            for (int i = 0; i < 1000000; i++) {
                list.Add(null);
                if (list.Capacity > capacity) {
                    capacity = list.Capacity;
                    Console.WriteLine($"count {list.Count} : capacity : {capacity}");
                }
            }

            for (int i = 0; i < 1000000; i++) {
                list.RemoveAt(list.Count - 1);
                if (list.Capacity < capacity) {
                    capacity = list.Capacity;
                    Console.WriteLine($"count {list.Count} : capacity : {capacity}");
                }
            }
        }

        public static void QuickSort(int num = 16, ArrayStyle arrayStyle = ArrayStyle.Random) {
            int[] arr;

            switch (arrayStyle) {
                case ArrayStyle.AscSorted:
                    arr = Enumerable.Range(0, num).ToArray();
                    break;
                case ArrayStyle.DescSorted:
                    arr = Enumerable.Range(0, num).Reverse().ToArray();
                    break;
                default:
                    var rnd = new Random();
                    arr = Enumerable.Range(0, num).OrderBy(n => rnd.Next()).ToArray();
                    break;
            }

            var arr2 = (int[])arr.Clone();

            Console.WriteLine("inpu: " + string.Join("", arr.Select(n => $" {n,2} ")));
            Glb.quicksort(arr, 0, arr.Length - 1);
            Console.WriteLine("rslt: " + string.Join("", arr.Select(n => $" {n,2} ")));
        }

        public static void DecryptVioletMessage(
            string text = "07.02.18.143.09.12.18.69.10.09.18.404.11.23.18.224.11.30.18.302.12.21.18") {
            var chars = text.Split('.').Select(word => Convert.ToChar(int.Parse(word))).ToArray();
        }

        public static void LazyEvaluationTest() {
            IEnumerable<int> GetInifinite() {
                int val = 0;
                while (true) {
                    yield return val++;
                }
            }

            var enums = GetInifinite();

            int i = 0;
            foreach (var val in enums) {
                Console.WriteLine(val);

                if (++i > 10)
                    break;
            }
        }

        public static void StringFormat() {
            string text = "text";
            Console.WriteLine(string.Format("|{0}|", text));
            Console.WriteLine(string.Format("|{0,10}|", text));
            Console.WriteLine(string.Format("|{0,-10}|", text));

            float f = 12.345f;
            Console.WriteLine(string.Format("|{0}|", f));
            Console.WriteLine(string.Format("|{0,10:f4}|", f));
            Console.WriteLine(string.Format("|{0,-10:f4}|", f));
        }

        public static void QueryPerformace() {
            var counter = Stopwatch.GetTimestamp();   // QueryPerformanceCounter
            var freq = Stopwatch.Frequency;           // QueryPerformanceFrequency
            Console.WriteLine("Stopwatch.GetTimestamp() = {0}", counter);
            Console.WriteLine("Stopwatch.Frequency = {0}", freq);
            Console.WriteLine("time : {0:F2}", (double)counter / freq);
        }

        public static Stopwatch sw = Stopwatch.StartNew();
        public static void StopWatch() {
            Console.WriteLine("StopWatch가 시작한 시점부터의 지난 시간으로 표시됨.");
            Console.WriteLine("sw.Elapsed = {0}", sw.Elapsed);
            Console.WriteLine("sw.ElapsedMilliseconds = {0}", sw.ElapsedMilliseconds);
            Console.WriteLine("sw.ElapsedTicks = {0}", sw.ElapsedTicks);
        }

        public static void Environment_TickCount() {
            Console.WriteLine("시스템시작으로부터 지난 millisecond, 정밀도가 15.6ms정도 됨, 24시간마다 50일이 되면 0이 됨");
            Console.WriteLine("Environment.TickCount = {0}", Environment.TickCount);
        }

        public static void DateTime_Now() {
            Console.WriteLine("서기 시각을 알려줌, 정밀도 10ms");
            var now = DateTime.Now;
            Console.WriteLine("DateTime.Now = {0}.{1,3:000}", now, now.Millisecond);
        }

        public static void InheritanceTest() {
            Sub sub = new Sub();
            sub.NormalPrint();
            sub.OverridePrint();
            Super sub2 = new Sub();
            sub2.NormalPrint();
            sub2.OverridePrint();
        }


        public static void FibonacciTest(int num = 42) {
            Func<int, int>[] funcs = { FobonacciClass.FibReq, FobonacciClass.FibExpBody, FobonacciClass.FibLambda, FobonacciClass.FibMemo, FobonacciClass.FibMemoGeneric };
            string[] funcNames = { nameof(FobonacciClass.FibReq), nameof(FobonacciClass.FibExpBody), nameof(FobonacciClass.FibLambda), nameof(FobonacciClass.FibMemo), nameof(FobonacciClass.FibMemoGeneric) };
            try {
                for (int i = 0; i < funcs.Length; i++) {
                    var timeOld = DateTime.Now;
                    int fib = funcs[i](num);
                    var timeSpan = DateTime.Now - timeOld;
                    string text = string.Format("{0}({1})={2}\r\n{3}sec", funcNames[i], num, fib, timeSpan.TotalSeconds);
                    System.Console.WriteLine(text + Environment.NewLine);
                }
            } catch (Exception ex) {
                System.Console.WriteLine(ex.Message);
            }
        }

        public static void ZipFileTest() {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            string filePath = ofd.FileName;
            if (Path.GetExtension(filePath).ToLower() != ".zip") {
                Console.WriteLine("no zip extension: " + filePath);
                return;
            }

            try {
                using (var fs = File.OpenRead(filePath)) {
                    using (var za = new ZipArchive(fs)) {
                        foreach (var ze in za.Entries) {
                            Console.WriteLine(ze.FullName);
                        }
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message + ": " + filePath);
            }
        }

        public static void ToStringTest() {
            // ToString() 메소드 호출시 실제 객체의 ToString() 메소드를 호출하는지
            Console.WriteLine((3).ToString());
            Console.WriteLine(((object)3).ToString());
            Console.WriteLine((new Object()).ToString());
        }

        public static void TypeConvertTest() {
            // TypeConverter.ConvertToString 과 ToString 함수가 동일한 결과를 하는지
            PointF pt = new PointF(1, 2);
            Console.WriteLine(pt.ToString());

            Type type = typeof(PointF);
            TypeConverter tc = TypeDescriptor.GetConverter(type);
            Console.WriteLine(tc.ConvertToString(pt));
        }

        public static void StructArrayTest() {
            Console.WriteLine("Struct Point array");
            Point[] pt = new Point[10];
            for (int i = 0; i < pt.Length; i++)
                Console.WriteLine("({0}, {1})", pt[0].X, pt[0].Y);

            Console.WriteLine("Class Point array");
            ClassPoint[] cpt = new ClassPoint[10];
            for (int i = 0; i < cpt.Length; i++)
                Console.WriteLine("({0}, {1})", cpt[0].X, cpt[0].Y);
        }

        public static void LocalFunctionAndLambdaTest(int n = 5) {
            Console.WriteLine("LocalFunctionFactorial({0}) = {1}", n, TestSub.LocalFunctionFactorial(n));
            Console.WriteLine("LambdaFactorial({0}) = {1}", n, TestSub.LambdaFactorial(n));
        }

        public static void EncodingTest(string input = "A가", bool showAsBinary = false) {
            Encoding[] encodings = {
                Encoding.UTF7,
                Encoding.BigEndianUnicode,
                Encoding.Unicode,
                Encoding.Default,
                Encoding.ASCII,
                Encoding.UTF8,
                Encoding.UTF32,
            };

            Console.WriteLine($"Input : \"{input}\"");
            Console.WriteLine($"{"Encoding",-15} : {"Hex",-30} : {"Decoded"}");
            Console.WriteLine($"{"---------------",-15} : {"------------------------------",-30} : {"--------"}");
            foreach (var encoding in encodings) {
                var bytes = encoding.GetBytes(input);
                var encodedString = showAsBinary ? Glb.ToBinaryString(bytes) : Glb.ToHexString(bytes);
                var textDecode = encoding.GetString(bytes);
                Console.WriteLine($"{encoding.BodyName,-15} : {encodedString,-30} : \"{textDecode}\"");
            }
        }

        // C# char타입은 ushort타입과 1:1 대응된다
        // UTF-16 유니코드는 내부적으로 초성, 중성, 종성의 조합형이다.
        public static void HangeulExplode(char input = '맥') {
            var explodeded = Hangeul.Explode(input);
            Console.WriteLine($"input    : {input}");
            Console.WriteLine($"exploded : {new string(explodeded)}");
        }

        public static void HangeulExplodeString(string input = "대한민국") {
            var partArray = input.Select(ch => Hangeul.Explode(ch)).SelectMany(ch => ch).ToArray();
            Console.WriteLine($"input    : {input}");
            Console.WriteLine($"exploded : {new string(partArray)}");
        }

        public static void HangeulComposite(char init = 'ㅎ', char median = 'ㅏ', char final = 'ㄴ') {
            char composited = Hangeul.Composite(init, median, final);
            Console.WriteLine($"input    : {init}{median}{final}");
            Console.WriteLine($"composited : {composited}");
        }

        public static void StringEqualTest(string a = "ABC", string b = "ABC") {
            Console.WriteLine("a.ComapreTo(b) : {0}", a.CompareTo(b));
            Console.WriteLine("a.Equals(b) : {0}", a.Equals(b));
            Console.WriteLine("a.SequenceEqual(b) : {0}", a.SequenceEqual(b));
            Console.WriteLine("a == b : {0}", a == b);
        }

        public static void MatrixTest() {
            float theta = 90f;

            var fmatRot = new System.Drawing.Drawing2D.Matrix();
            fmatRot.Rotate(theta);
            Console.WriteLine(string.Join(",", fmatRot.Elements));

            var dmatRot = new System.Windows.Media.Matrix();
            dmatRot.Rotate(theta);
            Console.WriteLine(dmatRot);

            var nmatRot = System.Numerics.Matrix3x2.CreateRotation(theta * (float)Math.PI / 180);
            Console.WriteLine(nmatRot);
        }

        public static void MatrixTest2() {
            float scaleX = 2.0f;
            float scaleY = 1.0f;
            float theta = 90f;
            float transX = 1.0f;
            float transY = 2.0f;

            var dmatScale = new System.Windows.Media.Matrix();
            dmatScale.Scale(scaleX, scaleY);
            var dmatRot = new System.Windows.Media.Matrix();
            dmatRot.Rotate(theta);
            var dmatTrans = new System.Windows.Media.Matrix();
            dmatTrans.Translate(transX, transY);
            var dmatMul = dmatScale * dmatRot * dmatTrans;

            Console.WriteLine("Scale     : {0}", dmatScale);
            Console.WriteLine("Rotate    : {0}", dmatRot);
            Console.WriteLine("Translate : {0}", dmatTrans);
            Console.WriteLine("Multiply  : {0}", dmatMul);
        }

        public static void AsynAwaitTest() {
            new FormAsyncAwait().ShowDialog();
        }

        public static void AsynAwaitMsTest() {
            new FormAsyncAwaitMs().ShowDialog();
        }

        public static async void AsyncAwaitTest2(int seconds = 5) {
            var task = Task.Run(() => {
                    for (int i=0; i<seconds; i++) {
                        Thread.Sleep(1000);
                    }
                    return seconds;
                }
            );
            int ret = await task;
            MessageBox.Show($"{ret} seconds passed");
        }

        public static void AsyncAwaitTest3(int seconds = 5) {
            new FormAsyncAwait3().ShowDialog();
        }

        public static void ExePath() {
            Console.WriteLine("Application.StartupPath : {0}", Application.StartupPath);
            Console.WriteLine("Application.ExecutablePath : {0}", Application.ExecutablePath);
        }

        public static void Crc32Test(string text = "hello, world") {
            var buf = Encoding.Default.GetBytes(text);
            var crc = Crc32.GetCRCT(buf);
            Console.WriteLine("{0} : {1}", text, crc);
        }

        public static void MakeComicWeb(string root = @"D:\github\ComicHolyBible\성경만화-k") {
            var dirs = Directory.GetDirectories(root);
            foreach (var dir in dirs) {
                var sbuf = new StringBuilder();
                var dirName = Path.GetFileName(dir);
                string header =
$@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <meta http-equiv=""X-UA-Compatible"" content=""ie=edge"">
    <title>{dirName}</title>
</head>
<body>
    <table border = ""1"" cellspacing = ""10"">
";
                sbuf.Append(header);
                var files = Directory.GetFiles(dir).OrderBy(file => file);
                foreach (var file in files) {
                    var fileName = Path.GetFileName(file);
                    if (fileName == "index.html")
                        continue;
                    string link =
$@"    <tr><td><img src = ""{fileName}"" /></td></tr>
";
                    sbuf.Append(link);
                }
                string footer =
$@"    </table>
</body>
</html>
";
                sbuf.Append(footer);
                var html = sbuf.ToString();
                var outFilePath = dir + "\\index.html";
                File.WriteAllText(outFilePath, html);
            }
        }

        public static void FormBorderStyleTest(FormBorderStyle formBorderStyle = FormBorderStyle.Sizable) {
            var formMain = Application.OpenForms[0];
            formMain.FormBorderStyle = formBorderStyle;
            Console.WriteLine("Change FOrmBorderStyle : {0}", formBorderStyle);
        }

        public static void NewFormTest(FormBorderStyle newFormBorderStyle = FormBorderStyle.Sizable) {
            var formMain = Application.OpenForms[0];
            var formNew = new Form { Text = "NewForm", Width = 300, Height = 200, FormBorderStyle = newFormBorderStyle };
            formNew.ShowDialog(formMain);
        }

        public static void InputBoxTest() {
            string text = null;
            var ret = TestSub.InputBox("InputBox", "I am a InputBox.", ref text);
            if (ret == DialogResult.OK) {
                Console.WriteLine("InputText : {0}", text);
            }
        }

        public static void ThreadLockTest() {
            int count = 0;

            object locker = new object();

            Thread t1 = new Thread(() => {
                for (int i = 0; i < 100; i++) {
                    lock (locker) {
                        count++;
                    }
                    Thread.Sleep(10);
                }
            });

            Thread t2 = new Thread(() => {
                for (int i = 0; i < 100; i++) {
                    lock (locker) {
                        count--;
                    }
                    Thread.Sleep(10);
                }
            });

            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();
            Console.WriteLine("count = {0}", count);
        }

        public static void TaskTest() {
            // 익명 메서드 사용
            //Action someAction = () => {
            //    Thread.Sleep(1000);
            //    Console.WriteLine("Printed asynchronously.");
            //};
            //Task myTask = new Task(someAction);
            //myTask.Start();

            // 람다 사용
            //Task myTask = new Task(() => {
            //    Thread.Sleep(1000);
            //    Console.WriteLine("Printed asynchronously.");
            //});
            //myTask.Start();

            // Task.Run static 메서드에 람다 직접 넘김
            Task myTask = Task.Run(() => {
                Thread.Sleep(1000);
                Console.WriteLine("Printed asynchronously.");
            });

            Console.WriteLine("Printed synchronously.");

            myTask.Wait();
        }
        public static void TaskOfTTest() {
            // Task.Run static 메서드에 람다 직접 넘김
            var myTask = Task<int>.Run(() => {
                Thread.Sleep(1000);
                Console.WriteLine("Printed asynchronously.");
                return 102;
            });

            Console.WriteLine("Printed synchronously.");
            var result = myTask.Result;
            Console.WriteLine("async task result : {0}", result);
        }

        public static void ParallelForTest() {
            var result = Parallel.For(0, 100, (i) => Console.WriteLine("{0} : {1}", i, Thread.CurrentThread.ManagedThreadId));
        }

        public static void HttpGetTest(string url = "https://api.jsonbin.io/b/5ce6bb555302fd1986c60aa0") {
            string responseText = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Timeout = 30 * 1000; // 30초
            request.Headers.Add("secret-key", "$2a$10$u9ScRpYp.bxUrZOpoYNYc.eWSfzDrfB2PksK5EUlS97QNKrRO77GG"); // 헤더 추가 방법

            using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse()) {
                HttpStatusCode status = resp.StatusCode;
                Console.WriteLine(status);  // 정상이면 "OK"

                Stream respStream = resp.GetResponseStream();
                using (StreamReader sr = new StreamReader(respStream)) {
                    responseText = sr.ReadToEnd();
                }
            }

            Console.WriteLine(responseText);
        }

        public static void HttpPutTest(string url = "https://api.jsonbin.io/b/5ce6bb555302fd1986c60aa0", string data = "{ \"id\": \"101\", \"name\" : \"Alex\" }") {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "PUT";
            request.Timeout = 30 * 1000;
            request.ContentType = "application/json";
            request.Headers.Add("secret-key", "$2a$10$u9ScRpYp.bxUrZOpoYNYc.eWSfzDrfB2PksK5EUlS97QNKrRO77GG");
            request.Headers.Add("versioning", "false");
            
            // POST할 데이타를 Request Stream에 쓴다
            byte[] bytes = Encoding.ASCII.GetBytes(data);
            request.ContentLength = bytes.Length; // 바이트수 지정

            using (Stream reqStream = request.GetRequestStream()) {
                reqStream.Write(bytes, 0, bytes.Length);
            }

            // Response 처리
            string responseText = string.Empty;
            using (WebResponse resp = request.GetResponse()) {
                Stream respStream = resp.GetResponseStream();
                using (StreamReader sr = new StreamReader(respStream)) {
                    responseText = sr.ReadToEnd();
                }
            }

            Console.WriteLine(responseText);
        }

        public static void ByteArrayToHexString(List<byte> bytes)
        {
            byte[] arr = bytes.ToArray();
            Console.Write("arr : ");
            Console.WriteLine(string.Join(",", arr));
            string hex = BitConverter.ToString(arr);
            Console.Write("hex : ");
            Console.WriteLine(hex);
        }

        public static void NativeAddTest(int a = 2, int b = 3) {
            Console.WriteLine("Native.Add({0}, {1}): {2}", a, b, Native.Add(a, b));
        }

        public static IntPtr buffer = IntPtr.Zero;
        public static long cb = 10 * 1024L * 1024L * 1024L;
        public static void Alloc() {
            if (buffer != IntPtr.Zero) {
                Console.WriteLine("Error : buffer not freed");
                return;
            }

            buffer = Marshal.AllocHGlobal((IntPtr)cb);
        }

        public static void Memset() {
            if (buffer == IntPtr.Zero) {
                Console.WriteLine("Error : buffer not allocated");
                return;
            }

            Msvcrt.memset(buffer, 0, (IntPtr)cb);
        }

        public static void Free() {
            if (buffer == IntPtr.Zero) {
                Console.WriteLine("Error : buffer not allocated");
                return;
            }
            
            Marshal.FreeHGlobal(buffer);
            buffer = IntPtr.Zero;
        }
    }
}
