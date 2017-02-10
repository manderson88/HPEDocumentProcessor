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
        private System.Windows.Forms.TextBox txtMSKeyin2;
        private System.Windows.Forms.TextBox txtMSKeyin3;
        private Label label1;
        private Label label5;
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

                if (null != myDocProcConfigData.MSKeyin2)
                    this.txtMSKeyin2.Text = myDocProcConfigData.MSKeyin2;

                if (null != myDocProcConfigData.MSKeyin3)
                    this.txtMSKeyin3.Text = myDocProcConfigData.MSKeyin3;

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

        public string MSKeyin3
        {
            get
            {
                return this.txtMSKeyin3.Text;
            }
        }

        public string MSKeyin2
        {
            get
            {
                return this.txtMSKeyin2.Text;
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
            this.txtMSKeyin2 = new System.Windows.Forms.TextBox();
            this.txtMSKeyin3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
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
            // txtMSKeyin1
            // 
            this.txtMDLAppName.Location = new System.Drawing.Point(102, 9);
            this.txtMDLAppName.Name = "MDL Application";
            this.txtMDLAppName.Size = new System.Drawing.Size(202, 20);
            this.txtMDLAppName.TabIndex = 2;
            // 
            // txtMSKeyin2
            // 
            this.txtMSKeyin2.Location = new System.Drawing.Point(102, 49);
            this.txtMSKeyin2.Name = "txtMSKeyin2";
            this.txtMSKeyin2.Size = new System.Drawing.Size(202, 20);
            this.txtMSKeyin2.TabIndex = 3;
            // 
            // txtMSKeyin3
            // 
            this.txtMSKeyin3.Location = new System.Drawing.Point(102, 88);
            this.txtMSKeyin3.Name = "txtMSKeyin3";
            this.txtMSKeyin3.Size = new System.Drawing.Size(202, 20);
            this.txtMSKeyin3.TabIndex = 4;
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
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(24, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "Secondary Keyin:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.txtMSKeyin4.Size = new System.Drawing.Size(100, 20);
            this.txtMSKeyin4.TabIndex = 21;
            // 
            // txtMSKeyin5
            // 
            this.txtMSKeyin5.Location = new System.Drawing.Point(102, 212);
            this.txtMSKeyin5.Name = "txtMSKeyin5";
            this.txtMSKeyin5.Size = new System.Drawing.Size(100, 20);
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
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMSKeyin3);
            this.Controls.Add(this.txtMSKeyin2);
            this.Controls.Add(this.txtMDLAppName);
            this.Name = "UserInterface";
            this.Size = new System.Drawing.Size(320, 338);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
    }
}
