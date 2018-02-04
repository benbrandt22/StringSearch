namespace StringSearch.Math {
    public class SubtractionOperation : BaseMathOperation {

        public static SubtractionOperation NewRandomNonNegative(int inputMin, int inputMax) {
            var a = random.Next(inputMin, inputMax + 1);
            var b = random.Next(inputMin, inputMax + 1);
            var max = System.Math.Max(a, b);
            var min = System.Math.Min(a, b);
            return new SubtractionOperation(max, min);
        }

        public SubtractionOperation(decimal a, decimal b) : base(a,b) {}

        public override decimal Value => (A-B);
        public override string ProblemDisplay => $"{Format(A)} - {Format(B)}";
    }
}