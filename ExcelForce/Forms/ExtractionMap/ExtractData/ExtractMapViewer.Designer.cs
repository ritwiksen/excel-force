namespace ExcelForce.Forms.ExtractionMap.ExtractData
{
    partial class ExtractMapViewer
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
            this.lblHeading = new System.Windows.Forms.Label();
            this.treeViewObject = new System.Windows.Forms.TreeView();
            this.btnExtract = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = true;
            this.lblHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.Location = new System.Drawing.Point(21, 21);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(164, 31);
            this.lblHeading.TabIndex = 3;
            this.lblHeading.Text = "Extract Data";
            // 
            // treeViewObject
            // 
            this.treeViewObject.Location = new System.Drawing.Point(27, 74);
            this.treeViewObject.Name = "treeViewObject";
            this.treeViewObject.Size = new System.Drawing.Size(447, 338);
            this.treeViewObject.TabIndex = 4;
            // 
            // btnExtract
            // 
            this.btnExtract.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnExtract.ForeColor = System.Drawing.SystemColors.Window;
            this.btnExtract.Location = new System.Drawing.Point(277, 448);
            this.btnExtract.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Size = new System.Drawing.Size(197, 46);
            this.btnExtract.TabIndex = 13;
            this.btnExtract.Text = "Extract";
            this.btnExtract.UseVisualStyleBackColor = false;
            this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnPrevious.ForeColor = System.Drawing.SystemColors.Window;
            this.btnPrevious.Location = new System.Drawing.Point(27, 448);
            this.btnPrevious.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(197, 46);
            this.btnPrevious.TabIndex = 14;
            this.btnPrevious.Text = "Previous";
            this.btnPrevious.UseVisualStyleBackColor = false;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // ExtractMapViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 530);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnExtract);
            this.Controls.Add(this.treeViewObject);
            this.Controls.Add(this.lblHeading);
            this.Name = "ExtractMapViewer";
            this.Text = "ExtractMapViewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.TreeView treeViewObject;
        private System.Windows.Forms.Button btnExtract;
        private System.Windows.Forms.Button btnPrevious;
    }
}