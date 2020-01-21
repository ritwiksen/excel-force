using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Microsoft.Office.Tools.Ribbon;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Windows.Forms;
using ExcelForce.Forms;
using ExcelForce.Business.Interfaces;
using ExcelForce.Models;
using ExcelForce.Forms.ExtractionMap;
using ExcelForce.Forms.ExtractionMap.ExtractData;
using Microsoft.Office.Tools.Excel;
using System.Threading.Tasks;
using ExcelForce.Business.Models.ExtractionMap;
using ExcelForce.Forms.ExtractionMap.Update;

namespace ExcelForce
{

    public partial class ExcelForce
    {
        public int MyProperty = 123;
        public String operationSelected = "";
        public String objectSelected = "";
        public String[] columnName;
        public String authToken;
        public String ServiceURL;
        private ExcelForce _ribbonBase;

        private readonly IExcelForceServiceFactory _excelForceServiceFactory;

        private void DropDown1_SelectionChanged(object sender, RibbonControlEventArgs e)
        {
        }


        private void dropDown2_SelectionChanged(object sender, RibbonControlEventArgs e)
        {



        }

        public HttpResponseMessage Query(string reqUrl)
        {
            using (var client = new HttpClient())
            {
                //string restRequest = ServiceURL + "/services/data/v43.0/query/?q=" + soqlQuery;
                string restRequest = reqUrl;
                var request = new HttpRequestMessage(HttpMethod.Get, restRequest);
                request.Headers.Add("Authorization", "Bearer " + authToken);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Add("X-PrettyPrint", "1");
                var response = client.SendAsync(request).Result;
                return response;
                // return response.Content.ReadAsStringAsync().Result;
            }
        }

