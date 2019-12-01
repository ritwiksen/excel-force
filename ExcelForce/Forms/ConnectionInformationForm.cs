using ExcelForce.Foundation.CoreServices.Repository;
using ExcelForce.Foundation.ProfileManagement;
using ExcelForce.Foundation.ProfileManagement.Models;
using System;
using System.Windows.Forms;

namespace ExcelForce.Forms
{
    public partial class ConnectionInformationForm : Form
    {
        private readonly IExcelForceRepository<ConnectionProfile, string> _connectionProfileRepository;

        public ConnectionInformationForm(ExcelForce ex)
        {
            _connectionProfileRepository = new ConnectionProfileRepository();

            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
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

                var f2 = new LoginForm(textBox2.Text, textBox3.Text, checkBox1.Checked);
                this.Close();
                f2.Show();
            }
            else
            {
                label6.Visible = true;
            }
        }
    }
}
