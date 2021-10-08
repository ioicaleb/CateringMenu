using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Order
    {
        public string Id;
        public string Name;
        public decimal OrderCost;
        public int Quantity;
        public Order(string id, string name, int quantity, decimal orderCost)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            OrderCost = orderCost;
        }
    }
}
