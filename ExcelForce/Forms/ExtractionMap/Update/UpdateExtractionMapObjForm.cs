using ExcelForce.Forms.ExtractionMap.Update;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using ExcelForce.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ExcelForce.Forms.ExtractionMapObj.Update
{
    public partial class UpdateExtractionMapObjForm : Form
    {
        private IList<SfField> _availableFields;

        private IList<SfField> _allFields;

        public UpdateExtractionMapObjForm()
        {
        }

        public UpdateExtractionMapObjForm(string selectedObject,
            IList<SfField> availableFields,
            IList<SfField> allFields) : this()
        {
            updateSelectExtMap.Text = selectedObject;

            _availableFields = availableFields;

            _allFields = allFields;
            
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var serviceFactory = Reusables.Instance.ExcelForceServiceFactory;

            var createExtractionService = serviceFactory.GetCreateExtractMapService();

            var result = createExtractionService
                .SubmitOnObjectSelection(updateSelectExtMap.Text);

            if (result.IsValid() && result.Model)
            {
                Close();

                var fieldListResponse = createExtractionService.LoadActionsOnFieldList();

                if (fieldListResponse.IsValid())
                {
                    var extractionMapFieldsForm = new UpdateExtractionMapFieldsForm(
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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
