using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExcelForce.Forms.ExtractionMap.Update
{
    public partial class UpdateExtractionMapFieldsForm : Form
    {
        private IList<SfField> _availableFields;

        private IList<SfField> _allFields;

        public UpdateExtractionMapFieldsForm()
        {
            InitializeComponent();
        }

        public UpdateExtractionMapFieldsForm(string selectedObject,
            IList<SfField> availableFields,
            IList<SfField> allFields) : this()
        {
            updateSelectExtMap.Text = selectedObject;

            _availableFields = availableFields;

            _allFields = allFields;

            AssignDataSourceToCheckBoxList();
        }

        private void AssignDataSourceToCheckBoxList()
        {
            BindFieldsToCheckList(_availableFields, true);

            var additionalFields = _allFields
                ?.Where(x => !_availableFields.Any(y => y.DisplayName() == x.DisplayName()));

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

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
