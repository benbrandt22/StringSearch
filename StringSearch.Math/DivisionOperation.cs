using System.Collections.Generic;
using System.Linq;
using StringSearch.Shared;

namespace StringSearch.Math {
    public class DivisionOperation : BaseMathOperation {

        public static DivisionOperation NewRandomIntegerResult(int dividendMin, int dividendMax) {
            var dividend = random.Next(dividendMin, dividendMax + 1);
            var divisor = GetFactors(dividend).ToList().GetRandom();
            return new DivisionOperation(dividend, divisor);
        }

        public DivisionOperation(decimal a, decimal b) : base(a,b) {}

        public override decimal Value => (A/B);
        public override string ProblemDisplay => $"{Format(A)} ÷ {Format(B)}";

        private static IEnumerable<int> GetFactors(int x) {
            for (int i = 1; i * i <= x; i++) {
                if (0 == (x % i)) {
                    yield return i;
                    if (i != (x / i)) {
                        yield return x / i;
                    }
                }
            }
        }

    }
}