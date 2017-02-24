/*--------------------------------------------------------------------------------------+
//----------------------------------------------------------------------------
// DOCUMENT ID:   
// LIBRARY:       
// CREATOR:       Mark Anderson
// DATE:          02-15-2017
//
// NAME:          ConfigData.cs
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
 * $Archive: /ProjectWise/ASFramework/HPEDocumentProcessor/ConfigData.cs $
 * $Revision: 1 $
 * $Modtime: 2/15/17 7:18a $
 * $History: ConfigData.cs $
 * 
 * *****************  Version 1  *****************
 * User: Mark.anderson Date: 2/15/17    Time: 7:43a
 * Created in $/ProjectWise/ASFramework/HPEDocumentProcessor
 * A General purpose document processor.  This will  use an application
 * name and command line to load in to the msprocessor
 * 
*/

using System;
using System.Xml;

namespace HPE.Automation.Extensions.HPEGeneralProcessor
{
    /// <summary>
    /// This class contains the job configuration information.  It will hold
    /// the PW user information to pass into the app if needed.  It holds the
    /// MDL application name and command  to execute.
    /// </summary>
    public class ConfigData
    {
        private string m_sPWUser = null;
        private string m_sPWPassword = null;
        private string m_sMDLAppName = null;
        private string m_sAppKeyin = null;
        private string m_sPWLoginCMD = null;
        private string m_sMSKeyin4 = null;
        private string m_sMSKeyin5 = null;
        private string m_sSMTPServer = null;
        private string m_sSentFromAddress = null;

        public ConfigData()
        {
            m_sPWUser = null;
            m_sPWPassword = null;
            m_sMDLAppName = null;
            m_sAppKeyin = null;
            m_sPWLoginCMD = null;
            m_sMSKeyin4 = null;
            m_sMSKeyin5 = null;
            m_sSMTPServer = null;
            m_sSentFromAddress = null;
        }

        /// <summary>
        /// Converts class fields into an XmlElement
        /// </summary>
        /// <returns></returns>
        public XmlElement ToXmlElement()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement xmlElem = xmlDoc.CreateElement("HPEGeneralProcessor");
            if (this.m_sPWUser != null)
                xmlElem.SetAttribute("PWUser", m_sPWUser);
            if (this.m_sPWPassword != null)
                xmlElem.SetAttribute("PWPassword", m_sPWPassword);
            if (this.m_sMDLAppName != null)
                xmlElem.SetAttribute("MDLAppName", m_sMDLAppName);
            if (this.m_sAppKeyin != null)
                xmlElem.SetAttribute("AppKeyin", m_sAppKeyin);
            if (this.m_sPWLoginCMD != null)
                xmlElem.SetAttribute("PWLoginCMD", m_sPWLoginCMD);
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
        /// Constructor from XML data.
        /// </summary>
        /// <param name="xmlBody">DPExtensionConfigData as an XmlElement</param>
        public ConfigData(XmlElement xmlConfigData)
        {
            m_sPWUser = xmlConfigData.GetAttribute("PWUser");
            m_sPWPassword = xmlConfigData.GetAttribute("PWPassword");
            m_sMDLAppName = xmlConfigData.GetAttribute("MDLAppName");
            m_sAppKeyin = xmlConfigData.GetAttribute("AppKeyin");
            m_sPWLoginCMD = xmlConfigData.GetAttribute("PWLoginCMD");
            m_sMSKeyin4 = xmlConfigData.GetAttribute("MSKeyin4");
            m_sMSKeyin5 = xmlConfigData.GetAttribute("MSKeyin5");
            m_sSMTPServer = xmlConfigData.GetAttribute("SMTPServer");
            m_sSentFromAddress = xmlConfigData.GetAttribute("SentFromAddress");
        }
        /// <summary>
        /// Accessor methods for the fields.
        /// </summary>
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

        public string PWLoginCMD
        {
            get
            {
                return m_sPWLoginCMD;
            }
            set
            {
                m_sPWLoginCMD = value;
            }
        }

        public string AppKeyin
        {
            get
            {
                return m_sAppKeyin;
            }
            set
            {
                m_sAppKeyin = value;
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
