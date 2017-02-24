/*--------------------------------------------------------------------------------------+
//----------------------------------------------------------------------------
// DOCUMENT ID:   
// LIBRARY:       
// CREATOR:       Mark Anderson
// DATE:          02-15-2017
//
// NAME:          UserInterface.cs
//
// DESCRIPTION:   Utility.
//
// REFERENCES:    ProjectWise.
//
// ---------------------------------------------------------------------------
// NOTICE
//    NOTICE TO ALL PERSONS HAVING ACCESS HERETO:  This document or
//    recording contains computer software or related information
//    constituting proprietary trade secrets of Black & Veatch, which
//    have been maintained in "unpublished" status under the copyright
//    laws, and which are to be treated by all persons having acdcess
//    thereto in manner to preserve the status thereof as legally
//    protectable trade secrets by neither using nor disclosing the
//    same to others except as may be expressly authorized in advance
//    by Black & Veatch.  However, it is intended that all prospective
//    rights under the copyrigtht laws in the event of future
//    "publication" of this work shall also be reserved; for which
//    purpose only, the following is included in this notice, to wit,
//    "(C) COPYRIGHT 1997 BY BLACK & VEATCH, ALL RIGHTS RESERVED"
// ---------------------------------------------------------------------------
/*
/* CHANGE LOG
 * $Archive: /ProjectWise/ASFramework/HPEDocumentProcessor/UserInterface.cs $
 * $Revision: 1 $
 * $Modtime: 2/15/17 7:18a $
 * $History: UserInterface.cs $
 * 
 * *****************  Version 1  *****************
 * User: Mark.anderson Date: 2/15/17    Time: 7:45a
 * Created in $/ProjectWise/ASFramework/HPEDocumentProcessor
 * A General purpose document processor.  This will  use an application
 * name and command line to load in to the msprocessor
 * 
*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using Bentley.Automation.JobConfiguration;

namespace HPE.Automation.Extensions.HPEGeneralProcessor
{
    /// <summary>
    /// Summary description for UserInterface.
    /// </summary>
    public class UserInterface : System.Windows.Forms.UserControl
    {
        private System.Windows.Forms.TextBox txtMDLAppName;
        private System.Windows.Forms.TextBox txtPrimaryKeyn;
        private System.Windows.Forms.TextBox txtPWLoginCMD;
        private Label label1;
        private Label lblPWLoginCMD;
        private Label label4;
        private Label label3;
        private TextBox txtPWUser;
        private TextBox txtPWPassword;
        private Label lblPWUser;
        private Label lblPWPassword;
        private TextBox txtMSKeyin4;
        private TextBox txtMSKeyin5;
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public UserInterface()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call

        }

        public UserInterface(JobDefinition jd, Guid DocumentProcessorGuid)
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            XmlNode xmlNode = (XmlNode)jd.GetCustomData(DocumentProcessorGuid);
            if (xmlNode != null)
            {
                ConfigData myDocProcConfigData = new ConfigData((XmlElement)xmlNode);

                if (null != myDocProcConfigData.PWUser)
                    this.txtPWUser.Text = myDocProcConfigData.PWUser;

                if (null != myDocProcConfigData.PWPassword)
                    this.txtPWPassword.Text = myDocProcConfigData.PWPassword;

                if (null != myDocProcConfigData.MDLAppName)
                    this.txtMDLAppName.Text = myDocProcConfigData.MDLAppName;

                if (null != myDocProcConfigData.AppKeyin)
                    this.txtPrimaryKeyn.Text = myDocProcConfigData.AppKeyin;

                if (null != myDocProcConfigData.PWLoginCMD)
                    this.txtPWLoginCMD.Text = myDocProcConfigData.PWLoginCMD;

                if (null != myDocProcConfigData.MSKeyin4)
                    this.txtMSKeyin4.Text = myDocProcConfigData.MSKeyin4;

                if (null != myDocProcConfigData.MSKeyin5)
                    this.txtMSKeyin5.Text = myDocProcConfigData.MSKeyin5;
            }
        }

        public string MSKeyin5
        {
            get
            {
                return this.txtMSKeyin5.Text;
            }
        }

        public string MSKeyin4
        {
            get
            {
                return this.txtMSKeyin4.Text;
            }
        }

        public string PWLoginCMD
        {
            get
            {
                return this.txtPWLoginCMD.Text;
            }
        }

        public string AppKeyin
        {
            get
            {
                return this.txtPrimaryKeyn.Text;
            }
        }

        public string MDLAppName
        {
            get
            {
                return this.txtMDLAppName.Text;
            }
        }

        public string PWPassword
        {
            get
            {
                return this.txtPWPassword.Text;
            }
        }

        public string PWUser
        {
            get
            {
                return this.txtPWUser.Text;
            }
        }


        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            this.txtMDLAppName = new System.Windows.Forms.TextBox();
            this.txtPrimaryKeyn = new System.Windows.Forms.TextBox();
            this.txtPWLoginCMD = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPWLoginCMD = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPWUser = new System.Windows.Forms.TextBox();
            this.txtPWPassword = new System.Windows.Forms.TextBox();
            this.lblPWUser = new System.Windows.Forms.Label();
            this.lblPWPassword = new System.Windows.Forms.Label();
            this.txtMSKeyin4 = new System.Windows.Forms.TextBox();
            this.txtMSKeyin5 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtMDLAppName
            // 
            this.txtMDLAppName.Location = new System.Drawing.Point(102, 9);
            this.txtMDLAppName.Name = "txtMDLAppName";
            this.txtMDLAppName.Size = new System.Drawing.Size(202, 20);
            this.txtMDLAppName.TabIndex = 2;
            // 
            // txtPrimaryKeyn
            // 
            this.txtPrimaryKeyn.Location = new System.Drawing.Point(102, 49);
            this.txtPrimaryKeyn.Name = "txtPrimaryKeyn";
            this.txtPrimaryKeyn.Size = new System.Drawing.Size(202, 20);
            this.txtPrimaryKeyn.TabIndex = 3;
            // 
            // txtPWLoginCMD
            // 
            this.txtPWLoginCMD.Location = new System.Drawing.Point(102, 88);
            this.txtPWLoginCMD.Name = "txtPWLoginCMD";
            this.txtPWLoginCMD.Size = new System.Drawing.Size(202, 20);
            this.txtPWLoginCMD.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 277);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(304, 40);
            this.label1.TabIndex = 16;
            this.label1.Text = "The command line will be executed with the copied out file name as first argument" +
    ".  The \"Arguments\" value will be appended after the copied out file name.";
            // 
            // lblPWLoginCMD
            // 
            this.lblPWLoginCMD.Location = new System.Drawing.Point(24, 89);
            this.lblPWLoginCMD.Name = "lblPWLoginCMD";
            this.lblPWLoginCMD.Size = new System.Drawing.Size(72, 16);
            this.lblPWLoginCMD.TabIndex = 15;
            this.lblPWLoginCMD.Text = "PW Login CMD:";
            this.lblPWLoginCMD.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "Primmary Keyin:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "MDL Application Name:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPWUser
            // 
            this.txtPWUser.Location = new System.Drawing.Point(102, 125);
            this.txtPWUser.Name = "txtPWUser";
            this.txtPWUser.Size = new System.Drawing.Size(202, 20);
            this.txtPWUser.TabIndex = 17;
            // 
            // txtPWPassword
            // 
            this.txtPWPassword.Location = new System.Drawing.Point(102, 160);
            this.txtPWPassword.Name = "txtPWPassword";
            this.txtPWPassword.PasswordChar = '*';
            this.txtPWPassword.Size = new System.Drawing.Size(202, 20);
            this.txtPWPassword.TabIndex = 18;
            this.txtPWPassword.UseSystemPasswordChar = true;
            // 
            // lblPWUser
            // 
            this.lblPWUser.AutoSize = true;
            this.lblPWUser.Location = new System.Drawing.Point(12, 128);
            this.lblPWUser.Name = "lblPWUser";
            this.lblPWUser.Size = new System.Drawing.Size(84, 13);
            this.lblPWUser.TabIndex = 19;
            this.lblPWUser.Text = "PW User Name:";
            // 
            // lblPWPassword
            // 
            this.lblPWPassword.AutoSize = true;
            this.lblPWPassword.Location = new System.Drawing.Point(19, 163);
            this.lblPWPassword.Name = "lblPWPassword";
            this.lblPWPassword.Size = new System.Drawing.Size(77, 13);
            this.lblPWPassword.TabIndex = 20;
            this.lblPWPassword.Text = "PW Password:";
            // 
            // txtMSKeyin4
            // 
            this.txtMSKeyin4.Location = new System.Drawing.Point(102, 186);
            this.txtMSKeyin4.Name = "txtMSKeyin4";
            this.txtMSKeyin4.Size = new System.Drawing.Size(202, 20);
            this.txtMSKeyin4.TabIndex = 21;
            // 
            // txtMSKeyin5
            // 
            this.txtMSKeyin5.Location = new System.Drawing.Point(102, 212);
            this.txtMSKeyin5.Name = "txtMSKeyin5";
            this.txtMSKeyin5.Size = new System.Drawing.Size(202, 20);
            this.txtMSKeyin5.TabIndex = 22;
            // 
            // UserInterface
            // 
            this.Controls.Add(this.txtMSKeyin5);
            this.Controls.Add(this.txtMSKeyin4);
            this.Controls.Add(this.lblPWPassword);
            this.Controls.Add(this.lblPWUser);
            this.Controls.Add(this.txtPWPassword);
            this.Controls.Add(this.txtPWUser);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblPWLoginCMD);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPWLoginCMD);
            this.Controls.Add(this.txtPrimaryKeyn);
            this.Controls.Add(this.txtMDLAppName);
            this.Name = "UserInterface";
            this.Size = new System.Drawing.Size(337, 338);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
    }
}
