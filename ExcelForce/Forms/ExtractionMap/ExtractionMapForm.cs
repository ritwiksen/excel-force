using ExcelForce.Business.Models.ExtractionMap;
using ExcelForce.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ExcelForce.Forms.ExtractionMap
{
    public partial class ExtractionMapForm : Form
    {
        public ExtractionMapForm()
        {
            InitializeComponent();
        }

        public ExtractionMapForm(ObjectSelectionFormModel model)
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

            txtPrimaryObjName.AutoCompleteCustomSource = stringCollection;

            if (string.IsNullOrWhiteSpace(model?.selectedObjectName))
            {
                txtPrimaryObjName.Name = model.selectedObjectName;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var serviceFactory = Reusables.Instance.ExcelForceServiceFactory;

            var createExtractionService = serviceFactory.GetCreateExtractMapService();

            var result = createExtractionService
                .SubmitOnObjectSelection(txtPrimaryObjName.Text);

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
