﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Windows.Media.Media3D;

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
                //swap(ref A[i], ref A[j]);   // 스왑
                (A[i], A[j]) = ReturnInput(A[j], A[i]);
            }
        }

        public static (T1, T2) ReturnInput<T1, T2>(T1 t1, T2 t2) {
            return (t1, t2);
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
            var strings = bytes.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')).ToArray();
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

    class TestSub {
        public static int LocalFunctionFactorial(int n) {
            return nthFactorial(n);

            int nthFactorial(int number) => (number < 2) ?
                1 : number * nthFactorial(number - 1);
        }

        public static int LambdaFactorial(int n) {
            Func<int, int> nthFactorial = default(Func<int, int>);

            nthFactorial = (number) => (number < 2) ?
                1 : number * nthFactorial(number - 1);

            return nthFactorial(n);
        }
        public static DialogResult InputBox(string title, string promptText, ref string value) {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
    }

    class SomeClass : IDisposable {
        public SomeClass() {
            Console.WriteLine($"SomeClass Constructed: {this.GetHashCode()}");
        }
        ~SomeClass() {
            Console.WriteLine($"SomeClass Finalized: {this.GetHashCode()}");
        }
        public void Dispose() {
            Console.WriteLine($"SomeClass Disposed: {this.GetHashCode()}");
        }
    }

    class Animal {
        protected string name;
        public Animal(string name) => this.name = name;
        public void Move() {
            Console.WriteLine($"{name} is moving");
        }
    }

    class Bird : Animal {
        public Bird(string name) : base(name) { }
        public void Fly() {
            Console.WriteLine($"{name} is flying");
        }
    }

    class Duck : Bird {
        public Duck(string name) : base(name) { }
        public void Swim() {
            Console.WriteLine($"{name} is swimming");
        }
    }

    enum ArrayStyle { Random, AscSorted, DescSorted }

    public class Super {
        public virtual void Virtual_Normal_Print() => Console.WriteLine("Super.Virtual_Normal_Print()");
        public virtual void Virtual_New_Print() => Console.WriteLine("Super.Virtual_New_Print()");
        public virtual void Virtual_Override_Print() => Console.WriteLine("Super.Virtual_Override_Print()");
    }

    public class Sub : Super {
        public void Virtual_Normal_Print() => Console.WriteLine("Sub.Virtual_Normal_Print()");
        public new void Virtual_New_Print() => Console.WriteLine("Sub.Virtual_New_Print()");
        public override void Virtual_Override_Print() => Console.WriteLine("Sub.Virtual_Override_Print()");
    }

    public class FobonacciClass {
        // 일반 함수
        public static int FibReq(int n) {
            return n < 2 ? n : FibReq(n - 1) + FibReq(n - 2);
        }

        // 식 본문 함수
        public static int FibExpBody(int n) => n < 2 ? n : FibExpBody(n - 1) + FibExpBody(n - 2);

        // 람다식 대리자
        public static Func<int, int> FibLambda = n => n < 2 ? n : FibLambda(n - 1) + FibLambda(n - 2);

        // 일반 함수
        public static Dictionary<int, int> dicMemo = new Dictionary<int, int>();
        public static int FibMemo(int n) {
            if (dicMemo.ContainsKey(n))
                return dicMemo[n];
            var result = n < 2 ? n : FibMemo(n - 1) + FibMemo(n - 2);
            dicMemo.Add(n, result);
            return result;
        }

        // 람다식 대리자
        public static Func<int, int> FibMemoGeneric = Memoize((int n) => {
            return n < 2 ? n : FibMemoGeneric(n - 1) + FibMemoGeneric(n - 2);
        });

        // 메모이제이션 함수
        public static Func<A, R> Memoize<A, R>(Func<A, R> f) {
            var map = new Dictionary<A, R>();
            return a => {
                R value;
                if (map.TryGetValue(a, out value) == false)
                    map[a] = f(a);
                return map[a];
            };
        }
    }

    public class ClassPoint {
        public int X;
        public int Y;
        public ClassPoint() {
            X = 0;
            Y = 0;
        }
    }

    public class Crc32 {
        private const uint CRC_TSIZE = 256U;
        private const uint CRC32_POLYNOMIAL = 0x04C11DB7U;
        private const uint CRC32_INIT = 0xFFFFFFFFU;

        private static readonly uint[] dwCRCTable = new uint[CRC_TSIZE];

        // CRC테이블 생성
        static Crc32() {
            uint CRC = 0;
            for (ushort wIndex = 0; wIndex < CRC_TSIZE; wIndex++) {
                CRC = wIndex;
                for (ushort wSize = 0; wSize < 8; wSize++) {
                    if ((CRC & 1) != 0U) {
                        CRC >>= 1;
                        CRC ^= CRC32_POLYNOMIAL;
                    } else {
                        CRC >>= 1;
                    }
                }
                dwCRCTable[wIndex] = CRC;
            }
        }

        public static uint GetCRCT(byte[] pData, int iPointFrom, int iPointTo) {
            uint CRC = CRC32_INIT;
            byte Index = 0;

            for (int i = iPointFrom; i < iPointTo; i++) {
                Index = (byte)(pData[i] ^ CRC);
                CRC >>= 8;
                CRC ^= dwCRCTable[Index];
            }

            return CRC;
        }

        public static uint GetCRCT(byte[] pData) {
            return GetCRCT(pData, 0, pData.Length);
        }
    }

    public static class ExtEnumerable {
        public static IEnumerable<TSource> RandomShuffle<TSource>(this IEnumerable<TSource> source) {
            return RandomShuffle(source, source.Count());
        }
        
        public static IEnumerable<TSource> RandomShuffle<TSource>(this IEnumerable<TSource> source, int count) {
            var array = source.ToArray();
            var rnd = new Random();
            for (int i = 0; i < count; i++) {
                int j = rnd.Next(i, array.Length);
                yield return array[j];
                array[j] = array[i];
            }
        }
    }

    public class JsonSerializerDataContract {
        public static string ObjectToJson(object obj, bool indent, bool useSimpleDictionaryFormat, EmitTypeInformation emitTypeInformation) {
            DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings();
            settings.UseSimpleDictionaryFormat = useSimpleDictionaryFormat;
            settings.EmitTypeInformation = emitTypeInformation;
            var ser = new DataContractJsonSerializer(obj.GetType(), settings);
            using (var ms = new MemoryStream())
            using (var writer = JsonReaderWriterFactory.CreateJsonWriter(ms, Encoding.UTF8, true, indent)) {
                ser.WriteObject(writer, obj);
                writer.Flush();
                var json = Encoding.UTF8.GetString(ms.ToArray());
                return json;
            }
        }

        public static T JsonToObject<T>(string json, bool useSimpleDictionaryFormat, EmitTypeInformation emitTypeInformation) {
            DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings();
            settings.UseSimpleDictionaryFormat = useSimpleDictionaryFormat;
            settings.EmitTypeInformation = emitTypeInformation;
            var ser = new DataContractJsonSerializer(typeof(T), settings);
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json))) {
                var obj = (T)ser.ReadObject(ms);
                return obj;
            }
        }
    }

    public class JsonSerializerJaveScript {
        public static string ObjectToJson(object obj) {
            var ser = new JavaScriptSerializer();
            return ser.Serialize(obj);
        }

        public static T JsonToObject<T>(string json) {
            var ser = new JavaScriptSerializer();
            return ser.Deserialize<T>(json);
        }
    }

    public class JsonSerializerNewton {
        public static string ObjectToJson(object obj, bool indent) {
            return JsonConvert.SerializeObject(obj, indent ? Formatting.Indented : Formatting.None);
        }

        public static T JsonToObject<T>(string json) {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }

    public class CData {
        public int ca = 0;
        public int cb = 0;
    }

    public struct SData {
        public int sa;
        public int sb;
    }


    class MyEnumerable : IEnumerable {
        private int[] array = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };
        public IEnumerator GetEnumerator() {
            return (IEnumerator)new MyEnumerator(array);
        }

        public static IEnumerable<int> Range(int start, int num) {
            while (num-- > 0) {
                yield return start++;
            }
        }
    }

    class MyEnumerator : IEnumerator {
        private int[] array;
        private int idx = -1;

        public MyEnumerator(int[] array) {
            this.array = array;
        }

        public object Current => array[idx];

        public bool MoveNext() {
            idx++;
            return idx < array.Length;
        }

        public void Reset() {
            idx = -1;
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

    public class MethodCompile {
        [MethodImpl(MethodCodeType = MethodCodeType.IL)]
        public static int Add_MethodCodeType_IL(int a, int b) {
            return a + b;
        }
        [MethodImpl(MethodCodeType = MethodCodeType.Native)]
        public static int Add_MethodCodeType_Native(int a, int b) {
            return a + b;
        }
        [MethodImpl(MethodCodeType = MethodCodeType.OPTIL)]
        public static int Add_MethodCodeType_OPTIL(int a, int b) {
            return a + b;
        }
        [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
        public static int Add_MethodCodeType_Runtime(int a, int b) {
            return a + b;
        }
    }

    public class Record {
        public string item0;
        public string item1;
        public string item2;
        public string item3;
        public string item4;
        public string item5;
        public string item6;
        public string item7;
        public string item8;
        public string item9;
        public string item10;
        public string item11;
        public string item12;
        public string item13;

        public Record(string item0, string item1, string item2, string item3, string item4, string item5, string item6, string item7, string item8, string item9, string item10, string item11, string item12, string item13) {
            this.item0 = item0;
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item3;
            this.item4 = item4;
            this.item5 = item5;
            this.item6 = item6;
            this.item7 = item7;
            this.item8 = item8;
            this.item9 = item9;
            this.item10 = item10;
            this.item11 = item11;
            this.item12 = item12;
            this.item13 = item13;
        }
    }
}