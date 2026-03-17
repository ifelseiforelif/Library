using Calculator;

namespace Books.Tests
{
    public class BookTest
    {
        [Fact]
        public void CalculatorSum_FiveAndTwo_ReturnsSeven()
        {
            int a = 5;
            int b = 2;
            

            int result = 7;

            int anwser = Calc.Sum(a, b);
            Assert.Equal(result, anwser);

        }

        [Fact]
        public void CalculatorMult_FiveAndTwo_ReturnsTen()
        {
            int a = 5;
            int b = 2;


            int result = 10;

            int anwser = Calc.Mult(a, b);
            Assert.Equal(result, anwser);

        }
    }
}