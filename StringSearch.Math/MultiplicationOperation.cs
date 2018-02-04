namespace StringSearch.Math {
    public class MultiplicationOperation : BaseMathOperation {

        public static MultiplicationOperation NewRandom(int inputMin, int inputMax) {
            return new MultiplicationOperation(random.Next(inputMin, inputMax + 1), random.Next(inputMin, inputMax + 1));
        }

        public MultiplicationOperation(decimal a, decimal b) : base(a, b) {}

        public override decimal Value => (A*B);
        public override string ProblemDisplay => $"{Format(A)} × {Format(B)}";
    }
}