        private void button4_Click(object sender, RibbonControlEventArgs e)
        {
            if (dropDown1.SelectedItem != null)
                objectSelected = dropDown1.SelectedItem.Label;
            if (dropDown2.SelectedItem != null)
                operationSelected = dropDown2.SelectedItem.Label;
            if (operationSelected == "Select")
            {

                List<String> sObjFieldLst = new List<string>();
                using (var client = new HttpClient())
                {
                    string restRequest = ServiceURL + "/services/data/v43.0/sobjects/" + objectSelected + "/describe/"; ;
                    var request = new HttpRequestMessage(HttpMethod.Get, restRequest);
                    request.Headers.Add("Authorization", "Bearer " + authToken);
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //request.Headers.Add("X-PrettyPrint", "1");
                    var response = client.SendAsync(request).Result;
                    var requestresponse = response.Content.ReadAsStringAsync().Result;
                    if (((int)response.StatusCode) >= 200 && ((int)response.StatusCode) < 300)
                    {
                        JObject sObjJObj = JObject.Parse(requestresponse);
                        JToken tokens = sObjJObj["fields"];
                        if (tokens.Children().Count() > 0)
                        {
                            foreach (JToken jt in tokens.Children())
                            {
                                foreach (JProperty jp in jt)
                                {
                                    if (jp.Name == "name")
                                    {
                                        sObjFieldLst.Add(jp.Value.ToString());
                                    }
                                }
                            }
                        }
                        String queryStr = "SELECT " + String.Join(",", sObjFieldLst) + " FROM " + objectSelected /*+ " LIMIT 10000"*/;
                        String restqueryRequest = ServiceURL + "/services/data/v43.0/query/?q=" + queryStr;
                        response = Query(restqueryRequest);
                        Boolean success = ((int)response.StatusCode) >= 200 && ((int)response.StatusCode) < 300;
                        requestresponse = response.Content.ReadAsStringAsync().Result;
                        //var jsonLinq = JObject.Parse(requestresponse);
                        var trgArray = new JArray();
                        String nextQueryStr;
                        if (success)
                        {
                            requestresponse = response.Content.ReadAsStringAsync().Result;
                            var jsonLinq = JObject.Parse(requestresponse);
                            var srcArray = jsonLinq.Descendants().Where(d => d is JArray).First();
                            if (srcArray.Count() > 0)
                            {
                                Globals.ThisAddIn.AddingRowsToArray(trgArray, requestresponse);
                                while (!(Boolean)jsonLinq.SelectToken("done"))
                                {
                                    nextQueryStr = jsonLinq.SelectToken("nextRecordsUrl").ToString();
                                    restqueryRequest = ServiceURL + nextQueryStr;
                                    response = Query(restqueryRequest);
                                    success = ((int)response.StatusCode) >= 200 && ((int)response.StatusCode) < 300;
                                    if (success)
                                    {
                                        requestresponse = response.Content.ReadAsStringAsync().Result;
                                        jsonLinq = JObject.Parse(requestresponse);
                                        Globals.ThisAddIn.AddingRowsToArray(trgArray, requestresponse);
                                    }
                                }
                                Globals.ThisAddIn.BindDatatoExcel(trgArray);
                            }
                            else
                            {
                                MessageBox.Show("No Records Found");
                            }
                        }
                        else
                        {
                            ExceptionResponse(requestresponse);
                        }
                    }
                    else
                    {
                        ExceptionResponse(requestresponse);
                    }
                }

            }
            else if (operationSelected == "Insert")
            {
                var JsonData = Globals.ThisAddIn.ToInsertJSON(Globals.ThisAddIn.GetValuesRange(), objectSelected);
                MessageBox.Show(JsonData);
                using (var client = new HttpClient())
                {
                    Uri restRequest = new Uri(ServiceURL + "/services/data/v45.0/composite/tree/" + objectSelected);
                    HttpContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
                    var request = new HttpRequestMessage(HttpMethod.Post, restRequest);
                    request.Headers.Add("Authorization", "Bearer " + authToken);
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Content = content;
                    //var response = client.PostAsync(restRequest, content).Result;
                    var response = client.SendAsync(request).Result;
                    String message = response.Content.ReadAsStringAsync().Result;
                    Boolean success = ((int)response.StatusCode) >= 200 && ((int)response.StatusCode) < 300;
                    if (!success)
                    {
                        ExceptionResponse(message);
                    }
                    else
                    {
                        MessageBox.Show("Inserted Records Successfully");
                    }

                }
            }
            else if (operationSelected == "Update")
            {
                var JsonData = Globals.ThisAddIn.ToUpdateJSON(Globals.ThisAddIn.GetValuesRange(), objectSelected);
                MessageBox.Show(JsonData);
                using (var client = new HttpClient())
                {
                    Uri restRequest = new Uri(ServiceURL + "/services/data/v45.0/composite/batch/");
                    HttpContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
                    var request = new HttpRequestMessage(HttpMethod.Post, restRequest);
                    request.Headers.Add("Authorization", "Bearer " + authToken);
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Content = content;
                    //var response = client.PostAsync(restRequest, content).Result;
                    var response = client.SendAsync(request).Result;
                    String message = response.Content.ReadAsStringAsync().Result;
                    Boolean success = ((int)response.StatusCode) >= 200 && ((int)response.StatusCode) < 300;
                    if (!success)
                    {
                        ExceptionResponse(message);
                    }
                    else
                    {
                        MessageBox.Show("Updated Records Successfully");
                    }

                }

            }
            else if (operationSelected == "Delete")
            {
                var JsonData = Globals.ThisAddIn.ToDeleteJson(Globals.ThisAddIn.GetValuesRange());
                MessageBox.Show(JsonData);
                using (var client = new HttpClient())
                {
                    Uri restRequest = new Uri(ServiceURL + "/services/data/v45.0/composite/sobjects?ids=" + JsonData);
                    HttpContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
                    var request = new HttpRequestMessage(HttpMethod.Delete, restRequest);
                    request.Headers.Add("Authorization", "Bearer " + authToken);
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.SendAsync(request).Result;
                    String message = response.Content.ReadAsStringAsync().Result;
                    Boolean success = ((int)response.StatusCode) >= 200 && ((int)response.StatusCode) < 300;
                    if (!success)
                    {
                        ExceptionResponse(message);
                    }
                    else
                    {
                        MessageBox.Show("Deleted Records Successfully");
                    }
                }
            }
        }
        public void ExceptionResponse(String message)
        {
            var errorDetails = JArray.Parse(message);
            foreach (JObject parsedObject in errorDetails.Children<JObject>())
            {
                foreach (JProperty parsedProperty in parsedObject.Properties())
                {
                    string propertyName = parsedProperty.Name;
                    if (propertyName.Equals("message"))
                    {
                        MessageBox.Show(parsedProperty.Value + "", "Error",
    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void templateList_SelectionChanged(object sender, RibbonControlEventArgs e)
        {

        }

        private void button2_Click(object sender, RibbonControlEventArgs e)
        {
            HttpClient LogoutCall = new HttpClient();
            String restCallURL = ServiceURL + "/services/oauth2/revoke?token=" + authToken;
            var request = new HttpRequestMessage(HttpMethod.Get, restCallURL);
            request.Headers.Add("Authorization", "Bearer " + authToken);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            request.Headers.Add("X-PrettyPrint", "1");
            var response = LogoutCall.SendAsync(request).Result;
            if (response.IsSuccessStatusCode)
            {

                dropDown1.SelectedItemIndex = -1;
                dropDown1.Items.Clear();
                objectSelected = "";
                operationSelected = "";
                MessageBox.Show("Logged out successfully");
            }
            else
            {
                MessageBox.Show("Something went wrong");
            }

        }

        private void button3_Click(object sender, RibbonControlEventArgs e)
        {

        }

        private void btnLogin_Click(object sender, RibbonControlEventArgs e)
        {
            if (_excelForceServiceFactory.GetRibbonBaseService().LoadConnectionProfilePopup())
            {
                var connectionInfoForm = new ConnectionInformationForm();

                connectionInfoForm.Show();
            }
            else
            {
                var loginForm = new LoginForm();

                loginForm.Show();
            }
        }

        private void btnLogout_Click(object sender, RibbonControlEventArgs e)
        {
            _ribbonBase = Globals.Ribbons.GetRibbon<ExcelForce>();
            var profileService = _excelForceServiceFactory.GetUserAuthenticationService();

            var response = profileService.Logout();
            if (response.Messages == null)
            {
                _ribbonBase.btnLogin.Visible = true;
                _ribbonBase.btnLogout.Visible = false;
                _ribbonBase.connectionProfileSplitButton.Visible = true;
                _ribbonBase.btnCreateExtractionMap.Enabled = false;
                _ribbonBase.button7.Enabled = false;
                _ribbonBase.button8.Enabled = false;
                _ribbonBase.btnInsert.Enabled = false;
                _ribbonBase.btnUpdate.Enabled = false;
                _ribbonBase.btnDelete.Enabled = false;
            }
            /* HttpClient LogoutCall = new HttpClient();
             String restCallURL = ServiceURL + "/services/oauth2/revoke?token=" + authToken;
             var request = new HttpRequestMessage(HttpMethod.Get, restCallURL);
             request.Headers.Add("Authorization", "Bearer " + authToken);
             request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
             request.Headers.Add("X-PrettyPrint", "1");
             var response = LogoutCall.SendAsync(request).Result;
             if (response.IsSuccessStatusCode)
             {

                 dropDown1.SelectedItemIndex = -1;
                 dropDown1.Items.Clear();
                 objectSelected = "";
                 operationSelected = "";
                 MessageBox.Show("Logged out successfully");
             }
             else
             {
                 MessageBox.Show("Something went wrong");
             }

             btnLogin.Visible = true;
             btnLogout.Visible = false;
             btnCreateExtractionMap.Enabled = false;
             button7.Enabled = false;
             button8.Enabled = false;*/
        }

        private void button8_Click(object sender, RibbonControlEventArgs e)
        {
            var extractDataService = Reusables.Instance.ExcelForceServiceFactory.GetExtractDataService();

            var mapSelectorModelResponse = extractDataService.GetExtractMapSelectionFormModel();

            if (mapSelectorModelResponse.IsValid())
            {
                var extractDataForm = new MapSelector(mapSelectorModelResponse?.Model);

                extractDataForm.Show();
            }
            else
            {
                //TODO::Handle error scenario
            }
        }

        private void splitButton1_Click(object sender, RibbonControlEventArgs e)
        {
            var connectionProfileForm = new ConnectionInformationForm();

            connectionProfileForm.Show();
        }

        private void LoadConnectionProfiles()
        {
            var profileService = _excelForceServiceFactory.GetConnectionProfileService();

            var connectionProfiles = profileService
                ?.GetSavedConnectionProfiles()
                ?.Select(x => new
                {
                    x.Name
                });

            if (connectionProfiles == null)
                return;

            foreach (var profile in connectionProfiles)
            {
                var button = Factory.CreateRibbonButton();

                button.Label = profile.Name;

                button.Tag = profile.Name;

                button.Click += btnConnectionProfile_OnClick;

                connectionProfileSplitButton.Items.Add(button);
            }
        }

        private void btnConnectionProfile_OnClick(object sender, RibbonControlEventArgs e)
        {
            var button = sender as RibbonButton;

            Reusables.Instance.ConnectionProfile
                = Convert.ToString(button.Tag);

            var loginForm = new LoginForm();

            loginForm.Show();
        }

        private void btnCreateExtractionMap_Click(object sender, RibbonControlEventArgs e)
        {
            var createExtractMapService = _excelForceServiceFactory.GetCreateExtractMapService();

            var response = createExtractMapService.LoadObjectSelectionScreen();

            if (!(response.Messages?.Any() ?? false))
            {
                var extractionMapForm = new ExtractionMapForm(response?.Model);

                extractionMapForm.Show();
            }
            else
            {
                //TODO:(RItwik):: Handle Error here
            }
        }

        private void btnTest_Click(object sender, RibbonControlEventArgs e)
        {
            var parent = new List<Dictionary<string, string>>();

            for (int i = 0; i < 1000; i++)
            {
                parent.Add(new Dictionary<string, string>
                {
                    { "key1","key"},
                    { "key2","key2"},
                      { "key3","key"},
                    { "key4","key2"},
                      { "key6","key"},
                    { "key5","key2"},
                      { "key7","key"},
                    { "key8","key2"},
                      { "key9","key"},
                    { "key10","key2"},
                      { "key11","key"},
                    { "key12","key2"}
                });
            }

            Task t = new Task(() =>
            {
                var worksheet = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.ActiveSheet);
                PopulateWorksheet(worksheet, parent);

            });

            t.Start();

         //   Worksheet worksheet123 = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.Worksheets.Add());


            Task t1 = new Task(() =>
            {
                Worksheet worksheet = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.Worksheets.Add());

                PopulateWorksheet(worksheet, parent);
            });

            t1.Start();

            //Task t2 = new Task(() =>
            //{
            //    Worksheet worksheet = Globals.Factory.GetVstoObject(Globals.ThisAddIn.Application.Worksheets.Add());

            //    PopulateWorksheet(worksheet, parent);
            //});

            //t2.Start();
        }

        private static bool PopulateWorksheet(Worksheet worksheet, List<Dictionary<string, string>> parent)
        {
            for (int i = 0; i < parent.Count; i++)
            {
                for (var j = 0; j < parent[i].Count; j++)
                {
                    var dictionaryItem = parent[i];
                    worksheet.Cells[i + 1, j + 1] = parent[i].ElementAt(j).Value;
                }
            }

            return true;
        }
        private void btnUpdateExtractionMap_Click(object sender, RibbonControlEventArgs e)
        {
            var updateExtractionMapService = _excelForceServiceFactory.GetUpdateExtractionMapService();

            var response = updateExtractionMapService.LoadMapSelectionScreen();

            if (!(response.Messages?.Any() ?? false))
            {
                var updateExtractionMapForm = new UpdateExtractionMapForm(response?.Model);

                updateExtractionMapForm.Show();
            }
            else
            {
                //TODO:(RItwik):: Handle Error here
            }
        }
    }
}
