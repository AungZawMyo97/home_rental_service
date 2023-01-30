using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeApplianceRentalService
{
    public partial class Login : Form
    {
        SqlCommand cmd;
        SqlConnection cn;
        SqlDataReader dr;
        DataSet ds;

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""F:\Users\Aung Zaw Myo\Projects\HomeApplianceRentalService\Database1.mdf"";Integrated Security=True");
            cn.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Registration registration = new Registration();
            registration.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (password.Text != string.Empty || username.Text != string.Empty)
            {
                cmd = new SqlCommand();
                cmd = cn.CreateCommand();
                cmd.CommandText = "select * from login where username = @username;";
                cmd.Parameters.AddWithValue("@username", username.Text);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                if (!DBNull.Value.Equals(ds.Tables[0].Rows[0]["password"]))
                {
                    var passwordDb = ds.Tables[0].Rows[0]["password"].ToString();

                    if (password.Text != passwordDb)
                    {
                        cmd.CommandText = "update login set fail_count = fail_count + 1 where username = @failusername;";
                        cmd.Parameters.AddWithValue("@failusername", username.Text);
                        cmd.ExecuteReader();
                        MessageBox.Show("Wrong credentials. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        this.Hide();
                        Home home = new Home();
                        home.ShowDialog();
                    }
                }
                cn.Close();
            }
            else
            {
                MessageBox.Show("Please enter value in all field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
