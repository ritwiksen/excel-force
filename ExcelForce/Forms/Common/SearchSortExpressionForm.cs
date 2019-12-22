using ExcelForce.Business.Models.ExtractionMap;
using ExcelForce.Forms.ExtractionMap;
using ExcelForce.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ExcelForce.Forms.Common
{
    public partial class SearchSortExpressionForm : Form
    {
        public SearchSortExpressionForm()
        {
            InitializeComponent();
        }

        public SearchSortExpressionForm(SearchSortExtractionModel model)
        {
            InitializeComponent();

            searchConditionTextBox.Text = model?.SearchExpression ?? string.Empty;

            sortConditionTextBox.Text = model?.SortExpression ?? string.Empty;

            if (!model.ShowAddChildSection)
            {
                ShowChildrenSection(false);

                ShowAddChildSection(false);
            }

            listChildObject.DataSource = model.Children?.Select(x => x.Name)?.ToList();
        }

        private void SearchSortExpressionForm_Load(object sender, EventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (radioButtonYes.Checked)
            {
                PerformActionsOnAdditionalChildren();
            }
            else
            {
                PerformActionsOnFinalSubmit();
            }
        }

        private void PerformActionsOnFinalSubmit()
        {
            var service = Reusables.Instance.ExcelForceServiceFactory?.GetCreateExtractMapService();

            var model = new SearchSortExtractionModel
            {
                SearchExpression = searchConditionTextBox.Text,
                SortExpression = sortConditionTextBox.Text,
                MapName = txtMapName.Text
            };

            var response = service.SubmitParameterSelectionScreen(model);

            if (response.IsValid())
            {
                Close();
            }
            //TODO:(Ritwik):: Show error
        }

        private void PerformActionsOnAdditionalChildren()
        {
            var submitModel = new SearchSortExtractionModel
            {
                SelectedChild = Convert.ToString(listChildObject.SelectedItem),
                SearchExpression = searchConditionTextBox.Text,
                SortExpression = sortConditionTextBox.Text
            };

            var service = Reusables.Instance.ExcelForceServiceFactory?.GetCreateExtractMapService();

            var response = service.SubmitForNewChild(submitModel);

            if (response.IsValid())
            {
                var formModel = response?.Model;

                var fieldSelectionForm = new ExtractionMapFieldsForm(
                    formModel.ObjectName,
                    formModel.AvailableFields, formModel.SfFields);

                Close();

                fieldSelectionForm.Show();
            }
            else
            {
                //TODO:(Ritwik):: Show error
            }
        }

        private void radioButtonYes_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonNo.Checked = false;

            ShowChildrenSection(true);

            ShowMapSection(false);
        }

        private void radioButtonNo_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonYes.Checked = false;

            ShowChildrenSection(false);

            ShowMapSection(true);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            var service = Reusables.Instance.ExcelForceServiceFactory?.GetCreateExtractMapService();

            var response = service.LoadActionsOnFieldList();

            if (response.IsValid())
            {
                var extractionMapFieldsForm = new ExtractionMapFieldsForm(
                          response.Model.ObjectName,
                          response?.Model.AvailableFields,
                          response?.Model.SfFields);

                Close();

                extractionMapFieldsForm.Show();
            }
        }

        private void ShowChildrenSection(bool show)
        {
            lblRelationshipDetails.Visible = show;

            lblChildObject.Visible = show;

            listChildObject.Visible = show;
        }

        private void ShowMapSection(bool show)
        {
            lblMapName.Visible = show;

            txtMapName.Visible = show;
        }

        private void ShowAddChildSection(bool show)
        {
            radioButtonNo.Visible = show;

            radioButtonYes.Visible = show;

            lblChildObject.Visible = show;

            lblAddChild.Visible = show;
        }

    }
}
