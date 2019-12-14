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
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var connectionProfile = Reusables.Instance.ConnectionProfile;

            var profileService = _excelForceServiceFactory.GetUserAuthenticationService();

            var test = profileService.Login(txtUserName.Text, txtPassword.Text, txtSecurityToken.Text, connectionProfile);

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
            ////    HttpClient apiCallClient = new HttpClient();
            ////    String restCallURL = ServiceURL + "/services/data/v43.0/sobjects";
            ////    HttpRequestMessage apirequest = new HttpRequestMessage(HttpMethod.Get, restCallURL);
            ////    apirequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ////    apirequest.Headers.Add("authorization", "Bearer " + authToken);
            ////    HttpResponseMessage apiCallResponse = apiCallClient.SendAsync(apirequest).Result;

            ////    String requestresponse = apiCallResponse.Content.ReadAsStringAsync().Result;

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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
