using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
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
    }
}
