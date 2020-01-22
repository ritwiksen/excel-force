using ExcelForce.Business.Constants;
using ExcelForce.Business.Models.ExtractionMap;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using ExcelForce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExcelForce.Forms.ExtractionMap.Update
{
    public partial class UpdateExtractionMapFieldsForm : Form
    {
        private SfQuery _sfQuery;

       

        public UpdateExtractionMapFieldsForm()
        {
            InitializeComponent();
        }

        public UpdateExtractionMapFieldsForm(SfQuery sfQuery) : this()
        {
            _sfQuery = sfQuery;
            updateSelectMap2.Text = sfQuery.Name;
            parentObjectName.Text = sfQuery.ParentObject.ApiName;
            childObject1.Text = sfQuery.Objects!=null && sfQuery.Objects.Count() > 0  ? sfQuery.Objects.First().DisplayName():null;
            childObject2.Text = sfQuery.Objects != null  && sfQuery.Objects.Count()>1 ? sfQuery.Objects?.Last()?.DisplayName():null;
            if (sfQuery.Objects != null) {
                if (sfQuery.Objects.Count() == 0)
                {
                    childObject1.Hide();
                    childObject2.Hide();
                    childDelete.Hide();
                    childEdit.Hide();
                    childObjectLabel.Hide();

                }
                else if(sfQuery.Objects.Count() == 1)
                {
                    childObject2.Hide();
                }
                
            }
        }


        private void btnNext_Click(object sender, EventArgs e)
        {

        }

      
      

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void childObjectUpdate_Click(object sender, EventArgs e)
        {
            //var extractionMapFieldsForm = new ExtractionMapFieldsForm();
            //extractionMapFieldsForm.Show();
            var childObj = "";
            if (childObject1.Checked && childObject2.Checked)
            {
                childEdit.Enabled = false;
            }
            if (childObject1.Checked)
            {
                childObj = Convert.ToString(childObject1.Text); 
            }
            if (childObject2.Checked)
            {
                childObj = Convert.ToString(childObject2.Text);
            }
            
            string[] childObjApi = childObj.Split('|');

            var submitModel = new SearchSortExtractionModel
            {
                SelectedChild = Convert.ToString(childObjApi[1]),
            };

            var service = Reusables.Instance.ExcelForceServiceFactory?.GetUpdateExtractionMapService();

            var response = service.SubmitForNewChild(submitModel);
            
            var formModel = response?.Model;

            var extractionMapFieldsForm = new ExtractionMapFieldsForm(
                formModel.ObjectName,
                formModel.AvailableFields, formModel.SfFields);

            Close();

            extractionMapFieldsForm.Show();
        }

        private void parentObjectUpdate_Click(object sender, EventArgs e)
        {
            var serviceFactory = Reusables.Instance.ExcelForceServiceFactory;

            var updateExtractionService = serviceFactory.GetUpdateExtractionMapService();

          
            var result = updateExtractionService
                .SubmitOnObjectSelection(parentObjectName.Text);

            if (result.IsValid() && result.Model)
            {
                Close();

                var fieldListResponse = updateExtractionService.LoadActionsOnFieldList();

                if (fieldListResponse.IsValid())
                {
                    var extractionMapFieldsForm = new ExtractionMapFieldsForm(
                          fieldListResponse.Model.ObjectName,
                          fieldListResponse?.Model.AvailableFields,
                          fieldListResponse?.Model.SfFields, null);

                    extractionMapFieldsForm.Show();
                }
                else
                {
                    //TODO:(Show error message);
                }
            }
            else
            {
                //TODO:(Show error message);
            }

        }
    }
}
