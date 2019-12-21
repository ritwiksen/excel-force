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

            if (model.Children.Count == 2)
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
        }

    }
}
