using System;

namespace StringSearch.Math {
    public abstract class BaseMathOperation : IMathOperation {
        public readonly decimal A;
        public readonly decimal B;

        protected static Random random = new Random();

        protected BaseMathOperation(decimal a, decimal b) {
            this.A = a;
            this.B = b;
        }

        public abstract decimal Value { get; }
        public abstract string ProblemDisplay { get; }
        public virtual string ValueDisplay => Format(Value);

        protected string Format(decimal num) {
            return num.ToString("0.###");
        }
    }
}
