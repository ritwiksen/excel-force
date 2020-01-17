using ExcelForce.Business.Models.ExtractionMap.ExtractData;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ExcelForce.Forms.ExtractionMap.ExtractData
{
    public partial class MapSelector : Form
    {
        public MapSelector()
        {
            InitializeComponent();
        }

        public MapSelector(ExtractMapSelectionFormModel model)
        {
            InitializeComponent();

            InitializeAutoComplete(model);
        }

        private void InitializeAutoComplete(ExtractMapSelectionFormModel model)
        {
            var stringCollection = new AutoCompleteStringCollection();

            if (model?.ExtractMapNames?.Any() ?? false)
            {
                stringCollection.AddRange(model?.ExtractMapNames.ToArray());
            }

            txtExtractMap.AutoCompleteCustomSource = stringCollection;

            if (!string.IsNullOrWhiteSpace(model?.SelectedExtractMap))
            {
                txtExtractMap.Text = model.SelectedExtractMap;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

        }
    }
}
