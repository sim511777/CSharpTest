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
using HtmlAgilityPack;
using System.Linq.Expressions;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Drawing.Imaging;

namespace CSharpTest {
    class Glb {
        public static void quicksort(int[] A, int lo, int hi) {
            if (lo < hi) {
                int p = partition(A, lo, hi);
                quicksort(A, lo, p);
                quicksort(A, p + 1, hi);
            }
        }

        public static int partition(int[] A, int lo, int hi) {
            Console.Write("part: " + new string(' ', lo * 4));
            Console.WriteLine(string.Join("", A.Skip(lo).Take(hi - lo + 1).Select((n, idx) => idx + lo == lo ? $"#{n,2}#" : $" {n,2} ")));
            int pivot = A[lo];  // 첫번째 요소값을 피벗으로
            int i = lo;     // 첫번째 요소 부터 조회
            int j = hi;     // 마지막 요소 부터 조회
            while (true) {
                while (A[i] < pivot) i++;   // 하나씩 뺌
                while (A[j] > pivot) j--;   // 하나씩 뺌
                if (i >= j) return j;       // 겹치거나 바뀌었다면 겹치거나 작은 값 리턴
                Console.Write("swap: " + new string(' ', lo * 4));
                Console.WriteLine(string.Join("", A.Skip(lo).Take(hi - lo + 1).Select((n, idx) => idx + lo == i || idx + lo == j ? $"[{n,2}]" : $" {n,2} ")));
                swap(ref A[i], ref A[j]);   // 스왑
            }
        }

        public static void swap(ref int a, ref int b) {
            int temp = a;
            a = b;
            b = temp;
        }

        public static string ToHexString(byte[] bytes) {
            var strings = bytes.Select(b => b.ToString("x2")).ToArray();
            var hexString = string.Join("-", strings);
            return hexString;
        }

        public static string ToBinaryString(byte[] bytes) {
            var strings = bytes.Select(b => Convert.ToString( b, 2 ).PadLeft( 8, '0' )).ToArray();
            var hexString = string.Join("-", strings);
            return hexString;
        }

        public static int Add(int a, int b) {
            return a + b;
        }

        public static int LongCalc(int n) {
            int r = 0;
            for (int i = 0; i < n; i++) {
                Thread.Sleep(1000);
                r++;
            }
            return r;
        }

        // ref : pass by reference RW
        // out : pass by reference W
        // int : pass by reference R

        public static void RefMethod(ref int v) {
            v += 1;
            Console.WriteLine(v);
        }

        public static void OutMethod(out int v) {
            v = 1;      // callee에서 할당 해주어야 함
            v += 1;
            Console.WriteLine(v);
        }

        public static void InMethod(in Numbers v) {
            //v = new Numbers();   // in 파라미터는 재할당 불가, C#7.2 이상에서 지원
            v.a = 10;   // 멤버는 재할당 가능
            Console.WriteLine(v);
        }
    }

    class Numbers {
        public int a = 0;
        public int b = 0;
    }

    class Test {
        public static void RefMethodTest() {
            int v = 1;   // caller에서 할당 해주어야 함
            Glb.RefMethod(ref v);
            Console.WriteLine(v);
        }

        public static void OutMethodTest() {
            int v;
            Glb.OutMethod(out v);
            Console.WriteLine(v);
        }

        public static void InMethodTest() {
            Numbers num = new Numbers();
            Glb.InMethod(in num);
            Console.WriteLine(num.a);
        }

        public static void HelloWorld() {
            Console.WriteLine("hello, world");
        }

        public static void Nothging() {
        }

