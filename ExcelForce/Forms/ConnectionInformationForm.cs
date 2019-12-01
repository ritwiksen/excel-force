using ExcelForce.Foundation.CoreServices.Repository;
using ExcelForce.UserProfile;
using ExcelForce.UserProfile.Models;
using System;
using System.Windows.Forms;

namespace ExcelForce.Forms
{
    public partial class ConnectionInformationForm : Form
    {
        ExcelForce ex1;

        private readonly IExcelForceRepository<ConnectionProfile, string> _connectionProfileRepository;

        public ConnectionInformationForm(ExcelForce ex)
        {
            ex1 = ex;

            _connectionProfileRepository = new ConnectionProfileRepository();

            InitializeComponent();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var test = Globals.Ribbons.GetRibbon<ExcelForce>();
            var testValue = test.MyProperty;
            if (!string.IsNullOrWhiteSpace(textBox1.Text.Trim())
                && !string.IsNullOrWhiteSpace(textBox2.Text.Trim())
                && !string.IsNullOrWhiteSpace(textBox3.Text.Trim()))
            {
                var connectionObject = new ConnectionProfile
                {
                    ClientSecret = textBox3.Text.Trim(),
                    Name = textBox1.Text.Trim(),
                    ConsumerKey = textBox2.Text.Trim(),
                    IsProduction = checkBox1.Checked
                };

                _connectionProfileRepository.AddRecord(connectionObject);

                var f2 = new LoginForm(ex1, textBox2.Text, textBox3.Text, checkBox1.Checked);
                this.Close();
                f2.Show();
            }
            else
            {
                label6.Visible = true;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
