using System;
using System.Collections.Generic;
using System.Linq;

namespace Coluseres
{
    class Program
    {
        /**
         * Colusers: is concept of making return function consume variables from its environmental scope, so returned function will access its outer
         *      scope, that make returned function not pure as it efected by outer environment.
         *  Colusers is the oppisit of purity princible, but that has advantages which its provide some benifits of OOP.
         *  Coluser main target is given returned function a memory so it could access outer scope, the parent function variable scope is end after 
         *      function returned but if variable used in inner function it excluded from function stack earseing and keeped alive as inner 
         *      function still alive
         * Note Colusers parent scope which keep variables alive is called Lexical scope.
         * Colusers can be say as: 
         *  1- Encapsulation for functional programming: where some data can be encapsulated into outer function then used in inner function.
         *  2- function with state: so returned function can keep some data alive that express about its state each time by read and write in state.
         *  3- function factory: where parent function work as a factory to return a different functions depend on its state and arguements.
         *  4- Object with single method: same as OOP where data/states can ecapsulated into outer function and only single function can consume it.
         *  Colusers says that is a limited edition from encapsulation.
         * **/
        static void Main(string[] args)
        {
            //var qr = Sqr(x: 3);                //function will return Func ptr of inner function which effect by given arguemnt to parent
            //Console.WriteLine(qr(3));
            //Console.WriteLine(Sqr(5)(2));       //imediate colusers function invokion 

            var employees = new List<(string Segment, double Salary)>
            {("c", 1200 ),("b", 1500 ),("a", 1400 )};

            var SalCalculator = employees.Select(e => (Id: e.Segment, SalCalculators: CalSalary(e.Salary))).ToList();

            Console.WriteLine($" segment {SalCalculator[0].Id} : {SalCalculator[0].SalCalculators(80)}");
            Console.WriteLine($" segment {SalCalculator[1].Id} : {SalCalculator[1].SalCalculators(100)}");
            Console.WriteLine($" segment {SalCalculator[2].Id} : {SalCalculator[2].SalCalculators(128)}");

            employees.Where(x => x.Segment == "c").Select(x => CalSalary(x.Salary)(50)).ToList().ForEach(x => Console.WriteLine($"total Sal : {x}"));
            employees.Where(x => x.Segment == "a").Select(x => CalSalary(x.Salary)(100)).ToList().ForEach(x => Console.WriteLine($"total Sal : {x}"));

        }

        public static Func<double, double> Sqr(double x)
        {
            var x2 = x * x;
            Console.WriteLine(x2);
            return (double x3) => x2 * x3;
        }

        public static Func<double, double> CalSalary(double salary)
        {
            var tax = salary * 0.02;
            Console.WriteLine(salary - tax);
            return bonus => (double)(salary - tax + bonus);
        }
    }
}
