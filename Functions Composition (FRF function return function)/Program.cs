using System;
using System.Collections.Generic;
using System.Linq;

namespace Functions_Composition__FRF_function_return_function_
{
    /*.
     * FRF: is high order function return function :  myFunc => { return x => x+1; } , so return delegate as pointer on lamada function
     * function Composition: is a function that compose or return another function or more 
     * high compuntac system: is system using mutple function compunations to get logic operation, in composition the mutiple combination can be 
     *   saved by build a generic function which can be return final logic function from combination functions it first App intialization
     *      example: calculate final cost of product which depend on customer type function, product type function, ship type function at first 
     *      intialization of App the total cost of product can be calculate and build pipeline to save each time calculation.
     * PipeLine vs Composition: pipeline : is a chain of functions for building ablock where the data passing from function to another, so it's    
     *      repersent data flow (numeric data flow), on other side, Composition: is a process of building sympolic block functions block where
     *      functions are the passing items (symblic flow).
     * 
     * functional programming at using composition can be repersented as a Tree where basic function define basic and pure leafs and then compose 
     *  with other functions to define higher level till define final function to define building block, so it's end with top parent or higher level
     *      which take arguements then pass to it's childs functions till get result, as that new behavior can be add then compose parent with 
     *      new features functions (composite pattern). that give stabiblity and predicatable behavior
     *  Note: FP: is all about building a pure functions and Granularity (divide large scal problem into sub-problems) for enhance testing and 
     *      maintenance, the operation of define any version of function will use define as App configuration (use at intializition).
     * .*/
    public static class Program
    {

        private static List<double> nums = new List<double> { 3, 4, 5, 16, 28, 20 };

        static void Main(string[] args)
        {
            #region building pipeline using Linq operators or nested calls
            //normal way for building pipe line using Linq operators
            nums.Where(n => n < 20).Select(n => AddOnces(n)).Select(n => SqrNum(n)).Select(n => SubTen(n)).ToList()
                .ForEach(n => Console.WriteLine(n));
            Console.ReadLine();
            //building pipe line using Linq operator with nested functions calling: addOnces() => SqrNum() => SubTen()
            nums.Where(n => n < 20).Select(n => SubTen(SqrNum(AddOnces(n)))).ToList()
                .ForEach(n => Console.WriteLine(n));
            #endregion

            Console.ReadLine();

            #region define ptr on function which define sepcific pipeline then use compose function delegate into Linq operators
            //composition function isn't generic as it's designed to accept only double datatypes.
            var composePtr = ComposeFunction(SubTen, SqrNum, AddOnces);
            nums.Where(n => n < 20).Select(n => composePtr(n)).ToList().ForEach(n => Console.WriteLine(n));
            #endregion

            #region building Generic composition function, Compose function can be more generic using Template/Generic programming <T1,T2> where
            //Compose Func is generic function which build pipeline of passing function ptrs
            // T1 => F <T1 , T2> => G(T2, T3) => Q(T3 , T4)... take T1 and back T4 res< T1 , T4 > == Func < F<T1 ,T2> , G<T2 , T3>, T4 > == Q(G(F(T1))) then each Funcion take Tn pramater and return Tn result, final is composePtr invole Functor.

            //compose functions nealy follow Composition pattern so pipeline can be modified or by adding new composition function with encapsulated behavior into new pipeline, pipeline chain can be designed in very long chain with compact defination for mutiple functions definations and chain will gradient linear as required.

            //define compose function as extention method which take first parameters T1 and return final result T3 , inner composed functions will pass as function paramters:     Func < T1 ,T3 > FuncName < T1,T2,T3> (Func<T1,T2> F , Func<T2,T3> G) => { return T1 => G(F(T1)) ) }; 
            
            //sol1: define compose function in one function
            Console.ReadLine();
            var genericCompose = ComposeFunction<double, double, double, double>(AddOnces, SqrNum, SubTen);
            nums.Select(n => genericCompose(n)).ToList().ForEach(x => Console.WriteLine(x));

            //sol2: define a generic function which can compose any 2 function and return func ptr in compose result so: f1.compose(f2) => f1(f2())
            Console.ReadLine();
            nums.Select(AddOneSqrSubTen()).ToList().ForEach(x => Console.WriteLine(x));     //auto invoke function and pass item to it 
            #endregion
        }


        //composition function: functions compose multiple function and return specific pipeline, take functions ptrs as parameters
        public static Func<double, double> ComposeFunction(Func<double, double> f1, Func<double, double> f2, Func<double, double> f3)
        {
            return num => f1(f2(f3(num)));
        }

        //Generic composition function
        public static Func<T1, T4> ComposeFunction<T1, T2, T3, T4>(Func<T1, T2> f1, Func<T2, T3> f2, Func<T3, T4> f3)
        {
            return num => f3(f2(f1(num)));
        }

        //compose function for compose any two method, define extension method on Func<> which can cancat any 2 methods
        public static Func<T1, T3> Compose<T1, T2, T3>(this Func<T1, T2> f1, Func<T2, T3> f2)
        {
            return (n) => f2(f1(n));
        }
        
        //define composition function
        public static Func<double,double> AddOneSqrSubTen()
        {
            Func<double,double> q1 = AddOnces;
            Func<double , double> q2 = SqrNum;
            Func<double, double> q3 = SubTen;
            return q1.Compose(q2).Compose(q3);
        }

        public static double AddOnces(double x) => x + 1;

        public static double SqrNum(double x) => x * x;

        public static double SubTen(double x) => x - 10;
    }
}
