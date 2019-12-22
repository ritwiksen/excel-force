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

            ddlConnectionProfiles.SelectedIndex = index;
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

                if (response.Model)
                {
                    _ribbonBase.btnLogin.Visible = false;
                    _ribbonBase.btnLogout.Visible = true;
                    _ribbonBase.connectionProfileSplitButton.Visible = false;
                    _ribbonBase.btnCreateExtractionMap.Enabled = true;
                    _ribbonBase.button7.Enabled = true;
                    _ribbonBase.button8.Enabled = true;
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

            //Right set of values
            //String sfdcUserName = "nissankulatejaswi@deloitte.com.excelforce"; //--c
            //String sfdcPassword = "Excelforce@1234"; //--c
            //                                         // String sfdcUserName = txtUserName.Text;
            //                                         // String sfdcPassword = txtPassword.Text;
            //                                         //String sfdcSecurityToken = txtSecurityToken.Text;
            //String sfdcSecurityToken = "3A5zKaGo11HQajMoLzsyIAIg";


            // String sfdcSecurityToken = "9mItEpwZzSVXl1gP9tdQPWBJU";
            //    String callbackUrl = "https://login.salesforce.com/services/oauth2/token"; //--c
            //String callbackUrl = "https://test.salesforce.com/services/oauth2/token"; --c
            // isProduction = true


            //  var loginResponse = profileService.Login(sfdcUserName, sfdcPassword, connectionProfile);

            //  String SfdcloginPassword = $"{sfdcPassword}{sfdcSecurityToken}";

            //   String SfdcloginPassword = $"{sfdcPassword}{sfdcSecurityToken}";

            //  ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            //    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            //  var dictonaryForUrl = new Dictionary<String, String>
            //{
            //    {"grant_type","password"},
            //    {"client_id",sfdcConsumerKey},
            //    {"client_secret",sfdcCounsumerSecret},
            //    {"username",sfdcUserName},
            //    {"password",SfdcloginPassword}
            //};
            //HttpClient authhc = new HttpClient();
            //HttpContent httpContent = new FormUrlEncodedContent(dictonaryForUrl);
            //HttpResponseMessage httpResponse = authhc.PostAsync(callbackUrl, httpContent).Result;
            //String message = httpResponse.Content.ReadAsStringAsync().Result;
            ////JObject jsonObj = JObject.Parse(message);
            ////authToken = (string)jsonObj["access_token"];
            ////ServiceURL = (string)jsonObj["instance_url"];
            ////String ErrorType = "";
            ////String ErrorMsg = "";
            ////ErrorType = (string)jsonObj["error"];
            ////ErrorMsg = (string)jsonObj["error_description"];
            ////if (authToken != null)
            ////{
            //HttpClient apiCallClient = new HttpClient();
            //String restCallURL = "https://login.salesforce.com" + "/services/data/v43.0/sobjects";
            //HttpRequestMessage apirequest = new HttpRequestMessage(HttpMethod.Get, restCallURL);
            //apirequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //apirequest.Headers.Add("authorization", "Bearer " + "00D2v000000NqEo!AQQAQJsG9hwurn5Wp_xj1F.0x9_rmKdmx48nbPsMI93VgsgaHFbiqaJMJZEVnVK7ng_W56s_GHt6gbW1veUk_yzSLe.My6G2");
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            //HttpResponseMessage apiCallResponse = apiCallClient.SendAsync(apirequest).Result;

            //String requestresponse = apiCallResponse.Content.ReadAsStringAsync().Result;

            ////    List<String> sObjLst = new List<string>();
            ////    if (apiCallResponse.IsSuccessStatusCode)
            ////    {
            ////        this.Close();
            ////        MessageBox.Show("Connection Established.");
            ////        JObject sObjJObj = JObject.Parse(requestresponse);
            ////        JToken tokens = sObjJObj["sobjects"];
            ////        if (tokens.Children().Count() > 0)
            ////        {
            ////            foreach (JToken jt in tokens.Children())
            ////            {
            ////                foreach (JProperty jp in jt)
            ////                {
            ////                    if (jp.Name == "name")
            ////                    {
            ////                        RibbonDropDownItem item = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
            ////                        item.Label = jp.Value.ToString();
            ////                        _ribbonBase.dropDown1.Items.Add(item);
            ////                        //sObjLst.Add(jp.Value.ToString());
            ////                    }
            ////                }
            ////            }
            ////        }
            ////        _ribbonBase.authToken = this.authToken;
            ////        _ribbonBase.ServiceURL = this.ServiceURL;

            ////        _ribbonBase.button10.Visible = true;
            ////        _ribbonBase.btnLogin.Visible = false;
            ////        _ribbonBase.button6.Enabled = true;
            ////        _ribbonBase.button7.Enabled = true;
            ////        _ribbonBase.button8.Enabled = true;

            ////    }
            ////    else
            ////    {
            ////        MessageBox.Show("Connection NOT Established.");
            ////        _ribbonBase.ExceptionResponse(requestresponse);
            ////    }

            //// }
            ////   else
            ////   {
            ////     txtPassword.Text = "";
            ////     txtSecurityToken.Text = "";
            ////   label4.Visible = true;
            //// MessageBox.Show("Please check your username and password.");
            ////}
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            txtUserName.Text = string.Empty;

            txtPassword.Text = string.Empty;

            txtSecurityToken.Text = string.Empty;
        }
    }
}
