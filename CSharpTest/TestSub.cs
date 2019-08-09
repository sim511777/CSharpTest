using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CSharpTest {
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
    }

    enum ArrayStyle { Random, AscSorted, DescSorted }

    public class Super {
        public void NormalPrint() {
            Console.WriteLine("Super.NormalPrint()");
        }
        public virtual void OverridePrint() {
            Console.WriteLine("Super.OverridePrint()");
        }
    }

    public class Sub : Super {
        new private void NormalPrint() {
            Console.WriteLine("Sub.NormalPrint()");
        }
        public override void OverridePrint() {
            Console.WriteLine("Sub.OverridePrint()");
        }
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
}