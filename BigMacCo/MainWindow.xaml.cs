using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Data;


namespace BigMacCo
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Stock> stocks;
        private string connectionString = "LAPTOP-RC5UGU1C\\SQLEXPRESS2019;Database=BigMacCo;User Id=sa;Password=Conestoga1;Trusted_Connection=True";

      

        public MainWindow()
        {
            InitializeComponent();
            LoadStockData();
        }

        private void LoadStockData()
        {
            stocks = new ObservableCollection<Stock>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Stock", connection);
                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    stocks.Add(new Stock
                    {
                        StockCode = reader.GetInt32(0),
                        ItemName = reader.GetString(1),
                        SupplierName = reader.GetString(2),
                        UnitCost = reader.GetDecimal(3),
                        NumberRequired = reader.GetInt32(4),
                        CurrentStockLevel = reader.GetInt32(5)
                    });
                }
            }

            StockDataGrid.ItemsSource = stocks;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Stock newStock = new Stock { StockCode = 0, ItemName = "", SupplierName = "", UnitCost = 0, NumberRequired = 0, CurrentStockLevel = 0 };
            StockDetailsWindow detailsWindow = new StockDetailsWindow(newStock);
            if (detailsWindow.ShowDialog() == true)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("INSERT INTO Stock (ItemName, SupplierName, UnitCost, NumberRequired, CurrentStockLevel) VALUES (@ItemName, @SupplierName, @UnitCost, @NumberRequired, @CurrentStockLevel)", connection);
                        command.Parameters.AddWithValue("@ItemName", newStock.ItemName);
                        command.Parameters.AddWithValue("@SupplierName", newStock.SupplierName);
                        command.Parameters.AddWithValue("@UnitCost", newStock.UnitCost);
                        command.Parameters.AddWithValue("@NumberRequired", newStock.NumberRequired);
                        command.Parameters.AddWithValue("@CurrentStockLevel", newStock.CurrentStockLevel);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                LoadStockData();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Stock selectedStock = (Stock)StockDataGrid.SelectedItem;
            if (selectedStock != null)
            {
                StockDetailsWindow detailsWindow = new StockDetailsWindow(selectedStock);
                if (detailsWindow.ShowDialog() == true)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("UPDATE Stock SET ItemName = @ItemName, SupplierName = @SupplierName, UnitCost = @UnitCost, NumberRequired = @NumberRequired, CurrentStockLevel = @CurrentStockLevel WHERE StockCode = @StockCode", connection);
                        command.Parameters.AddWithValue("@StockCode", selectedStock.StockCode);
                        command.Parameters.AddWithValue("@ItemName", selectedStock.ItemName);
                        command.Parameters.AddWithValue("@SupplierName", selectedStock.SupplierName);
                        command.Parameters.AddWithValue("@UnitCost", selectedStock.UnitCost);
                        command.Parameters.AddWithValue("@NumberRequired", selectedStock.NumberRequired);
                        command.Parameters.AddWithValue("@CurrentStockLevel", selectedStock.CurrentStockLevel);
                        command.ExecuteNonQuery();
                    }
                    LoadStockData();
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Stock selectedStock = (Stock)StockDataGrid.SelectedItem;
            if (selectedStock != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM Stock WHERE StockCode = @StockCode", connection);
                    command.Parameters.AddWithValue("@StockCode", selectedStock.StockCode);
                    command.ExecuteNonQuery();
                }
                LoadStockData();
            }
        }

        private void StockDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Stock selectedStock = (Stock)StockDataGrid.SelectedItem;
            if (selectedStock != null)
            {
                StockCodeTextBlock.Text = selectedStock.StockCode.ToString();
                ItemNameTextBlock.Text = selectedStock.ItemName;
                SupplierNameTextBlock.Text = selectedStock.SupplierName;
                UnitCostTextBlock.Text = selectedStock.UnitCost.ToString();
                NumberRequiredTextBlock.Text = selectedStock.NumberRequired.ToString();
                CurrentStockLevelTextBlock.Text = selectedStock.CurrentStockLevel.ToString();

                EditButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
                DetailsPanel.Visibility = Visibility.Visible;
            }
            else
            {
                EditButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
                DetailsPanel.Visibility = Visibility.Collapsed;
            }
        }
    }
}