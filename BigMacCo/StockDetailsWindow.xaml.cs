using System;
using System.Windows;

namespace BigMacCo
{
    public partial class StockDetailsWindow : Window
    {
        public Stock Stock { get; private set; }

        public StockDetailsWindow(Stock stock)
        {
            InitializeComponent();
            Stock = stock;

            // If editing an existing stock, populate the fields
            if (Stock.StockCode > 0)
            {
                ItemNameTextBox.Text = Stock.ItemName;
                SupplierNameTextBox.Text = Stock.SupplierName;
                UnitCostTextBox.Text = Stock.UnitCost.ToString();
                NumberRequiredTextBox.Text = Stock.NumberRequired.ToString();
                CurrentStockLevelTextBox.Text = Stock.CurrentStockLevel.ToString();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Stock.ItemName = ItemNameTextBox.Text;
                Stock.SupplierName = SupplierNameTextBox.Text;
                Stock.UnitCost = decimal.Parse(UnitCostTextBox.Text);
                Stock.NumberRequired = int.Parse(NumberRequiredTextBox.Text);
                Stock.CurrentStockLevel = int.Parse(CurrentStockLevelTextBox.Text);

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}