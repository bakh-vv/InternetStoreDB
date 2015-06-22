using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace InternetStoreDBase
{
    /// <summary>
    /// Interaction logic for OutOfStock.xaml
    /// </summary>
    public partial class OutOfStock : Window
    {
        public OutOfStock()
        {
            InitializeComponent();

            try
            {
                SqlConnection thisConnection = new SqlConnection(@"Server=(local);Database=StoreDBase;Trusted_Connection=Yes;");
                thisConnection.Open();

                string Get_Data = "select * From Album Where Availability <10";

                SqlCommand cmd = thisConnection.CreateCommand();
                cmd.CommandText = Get_Data;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("StockInfo");
                sda.Fill(dt);

                dataGridStock.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
