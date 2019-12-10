using ExcelForce.Business.Interfaces;
using ExcelForce.Foundation.ProfileManagement.Models;
using ExcelForce.Models;
using System;
using System.Windows.Forms;

namespace ExcelForce.Forms
{
    public partial class ConnectionInformationForm : Form
    {
        private readonly IExcelForceServiceFactory _excelForceServiceFactory;

        public ConnectionInformationForm(ExcelForce ex)
        {
            _excelForceServiceFactory = Reusables.Instance.ExcelForceServiceFactory;

            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtConnectionName.Text.Trim())
                && !string.IsNullOrWhiteSpace(txtConsumerKey.Text.Trim())
                && !string.IsNullOrWhiteSpace(txtSecretKey.Text.Trim()))
            {
                var profile = new ConnectionProfile
                {
                    ClientSecret = txtSecretKey.Text.Trim(),
                    Name = txtConnectionName.Text.Trim(),
                    ConsumerKey = txtConsumerKey.Text.Trim(),
                    IsProduction = chkIsProduction.Checked
                };

                var profileService = _excelForceServiceFactory.GetConnectionProfileService();

                var result = profileService.PerformConnectionSubmitActions(profile);

                var loginForm = new LoginForm(txtConsumerKey.Text, txtSecretKey.Text, chkIsProduction.Checked);

                Reusables.Instance.ConnectionProfile = txtConnectionName.Text.Trim();

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
