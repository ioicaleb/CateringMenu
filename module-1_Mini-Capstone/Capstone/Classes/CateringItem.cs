using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    /// <summary>
    /// This represents a single catering item in your system
    /// </summary>
    public class CateringItem
    {
        public string Type { get; }

        public string Id { get; }

        public string Name { get; }

        public int Quantity { get; set; }

        public decimal Price { get; }

        public CateringItem(string type, string id, string name, decimal price)
        {
            Type = type;
            Id = id;
            Name = name;
            Quantity = 25;
            Price = price;
        }


        public override string ToString()
        {
            string details = $"{Id} {Type}   {Name}   {Quantity}   {Price.ToString("C")}";
            if (Quantity == 0)
            {
                details += " SOLD OUT";
            }
            return details;
        }
    }
}
