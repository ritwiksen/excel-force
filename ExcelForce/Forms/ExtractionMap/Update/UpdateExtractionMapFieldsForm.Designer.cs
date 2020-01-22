namespace ExcelForce.Forms.ExtractionMap.Update
{
    partial class UpdateExtractionMapFieldsForm
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
            this.btnNext = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.parentObjectName = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.childObjectLabel = new System.Windows.Forms.Label();
            this.childDelete = new System.Windows.Forms.Button();
            this.childEdit = new System.Windows.Forms.Button();
            this.childObject2 = new System.Windows.Forms.CheckBox();
            this.childObject1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.updateSelectMap2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(342, 37);
            this.label2.TabIndex = 2;
            this.label2.Text = "Update Extraction Map";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(22, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 27);
            this.label5.TabIndex = 6;
            this.label5.Text = "Selected Map";
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
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 27);
            this.label1.TabIndex = 16;
            this.label1.Text = "Parent Object";
            // 
            // parentObjectName
            // 
            this.parentObjectName.Location = new System.Drawing.Point(28, 172);
            this.parentObjectName.Name = "parentObjectName";
            this.parentObjectName.Size = new System.Drawing.Size(494, 26);
            this.parentObjectName.TabIndex = 17;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(455, 142);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(67, 25);
            this.button2.TabIndex = 18;
            this.button2.Text = "Edit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.parentObjectUpdate_Click);
            // 
            // childObjectLabel
            // 
            this.childObjectLabel.AutoSize = true;
            this.childObjectLabel.Font = new System.Drawing.Font("Calibri Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.childObjectLabel.Location = new System.Drawing.Point(23, 231);
            this.childObjectLabel.Name = "childObjectLabel";
            this.childObjectLabel.Size = new System.Drawing.Size(118, 27);
            this.childObjectLabel.TabIndex = 19;
            this.childObjectLabel.Text = "Child Object";
            // 
            // childDelete
            // 
            this.childDelete.Location = new System.Drawing.Point(373, 218);
            this.childDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.childDelete.Name = "childDelete";
            this.childDelete.Size = new System.Drawing.Size(67, 25);
            this.childDelete.TabIndex = 24;
            this.childDelete.Text = "Delete";
            this.childDelete.UseVisualStyleBackColor = true;
            // 
            // childEdit
            // 
            this.childEdit.Location = new System.Drawing.Point(455, 218);
            this.childEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.childEdit.Name = "childEdit";
            this.childEdit.Size = new System.Drawing.Size(67, 25);
            this.childEdit.TabIndex = 23;
            this.childEdit.Text = "Edit";
            this.childEdit.UseVisualStyleBackColor = true;
            this.childEdit.Click += new System.EventHandler(this.childObjectUpdate_Click);
            // 
            // childObject2
            // 
            this.childObject2.AutoSize = true;
            this.childObject2.Location = new System.Drawing.Point(28, 334);
            this.childObject2.Name = "childObject2";
            this.childObject2.Size = new System.Drawing.Size(162, 24);
            this.childObject2.TabIndex = 26;
            this.childObject2.Text = "Name | API Name";
            this.childObject2.UseVisualStyleBackColor = true;
            // 
            // childObject1
            // 
            this.childObject1.AutoSize = true;
            this.childObject1.Location = new System.Drawing.Point(28, 283);
            this.childObject1.Name = "childObject1";
            this.childObject1.Size = new System.Drawing.Size(162, 24);
            this.childObject1.TabIndex = 25;
            this.childObject1.Text = "Name | API Name";
            this.childObject1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(28, 476);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(179, 58);
            this.button1.TabIndex = 27;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // updateSelectMap2
            // 
            this.updateSelectMap2.Location = new System.Drawing.Point(27, 101);
            this.updateSelectMap2.Name = "updateSelectMap2";
            this.updateSelectMap2.ReadOnly = true;
            this.updateSelectMap2.Size = new System.Drawing.Size(494, 26);
            this.updateSelectMap2.TabIndex = 28;
            // 
            // UpdateExtractionMapFieldsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 545);
            this.Controls.Add(this.updateSelectMap2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.childObject2);
            this.Controls.Add(this.childObject1);
            this.Controls.Add(this.childDelete);
            this.Controls.Add(this.childEdit);
            this.Controls.Add(this.childObjectLabel);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.parentObjectName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateExtractionMapFieldsForm";
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
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox parentObjectName;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label childObjectLabel;
        private System.Windows.Forms.Button childDelete;
        private System.Windows.Forms.Button childEdit;
        private System.Windows.Forms.CheckBox childObject2;
        private System.Windows.Forms.CheckBox childObject1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox updateSelectMap2;
    }
}