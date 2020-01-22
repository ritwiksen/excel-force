using ExcelForce.Business.Constants;
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
                }else if(sfQuery.Objects.Count() == 1)
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

        private void button3_Click(object sender, EventArgs e)
        {
            var extractionMapFieldsForm = new ExtractionMapFieldsForm();
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
