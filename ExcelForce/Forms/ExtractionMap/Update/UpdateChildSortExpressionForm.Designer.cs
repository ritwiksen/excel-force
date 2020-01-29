namespace ExcelForce.Forms.ExtractionMap.Update
{
    partial class UpdateChildSortExpressionForm
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
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblMapName = new System.Windows.Forms.Label();
            this.txtMapName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(342, 37);
            this.label2.TabIndex = 3;
            this.label2.Text = "Update Extraction Map";
            // 
            // SearchConditionLabel
            // 
            this.SearchConditionLabel.AutoSize = true;
            this.SearchConditionLabel.Font = new System.Drawing.Font("Calibri Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchConditionLabel.Location = new System.Drawing.Point(23, 70);
            this.SearchConditionLabel.Name = "SearchConditionLabel";
            this.SearchConditionLabel.Size = new System.Drawing.Size(222, 27);
            this.SearchConditionLabel.TabIndex = 7;
            this.SearchConditionLabel.Text = "Define Search Condition";
            // 
            // searchConditionTextBox
            // 
            this.searchConditionTextBox.Location = new System.Drawing.Point(28, 118);
            this.searchConditionTextBox.Name = "searchConditionTextBox";
            this.searchConditionTextBox.Size = new System.Drawing.Size(488, 96);
            this.searchConditionTextBox.TabIndex = 8;
            this.searchConditionTextBox.Text = "";
            // 
            // SortConditionLabel
            // 
            this.SortConditionLabel.AutoSize = true;
            this.SortConditionLabel.Font = new System.Drawing.Font("Calibri Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SortConditionLabel.Location = new System.Drawing.Point(23, 251);
            this.SortConditionLabel.Name = "SortConditionLabel";
            this.SortConditionLabel.Size = new System.Drawing.Size(199, 27);
            this.SortConditionLabel.TabIndex = 9;
            this.SortConditionLabel.Text = "Define Sort Condition";
            // 
            // sortConditionTextBox
            // 
            this.sortConditionTextBox.Location = new System.Drawing.Point(28, 302);
            this.sortConditionTextBox.Name = "sortConditionTextBox";
            this.sortConditionTextBox.Size = new System.Drawing.Size(488, 96);
            this.sortConditionTextBox.TabIndex = 10;
            this.sortConditionTextBox.Text = "";
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnPrevious.ForeColor = System.Drawing.SystemColors.Window;
            this.btnPrevious.Location = new System.Drawing.Point(28, 543);
            this.btnPrevious.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(215, 58);
            this.btnPrevious.TabIndex = 19;
            this.btnPrevious.Text = "Previous";
            this.btnPrevious.UseVisualStyleBackColor = false;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnNext.ForeColor = System.Drawing.SystemColors.Window;
            this.btnNext.Location = new System.Drawing.Point(294, 543);
            this.btnNext.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(222, 58);
            this.btnNext.TabIndex = 20;
            this.btnNext.Text = "Update";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblMapName
            // 
            this.lblMapName.AutoSize = true;
            this.lblMapName.Font = new System.Drawing.Font("Calibri Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMapName.Location = new System.Drawing.Point(25, 435);
            this.lblMapName.Name = "lblMapName";
            this.lblMapName.Size = new System.Drawing.Size(109, 27);
            this.lblMapName.TabIndex = 22;
            this.lblMapName.Text = "Map Name";
            // 
            // txtMapName
            // 
            this.txtMapName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMapName.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMapName.Location = new System.Drawing.Point(28, 477);
            this.txtMapName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMapName.Name = "txtMapName";
            this.txtMapName.ReadOnly = true;
            this.txtMapName.Size = new System.Drawing.Size(488, 34);
            this.txtMapName.TabIndex = 21;
            // 
            // UpdateChildSortExpressionForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 636);
            this.Controls.Add(this.lblMapName);
            this.Controls.Add(this.txtMapName);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.sortConditionTextBox);
            this.Controls.Add(this.SortConditionLabel);
            this.Controls.Add(this.searchConditionTextBox);
            this.Controls.Add(this.SearchConditionLabel);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateChildSortExpressionForm";
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
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblMapName;
        private System.Windows.Forms.TextBox txtMapName;
    }
}