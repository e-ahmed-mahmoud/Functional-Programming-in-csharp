using System;
using System.Collections.Generic;
using System.Text;

namespace Example_higher_order_functions
{
    public class Rule
    {
        public Func<Order , bool> Qulify { get; set; }
        public Func<Order , double> CalDiscount { get; set; }
    }
}
