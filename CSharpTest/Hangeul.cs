using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest {
    public class Hangeul {
        private static readonly string inits      = "ㄱㄲㄴㄷㄸㄹㅁㅂㅃㅅㅆㅇㅈㅉㅊㅋㅌㅍㅎ";
        private static readonly string medians    = "ㅏㅐㅑㅒㅓㅔㅕㅖㅗㅘㅙㅚㅛㅜㅝㅞㅟㅠㅡㅢㅣ";
        private static readonly string finals     = " ㄱㄲㄳㄴㄵㄶㄷㄹㄺㄻㄼㄽㄾㄿㅀㅁㅂㅄㅅㅆㅇㅈㅊㅋㅌㅍㅎ";
        private static readonly char firstCh  = '가';
        private static readonly char lastCh   = '힣';

        public static char[] Explode(char ch) {
            if (ch < firstCh || ch > lastCh)
                throw new Exception("Not Hangeul charactor");

            int chIdx = ch-firstCh;
            int initIdx = chIdx / (medians.Length * finals.Length);
            int medianIdx = chIdx % (medians.Length * finals.Length) / finals.Length;
            int finalIdx = chIdx % finals.Length;

            char init = inits[initIdx];
            char median = medians[medianIdx];
            char final = finals[finalIdx];
            
            return new char[]{ init, median, final };
        }

        public static char Composite(char init, char median, char final) {
            int initIdx = inits.IndexOf(init);
            int medianIdx = medians.IndexOf(median);
            int finalIdx = finals.IndexOf(final);

            if (initIdx < 0 || medianIdx < 0|| finalIdx < 0)
                throw new Exception("Not Hangeul charactor part");

            char ch = (char)(firstCh + initIdx * medians.Length * finals.Length + medianIdx * finals.Length + finalIdx);
            return ch;
        }
    }
}
