using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigMacCo
{
    public class Stock
    {
        public int StockCode { get; set; }
        public string ItemName { get; set; }
        public string SupplierName { get; set; }
        public decimal UnitCost { get; set; }
        public int NumberRequired { get; set; }
        public int CurrentStockLevel { get; set; }

        // Method to update stock details
        public void UpdateStockDetails(int numberRequired, int currentStockLevel)
        {
            NumberRequired = numberRequired;
            CurrentStockLevel = currentStockLevel;
        }
    }
}