﻿using ExcelForce.Business.ServiceFactory;

namespace ExcelForce
{
    partial class ExcelForce : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public ExcelForce()
            : base(Globals.Factory.GetRibbonFactory())
        {
            //TODO:(Ritwik):: To refactor this from Factory classes
            _excelForceServiceFactory = new ExcelForceServiceFactory();

            InitializeComponent();

            LoadConnectionProfiles();
        }

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItemImpl1 = this.Factory.CreateRibbonDropDownItem();
            Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItemImpl2 = this.Factory.CreateRibbonDropDownItem();
            Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItemImpl3 = this.Factory.CreateRibbonDropDownItem();
            Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItemImpl4 = this.Factory.CreateRibbonDropDownItem();
            Microsoft.Office.Tools.Ribbon.RibbonDropDownItem ribbonDropDownItemImpl5 = this.Factory.CreateRibbonDropDownItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExcelForce));
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.dropDown1 = this.Factory.CreateRibbonDropDown();
            this.dropDown2 = this.Factory.CreateRibbonDropDown();
            this.groupActions = this.Factory.CreateRibbonGroup();
            this.button3 = this.Factory.CreateRibbonButton();
            this.button4 = this.Factory.CreateRibbonButton();
            this.groupAuthentication = this.Factory.CreateRibbonGroup();
            this.btnLogin = this.Factory.CreateRibbonButton();
            this.button10 = this.Factory.CreateRibbonButton();
            this.connectionProfileSplitButton = this.Factory.CreateRibbonSplitButton();
            this.groupCrud = this.Factory.CreateRibbonGroup();
            this.button6 = this.Factory.CreateRibbonButton();
            this.separator1 = this.Factory.CreateRibbonSeparator();
            this.button7 = this.Factory.CreateRibbonButton();
            this.separator2 = this.Factory.CreateRibbonSeparator();
            this.button8 = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.group2.SuspendLayout();
            this.groupActions.SuspendLayout();
            this.groupAuthentication.SuspendLayout();
            this.groupCrud.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.Groups.Add(this.group2);
            this.tab1.Groups.Add(this.groupActions);
            this.tab1.Groups.Add(this.groupAuthentication);
            this.tab1.Groups.Add(this.groupCrud);
            this.tab1.Label = "ExcelForce";
            this.tab1.Name = "tab1";
            // 
            // group2
            // 
            this.group2.Items.Add(this.dropDown1);
            this.group2.Items.Add(this.dropDown2);
            this.group2.Label = "Selections";
            this.group2.Name = "group2";
            this.group2.Visible = false;
            // 
            // dropDown1
            // 
            this.dropDown1.Label = "Entities";
            this.dropDown1.Name = "dropDown1";
            this.dropDown1.SelectionChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.DropDown1_SelectionChanged);
            // 
            // dropDown2
            // 
            ribbonDropDownItemImpl1.Label = "-None-";
            ribbonDropDownItemImpl2.Label = "Select";
            ribbonDropDownItemImpl3.Label = "Insert";
            ribbonDropDownItemImpl4.Label = "Update";
            ribbonDropDownItemImpl5.Label = "Delete";
            this.dropDown2.Items.Add(ribbonDropDownItemImpl1);
            this.dropDown2.Items.Add(ribbonDropDownItemImpl2);
            this.dropDown2.Items.Add(ribbonDropDownItemImpl3);
            this.dropDown2.Items.Add(ribbonDropDownItemImpl4);
            this.dropDown2.Items.Add(ribbonDropDownItemImpl5);
            this.dropDown2.Label = "Operation";
            this.dropDown2.Name = "dropDown2";
            this.dropDown2.SelectionChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.dropDown2_SelectionChanged);
            // 
            // groupActions
            // 
            this.groupActions.Items.Add(this.button3);
            this.groupActions.Items.Add(this.button4);
            this.groupActions.Label = "Actions";
            this.groupActions.Name = "groupActions";
            this.groupActions.Visible = false;
            // 
            // button3
            // 
            this.button3.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Label = "Populate Data";
            this.button3.Name = "button3";
            this.button3.ShowImage = true;
            this.button3.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.Label = "Submit";
            this.button4.Name = "button4";
            this.button4.ShowImage = true;
            this.button4.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button4_Click);
            // 
            // groupAuthentication
            // 
            this.groupAuthentication.Items.Add(this.btnLogin);
            this.groupAuthentication.Items.Add(this.button10);
            this.groupAuthentication.Items.Add(this.connectionProfileSplitButton);
            this.groupAuthentication.Label = "Log In / Log Out";
            this.groupAuthentication.Name = "groupAuthentication";
            // 
            // btnLogin
            // 
            this.btnLogin.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnLogin.Image = ((System.Drawing.Image)(resources.GetObject("btnLogin.Image")));
            this.btnLogin.Label = "Login";
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.ShowImage = true;
            this.btnLogin.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnLogin_Click);
            // 
            // button10
            // 
            this.button10.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button10.Image = ((System.Drawing.Image)(resources.GetObject("button10.Image")));
            this.button10.Label = "Logout";
            this.button10.Name = "button10";
            this.button10.ShowImage = true;
            this.button10.Visible = false;
            this.button10.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button10_Click);
            // 
            // connectionProfileSplitButton
            // 
            this.connectionProfileSplitButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.connectionProfileSplitButton.Image = ((System.Drawing.Image)(resources.GetObject("connectionProfileSplitButton.Image")));
            this.connectionProfileSplitButton.Label = "Connection Profiles";
            this.connectionProfileSplitButton.Name = "connectionProfileSplitButton";
            this.connectionProfileSplitButton.Tag = "";
            this.connectionProfileSplitButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.splitButton1_Click);
            // 
            // groupCrud
            // 
            this.groupCrud.Items.Add(this.button6);
            this.groupCrud.Items.Add(this.separator1);
            this.groupCrud.Items.Add(this.button7);
            this.groupCrud.Items.Add(this.separator2);
            this.groupCrud.Items.Add(this.button8);
            this.groupCrud.Label = "Extract and Save (CRUD)";
            this.groupCrud.Name = "groupCrud";
            // 
            // button6
            // 
            this.button6.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button6.Enabled = false;
            this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
            this.button6.Label = "Create Extraction Map";
            this.button6.Name = "button6";
            this.button6.ShowImage = true;
            // 
            // separator1
            // 
            this.separator1.Name = "separator1";
            // 
            // button7
            // 
            this.button7.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button7.Enabled = false;
            this.button7.Image = ((System.Drawing.Image)(resources.GetObject("button7.Image")));
            this.button7.Label = "Update Existing Map";
            this.button7.Name = "button7";
            this.button7.ShowImage = true;
            // 
            // separator2
            // 
            this.separator2.Name = "separator2";
            // 
            // button8
            // 
            this.button8.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button8.Enabled = false;
            this.button8.Image = ((System.Drawing.Image)(resources.GetObject("button8.Image")));
            this.button8.Label = "Extract Data";
            this.button8.Name = "button8";
            this.button8.ShowImage = true;
            this.button8.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button8_Click);
            // 
            // ExcelForce
            // 
            this.Name = "ExcelForce";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab1);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();
            this.groupActions.ResumeLayout(false);
            this.groupActions.PerformLayout();
            this.groupAuthentication.ResumeLayout(false);
            this.groupAuthentication.PerformLayout();
            this.groupCrud.ResumeLayout(false);
            this.groupCrud.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown dropDown2;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupActions;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button3;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button4;
        internal Microsoft.Office.Tools.Ribbon.RibbonDropDown dropDown1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupAuthentication;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup groupCrud;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button6;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button7;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator separator2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button8;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnLogin;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button10;
        internal Microsoft.Office.Tools.Ribbon.RibbonSplitButton connectionProfileSplitButton;
    }

    partial class ThisRibbonCollection
    {
        internal ExcelForce MenuItems
        {
            get { return this.GetRibbon<ExcelForce>(); }
        }
    }
}
