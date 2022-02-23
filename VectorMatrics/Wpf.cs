using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace VectorMatrics {
    class Wpf {
        public static void AAA() {
            Vector v0 = new Vector();
            Vector v1 = new Vector();
            
            // 벡터 연산
            Vector v2 = v0 + v1;
            Vector v3 = Vector.Add(v0, v1);

            Matrix m0 = new Matrix();
            Matrix m1 = new Matrix();

            // 행렬 연산
            Matrix m2 = m0 * m1;
            Matrix m3 = Matrix.Multiply(m0, m1);

            // 변환
            Vector v5 = Vector.Multiply(v0, m0);
            Vector v6 = v0 * m0;
            Vector v7 = m0.Transform(v0);
        }
    }
}
