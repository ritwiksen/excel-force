﻿namespace ExcelForce.Forms.Common
{
    partial class SearchSortExpressionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.SearchConditionLabel = new System.Windows.Forms.Label();
            this.searchConditionTextBox = new System.Windows.Forms.RichTextBox();
            this.SortConditionLabel = new System.Windows.Forms.Label();
            this.sortConditionTextBox = new System.Windows.Forms.RichTextBox();
            this.lblAddChild = new System.Windows.Forms.Label();
            this.radioButtonYes = new System.Windows.Forms.RadioButton();
            this.radioButtonNo = new System.Windows.Forms.RadioButton();
            this.lblRelationshipDetails = new System.Windows.Forms.Label();
            this.listChildObject = new System.Windows.Forms.ListBox();
            this.lblChildObject = new System.Windows.Forms.Label();
            this.txtMapName = new System.Windows.Forms.TextBox();
            this.lblMapName = new System.Windows.Forms.Label();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(283, 31);
            this.label2.TabIndex = 3;
            this.label2.Text = "Create Extraction Map";
            // 
            // SearchConditionLabel
            // 
            this.SearchConditionLabel.AutoSize = true;
            this.SearchConditionLabel.Font = new System.Drawing.Font("Calibri Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchConditionLabel.Location = new System.Drawing.Point(20, 56);
            this.SearchConditionLabel.Name = "SearchConditionLabel";
            this.SearchConditionLabel.Size = new System.Drawing.Size(194, 23);
            this.SearchConditionLabel.TabIndex = 7;
            this.SearchConditionLabel.Text = "Define Search Condition";
            // 
            // searchConditionTextBox
            // 
            this.searchConditionTextBox.Location = new System.Drawing.Point(25, 94);
            this.searchConditionTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.searchConditionTextBox.Name = "searchConditionTextBox";
            this.searchConditionTextBox.Size = new System.Drawing.Size(434, 78);
            this.searchConditionTextBox.TabIndex = 8;
            this.searchConditionTextBox.Text = "";
            // 
            // SortConditionLabel
            // 
            this.SortConditionLabel.AutoSize = true;
            this.SortConditionLabel.Font = new System.Drawing.Font("Calibri Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SortConditionLabel.Location = new System.Drawing.Point(20, 201);
            this.SortConditionLabel.Name = "SortConditionLabel";
            this.SortConditionLabel.Size = new System.Drawing.Size(174, 23);
            this.SortConditionLabel.TabIndex = 9;
            this.SortConditionLabel.Text = "Define Sort Condition";
            // 
            // sortConditionTextBox
            // 
            this.sortConditionTextBox.Location = new System.Drawing.Point(25, 242);
            this.sortConditionTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sortConditionTextBox.Name = "sortConditionTextBox";
            this.sortConditionTextBox.Size = new System.Drawing.Size(434, 78);
            this.sortConditionTextBox.TabIndex = 10;
            this.sortConditionTextBox.Text = "";
            // 
            // lblAddChild
            // 
            this.lblAddChild.AutoSize = true;
            this.lblAddChild.Font = new System.Drawing.Font("Calibri Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddChild.Location = new System.Drawing.Point(20, 340);
            this.lblAddChild.Name = "lblAddChild";
            this.lblAddChild.Size = new System.Drawing.Size(83, 23);
            this.lblAddChild.TabIndex = 11;
            this.lblAddChild.Text = "Add Child";
            // 
            // radioButtonYes
            // 
            this.radioButtonYes.AutoSize = true;
            this.radioButtonYes.Location = new System.Drawing.Point(25, 375);
            this.radioButtonYes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButtonYes.Name = "radioButtonYes";
            this.radioButtonYes.Size = new System.Drawing.Size(53, 21);
            this.radioButtonYes.TabIndex = 12;
            this.radioButtonYes.TabStop = true;
            this.radioButtonYes.Text = "Yes";
            this.radioButtonYes.UseVisualStyleBackColor = true;
            this.radioButtonYes.CheckedChanged += new System.EventHandler(this.radioButtonYes_CheckedChanged);
            // 
            // radioButtonNo
            // 
            this.radioButtonNo.AutoSize = true;
            this.radioButtonNo.Location = new System.Drawing.Point(188, 375);
            this.radioButtonNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButtonNo.Name = "radioButtonNo";
            this.radioButtonNo.Size = new System.Drawing.Size(47, 21);
            this.radioButtonNo.TabIndex = 13;
            this.radioButtonNo.TabStop = true;
            this.radioButtonNo.Text = "No";
            this.radioButtonNo.UseVisualStyleBackColor = true;
            this.radioButtonNo.CheckedChanged += new System.EventHandler(this.radioButtonNo_CheckedChanged);
            // 
            // lblRelationshipDetails
            // 
            this.lblRelationshipDetails.AutoSize = true;
            this.lblRelationshipDetails.Font = new System.Drawing.Font("Calibri Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRelationshipDetails.Location = new System.Drawing.Point(18, 409);
            this.lblRelationshipDetails.Name = "lblRelationshipDetails";
            this.lblRelationshipDetails.Size = new System.Drawing.Size(158, 23);
            this.lblRelationshipDetails.TabIndex = 14;
            this.lblRelationshipDetails.Text = "Relationship Details";
            // 
            // listChildObject
            // 
            this.listChildObject.FormattingEnabled = true;
            this.listChildObject.ItemHeight = 16;
            this.listChildObject.Location = new System.Drawing.Point(188, 449);
            this.listChildObject.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listChildObject.Name = "listChildObject";
            this.listChildObject.Size = new System.Drawing.Size(274, 20);
            this.listChildObject.TabIndex = 15;
            // 
            // lblChildObject
            // 
            this.lblChildObject.AutoSize = true;
            this.lblChildObject.Font = new System.Drawing.Font("Calibri Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChildObject.Location = new System.Drawing.Point(18, 449);
            this.lblChildObject.Name = "lblChildObject";
            this.lblChildObject.Size = new System.Drawing.Size(101, 23);
            this.lblChildObject.TabIndex = 16;
            this.lblChildObject.Text = "Child Object";
            // 
            // txtMapName
            // 
            this.txtMapName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMapName.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMapName.Location = new System.Drawing.Point(22, 520);
            this.txtMapName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMapName.Name = "txtMapName";
            this.txtMapName.Size = new System.Drawing.Size(439, 30);
            this.txtMapName.TabIndex = 17;
            // 
            // lblMapName
            // 
            this.lblMapName.AutoSize = true;
            this.lblMapName.Font = new System.Drawing.Font("Calibri Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMapName.Location = new System.Drawing.Point(20, 486);
            this.lblMapName.Name = "lblMapName";
            this.lblMapName.Size = new System.Drawing.Size(94, 23);
            this.lblMapName.TabIndex = 18;
            this.lblMapName.Text = "Map Name";
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnPrevious.ForeColor = System.Drawing.SystemColors.Window;
            this.btnPrevious.Location = new System.Drawing.Point(22, 568);
            this.btnPrevious.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(191, 46);
            this.btnPrevious.TabIndex = 19;
            this.btnPrevious.Text = "Previous";
            this.btnPrevious.UseVisualStyleBackColor = false;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnNext.ForeColor = System.Drawing.SystemColors.Window;
            this.btnNext.Location = new System.Drawing.Point(264, 568);
            this.btnNext.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(197, 46);
            this.btnNext.TabIndex = 20;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // SearchSortExpressionForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 623);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.lblMapName);
            this.Controls.Add(this.txtMapName);
            this.Controls.Add(this.lblChildObject);
            this.Controls.Add(this.listChildObject);
            this.Controls.Add(this.lblRelationshipDetails);
            this.Controls.Add(this.radioButtonNo);
            this.Controls.Add(this.radioButtonYes);
            this.Controls.Add(this.lblAddChild);
            this.Controls.Add(this.sortConditionTextBox);
            this.Controls.Add(this.SortConditionLabel);
            this.Controls.Add(this.searchConditionTextBox);
            this.Controls.Add(this.SearchConditionLabel);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchSortExpressionForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label SearchConditionLabel;
        private System.Windows.Forms.RichTextBox searchConditionTextBox;
        private System.Windows.Forms.Label SortConditionLabel;
        private System.Windows.Forms.RichTextBox sortConditionTextBox;
        private System.Windows.Forms.Label lblAddChild;
        private System.Windows.Forms.RadioButton radioButtonYes;
        private System.Windows.Forms.RadioButton radioButtonNo;
        private System.Windows.Forms.Label lblRelationshipDetails;
        private System.Windows.Forms.ListBox listChildObject;
        private System.Windows.Forms.Label lblChildObject;
        private System.Windows.Forms.TextBox txtMapName;
        private System.Windows.Forms.Label lblMapName;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
    }
}