using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Dapper;
using System.IO;


namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        class BookTitle
        {
            public int Id { get; set; }
            public string Author { get; set; }
            public string FileName { get; set; }
            public byte[] FileData { get; set; }
        }

        class Books
        {
            public int Id { get; set; }
            public string BookName { get; set; }
            public int Pages { get; set; }
            public int Price { get; set; }
            public int VisitorId { get; set; }
            public int BookNameId { get; set; }
        }
        public Form1()
        {
            InitializeComponent();
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-*******\Даниил; Initial Catalog = BestLibrary; Integrated Security = true"))
            {
                var res = connection.Query<Books>("select * from Books");
                foreach (Books item in res)
                {
                    if (item.BookName.Length < 8)
                    {
                        if (item.BookNameId != 0)
                            listBox1.Items.Add(item.Id + "\t" + item.BookName + "\t\t\t" + item.Pages + "\t" + item.Price + "\t" + item.VisitorId + "\t" + item.BookNameId);
                        else
                            listBox1.Items.Add(item.Id + "\t" + item.BookName + "\t\t\t" + item.Pages + "\t" + item.Price + "\t" + item.VisitorId + "\t" + "null");
                    }
                    else if (item.BookName.Length > 8 && item.BookName.Length < 17)
                    {
                        if (item.BookNameId != 0)
                            listBox1.Items.Add(item.Id + "\t" + item.BookName + "\t\t" + item.Pages + "\t" + item.Price + "\t" + item.VisitorId + "\t" + item.BookNameId);
                        else
                            listBox1.Items.Add(item.Id + "\t" + item.BookName + "\t\t" + item.Pages + "\t" + item.Price + "\t" + item.VisitorId + "\t" + "null");

                    }
                    else if (item.BookName.Length >= 17)
                    {
                        if (item.BookNameId != 0)
                            listBox1.Items.Add(item.Id + "\t" + item.BookName + "\t" + item.Pages + "\t" + item.Price + "\t" + item.VisitorId + "\t" + item.BookNameId);
                        else
                            listBox1.Items.Add(item.Id + "\t" + item.BookName + "\t" + item.Pages + "\t" + item.Price + "\t" + item.VisitorId + "\t" + "null");

                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"D:\";
            openFileDialog1.ShowDialog();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.FileName != "openFileDialog1" && listBox1.SelectedItem != null)
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-*******\Даниил; Initial Catalog = BestLibrary; Integrated Security = true"))
                {
                    int id = Convert.ToInt32(connection.ExecuteScalar($"select * from BookTitle where FileName ='{openFileDialog1.SafeFileName}'"));
                    connection.Execute($"update Books set BookTitleId ={id} where Id={(listBox1.SelectedItem.ToString())[0]};");
                }
            }
            else if(listBox1.SelectedItem == null)
            {
                MessageBox.Show("Choose a book for the selected cover");
            }
            else if(openFileDialog1.FileName == "openFileDialog1")
            {
                MessageBox.Show("Choose a picture for the cover");
            }
            
        }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

            byte[] tmp = null;

            using (FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open))
            {
                tmp = new byte[fs.Length];

                fs.Read(tmp, 0, (int)fs.Length);

            }

            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-*******\Даниил; Initial Catalog = BestLibrary; Integrated Security=true");
            conn.Open();

            SqlCommand command = new SqlCommand();

            command.Connection = conn;
            command.CommandText = "insert into BookTitle values (@Alias, @FileName,@FileData)";

            command.Parameters.Add("@Alias", SqlDbType.VarChar, 20);
            command.Parameters.Add("@FileName", SqlDbType.VarChar, 20);
            command.Parameters.Add("@FileData", SqlDbType.VarBinary, tmp.Length);

            command.Parameters["@Alias"].Value = openFileDialog1.SafeFileName.Replace(".jpg", "");
            command.Parameters["@FileName"].Value = openFileDialog1.SafeFileName;
            command.Parameters["@FileData"].Value = tmp;

            command.ExecuteNonQuery();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-*******\Даниил; Initial Catalog = BestLibrary; Integrated Security = true"))
            {
                var res = connection.Query<Books>("select * from Books");
                
                foreach (Books item in res)
                {
                    if (item.BookName.Length < 8)
                    {
                        if (item.BookNameId != 0)
                            listBox1.Items.Add(item.Id + "\t" + item.BookName + "\t\t\t" + item.Pages + "\t" + item.Price + "\t" + item.VisitorId + "\t" + item.BookNameId);
                        else
                            listBox1.Items.Add(item.Id + "\t" + item.BookName + "\t\t\t" + item.Pages + "\t" + item.Price + "\t" + item.VisitorId + "\t" + "null");
                    }
                    else if (item.BookName.Length > 8 && item.BookName.Length < 17)
                    {
                        if (item.BookNameId != 0)
                            listBox1.Items.Add(item.Id + "\t" + item.BookName + "\t\t" + item.Pages + "\t" + item.Price + "\t" + item.VisitorId + "\t" + item.BookNameId);
                        else
                            listBox1.Items.Add(item.Id + "\t" + item.BookName + "\t\t" + item.Pages + "\t" + item.Price + "\t" + item.VisitorId + "\t" + "null");

                    }
                    else if (item.BookName.Length >= 17 )
                    {
                        if (item.BookNameId != 0)
                            listBox1.Items.Add(item.Id + "\t" + item.BookName + "\t" + item.Pages + "\t" + item.Price + "\t" + item.VisitorId + "\t" + item.BookNameId);
                        else
                            listBox1.Items.Add(item.Id + "\t" + item.BookName + "\t" + item.Pages + "\t" + item.Price + "\t" + item.VisitorId + "\t" + "null");

                    }
                }
            }
        }
    }
}
