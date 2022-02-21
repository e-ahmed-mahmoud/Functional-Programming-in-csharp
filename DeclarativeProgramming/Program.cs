using System;
using System.Collections.Generic;
using System.Linq;

namespace DeclarativeProgramming
{
    class Program
    {
        private static List<double> nums = new List<double> { 3, 4, 5, 16, 28, 20 };
        static void Main(string[] args)
        {
            #region impreative programming
            foreach (var item in nums)
            {
                Console.WriteLine(SubTen(SqrNum(AddOnces(item))));
            }
            #endregion

            #region Declarative programming
            Console.WriteLine("with Decelarative code");
            //use Linq operator(select, where , orderby ...)  then passing function.
            nums.Select(x => AddOnces(x)).Select(x => SqrNum(x)).Where(x => x < 70).Take(2).Select(x => SubTen(x)).ToList()
                .ForEach(x => Console.WriteLine(x));

            #endregion

        }

        public static double AddOnces(double x)
        {
            return x + 1;
        }

        public static double SqrNum(double x) => x * x;

        public static double SubTen(double x) => x - 10;
    }
}
