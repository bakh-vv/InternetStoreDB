using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace InternetStoreDBase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int initialNumber;
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                SqlConnection thisConnection = new SqlConnection(@"Server=(local);Database=StoreDBase;Trusted_Connection=Yes;");
                thisConnection.Open();

                string Get_Data = "SELECT * FROM Album";

                SqlCommand cmd = thisConnection.CreateCommand();
                cmd.CommandText = Get_Data;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Albums");
                sda.Fill(dt);

                dataGrid1.ItemsSource = dt.DefaultView;
                initialNumber = dataGrid1.Items.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {
            OrdersWindow ow = new OrdersWindow();
            ow.Show();
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            for (int i = initialNumber-1; i < dataGrid1.Items.Count - 1; i++)
            {
                dataGrid1.SelectedIndex = i;
                DataRowView rw = (DataRowView)dataGrid1.SelectedItems[0];
                try
                {


                    using (SqlConnection connection = new SqlConnection(@"Server=(local);Database=StoreDBase;Trusted_Connection=Yes;"))
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO Album (albumID, Name, Year, Duration, artistID, Availability, typeID, Price) VALUES (@aI, @Nm, @yr, @dr, @arI, @av, @tI, @pr)";

                        command.Parameters.AddWithValue("@aI", rw[0]);
                        command.Parameters.AddWithValue("@Nm", rw[1]);
                        command.Parameters.AddWithValue("@yr", rw[2]);
                        command.Parameters.AddWithValue("@dr", rw[3]);
                        command.Parameters.AddWithValue("@arI", rw[4]);
                        command.Parameters.AddWithValue("@av", rw[5]);
                        command.Parameters.AddWithValue("@tI", rw[6]);
                        command.Parameters.AddWithValue("@pr", rw[7]);

                        connection.Open();

                        command.ExecuteNonQuery();
                    }


                }
                catch (SqlException ex)
                {
                    //Log exception
                    //Display Error message
                }
            }
            for (int i = 0; i < dataGrid1.Items.Count-1; i++)
            {
                dataGrid1.SelectedIndex = i;
                DataRowView rw = (DataRowView)dataGrid1.SelectedItems[0];
                try
                {

                    using (SqlConnection connection = new SqlConnection(@"Server=(local);Database=StoreDBase;Trusted_Connection=Yes;"))
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        string comText = String.Format("UPDATE Album SET albumID=@aI, Name=@Nm, Year=@yr, Duration=@dr, artistID=@arI, Availability=@av, typeID=@tI, Price=@pr WHERE albumID = {0}", i+1);
                        command.CommandText = comText;

                        command.Parameters.AddWithValue("@aI", rw[0]);
                        command.Parameters.AddWithValue("@Nm", rw[1]);
                        command.Parameters.AddWithValue("@yr", rw[2]);
                        command.Parameters.AddWithValue("@dr", rw[3]);
                        command.Parameters.AddWithValue("@arI", rw[4]);
                        command.Parameters.AddWithValue("@av", rw[5]);
                        command.Parameters.AddWithValue("@tI", rw[6]);
                        command.Parameters.AddWithValue("@pr", rw[7]);

                        connection.Open();

                        command.ExecuteNonQuery();
                    }

                }
                catch (SqlException ex)
                {
                    //Log exception
                    //Display Error message
                }
                
            }
            MessageBox.Show("Изменения сохранены в базе данных.");
            

        }

        private void btnStats_Click(object sender, RoutedEventArgs e)
        {
            Stats sts = new Stats();
            sts.Show();
        }

        private void btnOutOfStock_Click(object sender, RoutedEventArgs e)
        {
            OutOfStock oos = new OutOfStock();
            oos.Show();
        }

       
    }
}
