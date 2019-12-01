using ExcelForce.Business.Interfaces;
using ExcelForce.Business.ServiceFactory;
using ExcelForce.Foundation.ProfileManagement.Models;
using System;
using System.Windows.Forms;

namespace ExcelForce.Forms
{
    public partial class ConnectionInformationForm : Form
    {
        private readonly IExcelForceServiceFactory _excelForceServiceFactory;

        public ConnectionInformationForm(ExcelForce ex)
        {
            _excelForceServiceFactory = new ExcelForceServiceFactory();

            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text.Trim())
                && !string.IsNullOrWhiteSpace(textBox2.Text.Trim())
                && !string.IsNullOrWhiteSpace(textBox3.Text.Trim()))
            {
                var profile = new ConnectionProfile
                {
                    ClientSecret = textBox3.Text.Trim(),
                    Name = textBox1.Text.Trim(),
                    ConsumerKey = textBox2.Text.Trim(),
                    IsProduction = checkBox1.Checked
                };

                var profileService = _excelForceServiceFactory.GetConnectionProfileService();

                var result = profileService.PerformConnectionSubmitActions(profile);

                var loginForm = new LoginForm(textBox2.Text, textBox3.Text, checkBox1.Checked);

                this.Close();

                loginForm.Show();
            }
            else
            {
                label6.Visible = true;
            }
        }
    }
}
