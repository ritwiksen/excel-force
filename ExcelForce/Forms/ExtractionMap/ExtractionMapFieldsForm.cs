using ExcelForce.Forms.Common;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using ExcelForce.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
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

            AssignDataSourceToDataGrid();
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

        private void AssignDataSourceToDataGrid()
        {
            var list = new List<SfFieldDataGrid>();

            list.AddRange(_availableFields.Cast<SfFieldDataGrid>());

            list?.ForEach(x => x.IsSelected = true);

            var additionalFields = _availableFields == null || !_availableFields.Any()
                ? _allFields
                : _allFields?.Where(x => !_availableFields.Any(y => y.DisplayName() == x.DisplayName()));

            list.AddRange(additionalFields.Cast<SfFieldDataGrid>());

            gridFieldList.DataSource = list;

            gridFieldList.Update();
        }


        private DataGridViewColumn GetColumn(string name, int index, string headerText)
        {
            return new DataGridViewColumn
            {
                Name = name,
                DisplayIndex = index
            };
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

            var areChildObjectsAvailable = mapService.AreChildrenAvailable();

            if (areChildObjectsAvailable?.Model ?? false)
            {
                var previousActionResponse = mapService.SubmitPreviousFieldSelection();

                if (previousActionResponse.IsValid())
                {
                    var fieldSelectionModelResponse = mapService.LoadSearchSortScreen();

                    if (fieldSelectionModelResponse.IsValid())
                    {
                        var searchSoryModel = fieldSelectionModelResponse?.Model;

                        var fieldSelectionScreen = new SearchSortExpressionForm(searchSoryModel);

                        Close();

                        fieldSelectionScreen.Show();
                    }
                    else
                    {
                        //TODO:: error scenario
                    }
                }
                else
                {
                    //TODO:: error scenario
                }

            }
            else
            {
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

        private void gridFieldList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var grid = sender as DataGridView;

            grid.RowsDefaultCellStyle.SelectionBackColor = Color.Transparent;

            grid.RowHeadersVisible = false;

            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            for (int i = 0; i < grid.Columns.Count; i++)
            {
                var column = grid.Columns[i];

                column.ReadOnly = i != 0;

                if (i == 0)
                {
                    column.Width = 50;
                }
            }
        }
    }
}
