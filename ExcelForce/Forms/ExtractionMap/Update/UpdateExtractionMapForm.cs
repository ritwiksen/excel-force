using ExcelForce.Business.Models.ExtractionMap;
//using ExcelForce.Forms.ExtractionMapFields.Update;
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
                .SubmitOnMapSelection(updateSelectExtMap.Text);

            if (result.IsValid())
            {
                Close();

                var updateExtractionMapFieldsForm = new UpdateExtractionMapFieldsForm(result.Model);

                updateExtractionMapFieldsForm.Show();
                
            }
            else
            {
                //TODO:(Show error message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
