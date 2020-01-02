using System;
using System.Windows.Forms;
using ExcelForce.Models;
using ExcelForce.Business.Interfaces;
using System.Linq;

namespace ExcelForce.Forms
{
    public partial class LoginForm : Form
    {
        private ExcelForce _ribbonBase;

        private readonly IExcelForceServiceFactory _excelForceServiceFactory;

        private const string _loginErrorMessage = "An error occurred while logging in";

        public LoginForm()
        {
            _ribbonBase = Globals.Ribbons.GetRibbon<ExcelForce>();

            _excelForceServiceFactory = Reusables.Instance.ExcelForceServiceFactory;

            InitializeComponent();
        }

        private void LoginForm_OnLoad(object sender, EventArgs e)
        {
            LoadConnectionProfileDropDown();
        }

        private void LoadConnectionProfileDropDown()
        {
            var connectionProfiles =
                _excelForceServiceFactory?.GetConnectionProfileService()?.GetSavedConnectionProfiles();

            var ribbonFactory = Globals.Factory.GetRibbonFactory();

            ddlConnectionProfiles.DataSource = connectionProfiles
                ?.Select(x =>
            {
                var dropDownItem = ribbonFactory.CreateRibbonDropDownItem();

                dropDownItem.Label = x.Name;

                dropDownItem.Tag = x.Name;

                return dropDownItem;
            })
            ?.ToList();

            var index = connectionProfiles
                ?.TakeWhile(x => !(x.Name == Reusables.Instance.ConnectionProfile))?.Count() ?? 0;

            ddlConnectionProfiles.SelectedIndex = Reusables.Instance.ConnectionProfile != null ? index : 0;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                lblErrorMessage.ResetText();

                var connectionProfile = ddlConnectionProfiles.SelectedValue.ToString();

                var profileService = _excelForceServiceFactory.GetUserAuthenticationService();

                var response = profileService.Login(txtUserName.Text, txtPassword.Text, txtSecurityToken.Text, connectionProfile);

                //var response = profileService.Login(
                //    "nissankulatejaswi@deloitte.com.excelforce",
                //    "Excelforce@1234",
                //    "3A5zKaGo11HQajMoLzsyIAIg",
                //    connectionProfile);

                if (response.IsValid())
                {
                    _ribbonBase.btnLogin.Visible = false;
                    _ribbonBase.btnLogout.Visible = true;
                    _ribbonBase.connectionProfileSplitButton.Visible = false;
                    _ribbonBase.btnCreateExtractionMap.Enabled = true;
                    _ribbonBase.button7.Enabled = true;
                    _ribbonBase.button8.Enabled = true;
                    _ribbonBase.btnInsert.Enabled = true;
                    _ribbonBase.btnUpdate.Enabled = true;
                    _ribbonBase.btnDelete.Enabled = true;
                    this.Close();
                }
                else if (response.Messages.Any())
                {
                    lblErrorMessage.Text = string.Join(",", response.Messages);
                }
            }
            catch (Exception ex)
            {

                lblErrorMessage.Text = _loginErrorMessage;
            }
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            txtUserName.Text = string.Empty;

            txtPassword.Text = string.Empty;

            txtSecurityToken.Text = string.Empty;
        }
    }
}
