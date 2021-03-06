﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Microsoft.Office.Tools.Ribbon;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Data;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.IO;
using ExcelDataReader;
using Microsoft.Office.Tools.Excel;
using System.Web;

namespace ExcelForce
{
    
    public partial class ExcelForce
    {

        public String operationSelected = "";
        public String objectSelected = "";
        public String[] columnName;
        public String authToken;
        public String ServiceURL;

        private void MenuItems_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void Button5_Click(object sender, RibbonControlEventArgs e)
        {

        }
        


        private void Button1_Click(object sender, RibbonControlEventArgs e)
        {
            Form2 f = new Form2(this);
            f.Show();
        }

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
            if(dropDown1.SelectedItem != null)
                objectSelected = dropDown1.SelectedItem.Label;
            if(dropDown2.SelectedItem != null)
                operationSelected = dropDown2.SelectedItem.Label;
            if (operationSelected == "Select") {

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
                    else {
                        ExceptionResponse(requestresponse);  
                    }
                }

            } else if (operationSelected == "Insert")
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
                    else {
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
                    else {
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
                    else {
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
            else {
                MessageBox.Show("Something went wrong");
            }

        }
    }  
}
