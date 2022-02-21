using FP_Higher_order_functions_and_Delegates;
using System;
using System.Collections.Generic;

namespace FunctionalProgramming
{
    class Program
    {
        //higher order functions: is function that take another function as arguement or return a function as output or both as Linq operators.
        //pure function: is function that return same output for same input so it's isolated from output environment and not touch external dependecies, by that ensure that function is pure and not effect by external resources and isolated from global variables and shared states, that make a function nearly to achieve first class citizen concept as seperated build unit and easy to test and mantain
        //FP paradiagm function must be pure and achieve purity concept.

        //define calculation parameters functions ptrs as private fileds as it is implmentation details in our class
        private static Func<int, (double x1, double x2)> calFoodParametersPtr = ProductParametersFood;
        private static Func<int, (double x1, double x2)> calBevParametersPtr = ProductParametersBevrage;
        private static Func<int, (double x1, double x2)> calRowMatParametersPtr = ProductParametersRowMaterial;



        static void Main(string[] args)
        {
            #region delegates and noraml functions calls
            //Delegates: strong type (input, output must define & can't change) function pionter class, intialize and define in stack and hold function adderess
            //func<argue1 type , argue2 type , .... , function return type or output variable > , unlike Action<argu1 , ...> no return type (void func)
            Func<double, double> test1FuncPtr = Test1;      //delegates must take pointed function signature
            Func<double, double> test2FuncPtr = Test2;      //delegates use in callbacks and work as variables so can be assign, call, make a collection, class property, ...

            List<Func<double, double>> functPtrsList = new List<Func<double, double>> { test1FuncPtr, test2FuncPtr };

            //isn't high order function, where normal calling to test1() will return double and the return will pass to call of Test2, Test1(Test2(5))
            //so as a function don't use a function or function pointer in its signature it isn't a higher order function
            Console.WriteLine(test2FuncPtr(test1FuncPtr(5)));
            Console.WriteLine(test1FuncPtr(test2FuncPtr(5)));

            Console.WriteLine(functPtrsList[0](5));         //delgates invoke == test1(5)
            Console.WriteLine(functPtrsList[1](5));

            Console.WriteLine(functPtrsList[0](functPtrsList[1](5)));       // as test1(test2(5)) not higher order function but pure function 
            #endregion

            #region higher order function that must take a function or function ptrs as argument or as a return type

            Console.WriteLine(Test3(test1FuncPtr, 5));     //higher order function take a func ptr as arguement and return function call, result test2(5)
            Console.WriteLine(Test3(Test2, 5));            // as Test2(5)

            //example2 calculate product discount based product Index type
            var order = new Order() { ProductIndex = 2, ProductPrice = 50 };
            var calParameterFunc = (order.ProductIndex == 1) ? calFoodParametersPtr : (order.ProductIndex == 2) ? calBevParametersPtr : calRowMatParametersPtr;
            Console.WriteLine($"price is: {order.ProductPrice} \t discount is : {CalProductDiscount(order.ProductIndex, calParameterFunc)} \t final price : {order.ProductPrice - CalProductDiscount(order.ProductIndex, calParameterFunc)}");
            
            var orders = new List<Order> {
                new Order { ProductIndex = 1, ProductPrice = 50 },
                new Order { ProductIndex = 2, ProductPrice = 50 },
                new Order { ProductIndex = 3, ProductPrice = 50 }
            };

            orders.ForEach(order => {
                Console.WriteLine($"price is: {order.ProductPrice} \t discount is : {CalProductDiscount(order.ProductIndex, calParameterFunc)} \t final price : {order.ProductPrice - CalProductDiscount(order.ProductIndex, calParameterFunc)}");
            });


            #endregion
        }

        public static double Test1(double x)
        {
            return x / 2;
        }
        public static double Test2(double x)
        {
            return x / 4 + 1;
        }
        // higher order function where a function take function ptr as parameter and return function call
        public static double Test3(Func<double, double> func, double value)
        {
            return func(value);
        }

        //calculate product discount parameters based on product index, where we have 3 product types each type has special logic for calculate discount
        public static (double x1, double x2) ProductParametersFood(int productIndex) =>
            (x1: productIndex + 10 / (double)(productIndex + 100), x2: productIndex + 10);

        public static (double x1, double x2) ProductParametersBevrage(int productIndex) =>
            (x1: productIndex + 10 / (double)(productIndex + 100), x2: productIndex + 10);

        public static (double x1, double x2) ProductParametersRowMaterial(int productIndex)
            => (x1: productIndex + 10 / (double)(productIndex + 100), x2: productIndex + 10);

        public static double CalProductDiscount(int productIndex, Func<int, (double x1, double x2)> calProductParasFunc)
        {
            (double x1, double x2) = calProductParasFunc(productIndex);
            return x1 + x2;
        }
    }
}
