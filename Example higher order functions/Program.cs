using System;
using System.Collections.Generic;
using System.Linq;

namespace Example_higher_order_functions
{
    class Program
    {
        //private static Func<Order, bool> _qulifyR1 = QulifyRule1;
        //private static Func<Order, bool> _qulifyR2 = QulifyRule2;
        //private static Func<Order, bool> _qulifyR3 = QulifyRule3;
        //private static Func<Order, bool> _qulifyR4 = QulifyRule4;
        //private static Func<Order, double> _calR1 = CalRule1;
        //private static Func<Order, double> _calR2 = CalRule2;
        //private static Func<Order, double> _calR3 = CalRule3;
        
        static void Main(string[] args)
        {
            var rules = new List<Rule> { 
                new Rule { Qulify = QulifyRule1 , CalDiscount= CalRule1},
                new Rule { Qulify = QulifyRule2 , CalDiscount= CalRule2},
                new Rule { Qulify = QulifyRule3 , CalDiscount= CalRule3},
                new Rule { Qulify = QulifyRule4 , CalDiscount= CalRule4}};

            var orders = new List<Order>
            {
                new Order { ExpireDate = DateTime.Now.AddMonths(-1) , ProductIndex = 1 , Quantity = 50 , UnitePrice = 100 },
                new Order { ExpireDate = DateTime.Now.AddMonths(1) , ProductIndex = 2 , Quantity = 500 , UnitePrice = 70 },
                new Order { ExpireDate = DateTime.Now.AddMonths(2) , ProductIndex = 3 , Quantity = 200 , UnitePrice = 60 },
                new Order { ExpireDate = DateTime.Now.AddMonths(-1) , ProductIndex = 4 , Quantity = 150 , UnitePrice = 350 },
            };

            var disAllOrders = CalCulateDiscount(orders, rules);

            disAllOrders.ToList().ForEach(d => Console.WriteLine($"discount for order is \t : {d}  "));
        }

        public static IEnumerable<double> CalCulateDiscount(List<Order> orders, List<Rule> rules)
        {
            var res = from order in orders
                       where order != null
                       select (from rule in rules 
                            where rule.Qulify(order) 
                            select (new { discount = rule.CalDiscount(order) }))
                            .OrderBy(d => d.discount).Take(3).Average(dis => dis.discount);
            return res ;
        }

        #region rules functions defination 
        //rule 1 expire date
        public static bool QulifyRule1(Order order) => order.ExpireDate <= DateTime.Now.AddMonths(-1);
        public static double CalRule1(Order order) => (10 * order.UnitePrice / 100) ;

        //rule 2 Quntity > 50
        public static bool QulifyRule2(Order order) => order.Quantity > 50;
        public static double CalRule2(Order order) => (5 * order.UnitePrice / 100);

        //rule 3 Unite Price > 200$
        public static bool QulifyRule3(Order order) => order.UnitePrice >= 200 ;
        public static double CalRule3(Order order) => (12 * order.UnitePrice / 100);

        //rule 4 expire date < 1 month and unite price > 300$
        public static bool QulifyRule4(Order order) => order.ExpireDate <= DateTime.Now.AddMonths(-1) && order.UnitePrice >= 300;
        public static double CalRule4(Order order) => (20 * order.UnitePrice / 100);



        #endregion
    }
}
