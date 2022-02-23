using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointFMatrics {
    class Gdip {
        public static void AAA() {
            PointF v0 = new PointF();
            PointF v1 = new PointF();

            // 벡터 연산
            PointF v2 = v0 + new SizeF(v1);
            PointF v3 = PointF.Add(v0, new SizeF(v1));

            Matrix m0 = new Matrix();
            Matrix m1 = new Matrix();

            // 행렬 연산
            m0.Multiply(m1);

            // 변환
            PointF v5 = new PointF();
            m0.TransformPoints(new PointF[] { v5 });
        }
    }
}
