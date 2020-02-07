﻿using ExcelForce.Business.Constants;
using ExcelForce.Business.Models.ExtractionMap;
using ExcelForce.Forms.ExtractionMap;
using ExcelForce.Forms.ExtractionMap.Update;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using ExcelForce.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExcelForce.Forms.Common
{
    public partial class SearchSortExpressionForm : Form
    {
        private readonly IList<SfChildRelationship> sfChildRelationships;

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

            sfChildRelationships = model.ChildRelationships;

            listChildObject.DataSource = model.Children?.Select(x => x.Name)?.ToList();

            listRelationshipName.DataSource = model.ChildRelationships
                ?.FirstOrDefault(x => x.ObjectName == Convert.ToString(listChildObject.SelectedValue))
                ?.RelationshipFields;
        }

        public SearchSortExpressionForm(SearchSortExtractionModel model, Boolean isUpdate)
        {
            InitializeComponent();

            searchConditionTextBox.Text = model?.SearchExpression ?? string.Empty;

            sortConditionTextBox.Text = model?.SortExpression ?? string.Empty;
            label2.Text = "Update Extraction Map";
            btnNext.Text = model.ShowAddChildSection ? "Next" : "Update";
            txtMapName.Text = model?.MapName ?? string.Empty;
            txtMapName.Enabled = false;
            ShowMapSection(model.ShowMapNameSection);

            ShowAddChildSection(model.ShowAddChildSection);

            ShowChildrenSection(false);

            sfChildRelationships = model.ChildRelationships;

            listChildObject.DataSource = model.Children?.Select(x => x.Name)?.ToList();

            listRelationshipName.DataSource = model.ChildRelationships
                ?.FirstOrDefault(x => x.ObjectName == Convert.ToString(listChildObject.SelectedValue))
                ?.RelationshipFields;
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
            

            var model = new SearchSortExtractionModel
            {
                SearchExpression = searchConditionTextBox.Text,
                SortExpression = sortConditionTextBox.Text,
                MapName = txtMapName.Text                
            };

            if (_isUpdate)
            {
                var service = Reusables.Instance.ExcelForceServiceFactory?.GetUpdateExtractionMapService();
                var response = service.SubmitParameterSelectionScreen(model);

                if (response.IsValid())
                {
                    Close();
                    var confirmResult = MessageBox.Show(BusinessConstants.ParentObjUpdateConfirm,
                                     string.Empty,
                                     MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        var returnResponse = service.SubmitOnMapSelection(null);
                        var updateExtractionMapFieldsForm = new UpdateExtractionMapFieldsForm(returnResponse?.Model);
                        updateExtractionMapFieldsForm.Show();
                    }
                    else
                    {
                        service.clear();

                    }
                    
                }
            }
            else
            {
                var service = Reusables.Instance.ExcelForceServiceFactory?.GetCreateExtractMapService();
                var response = service.SubmitParameterSelectionScreen(model);

                if (response.IsValid())
                {
                    Close();
                    MessageBox.Show("Map Created Successfully!", "",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                }
            }
            //TODO:(Ritwik):: Show error
        }

        private void PerformActionsOnAdditionalChildren()
        {
            var submitModel = new SearchSortExtractionModel
            {
                SelectedChild = Convert.ToString(listChildObject.SelectedItem),
                SearchExpression = searchConditionTextBox.Text,
                SortExpression = sortConditionTextBox.Text,
                SelectedChildRelationshipName = Convert.ToString(listRelationshipName.SelectedItem)
            };

            if (_isUpdate)
            {
                var service = Reusables.Instance.ExcelForceServiceFactory?.GetUpdateExtractionMapService();

                var response = service.SubmitForNewChild(submitModel);

                if (response.IsValid())
                {
                    //TODO:(Ritwik):: Show error
                }

                var formModel = response?.Model;

                var fieldSelectionForm = new ExtractionMapFieldsForm(
                    formModel.ObjectName,
                    formModel.AvailableFields, formModel.SfFields, new SearchSortExtractionModel { });

                Close();

                fieldSelectionForm.Show();
            }
            else
            {
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
        }

        private void radioButtonYes_CheckedChanged(object sender, EventArgs e)
        {
            //radioButtonNo.Checked = false;

            ShowChildrenSection(true);

            ShowMapSection(false);

            btnNext.Text = "Next";

            Height = 835;

            btnPrevious.Location = new Point(25, 710);

            btnNext.Location = new Point(297, 710);
        }

        private void radioButtonNo_CheckedChanged(object sender, EventArgs e)
        {
            //radioButtonYes.Checked = false;

            ShowChildrenSection(false);

            ShowMapSection(true);

            btnNext.Text = _isUpdate ? "Update" : "Save";

            Height = 750;

            btnPrevious.Location = new Point(22, 608);

            btnNext.Location = new Point(297, 608);

        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (_isUpdate)
            {
                var updateExtractionService = Reusables.Instance.ExcelForceServiceFactory.GetUpdateExtractionMapService();

                var fieldListResponse = updateExtractionService.LoadActionsOnFieldList();


                if (fieldListResponse.IsValid())
                {
                    var extractionMapFieldsForm = new ExtractionMapFieldsForm(
                          fieldListResponse.Model.ObjectName,
                          fieldListResponse?.Model.AvailableFields,
                          fieldListResponse?.Model.SfFields, new SearchSortExtractionModel { });
                    Close();
                    extractionMapFieldsForm.Show();
                }
            }
            else
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
        }

        private void ShowChildrenSection(bool show)
        {
            lblRelationshipDetails.Visible = show;

            lblChildObject.Visible = show;

            listChildObject.Visible = show;

            lblRelationshipName.Visible = show;

            listRelationshipName.Visible = show;
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

        private void listChildObject_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void lblRelationshipName_Click(object sender, EventArgs e)
        {

        }

        private void listRelationshipName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listChildObject_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            listRelationshipName.DataSource = sfChildRelationships
              ?.FirstOrDefault(x => x.ObjectName == Convert.ToString(listChildObject.SelectedValue))
              ?.RelationshipFields;
        }
    }
}
