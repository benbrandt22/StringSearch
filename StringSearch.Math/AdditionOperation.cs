namespace StringSearch.Math {
    public class AdditionOperation : BaseMathOperation {

        public static AdditionOperation NewRandom(int inputMin, int inputMax) {
            return new AdditionOperation(random.Next(inputMin, inputMax + 1), random.Next(inputMin, inputMax + 1));
        }

        public AdditionOperation(decimal a, decimal b) : base(a, b) {}

        public override decimal Value => (A+B);
        public override string ProblemDisplay => $"{Format(A)} + {Format(B)}";
    }
}