using ExcelForce.Business.Models.ExtractionMap;
using ExcelForce.Forms.ExtractionMapObj.Update;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using ExcelForce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExcelForce.Forms.ExtractionMap.Update
{
    public partial class UpdateExtractionMapForm : Form
    {
        public UpdateExtractionMapForm()
        {
            InitializeComponent();

        }

        public UpdateExtractionMapForm(ObjectSelectionFormModel model)
        {
            InitializeComponent();

            InitializeAutoComplete(model);
        }
        private void InitializeAutoComplete(ObjectSelectionFormModel model)
        {
            var stringCollection = new AutoCompleteStringCollection();

            if (model?.ObjectNames?.Any() ?? false)
            {
                stringCollection.AddRange(model?.ObjectNames.ToArray());
            }

            updateSelectExtMap.AutoCompleteCustomSource = stringCollection;

            if (!string.IsNullOrWhiteSpace(model?.selectedObjectName))
            {
                updateSelectExtMap.Text = model.selectedObjectName;
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            var serviceFactory = Reusables.Instance.ExcelForceServiceFactory;

            var updateExtractionService = serviceFactory.GetUpdateExtractionMapService();

            var result = updateExtractionService
                .SubmitOnObjectSelection(updateSelectExtMap.Text);

            if (result.IsValid() && result.Model)
            {
                Close();

                var fieldListResponse = updateExtractionService.LoadActionsOnFieldList();

                if (fieldListResponse.IsValid())
                {

                    var updateExtractionMapFieldsForm = new UpdateExtractionMapObjForm(
                          fieldListResponse.Model.ObjectName,
                          fieldListResponse?.Model.AvailableFields,
                          fieldListResponse?.Model.SfFields);

                    updateExtractionMapFieldsForm.Show();
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
