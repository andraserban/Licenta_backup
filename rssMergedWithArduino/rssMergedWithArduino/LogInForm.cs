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

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=ANDRA;Initial Catalog=AuthenticationCodeFirst;Integrated Security=True"); // making connection   
            SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM dbo.LogInDataBases WHERE username='" + txt_UserName.Text + "' AND password='" + txt_Password.Text + "'", con);
          
            DataTable dt = new DataTable(); //this is creating a virtual table  
            sda.Fill(dt);

              if (txt_UserName.Text == "" || txt_Password.Text == "")
              {
                     MessageBox.Show("Please provide username and password");
                       return;
               }
               if (dt.Rows[0][0].ToString() == "1")
               {
             
                this.Hide();
                new Form1().Show();
               }
                else
                   MessageBox.Show("Invalid username or password");
        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
        }
    }
}
