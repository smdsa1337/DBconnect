using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace DBconnect
{
    public partial class MainWindow : Window
    {

        private SqlConnection SqlConnection = new SqlConnection(@"Data Source=192.168.1.249; Initial catalog=DBconnect; User id=ws1; Password=ws1");

        private SqlCommand SqlCommand = new SqlCommand();

        private SqlDataAdapter SqlDataAdapter = new SqlDataAdapter();

        private DataTable dataTable;

        private String editTextString;

        private String comboBoxItem;

        private String query;

        public MainWindow()
        {
            InitializeComponent();
            SqlConnection.Open();
            Update();
        }

        private void Delete()
        {
            if(dataGrid.SelectedIndex >= 0)
            {
                int sa = dataGrid.SelectedIndex;
                DataRow dataRow = dataTable.Rows[sa];
                string ass = dataRow[0].ToString(); 
                query = "DELETE FROM [DBconnect].[dbo].[Check] WHERE id=" + ass;
                SqlCommand = new SqlCommand(query, SqlConnection);
                SqlCommand.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("Не выбран элемент");
            }
            Update();
        }

        private void Add()
        {
            if(editText.Text != String.Empty)
            {
                editTextString = editText.Text;
                query = "INSERT INTO [DBconnect].[dbo].[Check] (name) VALUES ('" + editTextString + "')";
                SqlCommand = new SqlCommand(query, SqlConnection);
                SqlCommand.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("Где текст?");
            }
            Update();
        }

        private void Change()
        {
            if (dataGrid.SelectedIndex >= 0 && editText.Text != String.Empty)
            {
                int sa = dataGrid.SelectedIndex;
                DataRow dataRow = dataTable.Rows[sa];
                string ass = dataRow[0].ToString();
                editTextString = editText.Text;
                query = "UPDATE [DBconnect].[dbo].[Check] SET name='" + editTextString + "' WHERE id=" + ass;
                SqlCommand = new SqlCommand(query, SqlConnection);
                SqlCommand.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("Не выбран элемент или нет текста");
            }
            Update();
        }

        private void Update()
        {
            query = "SELECT * FROM [DBconnect].[dbo].[Check]";
            SqlCommand = new SqlCommand(query,SqlConnection);
            SqlCommand.ExecuteNonQuery();
            SqlDataAdapter = new SqlDataAdapter(SqlCommand);
            dataTable = new DataTable();
            SqlDataAdapter.Fill(dataTable);
            dataGrid.ItemsSource = dataTable.DefaultView;
            editText.Text = String.Empty;
            comboBox.Items.Clear();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                comboBox.Items.Add(dataRow[0].ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Add();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Delete();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Change();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            SqlConnection.Close();
        }
    }
}