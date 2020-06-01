using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnBoard.Models
{
    public class Sales
    {
        public int SalesID { get; set; }
        
        public string DateSold { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public int StoreID { get; set; }
    }
}