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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btn_submitRegister_Click(object sender, EventArgs e)
        {
            if (txt_password.Text != txt_confirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match");
            }
                

            SqlConnection con = new SqlConnection(@"Data Source=ANDRA;Initial Catalog=AuthenticationCodeFirst;Integrated Security=True");
            con.Open();

            SqlCommand sqlCmd_register = new SqlCommand("INSERT INTO dbo.RegisterDataBases (firstName,lastName,username,password) VALUES (@firstName,@lastName,@username,@password)", con);
            SqlCommand sqlCmd_login = new SqlCommand("INSERT INTO dbo.LogInDataBases (username,password) VALUES (@username,@password)", con);

            sqlCmd_register.Parameters.AddWithValue("@firstName", txt_fistName.Text.Trim());
            sqlCmd_register.Parameters.AddWithValue("@lastName", txt_lastName.Text.Trim());
            sqlCmd_register.Parameters.AddWithValue("@username", txt_username.Text.Trim());
            sqlCmd_register.Parameters.AddWithValue("@password", txt_password.Text.Trim());

           sqlCmd_login.Parameters.AddWithValue("@username", txt_username.Text.Trim());
           sqlCmd_login.Parameters.AddWithValue("@password", txt_password.Text.Trim());

            sqlCmd_register.ExecuteNonQuery();
            sqlCmd_login.ExecuteNonQuery();

            MessageBox.Show("Registration is successful");

            this.Hide();
            LogInForm loginForm = new LogInForm();
            loginForm.Show();

        }
    }
}
