using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Data;
using System.Windows.Forms;
using System.Windows.Input;
using System.IO;
using ExcelDataReader;
using Spire.Xls;
using System.Data.OleDb;
using System.Data.SqlClient;
using OfficeOpenXml.Style;
using System.Linq;
using OfficeOpenXml;

namespace databaseGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Person> people = new List<Person>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void add(string Vname,int Vage, string Vgender,string Vid = "", string Venergy = "")
        {
            Person p = new Person(Vname, Vage, Vgender, Vid, Venergy);
            if (manualAdd.IsChecked == true)
            {
                p = new Person(name.Text, Int16.Parse(age.Text), gender.Text);
            }

            //people.Add(per);

            Console.WriteLine("Getting Connection ...");
            SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbgame"].ConnectionString);
             conn.Open();
            string updateScheduleId = "insert into [dbo].[Character] ( Name, Age, Gender, ID, Energy) values( @name, @age, @gender, @id, @energy)";


            using (SqlCommand cmd = new SqlCommand(updateScheduleId, conn))
            {
                cmd.Parameters.Add("@name", SqlDbType.VarChar);
                cmd.Parameters.Add("@age", SqlDbType.Int);
                cmd.Parameters.Add("@gender", SqlDbType.VarChar);
             cmd.Parameters.Add("@id", SqlDbType.VarChar);
               cmd.Parameters.Add("@energy", SqlDbType.VarChar);

                cmd.Parameters["@name"].Value = p.name;
                cmd.Parameters["@age"].Value = p.age;
                cmd.Parameters["@gender"].Value = p.gender;
                cmd.Parameters["@id"].Value = p.ID;
               cmd.Parameters["@energy"].Value = p.energy;
               cmd.ExecuteNonQuery();

                conn.Close();

                conn.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Select Num, Name, Age, Gender, ID, Energy FROM [dbo].[Character]", conn);
                da.Fill(dt);
                dg.ItemsSource = dt.AsDataView();
                conn.Close();

            }
        }

        private void import_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbgame"].ConnectionString);
            conn.Open();
            SqlCommand cmd1 = new SqlCommand("DELETE FROM [dbo].[Character]", conn);
            cmd1.ExecuteNonQuery();
            SqlCommand cmd = new SqlCommand("DELET FROM [dbo].[Character]", conn);
            conn.Close();
            using (var package = new ExcelPackage(new FileInfo(textbox1.Text)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];

                for (int i = workSheet.Dimension.Start.Row + 1;
                    i <= workSheet.Dimension.End.Row;
                    i++)
                {
                    add(workSheet.Cells[i, 2].Value.ToString(), Int16.Parse(workSheet.Cells[i, 3].Value.ToString()), workSheet.Cells[i, 4].Value.ToString(), workSheet.Cells[i, 5].Value.ToString(), workSheet.Cells[i, 6].Value.ToString());

                }

            }
           
        }

       

        

        private void dg_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }


        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            add(name.Text, Int16.Parse(age.Text), gender.Text);
            System.Windows.MessageBox.Show("successfully add");
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbgame"].ConnectionString;
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM [dbo].[Character]", conn);
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select Num, Name, Age, Gender, ID, Energy FROM [dbo].[Character]", conn);
            da.Fill(dt);
            dg.ItemsSource = dt.AsDataView();
            conn.Close();
        }

        private void load_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "Text Files (.xlsx)|*.xlsx|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            openFileDialog1.Multiselect = false;
            DialogResult result = openFileDialog1.ShowDialog();

            if (result.ToString() == "OK")
            {
                textbox1.Text = openFileDialog1.FileName;
            }
            else
            {

            }
        }

        private void email_Click(object sender, RoutedEventArgs e)
        {
            SqlDataReader rdr = null;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["dbgame"].ConnectionString;
            SqlCommand cmd = new SqlCommand(
                "select * from [dbo].[Character]", conn);
            try
            {
                // open the connection
                conn.Open();

                // 1. get an instance of the SqlDataReader
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    // get the results of each column
                    int numval = (int)rdr["Num"];
                    string nameval = (string)rdr["Name"];
                    int ageval = (int)rdr["Age"];
                    string genderval = (string)rdr["Gender"];
                    string idval = (string)rdr["ID"];
                    string energyval = (string)rdr["Energy"];

                    // print out the results
                    Transat t1 = new Transat(nameval, idval, genderval);
                    t1.transatRun();
                    
                }
                Console.ReadLine();
            }
            finally
            {
                // 3. close the reader
                if (rdr != null)
                {
                    rdr.Close();
                }

                // close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        }

        }

