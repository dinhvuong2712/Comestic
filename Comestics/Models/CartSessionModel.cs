using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Comestics.Models
{
    public class CartSessionModel
    {
        public int IdCart { get; set; }
        public long PrId { get; set; }
        public string PrName { get; set; }
        public string Images { get; set; }
        public int Amounts { get; set; }
        public int AmountsMax { get; set; }
        public int Price { get; set; }
        public int Status { get; set; }
        public int IntoMoney
        {
            get
            {
                return Amounts * Price;
            }
        }
    }
}