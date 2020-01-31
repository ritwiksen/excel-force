using ExcelForce.Business.Models.ExtractionMap;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using ExcelForce.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelForce.Forms.ExtractionMap.Update
{
    public partial class UpdateChildSortExpressionForm : Form
    {
        Boolean _isUpdate = false;

        string childObj;

        public UpdateChildSortExpressionForm()
        {
            InitializeComponent();
        }

        public UpdateChildSortExpressionForm(SearchSortExtractionModel model, Boolean isUpdate)
        {
            InitializeComponent();

            searchConditionTextBox.Text = model?.SearchExpression ?? string.Empty;

            sortConditionTextBox.Text = model?.SortExpression ?? string.Empty;
            label2.Text = "Update Extraction Map";
            txtMapName.Text = model?.MapName ?? string.Empty;
            txtMapName.Enabled = false;

            childObj = model.SelectedChild;

            updateChildRelationshipName.DataSource = model.ChildRelationships
               ?.FirstOrDefault(x => string.Equals(x.ObjectName, childObj,StringComparison.InvariantCultureIgnoreCase))
               ?.RelationshipFields;

            _isUpdate = isUpdate;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            var model = new SearchSortExtractionModel
            {
                SearchExpression = searchConditionTextBox.Text,
                SortExpression = sortConditionTextBox.Text,
                MapName = txtMapName.Text,
                SelectedChild = childObj,
                SelectedChildRelationshipName=updateChildRelationshipName.Text

            };
            
                var service = Reusables.Instance.ExcelForceServiceFactory?.GetUpdateExtractionMapService();
                var response = service.SubmitParameterSelectionScreen(model);

                if (response.IsValid())
                {
                    Close();
                    MessageBox.Show("Map Updated!", "",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                }
            
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (_isUpdate)
            {
                var updateExtractionService = Reusables.Instance.ExcelForceServiceFactory.GetUpdateExtractionMapService();

                var fieldListResponse = updateExtractionService.LoadActionsOnFieldList();


                if (fieldListResponse.IsValid())
                {
                    var extractionMapFieldsForm = new ExtractionMapFieldsForm(
                          fieldListResponse.Model.ObjectName,
                          fieldListResponse?.Model.AvailableFields,
                          fieldListResponse?.Model.SfFields, new SearchSortExtractionModel { });
                    Close();
                    extractionMapFieldsForm.Show();
                }
            }
        }
    }
}
