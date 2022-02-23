using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace VectorMatrics {
    class Numeric {
        public static void AAA() {
            Vector2 v0 = new Vector2();
            Vector2 v1 = new Vector2();

            // 벡터 연산
            Vector2 v2 = v0 + v1;
            Vector2 v3 = Vector2.Add(v0, v1);

            Matrix3x2 m0 = new Matrix3x2();
            Matrix3x2 m1 = new Matrix3x2();

            // 행렬 연산
            Matrix3x2 m2 = m0 * m1;
            Matrix3x2 m3 = Matrix3x2.Multiply(m0, m1);

            // 변환
            Vector2 v5 = Vector2.Transform(v0, m0);
        }
    }
}
