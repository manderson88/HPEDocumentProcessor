/*--------------------------------------------------------------------------------------+
//----------------------------------------------------------------------------
// DOCUMENT ID:   
// LIBRARY:       
// CREATOR:       Mark Anderson
// DATE:          02-15-2017
//
// NAME:          JobDefinitionExtension.cs
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
 * $Archive: /ProjectWise/ASFramework/HPEDocumentProcessor/JobDefinitionExtension.cs $
 * $Revision: 1 $
 * $Modtime: 2/15/17 7:18a $
 * $History: JobDefinitionExtension.cs $
 * 
 * *****************  Version 1  *****************
 * User: Mark.anderson Date: 2/15/17    Time: 7:44a
 * Created in $/ProjectWise/ASFramework/HPEDocumentProcessor
 * A General purpose document processor.  This will  use an application
 * name and command line to load in to the msprocessor
 * 
*/
using System;
using Bentley.Automation.Extensions;
using Bentley.Automation.JobConfiguration;
using Bentley.Automation;

namespace HPE.Automation.Extensions.HPEGeneralProcessor
{
    /// <summary>
    /// Summary description for JobDefinitionExtension.
    /// </summary>
    public class JobDefinitionExtension : ASJobDefinitionExtension
    {
        public JobDefinitionExtension()
            : base(Constants.DocumentProcessorName,
                new Guid(Constants.DocumentProcessorGuid),
                Constants.DocumentProcessorDescription)
        {
            System.Diagnostics.Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            if (0 == String.Compare(currentProcess.ProcessName, "orchadmin", true))
            {
                LogServer.Log(LogServer.LogASConstants.AutomationServicesLoggerNamespace,
                    LogServer.LogASConstants.LAYER_JobBuilder,
                    LogServer.LEVEL_Info,
                    "*********  HPEGeneralProcessor.dll loaded  *************");
            }
            else
            {
            }
        }

        /// <summary>
        /// Provides a chance to load the current job definition. 
        /// It is called when the document processor tab is initialized 
        /// </summary>
        /// <param name="jd">current job definition</param>
        public override void OnLoad(JobDefinition jd)
        {
            System.Windows.Forms.Control myDocProcessorControl = null;

            // If the MyDocProcessorControls was already constructed use it.
            // The ExtendedProperties is a in memory hash map for application development.
            // It persists as long as the job defintion exists.  It is a good place to 
            // store the controls for the document processors.

            // jd.CheckOutFiles = true;

            if (jd.ExtendedProperties.Contains(DocumentProcessorGuid))
                myDocProcessorControl =
                    (System.Windows.Forms.Control)jd.ExtendedProperties[DocumentProcessorGuid];

            // First time, construct a MyDocProcessorControls and store it in the ExtendedProperties
            // using the DocumentProcessorGuid as a unique key.
            if (null == myDocProcessorControl)
            {
                myDocProcessorControl = new UserInterface(jd, DocumentProcessorGuid);
                jd.ExtendedProperties.Add(DocumentProcessorGuid, myDocProcessorControl);
            }
        }

        /// <summary>
        /// Provides a customized windows control a user can populate with values used in
        /// MyDocumentProcessor processing.  It is displayed in the Document Processor tab 
        /// of the Job Builder dialog.
        /// </summary>
        /// <param name="jd">current job definition</param>
        /// <returns>MyDocumentProcessor's window control</returns>
        public override System.Windows.Forms.Control GetWindowsControl(JobDefinition jd)
        {
            return (System.Windows.Forms.Control)jd.ExtendedProperties[DocumentProcessorGuid];
        }

        /// <summary>
        /// Provides a chance to persist the current job defintion. 
        /// It is called when a job is saved through the Save As dialog.  
        /// </summary>
        /// <param name="jd"></param>
        public override void OnSave(JobDefinition jd)
        {
            UserInterface myDocProcessorControls;
            // Getting your window control.
            myDocProcessorControls = (UserInterface)jd.ExtendedProperties[DocumentProcessorGuid];

            if (null != myDocProcessorControls)
            {
                // collect the config data from the windows control panel and store it 
                // in a MyDocProcConfigData object.
                ConfigData myDocProcConfigData = new ConfigData();
                myDocProcConfigData.PWUser = myDocProcessorControls.PWUser;
                myDocProcConfigData.PWPassword = myDocProcessorControls.PWPassword;
                myDocProcConfigData.MDLAppName = myDocProcessorControls.MDLAppName;
                myDocProcConfigData.AppKeyin = myDocProcessorControls.AppKeyin;
                myDocProcConfigData.PWLoginCMD = myDocProcessorControls.PWLoginCMD;
                myDocProcConfigData.MSKeyin4 = myDocProcessorControls.MSKeyin4;
                myDocProcConfigData.MSKeyin5 = myDocProcessorControls.MSKeyin5;

                // Permanently store the configuration data collected in the 
                // MyDocProcConfigData in the job definition.
                jd.SetCustomData(DocumentProcessorGuid, myDocProcConfigData.ToXmlElement());
                // jd.CheckOutFiles = true;
            }
        }

        /// <summary>
        /// Called when an array of all the steps for 
        /// MyDocumentProcessor is needed. 
        /// </summary>
        /// <returns></returns>
        public override string[] GetSteps()
        {
            return null;
        }
    }
}
