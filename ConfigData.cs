using System;
using System.Xml;

namespace HPE.Automation.Extensions.HPEGeneralProcessor
{
    /// <summary>
    /// Summary description for ConfigData.
    /// </summary>
    public class ConfigData
    {
        private string m_sPWUser = null;
        private string m_sPWPassword = null;
        private string m_sMDLAppName = null;
        private string m_sMSKeyin2 = null;
        private string m_sMSKeyin3 = null;
        private string m_sMSKeyin4 = null;
        private string m_sMSKeyin5 = null;
        private string m_sSMTPServer = null;
        private string m_sSentFromAddress = null;

        public ConfigData()
        {
            m_sPWUser = null;
            m_sPWPassword = null;
            m_sMDLAppName = null;
            m_sMSKeyin2 = null;
            m_sMSKeyin3 = null;
            m_sMSKeyin4 = null;
            m_sMSKeyin5 = null;
            m_sSMTPServer = null;
            m_sSentFromAddress = null;
        }

        /// <summary>
        /// Converts class into an XmlElement
        /// </summary>
        /// <returns></returns>
        public XmlElement ToXmlElement()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement xmlElem = xmlDoc.CreateElement("GenericMSDocumentProcessor");
            if (this.m_sPWUser != null)
                xmlElem.SetAttribute("PWUser", m_sPWUser);
            if (this.m_sPWPassword != null)
                xmlElem.SetAttribute("PWPassword", m_sPWPassword);
            if (this.m_sMDLAppName != null)
                xmlElem.SetAttribute("MDLAppName", m_sMDLAppName);
            if (this.m_sMSKeyin2 != null)
                xmlElem.SetAttribute("MSKeyin2", m_sMSKeyin2);
            if (this.m_sMSKeyin3 != null)
                xmlElem.SetAttribute("MSKeyin3", m_sMSKeyin3);
            if (this.m_sMSKeyin4 != null)
                xmlElem.SetAttribute("MSKeyin4", m_sMSKeyin4);
            if (this.m_sMSKeyin5 != null)
                xmlElem.SetAttribute("MSKeyin5", m_sMSKeyin5);
            if (this.m_sSMTPServer != null)
                xmlElem.SetAttribute("SMTPServer", m_sSMTPServer);
            if (this.m_sSentFromAddress != null)
                xmlElem.SetAttribute("SentFromAddress", m_sSentFromAddress);

            return xmlElem;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xmlBody">DPExtensionConfigData as an XmlElement</param>
        public ConfigData(XmlElement xmlConfigData)
        {
            m_sPWUser = xmlConfigData.GetAttribute("PWUser");
            m_sPWPassword = xmlConfigData.GetAttribute("PWPassword");
            m_sMDLAppName = xmlConfigData.GetAttribute("MDLAppName");
            m_sMSKeyin2 = xmlConfigData.GetAttribute("MSKeyin2");
            m_sMSKeyin3 = xmlConfigData.GetAttribute("MSKeyin3");
            m_sMSKeyin4 = xmlConfigData.GetAttribute("MSKeyin4");
            m_sMSKeyin5 = xmlConfigData.GetAttribute("MSKeyin5");
            m_sSMTPServer = xmlConfigData.GetAttribute("SMTPServer");
            m_sSentFromAddress = xmlConfigData.GetAttribute("SentFromAddress");
        }

        public string SentFromAddress
        {
            get
            {
                return m_sSentFromAddress;
            }
            set
            {
                m_sSentFromAddress = value;
            }
        }

        public string SMTPServer
        {
            get
            {
                return m_sSMTPServer;
            }
            set
            {
                m_sSMTPServer = value;
            }
        }

        public string MSKeyin5
        {
            get
            {
                return m_sMSKeyin5;
            }
            set
            {
                m_sMSKeyin5 = value;
            }
        }

        public string MSKeyin4
        {
            get
            {
                return m_sMSKeyin4;
            }
            set
            {
                m_sMSKeyin4 = value;
            }
        }

        public string MSKeyin3
        {
            get
            {
                return m_sMSKeyin3;
            }
            set
            {
                m_sMSKeyin3 = value;
            }
        }

        public string MSKeyin2
        {
            get
            {
                return m_sMSKeyin2;
            }
            set
            {
                m_sMSKeyin2 = value;
            }
        }

        public string MDLAppName
        {
            get
            {
                return m_sMDLAppName;
            }
            set
            {
                m_sMDLAppName = value;
            }
        }

        public string PWPassword
        {
            get
            {
                return m_sPWPassword;
            }
            set
            {
                m_sPWPassword = value;
            }
        }

        public string PWUser
        {
            get
            {
                return m_sPWUser;
            }
            set
            {
                m_sPWUser = value;
            }
        }



    }
}
