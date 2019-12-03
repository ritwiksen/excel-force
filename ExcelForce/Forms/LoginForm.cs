using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.Office.Tools.Ribbon;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using ExcelForce.Models;

namespace ExcelForce.Forms
{
    public partial class LoginForm : Form
    {
        private ExcelForce _ribbonBase;
        String authToken = "";
        String ServiceURL = "https://login.salesforce.com";
        // String ServiceURL = "https://test.salesforce.com";
        String sfdcConsumerKey = "";// = "3MVG9G9pzCUSkzZt2nVdX1o9BZwzYyltwWBP5irkgbk71BDmc41ujJIiVm5C_IlVfZ7jP92JYIS9zfXKmiVVq";
        // String sfdcConsumerKey = "3MVG9LzKxa43zqdKPikPxc3sMUZ7jacalJD.HFa3LG_TyBmyZuISofWytdsXwn3ZomqLbdsIdyMUqBBMKgZyN";
        //String sfdcCounsumerSecret = "AA2E6E9F9DC6843FCD673542E0A4AF727D7FF59333E002017472E6D2E9D3D15E";
        String sfdcCounsumerSecret = "";// = "786F7BEB38D66411EFD6E7E5D8CAE56F4F9237EAB405E1EF5C40ED792096DEB2";
        String callbackUrl = "";

        public LoginForm(String conKey, String secKey, Boolean prod) : this()
        {
            sfdcConsumerKey = conKey;
            sfdcCounsumerSecret = secKey;
            callbackUrl = (prod == true) ? "https://login.salesforce.com/services/oauth2/token" : "https://test.salesforce.com/services/oauth2/token";
        }

        public LoginForm()
        {
            _ribbonBase= Globals.Ribbons.GetRibbon<ExcelForce>();

            InitializeComponent();
        }

        public String[] columnName;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var connectionProfile = Reusables.Instance.ConnectionProfile;
            //String sfdcUserName = "nissankulatejaswi@deloitte.com.excelforce";
            //String sfdcPassword = "Excelforce@1234";
            String sfdcUserName = txtUserName.Text;
            String sfdcPassword = txtPassword.Text;
            String sfdcSecurityToken = txtSecurityToken.Text;
            // String sfdcSecurityToken = "DgvAbALBLgDSNJOPWzHP3318";
            // String sfdcSecurityToken = "9mItEpwZzSVXl1gP9tdQPWBJU";
            // String callbackUrl = "https://login.salesforce.com/services/oauth2/token";
            //String callbackUrl = "https://test.salesforce.com/services/oauth2/token";

            String SfdcloginPassword = sfdcPassword + sfdcSecurityToken;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            var dictonaryForUrl = new Dictionary<String, String>
            {
                {"grant_type","password"},
                {"client_id",sfdcConsumerKey},
                {"client_secret",sfdcCounsumerSecret},
                {"username",sfdcUserName},
                {"password",SfdcloginPassword}
            };
            HttpClient authhc = new HttpClient();
            HttpContent httpContent = new FormUrlEncodedContent(dictonaryForUrl);
            HttpResponseMessage httpResponse = authhc.PostAsync(callbackUrl, httpContent).Result;
            String message = httpResponse.Content.ReadAsStringAsync().Result;
            JObject jsonObj = JObject.Parse(message);
            authToken = (string)jsonObj["access_token"];
            ServiceURL = (string)jsonObj["instance_url"];
            String ErrorType = "";
            String ErrorMsg = "";
            ErrorType = (string)jsonObj["error"];
            ErrorMsg = (string)jsonObj["error_description"];
            if (authToken != null)
            {
                HttpClient apiCallClient = new HttpClient();
                String restCallURL = ServiceURL + "/services/data/v43.0/sobjects";
                HttpRequestMessage apirequest = new HttpRequestMessage(HttpMethod.Get, restCallURL);
                apirequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                apirequest.Headers.Add("authorization", "Bearer " + authToken);
                HttpResponseMessage apiCallResponse = apiCallClient.SendAsync(apirequest).Result;

                String requestresponse = apiCallResponse.Content.ReadAsStringAsync().Result;

                List<String> sObjLst = new List<string>();
                if (apiCallResponse.IsSuccessStatusCode)
                {
                    this.Close();
                    MessageBox.Show("Connection Established.");
                    JObject sObjJObj = JObject.Parse(requestresponse);
                    JToken tokens = sObjJObj["sobjects"];
                    if (tokens.Children().Count() > 0)
                    {
                        foreach (JToken jt in tokens.Children())
                        {
                            foreach (JProperty jp in jt)
                            {
                                if (jp.Name == "name")
                                {
                                    RibbonDropDownItem item = Globals.Factory.GetRibbonFactory().CreateRibbonDropDownItem();
                                    item.Label = jp.Value.ToString();
                                    _ribbonBase.dropDown1.Items.Add(item);
                                    //sObjLst.Add(jp.Value.ToString());
                                }
                            }
                        }
                    }
                    _ribbonBase.authToken = this.authToken;
                    _ribbonBase.ServiceURL = this.ServiceURL;

                    _ribbonBase.button10.Visible = true;
                    _ribbonBase.btnLogin.Visible = false;
                    _ribbonBase.button6.Enabled = true;
                    _ribbonBase.button7.Enabled = true;
                    _ribbonBase.button8.Enabled = true;

                }
                else
                {
                    MessageBox.Show("Connection NOT Established.");
                    _ribbonBase.ExceptionResponse(requestresponse);
                }

            }
            else
            {
                txtPassword.Text = "";
                txtSecurityToken.Text = "";
                label4.Visible = true;
                // MessageBox.Show("Please check your username and password.");
            }
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
