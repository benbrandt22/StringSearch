using StringSearch.Core;

namespace StringSearch.Math
{
    public class MathProblemPuzzleEntry : IPuzzleEntry
    {
        public readonly IMathOperation MathOperation;

        public MathProblemPuzzleEntry(IMathOperation mathOperation)
        {
            this.MathOperation = mathOperation;
        }

        public string HiddenValue => MathOperation.ValueDisplay;
        public string DisplayValue => MathOperation.ProblemDisplay;
    }
}