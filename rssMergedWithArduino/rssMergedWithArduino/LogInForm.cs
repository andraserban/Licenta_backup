using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rssMergedWithArduino
{
    public partial class LogInForm : Form
    {
        public LogInForm()
        {
            InitializeComponent();
        }

        //Connection String
        //  string cs = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\MyDatabase.mdf;Integrated Security=True;";

        string cs = @"Data Source=ANDRA;" + "Initial Catalog=AuthenticationCodeFirst;" + "Integrated Security=SSPI;" + "providerName = System.Data.SqlCLient";
        //btn_Submit Click event
        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (txt_UserName.Text == "" || txt_Password.Text == "")
            {
                MessageBox.Show("Please provide UserName and Password");
                return;
            }
            try
            {
                //Create SqlConnection
                SqlConnection con = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("Select * from tbl_LogInDatabases where UserName=@username and Password=@password", con);
                cmd.Parameters.AddWithValue("@username", txt_UserName.Text);
                cmd.Parameters.AddWithValue("@password", txt_Password.Text);
                con.Open();
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                con.Close();
                int count = ds.Tables[0].Rows.Count;
                
                if (count == 1)
                {
                    MessageBox.Show("Login Successful!");
                    this.Hide();
                    Form1 form = new Form1();
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Login Failed!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
