using ExcelForce.Forms.Common;
using ExcelForce.Forms.ExtractionMap.Update;
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

        private Dictionary<string, string> _updateMap;

        Boolean isUpdate = false;

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
            label2.Text = "Create Extraction Map";
            AssignDataSourceToDataGrid();
        }

        public ExtractionMapFieldsForm(string selectedObject,
          IList<SfField> availableFields,
          IList<SfField> allFields,Dictionary<string,string> updateMap) : this()
        {
            txtObjectName.Text = selectedObject;

            _availableFields = availableFields;

            _allFields = allFields;
            _updateMap = updateMap;
            label2.Text = "Update Extraction Map";
            AssignDataSourceToDataGrid();
            isUpdate = true;
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (isUpdate)
            {
                Close();
                var searchSortForm = new SearchSortExpressionForm();
                searchSortForm.Show();
            }
            else
            {
                var createExtractionMapService
                = Reusables.Instance.ExcelForceServiceFactory?.GetCreateExtractMapService();

                var submittedFields = GetCheckedFields();

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
            
        }

        private IList<string> GetCheckedFields()
        {
            if (gridFieldList.Rows.Count == 0)
                return null;

            IList<string> result = null;

            foreach (DataGridViewRow row in gridFieldList.Rows)
            {
                bool.TryParse(Convert.ToString(row.Cells[0].Value), out bool isSelected);

                if (isSelected)
                {
                    result = result ?? new List<string>();

                    result.Add(SfField.GetDisplayName(
                        Convert.ToString(row.Cells[1]?.Value),
                        Convert.ToString(row.Cells[2]?.Value)));
                }
            }

            return result;
        }

        private void AssignDataSourceToDataGrid()
        {
            var list = new List<SfFieldDataGrid>();

            if (_availableFields != null)
                list.AddRange(_availableFields.Select(x => new SfFieldDataGrid
                {
                    ApiName = x.ApiName,
                    Name = x.Name,
                    Type = x.Type,
                    Length = x.Length
                }));

            list?.ForEach(x => x.IsSelected = true);

            var additionalFields = _availableFields == null || !_availableFields.Any()
                ? _allFields
                : _allFields?.Where(x => !_availableFields.Any(y => y.DisplayName() == x.DisplayName()));

            if (additionalFields != null)
                list.AddRange(additionalFields.Select(x => new SfFieldDataGrid
                {
                    ApiName = x.ApiName,
                    Name = x.Name,
                    Type = x.Type,
                    Length = x.Length
                }));

            gridFieldList.DataSource = list;

            gridFieldList.Update();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            var mapService = Reusables.Instance.ExcelForceServiceFactory?.GetCreateExtractMapService();

            var areChildObjectsAvailable = mapService.AreChildrenAvailable();

            if (isUpdate)
            {
                Close();
                var updateExtractionMapFieldsForm = new UpdateExtractionMapFieldsForm();
                updateExtractionMapFieldsForm.Show();
            }

            else if (areChildObjectsAvailable?.Model ?? false)
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
