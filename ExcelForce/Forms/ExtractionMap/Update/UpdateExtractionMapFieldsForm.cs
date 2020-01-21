using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using ExcelForce.Foundation.EntityManagement.Models.UpdateMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExcelForce.Forms.ExtractionMap.Update
{
    public partial class UpdateExtractionMapFieldsForm : Form
    {
        private UpdateMap _updateMap;

       

        public UpdateExtractionMapFieldsForm()
        {
            InitializeComponent();
        }

        public UpdateExtractionMapFieldsForm(UpdateMap updateMap) : this()
        {
            _updateMap = updateMap;
            updateSelectMap2.Text = updateMap.Name;
            parentObjectName.Text = updateMap.ParentObject.ApiName;
            childObject1.Text = updateMap.ChildObjects.First().DisplayName();
            childObject2.Text = updateMap.ChildObjects.Last()?.DisplayName();
        }


        private void btnNext_Click(object sender, EventArgs e)
        {

        }

      
      

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
