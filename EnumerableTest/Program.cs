using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumerableTest {
    // Custom comparer for the Product class
    class EvenOddCompare : IEqualityComparer<int>
    {
        // Products are equal if their names and product numbers are equal.
        public bool Equals(int x, int y)
        {

            return (x % 2) == (y % 2);
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(int x)
        {
            return (x % 2).GetHashCode();
        }

    }

    class Program {
        static void Print(string name, object value) {
            Console.WriteLine($"{name} = {value}");
        }

        static void Print<T>(string name, IEnumerable<T> seq) {
            Console.WriteLine($"{name} = {string.Join(",", seq)}");
        }

        static void Main(string[] args) {
            var seq1 = Enumerable.Range(0, 10);
            var seq2 = Enumerable.Range(5, 10);
            
            var Distinct1 = seq1.Concat(seq2).Distinct();
            Print("Distinct1", Distinct1);
            var Distinct2 = seq1.Concat(seq2).Distinct(new EvenOddCompare());
            Print("Distinct2", Distinct2);

            // Cast : 지정된 타입의 제너릭 시퀀스로 캐스팅, 요소중 하나라도 캐스팅이 안되면 익셉션 발생
            var cast = seq1.Cast<int>();
            // OfType : 지정된 타입의 제너릭 시퀀스로 캐스팅, 캐스팅이 가능한 요소만 캐스팅
            var oftype = seq1.OfType<int>();

            // Except : 차집합
            var except = seq1.Except(seq2);
            Print("except", except);

            // Intersect : 교집합
            var intersect = seq1.Intersect(seq2);
            Print("intersect", intersect);

            // Union : 합집합
            var untion = seq1.Union(seq2);
            Print("untion", untion);

            // Concat : 단순 연결
            var concat = seq1.Concat(seq2);
            Print("concat", concat);

            // Zip : 두 시퀀스를 같은 인덱스끼리 결합하여 새로운 시퀀스 생성, 크기가 작은 시퀀스 크기 만큼
            var zip = seq1.Zip(seq2, (i1, i2) => $"{{{i1},{i2}}}");
            Print("zip", zip);

            // GroupBy : 지정한 키로 요소를 그루핑, 지연 실행됨, Linq To SQL 가능
            // 인덱서 사용 불가
            var groups = seq1.GroupBy(i => i % 2);
            foreach (var group in groups) {
                Print($"group[{group.Key}]", group);
            }

            // ToLookup : 지정한 키로 요소를 그루핑, 직접 실행됨
            // 지정된 키와 요소 그룹으로 HashTable 생성, 일대다 사전, 키가 중복되면 요소그룹으로 만드므로 Exception 발생 안함
            var lu = seq1.ToLookup(i => i % 2);
            foreach (var luitem in lu) {
                Print($"lu[{luitem.Key}]", luitem);
            }

            // SelectMany : 서브 시퀀스 연결
            var selectmany = groups.SelectMany(group => group);
            Print("selectmany", selectmany);

            // ToDictionary : 지정한 키와 요소로 HashTable 생성, 일대일 사전, 키가 중복되면 Exception 발생 
            var dic = seq1.ToDictionary(i => i.ToString());
            Print("dic", dic);

            // Join : 두 시퀀스에서 키가 같은 모든 조합리스트를 생성해 냄
            var joinseq1 = new int[] { 0, 1, 2, };
            var joinseq2 = new int[] { 0, 0, 1, 1, 2, 2 };
            var join = joinseq1.Join(joinseq2, i1 => i1, i2 => i2, (i1, i2) => Tuple.Create(i1, i2));
            Print("join", join);

            // GroupJoin : 두 시퀀스에서 Outter요소와 키가 같은 Inner 요소들을 그루핑 함 
            var groupjoin = joinseq1.GroupJoin(joinseq2, i1 => i1, i2 => i2, (i1, i2) => new {i1, i2});
            foreach (var groupjoinItem in groupjoin) {
                Print($"groupjoin[{groupjoinItem.i1}]", groupjoinItem.i2);
            }
        }
    }
}