        public unsafe static void BitCheck() {
            Console.WriteLine($"IntPtr.Size: {IntPtr.Size}");
            Console.WriteLine($"sizeof(IntPtr): {sizeof(IntPtr)}");
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
            // 공변성, 반공변성
            // ref 타입의 암시적 변환 가능성 여부
            // 공변성 : 기반 타입으로 변환 가능 : 단일 객체, 배열, IEnumerable<out T>, Func<out TResult>
            // 반공변성 : 파생 타입으로 변환 가능 : Action<in T>
            // .net 4.0 인터페이스, 대리자 제네릭 가변성 키워드 지원 <out T>, <in T>

            // 배열은 공변성 됨
            Console.WriteLine("covariance");
            Animal[] animals = { new Bird("bird"), new Bird("bird"), new Bird("bird") };
            animals.ToList().ForEach(animal => animal.Move());

            // error : 배열은 반공변성 안됨
            //Duck[] ducks = {new Bird(), new Bird(), new Bird()};

            // IEnumerable<T> 는 공변성 됨 .net 4.0부터 지원
            Console.WriteLine("\r\ngeneric covariance");
            IEnumerable<Animal> animals2 = new List<Bird>() { new Bird("bird"), new Bird("bird"), new Bird("bird") };
            animals.ToList().ForEach(animal2 => animal2.Move());

            // error : IEnumerable<T> 는 반공변성 안됨
            //IEnumerable<Duck> ducks = new List<Bird>(){new Bird(), new Bird(), new Bird()};

            Console.WriteLine("\r\ncontravariance");
            Action<Bird> aBird = (Bird bird) => bird.Fly();
            Action<Duck> aDuck = (Duck duck) => duck.Swim();
            aDuck = aBird;  // 델리게이트의 입력 타입은 반공변성 됨
            //aBird = aDuck; error : 델리게이트의 입력 타입 공변성 안됨
            aDuck(new Duck("duck"));

            Console.WriteLine("\r\ncovariance");
            Func<Bird> fBird = () => new Bird("bird");
            Func<Animal> fAnim = () => new Animal("animal");
            fAnim = fBird;      // 델리게이트의 출력 타입은 공변성 됨
            //fBird = fAnim;    // 델리게이트의 출력 타입은 반공변성 안됨
            fAnim().Move();
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
            Sub obj1 = new Sub();
            Console.WriteLine("Sub obj1 = new Sub();");
            obj1.Virtual_Normal_Print();
            obj1.Virtual_New_Print();
            obj1.Virtual_Override_Print();
            Console.WriteLine();
            Super obj2 = new Sub();
            Console.WriteLine("Super obj2 = new Sub();");
            obj2.Virtual_Normal_Print();
            obj2.Virtual_New_Print();
            obj2.Virtual_Override_Print();
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

        public static void ThreadLockTest(int incCount = 100000, bool useLock = false) {
            int count = 0;
            object locker = new object();

            void ts() {
                for (int i = 0; i < incCount; i++) {
                    if (useLock) {
                        lock (locker) {
                            ++count; 
                        }
                    } else {
                        ++count;
                    }
                }
            }

            var t1 = new Thread(ts);
            var t2 = new Thread(ts);

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

        public static IntPtr bufHGlobal = IntPtr.Zero;
        public static IntPtr bufNewBuffer = IntPtr.Zero;
        public static IntPtr bufMallocBuffer = IntPtr.Zero;
        public static long cb = 10 * 1024L * 1024L * 1024L;
        
        public static void AllocHGlobalTest() {
            if (bufHGlobal != IntPtr.Zero || bufNewBuffer != IntPtr.Zero || bufMallocBuffer != IntPtr.Zero) {
                Console.WriteLine("Error : buffer not freed");
                return;
            }

            bufHGlobal = Marshal.AllocHGlobal((IntPtr)cb);
        }

        public static void FreeHGlobalTest() {
            if (bufHGlobal == IntPtr.Zero) {
                Console.WriteLine("Error : buffer not allocated");
                return;
            }
            
            Marshal.FreeHGlobal(bufHGlobal);
            bufHGlobal = IntPtr.Zero;
        }

        public static void MemsetHGlobalTest(int val = 0) {
            if (bufHGlobal == IntPtr.Zero) {
                Console.WriteLine("Error : buffer not allocated");
                return;
            }

            Msvcrt.memset(bufHGlobal, val, (IntPtr)cb);
        }

        public static void NewBufferTest() {
            if (bufHGlobal != IntPtr.Zero || bufNewBuffer != IntPtr.Zero || bufMallocBuffer != IntPtr.Zero) {
                Console.WriteLine("Error : buffer not freed");
                return;
            }

            bufNewBuffer = Native.NewBuffer((IntPtr)cb);
        }

        public static void DeleteBufferTest() {
            if (bufNewBuffer == IntPtr.Zero) {
                Console.WriteLine("Error : buffer not allocated");
                return;
            }

            Native.DeleteBuffer(bufNewBuffer);
            bufNewBuffer = IntPtr.Zero;
        }

        public static void MemsetNewBufferTest(int val = 0) {
            if (bufNewBuffer == IntPtr.Zero) {
                Console.WriteLine("Error : buffer not allocated");
                return;
            }

            Msvcrt.memset(bufNewBuffer, val, (IntPtr)cb);
        }

        public static void MallocBufferTest() {
            if (bufHGlobal != IntPtr.Zero || bufNewBuffer != IntPtr.Zero || bufMallocBuffer != IntPtr.Zero) {
                Console.WriteLine("Error : buffer not freed");
                return;
            }

            bufMallocBuffer = Native.MallocBuffer((IntPtr)cb);
        }

        public static void FreeBufferTest() {
            if (bufMallocBuffer == IntPtr.Zero) {
                Console.WriteLine("Error : buffer not allocated");
                return;
            }

            Native.FreeBuffer(bufMallocBuffer);
            bufMallocBuffer = IntPtr.Zero;
        }

        public static void MemsetMallocBufferTest(int val = 0) {
            if (bufMallocBuffer == IntPtr.Zero) {
                Console.WriteLine("Error : buffer not allocated");
                return;
            }

            Msvcrt.memset(bufMallocBuffer, val, (IntPtr)cb);
        }

        public static void LambdaLocalFunctionTest() {
            Func<int, int, int> lambda1 = delegate(int a, int b) {
                return a - b;
            };
            
            Func<int, int, int> lambda2 = (int a, int b) => a - b;
            
            int localFunction1(int a, int b) {
                return a - b;
            }
            
            int localFunction2(int a, int b) => a - b;
            
            Console.WriteLine(lambda1(1, 2));
            Console.WriteLine(lambda2(1, 2));
            Console.WriteLine(localFunction1(1, 2));
            Console.WriteLine(localFunction2(1, 2));
        }

        public static void ListElementAccessTest() {
            List<int> intList = Enumerable.Range(0, 10).ToList();
            Console.WriteLine(string.Join(" ", intList.ToArray()));

            int a = intList.ElementAt(3);   // get (LINQ)
            Console.WriteLine(a);
            
            int b = intList[4];             // get (List indexer)
            Console.WriteLine(b);

            intList[3] = 8; // set (List indexer)
            Console.WriteLine(string.Join(" ", intList.ToArray()));
        }

        public static void LinkedListElementAccessTest() {
            LinkedList<int> intList = new LinkedList<int>(Enumerable.Range(0,10));

            Console.WriteLine(string.Join(" ", intList.ToArray()));
            
            int a = intList.ElementAt(3);   // get (LINQ)
            Console.WriteLine(a);
            
            //int b = intList[4]; // error (indexer not supported)
            //intList[3] = 8; // error (indexer not supported)
            // no replace or remove or add by index

            Console.WriteLine(string.Join(" ", intList.ToArray()));
        }

        public static void RandomShuffleTest() {
            var intList = Enumerable.Range(0, 10);
            var rndList = intList.RandomShuffle();
            foreach (var rnd in rndList) {
                Console.WriteLine(rnd);
            }
        }

        public static void RandomShuffleCountTest(int count = 5) {
            var intList = Enumerable.Range(0, 10);
            var rndList = intList.RandomShuffle(count);
            foreach (var rnd in rndList) {
                Console.WriteLine(rnd);
            }
        }

        public static void DateTimeResolutionTest() {
            var time1 = DateTime.Now;
            while (true) {
                var time2 = DateTime.Now;
                if (time2 != time1) {
                    double ms = (time2 - time1).TotalMilliseconds;
                    Console.WriteLine($"{ms}ms");
                    break;
                }
            }
        }

        public static void StopwatchResolutionTest() {
            var time1 = Stopwatch.GetTimestamp();
            while (true) {
                var time2 = Stopwatch.GetTimestamp();
                if (time2 != time1) {
                    double ms = (time2 - time1) * 1000.0 / Stopwatch.Frequency;
                    Console.WriteLine($"{ms}ms");
                    break;
                }
            }
        }

        public static void MathRoundTest(double start = -10.0, double end = 10.0, double step = 0.5) {
            // 반올림시 0.5에 대한 처리 방법
            // MidpointRounding.AwayFromZero : 0.5는 위로 올린다
            // MidpointRounding.ToEven : 번갈아 가면서 올리고 내리고 한다
            // 0.5->0, 1.5->2, 2.5->2, 3.5->4
            // 결국 짝수(0,2,4,...)로 되버림
            Console.WriteLine(
@"input
      : Round(ToEven)
      :     : Round(AwayFromZero)
      :     :     : Floor
      :     :     :     : (int)
      :     :     :     :     : Floor(+0.5)
      :     :     :     :     :     : (int)(+0.5)");
            for (double item = start; item < end; item += 0.5) {
                var roundToEven = Math.Round(item, MidpointRounding.ToEven);
                var roundToAwayFromZero = Math.Round(item, MidpointRounding.AwayFromZero);
                var floor = Math.Floor(item);
                var castInt = (int)item;
                var addHalffloor = Math.Floor(item + 0.5);
                var addHalfIntCast = (int)(item + 0.5);
                Console.WriteLine($"{item,5} : {roundToEven,3} : {roundToAwayFromZero,3} : {floor,3} : {castInt,3} : {addHalffloor,3} : {addHalfIntCast,3}");
            }
        }

        public static void LiteralTest() {
            Console.WriteLine("리터럴은 값의 소스 코드 표현입니다.");
            Console.WriteLine();
            Console.WriteLine("== type");
            Console.WriteLine("true  : " + $"{(true).GetType()}");
            Console.WriteLine("123   : " + $"{(123).GetType()}");
            Console.WriteLine("123U  : " + $"{(123U).GetType()}");
            Console.WriteLine("123L  : " + $"{(123L).GetType()}");
            Console.WriteLine("123UL : " + $"{(123UL).GetType()}");
            Console.WriteLine("1.23  : " + $"{(1.23).GetType()}");
            Console.WriteLine("1.23f : " + $"{(1.23f).GetType()}");
            Console.WriteLine("1.23d : " + $"{(1.23d).GetType()}");
            Console.WriteLine("1.23m : " + $"{(1.23m).GetType()}");
            Console.WriteLine();
            Console.WriteLine("== small integer");
            Console.WriteLine("(sbyte)123   : " + $"{((sbyte)123).GetType()}");
            Console.WriteLine("(byte)123    : " + $"{((byte)123).GetType()}");
            Console.WriteLine("(short)123   : " + $"{((short)123).GetType()}");
            Console.WriteLine("(ushort)123  : " + $"{((ushort)123).GetType()}");
            Console.WriteLine();
            Console.WriteLine("== hex integer");
            Console.WriteLine("0xff               : " + $"{(0xff).GetType()} {0xff}");
            Console.WriteLine("0x00ffffff         : " + $"{(0x00ffffff).GetType()} {0x00ffffff}");
            Console.WriteLine("0xffffffff         : " + $"{(0xffffffff).GetType()} {0xffffffff}");
            Console.WriteLine("0xffffffffff       : " + $"{(0xffffffffff).GetType()} {0xffffffffff}");
            Console.WriteLine("0xffffffffffffffff : " + $"{(0xffffffffffffffff).GetType()} {0xffffffffffffffff}");
            Console.WriteLine("0b01010101010101010101010101010101 : " + $"{(0b01010101010101010101010101010101).GetType()} {0b01010101010101010101010101010101}");
            Console.WriteLine("0b0101010101010101010101010101010101010101010101010101010101010101 : " + $"{(0b0101010101010101010101010101010101010101010101010101010101010101).GetType()} {0b0101010101010101010101010101010101010101010101010101010101010101}");
            Console.WriteLine();
            Console.WriteLine("== floating point");
            Console.WriteLine("1.23e+2  : " + $"{(1.23e+2).GetType()} {1.23e+2}");
            Console.WriteLine("1.23e-2  : " + $"{(1.23e-2).GetType()} {1.23e-2}");
            Console.WriteLine("1.23e+2f : " + $"{(1.23e+2f).GetType()} {1.23e+2f}");
            Console.WriteLine("1.23e-2f : " + $"{(1.23e-2f).GetType()} {1.23e-2f}");
            Console.WriteLine("1.23e+2d : " + $"{(1.23e+2d).GetType()} {1.23e+2d}");
            Console.WriteLine("1.23e-2d : " + $"{(1.23e-2d).GetType()} {1.23e-2d}");
            Console.WriteLine("1.23e+2m : " + $"{(1.23e+2m).GetType()} {1.23e+2m}");
            Console.WriteLine("1.23e-2m : " + $"{(1.23e-2m).GetType()} {1.23e-2m}");
            Console.WriteLine();
            Console.WriteLine("== string");
            Console.WriteLine("\'a\'      : " + $"{('a').GetType()}");
            Console.WriteLine("\"abc\"    : " + $"{("abc").GetType()}");
            Console.WriteLine("@\"abc\"   : " + $"{(@"abc").GetType()}");
            Console.WriteLine("$\"abc\"   : " + $"{($"abc").GetType()}");
            Console.WriteLine("$@\"abc\"  : " + $"{($@"abc").GetType()}");
        }

        public static void SizeOfPrimitiveTest() {
            Console.WriteLine($"sizeof(bool) = {sizeof(bool)}");
            Console.WriteLine($"Marshal.SizeOf(typeof(bool)) = {Marshal.SizeOf(typeof(bool))}");
        }

        public static void ValutTupleTest() {
            Tuple<string, int> GiveMe7() {
                return Tuple.Create("Seven", 7);
            }

            (string, int) GiveMe8() {
                return ("Eight", 8);
            }

            (string name, int num) GiveMe9() {
                return ("Nine", 9);
            }

            var tuple7 = GiveMe7();
            var tuple8 = GiveMe8();
            var tuple9 = GiveMe9();
            Console.WriteLine($"tuple7.Item1={tuple7.Item1}, tuple7.Item2={tuple7.Item2}");
            Console.WriteLine($"tuple8.Item1={tuple8.Item1}, tuple8.Item2={tuple8.Item2}");
            Console.WriteLine($"tuple9.name={tuple9.name}, tuple9.num={tuple9.num}");
            Console.WriteLine();

            var (name7, num7) = GiveMe7();
            var (name8, num8) = GiveMe8();
            var (name9, num9) = GiveMe9();
            Console.WriteLine($"name7={name7}, num7={num7}");
            Console.WriteLine($"name8={name8}, num8={num8}");
            Console.WriteLine($"name9={name9}, num9={num9}");
        }

        public static void FuncCombinationTest() {
            Func<int, int, int> sub = (a, b) => a - b;
            Func<int, int> sub3 = (a) => sub(a, 3);
            Func<int, Func<int,int>> subCurry = (b) => (a) => sub(a, b);
            Console.WriteLine($"sub(5, 3) = {sub(5, 3)}");
            Console.WriteLine($"sub3(5) = {sub3(5)}");
            Console.WriteLine($"subCurry(3)(5) = {subCurry(3)(5)}");
        }

        public static void HtmlAgilityPackTest(string url = "http://aha-dic.com/View.asp?word=pitcher") {
            using (var client = new WebClient()) {
                client.Encoding = Encoding.UTF8;
                var html = client.DownloadString(url);
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);
                var nodes = doc.DocumentNode.SelectNodes("//div[@class='phonetic']");
                var texts = nodes.ElementAt(0).ChildNodes.Where(cn => cn.NodeType == HtmlNodeType.Text);
                foreach (var text in texts) {
                    Console.WriteLine("r = " + text.OuterHtml.Trim());
                }
            }
        }

        public static void CurryTest() {
            Func<int, Func<int, int>> delegateVar = p1 => (p2 => p1 - p2);
            // delegateVar은 int를 받아서 int(int)함수를 리턴하는 딜리게이트변수인데
            // 그 함수는 p2를 받아서 p1에 더한 값을 리턴해주는함수 

            Func<int, int> localFunction(int p1) => (p2 => p1 - p2);
            // localFunction은 int p1을 인자로 받고 int(int)함수를 리턴하는 함수인데
            // 그 함수는 p2를 받아서 p1에 더한 값을 리턴해주는 함수

            Console.WriteLine(delegateVar(3)(2));
            Console.WriteLine(localFunction(3)(2));
        }

        public static void CurryTest2() {
            Func<int, int> Curry(Func<int, int, int> f, int a) => b => f(a, b);
            int Add(int a, int b) => a + b;
            Func<int, int> Add5 = Curry(Add, 5);
        
            Console.WriteLine(Add5(3));
        }

        public static void CurryTest3() {
            Func<B, R> Curry<A, B, R>(Func<A, B, R> f, A a) => b => f(a, b);
            Func<int, int, int> Add = (a, b) => a + b;
            var Add3 = Curry(Add, 3);
            Console.WriteLine(Add3(2));
        }

        public static void ExpressionTreeTest(bool indent = true, bool useSimpleDictionaryFormat = true, EmitTypeInformation emitTypeInformation = EmitTypeInformation.Never) {
            Func<int, int, int> func = (a, b) => a + b;
            Expression<Func<int, int, int>> exp = (a, b) => a + b;
            var compiled = exp.Compile();
            Console.WriteLine($"{func(3, 4)}");
            Console.WriteLine($"{compiled(3, 4)}");

            try {
                string funcJson = JsonSerializerDataContract.ObjectToJson(func, indent, useSimpleDictionaryFormat, emitTypeInformation);
                Console.WriteLine(funcJson);
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
            try {
                string expJson = JsonSerializerDataContract.ObjectToJson(exp, indent, useSimpleDictionaryFormat, emitTypeInformation);
                Console.WriteLine(expJson);
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void RectSerialize(bool indent = true, bool useSimpleDictionaryFormat = true, EmitTypeInformation emitTypeInformation = EmitTypeInformation.Never) {
            var rectList = new List<Rectangle> {
                new Rectangle(0,1,2,3),
                new Rectangle(1,2,3,4),
                new Rectangle(2,3,4,5),
                new Rectangle(3,4,5,6),
                };
            var json = JsonSerializerDataContract.ObjectToJson(rectList, indent, useSimpleDictionaryFormat, emitTypeInformation);
            Console.WriteLine(json);
            File.WriteAllText("rectList.json", json);
        }

        public static void RectDeserialize(bool useSimpleDictionaryFormat = true, EmitTypeInformation emitTypeInformation = EmitTypeInformation.Never) {
            var json = File.ReadAllText("rectList.json");
            Console.WriteLine(json);
            var rectList = JsonSerializerDataContract.JsonToObject<List<Rectangle>>(json, useSimpleDictionaryFormat, emitTypeInformation);
            foreach (var rect in rectList) {
                Console.WriteLine(rect);
            }
        }

        public static void DictionarySerialize(bool indent = true, bool useSimpleDictionaryFormat = true, EmitTypeInformation emitTypeInformation = EmitTypeInformation.Never) {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            dic["a"] = 0;
            dic["b"] = 1;
            dic["c"] = 2;
            dic["d"] = 3;
            var json = JsonSerializerDataContract.ObjectToJson(dic, indent, useSimpleDictionaryFormat, emitTypeInformation);
            Console.WriteLine(json);
            File.WriteAllText("dic.json", json);
        }

        public static void DictionaryDeserialize(bool useSimpleDictionaryFormat = true, EmitTypeInformation emitTypeInformation = EmitTypeInformation.Never) {
            var json = File.ReadAllText("dic.json");
            Console.WriteLine(json);
            var dic = JsonSerializerDataContract.JsonToObject<Dictionary<string, int>>(json, useSimpleDictionaryFormat, emitTypeInformation);
            foreach (var pair in dic) {
                Console.WriteLine(pair);
            }
        }

        public static void SubDictionarySerialize_DataContract(bool indent = true, bool useSimpleDictionaryFormat = true, EmitTypeInformation emitTypeInformation = EmitTypeInformation.Never) {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            Dictionary<string, object> dicSub = new Dictionary<string, object>();
            dic["a"] = dicSub;
            dicSub["a"] = 0;
            dicSub["b"] = 1;
            dicSub["c"] = 2;
            dicSub["d"] = 3;

            var json = JsonSerializerDataContract.ObjectToJson(dic, indent, useSimpleDictionaryFormat, emitTypeInformation);
            Console.WriteLine(json);
            File.WriteAllText("dic.json", json);
        }

        public static void SubDictionarySerialize_JavaScript() {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            Dictionary<string, object> dicSub = new Dictionary<string, object>();
            dic["a"] = dicSub;
            dicSub["a"] = 0;
            dicSub["b"] = 1;
            dicSub["c"] = 2;
            dicSub["d"] = 3;

            var json = JsonSerializerJaveScript.ObjectToJson(dic);
            Console.WriteLine(json);
            File.WriteAllText("dic.json", json);
        }

        public static void SubDictionarySerialize_Newton(bool indent = true) {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            Dictionary<string, object> dicSub = new Dictionary<string, object>();
            dic["a"] = dicSub;
            dicSub["a"] = 0;
            dicSub["b"] = 1;
            dicSub["c"] = 2;
            dicSub["d"] = 3;

            var json = JsonSerializerNewton.ObjectToJson(dic, indent);
            Console.WriteLine(json);
            File.WriteAllText("dic.json", json);
        }

        public static void StructClassTest() {
            CData cobj = new CData();
            List<CData> cdatas = new List<CData>();
            cdatas.Add(cobj);
            cobj.ca = 10;
            Console.WriteLine($"cdatas[0].ca={cdatas[0].ca} cobj.ca={cobj.ca}");
            
            SData sobj = new SData();
            List<SData> sdatas = new List<SData>();
            sdatas.Add(sobj);
            sobj.sa = 10;
            Console.WriteLine($"sdatas[0].ca={sdatas[0].sa} sobj.ca={sobj.sa}");
            Console.WriteLine("struct는 List<>에 넣을 때 인스턴스 카피가 일어난다.");
        }

        public static void CharFrequency(string text = "hello", bool ignoreCase = true) {
            var charGroup = text.GroupBy(ch => ignoreCase ? char.ToLower(ch) : ch);
            var items = charGroup.Select(group => $"{group.Key}:{group.Count()}");
            Console.WriteLine(string.Join(" ", items));
        }

        public static void EncodingStringTest(string text = "ABCD가각갂갃") {
            Encoding[] encodings = {
                Encoding.ASCII,     // 모든 글자를 1바이트로, 영문알파벳만 정상 인코딩됨
                Encoding.Default,   // EUC-KR, 영문 1바이트, 한글 2바이트
                Encoding.Unicode,   // UTF-16, 모든 글자 2바이트로
                Encoding.UTF8,      // 영문 1바이트, 한글 3바이트
                Encoding.BigEndianUnicode, // UTF-16 Big-Endian, 모든 글자 2바이트로 바이트 순서 뒤집어서
                Encoding.UTF32,     // 모든 글자 4바이트로
                Encoding.UTF7,      // 몰라
            };
            foreach (var encoding in encodings) {
                File.WriteAllBytes($@"c:\test\{encoding.BodyName}.txt", encoding.GetBytes(text));
            }
        }

        public static void DebugViewTest() {
            foreach (var v in Enumerable.Range(0, 10)) {
                Trace.WriteLine(v.ToString());
            }
        }

        public static void CacheGood(int num = 100000000) {
            int[] arr1 = Enumerable.Range(0, num).ToArray();
            int[] arr2 = Enumerable.Range(0, num).ToArray();
            var st = Stopwatch.GetTimestamp();
            for (int i = 0; i < num; i++) {
                arr1[i] *= 3;
            }
            for (int i = 0; i < num; i++) {
                arr2[i] *= 3;
            }
            var dt = Stopwatch.GetTimestamp() - st;
            var ms = dt * 1000.0 / Stopwatch.Frequency;
            Console.WriteLine($"{ms:f3}ms");
        }

        public static void CacheBad(int num = 100000000) {
            int[] arr1 = Enumerable.Range(0, num).ToArray();
            int[] arr2 = Enumerable.Range(0, num).ToArray();
            var st = Stopwatch.GetTimestamp();
            for (int i = 0; i < num; i++) {
                arr1[i] *= 3;
                arr2[i] *= 3;
            }
            var dt = Stopwatch.GetTimestamp() - st;
            var ms = dt * 1000.0 / Stopwatch.Frequency;
            Console.WriteLine($"{ms:f3}ms");
        }

        public enum LockMode { Unlock, Lock, InterlockedClass}
        public static void LockTest(int num = 1000000, LockMode lockMode = LockMode.Unlock) {
            int a = 0;
            object locker = new object();
            void Job() {
                for (int i = 0; i < num; i++) {
                    if (lockMode ==  LockMode.Unlock) {
                        a += 10;
                    } else if (lockMode == LockMode.Lock) {
                        lock (locker)
                            a += 10;
                    } else {
                        Interlocked.Add(ref a, 10);
                    }
                }
            }

            var st = Stopwatch.GetTimestamp();
            var task1 = Task.Run(() => Job());
            var task2 = Task.Run(() => Job());
            task1.Wait();
            task2.Wait();
            var dt = Stopwatch.GetTimestamp() - st;
            var ms = dt * 1000.0 / Stopwatch.Frequency;
            Console.WriteLine($"lockMode = {lockMode}, a = {a}, {ms:f3}ms");
        }

        public static void DelegateTest() {
            Func<int, int, int> add = Glb.Add;
            
            Func<int, int, int> mul = delegate(int a, int b) {
                return a * b;
            };
            
            Func<int, int, int> mod = (a, b) => a % b;
            
            // Delegate.Method : MethodInfo
            // Delegate.Target : object of called method, if static value is null 
            Console.WriteLine($"{add.Method}, {add.Target}");
            Console.WriteLine($"{mul.Method}, {mul.Target}");
            Console.WriteLine($"{mod.Method}, {mod.Target}");
        }

        public static void AnonymousTypeTest() {
            var v = new[] {
                new { Name="Lee", Age=27, Phone="02-111-1111"},
                new { Name="kim", Age=31, Phone="02-222-2222"},
                new { Name="Parn", Age=37, Phone="02-333-3333"},
            };
            var list = v
                .Where(p => p.Age >= 30)
                .Select(p => new { p.Name, p.Age }); 
            foreach (var t in list) {
                Console.WriteLine($"{t.Name} : {t.Age}");
            }


            string Name = "Kim";
            int Age = 31;
            string Phone = "02-222-2222";

            // 익명 형식 멤버는 멤버할당, 단순이름, 멤버엑세스 이어야 함
            var a = new { Name="Lee", Age=27, Phone="02-111-1111"}; // 멤버할당
            var b = new { Name, Age, Phone };                       // 단순이름
            var c = new { b.Name, b.Age, b.Phone };                 // 멤버엑세스
        }

        public static void DynamicTest() {
            dynamic a = "aaa";
            Console.WriteLine(a.GetType()); // System.String
            a = 123;
            Console.WriteLine(a.GetType()); // System.Int32

            // object vs dynamic
            object  i = 10;
            i = (int)i + 20;

            i = "Hello";
            string s = ((string)i).ToUpper();

            dynamic d = 10;
            d = d + 20;
            d = "Hello";
            string ss = d.ToUpper();
        }

        public static void SyncTest() {
            int r = Glb.LongCalc(10);
            Console.WriteLine($"Sync LongCalc : r = {r}");
        }

        public static async void AsyncTest() {
            Task<int> task = Task.Run(() => Glb.LongCalc(10));
            int r = await task;
            //int r = await Task.Run(() => Glb.LongCalc(10));
            Console.WriteLine($"Async LongCalc : r = {r}");
        }

        public static void DebuggerBreakTest(int n = 0) {
            Console.WriteLine("Function start");
            if (n == 5)
                Debugger.Break();
            Console.WriteLine("Function end");
        }

        public static byte[] oriBuf = null;
        public static void LoadBigFile() {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            string filePath = ofd.FileName;
            oriBuf = File.ReadAllBytes(filePath);
            Console.WriteLine($"{filePath}");
            Console.WriteLine($"{oriBuf.LongLength}byes read");
        }

        public static void GzipTest() {
            if (oriBuf == null)
                return;

            long t0, t1, t2, t3;

            byte[] zipBuf = null;
            using (MemoryStream msZip = new MemoryStream()) {
                using (GZipStream compressionStream = new GZipStream(msZip, CompressionMode.Compress)) {
                    t0 = Stopwatch.GetTimestamp();
                    compressionStream.Write(oriBuf, 0, oriBuf.Length);
                    t1 = Stopwatch.GetTimestamp();
                }
                zipBuf = msZip.ToArray();
                long compressBytes = zipBuf.LongLength;
                Console.WriteLine($"compress   : {compressBytes}bytes");
            }

            using (MemoryStream msZip = new MemoryStream(zipBuf)) {
                using (GZipStream decompressionStream = new GZipStream(msZip, CompressionMode.Decompress)) {
                    t2 = Stopwatch.GetTimestamp();
                    long decompressBytes = decompressionStream.Read(oriBuf, 0, oriBuf.Length);
                    t3 = Stopwatch.GetTimestamp();
                    Console.WriteLine($"decompress : {decompressBytes}bytes");
                }
            }

            Console.WriteLine($"Gzip Compress   : {(t1-t0) / Stopwatch.Frequency:0.}s");
            Console.WriteLine($"Gzip Decompress : {(t3-t2) / Stopwatch.Frequency:0.}s");
        }

        public static void DeplateTest() {
            if (oriBuf == null)
                return;

            long t0, t1, t2, t3;

            byte[] zipBuf = null;
            using (MemoryStream msZip = new MemoryStream()) {
                using (DeflateStream compressionStream = new DeflateStream(msZip, CompressionMode.Compress)) {
                    t0 = Stopwatch.GetTimestamp();
                    compressionStream.Write(oriBuf, 0, oriBuf.Length);
                    t1 = Stopwatch.GetTimestamp();
                }
                zipBuf = msZip.ToArray();
                long compressBytes = zipBuf.LongLength;
                Console.WriteLine($"compress   : {compressBytes}bytes");
            }

            using (MemoryStream msZip = new MemoryStream(zipBuf)) {
                using (DeflateStream decompressionStream = new DeflateStream(msZip, CompressionMode.Decompress)) {
                    t2 = Stopwatch.GetTimestamp();
                    long decompressBytes = decompressionStream.Read(oriBuf, 0, oriBuf.Length);
                    t3 = Stopwatch.GetTimestamp();
                    Console.WriteLine($"decompress : {decompressBytes}bytes");
                }
            }

            Console.WriteLine($"Gzip Compress   : {(t1-t0) / Stopwatch.Frequency:0.}s");
            Console.WriteLine($"Gzip Decompress : {(t3-t2) / Stopwatch.Frequency:0.}s");
        }

        public static void MemoryStreamTest() {
            using (var ms = new MemoryStream()) {
                for (int i = 0; i < 2049; i++) {
                    ms.WriteByte(1);
                    Console.WriteLine($"MemoryStream.Lenght : {ms.Length} , MemroyStream.Capacity : {ms.Capacity}");
                }
            }
        }

        public static void OperatorOperatorOperator() {
            int @int = 3;                   // @ : 키워드를 식별자로 사용
            string s = @"\thello";          // @ : escape 문자 무시
            string s2 = $"{100}";           // $ : 문자열 보간
            int? a;                         // ? : nullable 타입 선언 Nullable<int> a;
            Point? pt = Point.Empty;
            int? cnt = pt?.X;               // ?. ?[] : null 조건 연산자. 객체가 null이면 null 리턴, 아니면 계속 평가
            int b = cnt ?? 100;             // ?? : null 병합 연산자. 객체가 null이 아니면 객체 리턴, null이면 뒤에값 리턴
            int cnt2 = pt?.X ?? 100;        // ex) row가 null이 아니면 .Count, null이면 100 리턴
            //list ??= new List<int>();     // ??== : null 병합할당 연산자(C#8.0). list가 null 이면 new List<int>() 할당
        }

        public enum EImageFormat {
            Bmp,
            Jpeg,
            Png,
            Tiff,
        }
        public static Dictionary<EImageFormat, ImageFormat> ImageFormats = new Dictionary<EImageFormat, ImageFormat>() {
            { EImageFormat.Bmp, ImageFormat.Bmp },
            { EImageFormat.Jpeg, ImageFormat.Jpeg },
            { EImageFormat.Png, ImageFormat.Png },
            { EImageFormat.Tiff, ImageFormat.Tiff },
        };

        public static void ImageSave(int width = 1920, int height = 1080, PixelFormat pf = PixelFormat.Format32bppArgb, EImageFormat eimgFmt = EImageFormat.Bmp) {
            var imgFmt = ImageFormats[eimgFmt];
            using (var bmp = new Bitmap(width, height, pf)) {
                //using (var g = Graphics.FromImage(bmp)) {
                //    g.Clear(Color.Blue);
                //    g.FillEllipse(Brushes.Yellow, 100, 100, 500, 500);
                //}
                var filePath = $@"c:\test\{width}x{height}_{pf}.{imgFmt}";
                bmp.Save(filePath, imgFmt);
                Console.WriteLine($"{filePath} succeed");
            }
        }

        public static void EnumFlagsTest() {
            Direction dir = Direction.North | Direction.East;
            Console.WriteLine(dir.HasFlag(Direction.None));
            dir = 0;
            Console.WriteLine(dir.HasFlag(Direction.None));
        }

        public static void DateTime_ToString_Standard() {
            // 표준 DateTime 형식 문자열
            // "d" : 2021-07-18
            // "D" : 2021년 7월 18일 일요일
            // "t" : 오후 5:16
            // "T" : 오후 5:16:47
            // "f" : 2021년 7월 18일 일요일 오후 5:16
            // "F" : 2021년 7월 18일 일요일 오후 5:16:47
            // "g" : 2021-07-18 오후 5:16
            // "G" : 2021-07-18 오후 5:16:47
            // "M" : 7월 18일
            // "m" : 7월 18일
            // "R" : Sun, 18 Jul 2021 17:16:47 GMT
            // "r" : Sun, 18 Jul 2021 17:16:47 GMT
            // "s" : 2021-07-18T17:16:47
            // "u" : 2021-07-18 17:16:47Z
            // "U" : 2021년 7월 18일 일요일 오전 8:16:47
            // "Y" : 2021년 7월
            // "y" : 2021년 7월
            DateTime now = DateTime.Now;
            var fmtList = "d,D,t,T,f,F,g,G,M,m,R,r,s,u,U,Y,y".Split(',');
            foreach (var fmt in fmtList) {
                Console.WriteLine($"{fmt} : {now.ToString(fmt)}");
            }

        }

        public static void DateTime_ToString_Custom(string fmt = "[(gg)yyyy/MM/dd(dddd) (tt)HH:mm:ss.fff(zzzz)]") {
            // 사용자 지정 DateTime 형식 문자열
            // yy:년 두자리, yyyy:4자리
            // M:월, MM:두자리 채움
            // d:일, dd:두자리 채움

            // h:12시간 hh:두자리 채움, H:24시간 HH:두자리 채움
            // m:분, mm:두자리 채움
            // s:초, ss:두자리 채움
            // fff...:밀리초...

            // ddd:요일 한글자, dddd:3글자
            // gg:서기
            // tt:오전/오후
            // zzzz:offset from UTC
            var now = DateTime.Now;
            Console.WriteLine($"{fmt} : {now.ToString(fmt)}");
        }
    }

    [Flags]
    enum Direction {
        None = 0,
        North = 1 << 0,
        East = 1 << 1,
        West = 1 << 2,
        South = 1 << 3,
    }
}
