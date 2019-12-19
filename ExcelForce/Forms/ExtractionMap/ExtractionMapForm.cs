using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using ExcelForce.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ExcelForce.Forms.ExtractionMap
{
    public partial class ExtractionMapForm : Form
    {
        public ExtractionMapForm()
        {
            InitializeComponent();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var serviceFactory = Reusables.Instance.ExcelForceServiceFactory;

            var createExtractionService = serviceFactory.GetCreateExtractMapService();

            var result = createExtractionService
                .SubmitOnObjectSelection(txtPrimaryObjName.Text, false);

            if (result.IsValid() && result.Model)
            {
                Close();

                var fieldListResponse = createExtractionService.LoadActionsOnFieldList();

                if (fieldListResponse.IsValid())
                {
                    var extractionMapFieldsForm = new ExtractionMapFieldsForm(
                          fieldListResponse.Model.ObjectName,
                          null,
                          fieldListResponse?.Model.SfFields);

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
