using ExcelForce.Forms.Common;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using ExcelForce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExcelForce.Forms.ExtractionMap
{
    public partial class ExtractionMapFieldsForm : Form
    {
        private IList<SfField> _availableFields;

        private IList<SfField> _allFields;

        public ExtractionMapFieldsForm()
        {
            InitializeComponent();
        }

        public ExtractionMapFieldsForm(string selectedObject,
            IList<SfField> availableFields,
            IList<SfField> allFields) : this()
        {
            txtObjectName.Text = selectedObject;

            _availableFields = availableFields;

            _allFields = allFields;

            AssignDataSourceToCheckBoxList();
        }


        private void btnNext_Click(object sender, EventArgs e)
        {
            var createExtractionMapService
                = Reusables.Instance.ExcelForceServiceFactory?.GetCreateExtractMapService();

            var submittedFields = checkedFieldList.CheckedItems
                ?.Cast<string>()
                ?.ToList();

            var listOfFieldNames = _allFields?.Where(
                x => submittedFields.Any(
                    y => string.Equals(y, x.DisplayName(), StringComparison.InvariantCultureIgnoreCase)))
                    ?.ToList();

            var response = createExtractionMapService.SubmitFieldSelection(
                txtObjectName.Text, listOfFieldNames);

            if (response.IsValid())
            {
                Close();

                var searchSortFormResponse = createExtractionMapService.LoadSearchSortScreen();

                if (searchSortFormResponse.IsValid())
                {
                    var searchSortForm = new SearchSortExpressionForm(searchSortFormResponse?.Model);

                    searchSortForm.Show(); 
                }
                else
                {
                    //TODO:(Ritwik):: Handle error scenario
                }
            }
            else
            {
                //TODO:(Ritwik):: Handle error scenario
            }
        }

        private void AssignDataSourceToCheckBoxList()
        {
            BindFieldsToCheckList(_availableFields, true);

            var additionalFields = _availableFields == null || !_availableFields.Any()
                ? _allFields
                : _allFields?.Where(x => !_availableFields.Any(y => y.DisplayName() == x.DisplayName()));

            BindFieldsToCheckList(additionalFields, false);
        }

        private void BindFieldsToCheckList(IEnumerable<SfField> fields, bool isChecked)
        {
            if (fields != null)
            {
                foreach (var item in fields)
                {
                    checkedFieldList.Items.Add(item.DisplayName(), isChecked);
                }
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            var mapService = Reusables.Instance.ExcelForceServiceFactory?.GetCreateExtractMapService();

            var response = mapService.LoadObjectSelectionScreen();

            if (response.IsValid())
            {
                var objectSelectionScreen = new ExtractionMapForm(response?.Model);

                Close();

                objectSelectionScreen.Show();
            }
            else
            {
                //TODO:(RItwik) :: Handle error messages
            }
        }
    }
}
