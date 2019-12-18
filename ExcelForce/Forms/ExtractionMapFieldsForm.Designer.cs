namespace ExcelForce
{
    partial class ExtractionMapFieldsForm
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
            this.label5 = new System.Windows.Forms.Label();
            this.checkedFieldList = new System.Windows.Forms.CheckedListBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.txtPrimaryObjectName = new System.Windows.Forms.TextBox();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(334, 37);
            this.label2.TabIndex = 2;
            this.label2.Text = "Create Extraction Map";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(23, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 27);
            this.label5.TabIndex = 6;
            this.label5.Text = "Primary object";
            // 
            // checkedFieldList
            // 
            this.checkedFieldList.FormattingEnabled = true;
            this.checkedFieldList.Location = new System.Drawing.Point(28, 166);
            this.checkedFieldList.Name = "checkedFieldList";
            this.checkedFieldList.Size = new System.Drawing.Size(494, 235);
            this.checkedFieldList.TabIndex = 7;
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnNext.ForeColor = System.Drawing.SystemColors.Window;
            this.btnNext.Location = new System.Drawing.Point(300, 476);
            this.btnNext.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(222, 58);
            this.btnNext.TabIndex = 11;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = false;
            // 
            // txtPrimaryObjectName
            // 
            this.txtPrimaryObjectName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrimaryObjectName.Enabled = false;
            this.txtPrimaryObjectName.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrimaryObjectName.Location = new System.Drawing.Point(28, 100);
            this.txtPrimaryObjectName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPrimaryObjectName.Name = "txtPrimaryObjectName";
            this.txtPrimaryObjectName.Size = new System.Drawing.Size(494, 34);
            this.txtPrimaryObjectName.TabIndex = 12;
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnPrevious.ForeColor = System.Drawing.SystemColors.Window;
            this.btnPrevious.Location = new System.Drawing.Point(28, 476);
            this.btnPrevious.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(215, 58);
            this.btnPrevious.TabIndex = 13;
            this.btnPrevious.Text = "Previous";
            this.btnPrevious.UseVisualStyleBackColor = false;
            // 
            // ExtractionMapFieldsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 545);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.txtPrimaryObjectName);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.checkedFieldList);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExtractionMapFieldsForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckedListBox checkedFieldList;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.TextBox txtPrimaryObjectName;
        private System.Windows.Forms.Button btnPrevious;
    }
}