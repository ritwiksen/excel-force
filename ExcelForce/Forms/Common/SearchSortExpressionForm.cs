using ExcelForce.Business.Models.ExtractionMap;
using ExcelForce.Forms.ExtractionMap;
using ExcelForce.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExcelForce.Forms.Common
{
    public partial class SearchSortExpressionForm : Form
    {
        Boolean _isUpdate = false;
        public SearchSortExpressionForm()
        {
            InitializeComponent();
        }

        public SearchSortExpressionForm(SearchSortExtractionModel model)
        {
            InitializeComponent();

            searchConditionTextBox.Text = model?.SearchExpression ?? string.Empty;

            sortConditionTextBox.Text = model?.SortExpression ?? string.Empty;

            btnNext.Text = model.ShowAddChildSection ? "Next" : "Create";

            ShowMapSection(model.ShowMapNameSection);

            ShowAddChildSection(model.ShowAddChildSection);

            ShowChildrenSection(false);

            listChildObject.DataSource = model.Children?.Select(x => x.Name)?.ToList();
        }

        public SearchSortExpressionForm(SearchSortExtractionModel model,Boolean isUpdate)
        {
            InitializeComponent();

            searchConditionTextBox.Text = model?.SearchExpression ?? string.Empty;

            sortConditionTextBox.Text = model?.SortExpression ?? string.Empty;

            btnNext.Text = model.ShowAddChildSection ? "Next" : "Create";

            ShowMapSection(model.ShowMapNameSection);

            ShowAddChildSection(model.ShowAddChildSection);

            ShowChildrenSection(false);

            listChildObject.DataSource = model.Children?.Select(x => x.Name)?.ToList();
            _isUpdate = isUpdate;
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
                //TODO:(Ritwik):: Show error
            }

            var formModel = response?.Model;

            var fieldSelectionForm = new ExtractionMapFieldsForm(
                formModel.ObjectName,
                formModel.AvailableFields, formModel.SfFields);

            Close();

            fieldSelectionForm.Show();

        }

        private void radioButtonYes_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonNo.Checked = false;

            ShowChildrenSection(true);

            ShowMapSection(false);

            btnNext.Text = "Next";

            Height = 835;

            btnPrevious.Location = new Point(25, 710);

            btnNext.Location = new Point(297, 710);
        }

        private void radioButtonNo_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonYes.Checked = false;

            ShowChildrenSection(false);

            ShowMapSection(true);

            btnNext.Text = "Save";

            Height = 750;

            btnPrevious.Location = new Point(22, 608);

            btnNext.Location = new Point(297, 608);

        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            var service = Reusables.Instance.ExcelForceServiceFactory?.GetCreateExtractMapService();

            var response = service.LoadActionsOnFieldList();

            if (!response.IsValid())
            {
                //TODO:: do actions for error
            }

            var extractionMapFieldsForm = new ExtractionMapFieldsForm(
                         response.Model.ObjectName,
                         response?.Model.AvailableFields,
                         response?.Model.SfFields);

            Close();

            extractionMapFieldsForm.Show();
        }

        private void ShowChildrenSection(bool show)
        {
            lblRelationshipDetails.Visible = show;

            lblChildObject.Visible = show;

            listChildObject.Visible = show;
        }

        private void ShowMapSection(bool show)
        {
            lblMapName.Location = new Point(20, 511);

            txtMapName.Location = new Point(20, 561);

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
