/*--------------------------------------------------------------------------------------+
//----------------------------------------------------------------------------
// DOCUMENT ID:   
// LIBRARY:       
// CREATOR:       Mark Anderson
// DATE:          02-15-2017
//
// NAME:          DelegateExtension.cs
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
 * $Archive: /ProjectWise/ASFramework/HPEDocumentProcessor/DelegateExtension.cs $
 * $Revision: 1 $
 * $Modtime: 2/15/17 7:18a $
 * $History: DelegateExtension.cs $
 * 
 * *****************  Version 1  *****************
 * User: Mark.anderson Date: 2/15/17    Time: 7:43a
 * Created in $/ProjectWise/ASFramework/HPEDocumentProcessor
 * A General purpose document processor.  This will  use an application
 * name and command line to load in to the msprocessor
 * 
*/
using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Web;
using System.Web.Mail;
using System.Collections;
using System.Collections.Generic;

using Bentley.Automation;
using Bentley.Automation.Extensions;
using Bentley.Automation.Messaging;
using Bentley.Automation.JobConfiguration;

using System.Runtime.InteropServices;

namespace HPE.Automation.Extensions.HPEGeneralProcessor
{
    /// <summary>
    /// Summary description for DelegateExtension.
    /// </summary>
    public class DelegateExtension : ASDelegateProcessorExtension
    {
        private string m_sLogFileName = "";

        /// <summary>
        /// Constructor
        /// </summary>
        public DelegateExtension()
            : base(Constants.DocumentProcessorName,
            new Guid(Constants.DocumentProcessorGuid),
            Constants.DocumentProcessorDescription)
        {
            // The smart dispatcher extension is only used by pasSmartDispatcher.exe, so only pasSmartDispatcher should load it.
            System.Diagnostics.Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            if (0 == String.Compare(currentProcess.ProcessName, "pasDelegateProcessor", true))
            {
                LogServer.Log(LogServer.LogASConstants.AutomationServicesLoggerNamespace,
                    LogServer.LogASConstants.LAYER_JobBuilder,
                    LogServer.LEVEL_Warning,
                    "HPE.Automation.Extensions.HPEGeneralProcessor.DelegateExtension successfully loaded and constructed in process with the pasDelegateProcessor.exe");
            }
            else
            {
            }
        }


        /// <summary>
        /// Called when the processor is started
        /// </summary>
        public override void OnStart()
        {
            PwApiWrapper.dmscli.aaApi_Initialize(0);

            LogServer.Log(LogServer.LogASConstants.AutomationServicesLoggerNamespace,
                LogServer.LogASConstants.LAYER_DelegateProcessor,
                LogServer.LEVEL_Info,
                "HPE.Automation.Extensions.HPEGeneralProcessor.DelegateExtension successfully loaded and initialized ProjectWise in the delegate processor.");
        }

        /// <summary>
        /// Called when the processor is shut down
        /// </summary>
        public override void OnShutdown()
        {
            // AAODSAPI.Uninitialize ();
            PwApiWrapper.dmscli.aaApi_Uninitialize();

            LogServer.Log(LogServer.LogASConstants.AutomationServicesLoggerNamespace,
                LogServer.LogASConstants.LAYER_DelegateProcessor,
                LogServer.LEVEL_Info,
                "HPE.Automation.Extensions.HPEGeneralProcessor.DelegateExtension successfully unloaded in the delegate processor.");
        }

        /// <summary>
        /// Init ODS context
        /// </summary>
        /// <param name="csm"></param>
        private void InitODS(ASContext asContext)
        {
            try
            {
                JobDefinition jd = asContext.JobDefinition;
                // Log into to ProjectWise
                Bentley.Automation.Runtime runtime = new Bentley.Automation.Runtime(jd);
                if (!runtime.ValidateLogin())
                {
                    throw new Exception("InitODS: Failed to log into ProjectWise.");
                }

                // cache ODS classes
                //AAODSAPI.LoadAllClasses (); 
            }
            catch (Exception ex)
            {
                LogServer.Log(LogServer.LogASConstants.AutomationServicesLoggerNamespace,
                    LogServer.LogASConstants.LAYER_DelegateProcessor,
                    LogServer.LEVEL_Error,
                    "InitODS: Failed to log into ProjectWise.");
                throw ex;
            }
        }

        private bool SendMail(string sTo, string sFrom, string sSubject, string sBody, string sServer)
        {
            try
            {
                //SMTPMailSender.SMTPMailSender.SendMessageToFrom(sSubject, sBody, sTo, sFrom);

                //MailMessage Message = new MailMessage();
                //Message.To = sTo;
                //Message.From = sFrom;
                //Message.Subject = sSubject;
                //Message.Body = sBody;

                //Message.BodyEncoding = System.Text.Encoding.ASCII;

                //Message.BodyFormat = System.Web.Mail.MailFormat.Html;

                //try
                //{
                //    // SmtpMail.SmtpServer = sServer;
                //    SmtpMail.SmtpServer.Insert(0, sServer);
                //    SmtpMail.Send(Message);
                //}
                //catch (System.Web.HttpException ehttp)
                //{
                //    System.Diagnostics.Debug.WriteLine("{0}", ehttp.Message);
                //    System.Diagnostics.Debug.WriteLine("Here is the full error message output");
                //    System.Diagnostics.Debug.Write("{0}", ehttp.ToString());

                //    System.Exception ex = (System.Exception)ehttp;

                //    while (ex.InnerException != null)
                //    {
                //        System.Diagnostics.Debug.WriteLine("--------------------------------");
                //        System.Diagnostics.Debug.WriteLine("The following InnerException reported: " + ex.InnerException.ToString());
                //        ex = ex.InnerException;
                //    }

                //    return false;
                //}
            }
            catch (System.Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Unknown Exception occurred {0}", e.Message);
                System.Diagnostics.Debug.WriteLine("Here is the Full Message output");
                System.Diagnostics.Debug.WriteLine("{0}", e.ToString());

                System.Exception ex = (System.Exception)e;

                while (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine("--------------------------------");
                    System.Diagnostics.Debug.WriteLine("The following InnerException reported: " + ex.InnerException.ToString());
                    ex = ex.InnerException;
                }

                return false;
            }

            return true;
        } // SendMessage

        /// <summary>
        /// Processes MyDocumentProcessor message anyway you see fit, just no modal dialog boxes.
        /// </summary>
        /// <param name="asContext">Automation Services context object</param>
        /// <param name="currentProcessingInstruction">Currently active processing instruction.</param>
        /// <returns>true - message sucessfully processed.</returns>
        private bool DoMessageProcessingOld(ASContext asContext, ProcessingInstruction currentProcessingInstruction)
        {
            ProcessingInstruction pi = new ProcessingInstruction();
            pi.ProcessorType = ASConstants.DelegateProcessorTypeID;
            pi.DocumentProcessorName = DocumentProcessorName;
            pi.DocumentProcessorGuid = DocumentProcessorGuid.ToString();
            pi.Step = Constants.Steps.ReplaceFileStep;

            try
            {
                if (asContext.JobDefinition.CheckOutFiles == true)
                {
                    string p = PwApiWrapper.Util.GetProjectWisePath();
                    PwApiWrapper.Util.AppendProjectWiseDllPathToEnvironmentPath();

                    string targetfile = System.IO.Path.GetTempPath();
                    targetfile += @System.IO.Path.GetFileName(asContext.WorkingDocumentInfo.FilePath);

                    System.IO.File.Copy(asContext.WorkingDocumentInfo.FilePath,
                        targetfile, true);

                    LogServer.Log(LogServer.LogASConstants.AutomationServicesLoggerNamespace,
                        LogServer.LogASConstants.LAYER_DelegateProcessor,
                        LogServer.LEVEL_Warning,
                        String.Format("Document copied from {0} to {1}",
                        asContext.WorkingDocumentInfo.FilePath, targetfile));

                    targetfile = asContext.WorkingDocumentInfo.FilePath;

                    PwApiWrapper.dmscli.aaApi_Initialize(0);

                    if (PwApiWrapper.dmscli.aaApi_Login(
                        (PwApiWrapper.dmawin.DataSourceType)asContext.JobDefinition.ProjectWiseDataSourceNativeType,
                        asContext.JobDefinition.ProjectWiseDataSource,
                        asContext.JobDefinition.ProjectWiseUserName,
                        asContext.JobDefinition.ProjectWisePassword,
                        null, true))
                    {
                        // Get the GUID and the local path of the document
                        Guid guid = new System.Guid(asContext.WorkingDocumentInfo.DocumentGuid);
                        String fileName = asContext.WorkingDocumentInfo.FilePath;

                        // Retrieving custom data on a job definition as an XmlElement.
                        XmlNode xmlNode =
                            (XmlNode)asContext.JobDefinition.GetCustomData(DocumentProcessorGuid);

                        if (xmlNode != null)
                        {
                            ConfigData myDocProcConfigData = new ConfigData((XmlElement)xmlNode);

                            if (false == PwApiWrapper.dmscli.aaApi_ChangeDocumentFile(
                                asContext.WorkingDocumentInfo.VaultID,
                                asContext.WorkingDocumentInfo.DocumentID,
                                targetfile))
                            {
                                LogServer.Log(LogServer.LogASConstants.AutomationServicesLoggerNamespace,
                                    LogServer.LogASConstants.LAYER_DelegateProcessor,
                                    LogServer.LEVEL_Error,
                                    String.Format("Document Replace failed: {0}, {1}, {2}, {3}, {4}",
                                    PwApiWrapper.dmsgen.aaApi_GetLastErrorId(),
                                    PwApiWrapper.dmsgen.aaApi_GetLastErrorMessage(),
                                    asContext.WorkingDocumentInfo.VaultID,
                                    asContext.WorkingDocumentInfo.DocumentID,
                                    targetfile));

                                pi.AddProcessingResults(new ProcessingResults(false, "Could not put " + asContext.WorkingDocumentInfo.FilePath + " back in PW"));
                            }
                            else
                            {
                                LogServer.Log(LogServer.LogASConstants.AutomationServicesLoggerNamespace,
                                    LogServer.LogASConstants.LAYER_DelegateProcessor,
                                    LogServer.LEVEL_Warning,
                                    "Document replace succeeded");

                                pi.AddProcessingResults(new ProcessingResults(true, "Put " + asContext.WorkingDocumentInfo.FilePath + " back in PW"));
                            }
                        }
                        else
                        {
                            LogServer.Log(LogServer.LogASConstants.AutomationServicesLoggerNamespace,
                                LogServer.LogASConstants.LAYER_DelegateProcessor,
                                LogServer.LEVEL_Error,
                                "Configuration data was null");

                            pi.AddProcessingResults(new ProcessingResults(false, "Configuration data was NULL"));
                        }
                    }
                    else
                    {
                        LogServer.Log(LogServer.LogASConstants.AutomationServicesLoggerNamespace,
                            LogServer.LogASConstants.LAYER_DelegateProcessor,
                            LogServer.LEVEL_Error,
                            String.Format("Error logging in to PW: {0}, {1}, {2}, {3}, {4}",
                            PwApiWrapper.dmsgen.aaApi_GetLastErrorId(),
                            PwApiWrapper.dmsgen.aaApi_GetLastErrorMessage(),
                            asContext.JobDefinition.ProjectWiseDataSource,
                            asContext.JobDefinition.ProjectWiseUserName,
                            asContext.JobDefinition.ProjectWisePassword));

                        pi.AddProcessingResults(new ProcessingResults(false, "Error logging in to PW"));
                    }
                } // if we're supposed to put the file back
                else
                {
                    pi.AddProcessingResults(new ProcessingResults(true, "Changes to " + asContext.WorkingDocumentInfo.FilePath + " discarded"));
                }
            }

            catch (Exception ex)
            {
                pi.AddProcessingResults(new ProcessingResults(false, ex.Message));
                LogServer.Log(LogServer.LogASConstants.AutomationServicesLoggerNamespace,
                    LogServer.LogASConstants.LAYER_DelegateProcessor,
                    LogServer.LEVEL_Error,
                    "Replace failed");
            }
            finally
            {
                asContext.ReplaceProcessingInstructions(new ProcessingInstruction[] { pi });
            }

            return true;
        }

        private int GetCurrentDocCount(JobDefinition jd)
        {
            Bentley.Automation.Runtime runtime = new Bentley.Automation.Runtime(jd);

            // System.Diagnostics.Debugger.Break();

            Bentley.Automation.JobConfiguration.ProjectWiseDocument[] failedDocs =
                new Bentley.Automation.JobConfiguration.ProjectWiseDocument[1];

            Bentley.Automation.JobConfiguration.ProjectWiseDocument[] pwDocs =
                runtime.GetExpandedInputSet(ref failedDocs);

            PwApiWrapper.dmawin.AaDocItem docItem;
            System.Guid[] docGuids = new System.Guid[2];
            String sDocGuid;

            for (int i = 0; i < pwDocs.Length; i++)
            {
                System.Diagnostics.Debug.WriteLine("Document: " + pwDocs[i].pwVaultID + ", " + pwDocs[i].pwDocID);
                docItem.lProjectId = pwDocs[i].pwVaultID;
                docItem.lDocumentId = pwDocs[i].pwDocID;

                if (PwApiWrapper.dmscli.aaApi_GetDocumentGUIDsByIds(1, ref docItem, docGuids))
                {
                    sDocGuid = docGuids[0].ToString();
                    System.Diagnostics.Debug.WriteLine(sDocGuid);
                }
            }

            return pwDocs.Length;
        }

        private bool GetActiveBatch(ConfigData cfgData, JobDefinition jd)
        {
            XmlDocument xmlDoc = new XmlDocument();

            string[] sOutputFile = jd.DataSource.GetConfigVar("CopyOutOutputFile_" + jd.ID.ToString());
            string[] sCurrDocCount = jd.DataSource.GetConfigVar("CopyOutDocumentCount_" + jd.ID.ToString());

            if (sCurrDocCount.Length == 0)
            {
                // this should be a new job because CopyOutDocumentCount_XXX is not set
                int iDocCount = GetCurrentDocCount(jd);

                jd.DataSource.DeleteConfigVar("CopyOutDocumentCount_" + jd.ID.ToString());
                jd.DataSource.SetConfigVar("CopyOutDocumentCount_" + jd.ID.ToString(), iDocCount.ToString());

                this.m_sLogFileName = String.Format("{0}\\Job_{1}.log", cfgData.MDLAppName, jd.ID.ToString());

                if (System.IO.File.Exists(m_sLogFileName))
                    System.IO.File.Delete(m_sLogFileName);

                jd.DataSource.DeleteConfigVar("CopyOutOutputFile_" + jd.ID.ToString());
                jd.DataSource.SetConfigVar("CopyOutOutputFile_" + jd.ID.ToString(), this.m_sLogFileName);

                return true;
            }
            else if (sOutputFile.Length > 0 && sCurrDocCount.Length > 0) // old job
            {
                m_sLogFileName = sOutputFile[sOutputFile.Length - 1];

                if (m_sLogFileName.Length > 0)
                {
                    return true;
                }
            }
            else
            {
                // if we get to here, some kind of problem, delete all vars and start again
                jd.DataSource.DeleteConfigVar("CopyOutDocumentCount_" + jd.ID.ToString());
                jd.DataSource.DeleteConfigVar("CopyOutOutputFile_" + jd.ID.ToString());
            }

            return false;
        }

        [DllImport("dmscli.dll", EntryPoint = "aaApi_SelectRichProjectOfFolder", CharSet = CharSet.Unicode)]
        private static extern IntPtr aaApi_SelectRichProjectOfFolder(int iProjectId);

        [DllImport("dmscli.dll", EntryPoint = "aaApi_DmsDataBufferGetCount", CharSet = CharSet.Unicode)]
        private static extern int aaApi_DmsDataBufferGetCount(IntPtr targetBuf);

        [DllImport("dmscli.dll", EntryPoint = "aaApi_DmsDataBufferFree", CharSet = CharSet.Unicode)]
        private static extern void aaApi_DmsDataBufferFree(IntPtr targetBuf);

        [DllImport("dmscli.dll", EntryPoint = "aaApi_DmsDataBufferGetStringProperty", CharSet = CharSet.Unicode)]
        private static extern IntPtr unsafe_aaApi_DmsDataBufferGetStringProperty(IntPtr dataBuf,
            PwApiWrapper.dmscli.ProjectProperty PropertyId, int lIndex);

        private static string aaApi_DmsDataBufferGetStringProperty(IntPtr dataBuf,
            PwApiWrapper.dmscli.ProjectProperty PropertyId, int lIndex)
        {
            return Marshal.PtrToStringUni(unsafe_aaApi_DmsDataBufferGetStringProperty(dataBuf,
                PropertyId, lIndex));
        }

        [DllImport("dmscli.dll", EntryPoint = "aaApi_DmsDataBufferGetNumericProperty", CharSet = CharSet.Unicode)]
        private static extern int aaApi_DmsDataBufferGetNumericProperty(IntPtr dataBuf,
            PwApiWrapper.dmscli.ProjectProperty PropertyId, int lIndex);

        //[DllImport("RichProjectInterface.dll", EntryPoint = "GetRichProjectPropertiesCount", CharSet = CharSet.Unicode)]
        //private static extern int GetRichProjectPropertiesCount(int iProjectId);

        //[DllImport("RichProjectInterface.dll", EntryPoint = "GetRichProjectProperty", CharSet = CharSet.Unicode)]
        //private static extern bool GetRichProjectProperty(int iProjectId, int iPropertyIndex,
        //    System.Text.StringBuilder sbPropertyName, int iNameSize,
        //    System.Text.StringBuilder sbPropertyValue, int iValueSize);

        //[DllImport("RichProjectInterface.dll", EntryPoint = "GetRichProjectGeneralProperties",
        //     CharSet = CharSet.Unicode)]
        //private static extern bool GetRichProjectGeneralProperties(int iProjectId,
        //    ref int iRichProjectIdP,
        //    System.Text.StringBuilder sbRichProjectName,
        //    int iNameSize,
        //    ref int iClassIdP,
        //    System.Text.StringBuilder sbClassName,
        //    int iClassNameSize,
        //    ref int iInstanceIdP);

        // have to "allow unsafe code blocks" in project properties
        //private static unsafe string ConvertIntPtrToStringUnicode(IntPtr ptr)
        //{
        //    if (ptr.ToInt32() == 0)
        //        return null;

        //    sbyte[] val = new sbyte[2048];
        //    val[0] = 0;

        //    fixed (sbyte* pDest = val)
        //    {
        //        sbyte* pSrc = (sbyte*)ptr.ToPointer();
        //        int index;
        //        for (index = 0; (pSrc[index] != '\0') || (pSrc[index + 1] != '\0'); index += 2)
        //        {
        //            pDest[index] = pSrc[index];
        //            pDest[index + 1] = pSrc[index + 1];
        //        }
        //        string ret = new String(pDest, 0, index + 1, System.Text.Encoding.Unicode);
        //        int l = ret.Length;
        //        return ret;
        //    }
        //}

        private string GetRichProjectName(int iProjectId)
        {
            string sRichProjectName = "";

            IntPtr dataBuf = aaApi_SelectRichProjectOfFolder(iProjectId);

            if (dataBuf != IntPtr.Zero)
            {
                if (1 == aaApi_DmsDataBufferGetCount(dataBuf))
                {
                    sRichProjectName = String.Format("\\{0}",
                        aaApi_DmsDataBufferGetStringProperty(dataBuf,
                            PwApiWrapper.dmscli.ProjectProperty.Name, 0));
                }

                aaApi_DmsDataBufferFree(dataBuf);
            }

            return sRichProjectName;
        }

        private int GetRichProjectId(int iProjectId)
        {
            int iRichProjectId = 0;

            IntPtr dataBuf = aaApi_SelectRichProjectOfFolder(iProjectId);

            if (dataBuf != IntPtr.Zero)
            {
                if (1 == aaApi_DmsDataBufferGetCount(dataBuf))
                {
                    iRichProjectId = aaApi_DmsDataBufferGetNumericProperty(dataBuf,
                        PwApiWrapper.dmscli.ProjectProperty.ID, 0);
                }

                aaApi_DmsDataBufferFree(dataBuf);
            }

            return iRichProjectId;
        }

        [DllImport("dmscli.dll", EntryPoint = "aaApi_GetEnvTableInfoByProject", CharSet = CharSet.Unicode)]
        public static extern bool aaApi_GetEnvTableInfoByProject
            (
            int lProjectId,         /* i  Project  id                    */
            ref int lplEnvironmentId,   /* o  Environment id (or NULL)       */
            ref int lplTableId,         /* o  Table id (or NULL)             */
            ref int lplIdColumnId       /* o  Identifier column id (or NULL) */
            );

        private string CleanUpString(string sInString)
        {
            string sOutString = (string)sInString.Clone();
            string sWorkString = "";

            sWorkString = sOutString.Replace("&", "&amp;");
            sOutString = sWorkString.Replace(">", "&gt;");
            sWorkString = sOutString.Replace("<", "&lt;");
            sOutString = sWorkString.Replace("\"", "&quot;");
            sWorkString = sOutString.Replace("#", "");
            sOutString = sWorkString.Replace("$", "");
            sWorkString = sOutString.Replace("%", "");
            sOutString = sWorkString.Replace("@", "");
            // sWorkString = sOutString.Replace("/", "");
            // sOutString = sWorkString.Replace("\\", "");
            sWorkString = sOutString.Replace("\t", "");
            sOutString = sWorkString.Replace("\n", "");

            // return sOutString.ToUpper();
            return sOutString;
        }

        private void AddSubNode
            (
            ref XmlElement parentElement,
            string sNodeName,
            string sAttributeName,
            string sAttributeValue,
            string sValue
            )
        {
            try
            {
                XmlElement childNode = parentElement.OwnerDocument.CreateElement(sNodeName);

                if (sAttributeName != null && sAttributeName.Length > 0)
                {
                    childNode.SetAttribute(sAttributeName, sAttributeValue);
                }

                childNode.InnerText = CleanUpString(sValue);

                parentElement.AppendChild(childNode);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private bool WriteAttributesToXML(int iProjectNo, int iDocumentNo,
            string sXMLFile, string sCopiedOutFile, string sDatasourceName)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.LoadXml("<Document></Document>");

                xmlDoc.DocumentElement.SetAttribute("LocalFile", sCopiedOutFile);
                xmlDoc.DocumentElement.SetAttribute("ProjectNumber", string.Format("{0}", iProjectNo));
                xmlDoc.DocumentElement.SetAttribute("DocumentNumber", string.Format("{0}", iDocumentNo));
                xmlDoc.DocumentElement.SetAttribute("DatasourceName", sDatasourceName);

                System.Text.StringBuilder vaultPathBuilder =
                    new System.Text.StringBuilder(2048);
                if (PwApiWrapper.dmscli.aaApi_GetProjectNamePath2(iProjectNo,true,'/',vaultPathBuilder,2048))
                    //aaApi_GetProjectNamePath(iProjectNo,
               // if (PwApiWrapper.dmscli.aaApi_GetProjectNamePath(iProjectNo,
                    //false, '/', vaultPathBuilder, 2048))
                {
                    xmlDoc.DocumentElement.SetAttribute("ProjectPath", vaultPathBuilder.ToString());
                }

                if (1 == PwApiWrapper.dmscli.aaApi_SelectDocument(iProjectNo,
                    iDocumentNo))
                {
                    XmlElement xmlGeneral = xmlDoc.CreateElement("General");

                    AddSubNode(ref xmlGeneral, "GeneralAttribute", "Name", "DocumentName",
                        PwApiWrapper.dmscli.aaApi_GetDocumentStringProperty(PwApiWrapper.dmscli.DocumentProperty.Name, 0));
                    AddSubNode(ref xmlGeneral, "GeneralAttribute", "Name", "Description",
                        PwApiWrapper.dmscli.aaApi_GetDocumentStringProperty(PwApiWrapper.dmscli.DocumentProperty.Desc, 0));
                    AddSubNode(ref xmlGeneral, "GeneralAttribute", "Name", "FileName",
                        PwApiWrapper.dmscli.aaApi_GetDocumentStringProperty(PwApiWrapper.dmscli.DocumentProperty.FileName, 0));
                    AddSubNode(ref xmlGeneral, "GeneralAttribute", "Name", "Version",
                        PwApiWrapper.dmscli.aaApi_GetDocumentStringProperty(PwApiWrapper.dmscli.DocumentProperty.Version, 0));
                    AddSubNode(ref xmlGeneral, "GeneralAttribute", "Name", "CreateTime",
                        PwApiWrapper.dmscli.aaApi_GetDocumentStringProperty(PwApiWrapper.dmscli.DocumentProperty.CreateTime, 0));
                    AddSubNode(ref xmlGeneral, "GeneralAttribute", "Name", "DMSDate",
                        PwApiWrapper.dmscli.aaApi_GetDocumentStringProperty(PwApiWrapper.dmscli.DocumentProperty.DMSDate, 0));
                    AddSubNode(ref xmlGeneral, "GeneralAttribute", "Name", "FileUpdateTime",
                        PwApiWrapper.dmscli.aaApi_GetDocumentStringProperty(PwApiWrapper.dmscli.DocumentProperty.FileUpdateTime, 0));
                    AddSubNode(ref xmlGeneral, "GeneralAttribute", "Name", "UpdateTime",
                        PwApiWrapper.dmscli.aaApi_GetDocumentStringProperty(PwApiWrapper.dmscli.DocumentProperty.UpdateTime, 0));

                    string sURL = String.Format("pw://{0}/Documents/{1}/{2}", sDatasourceName,
                        vaultPathBuilder.ToString(),
                        PwApiWrapper.dmscli.aaApi_GetDocumentStringProperty(PwApiWrapper.dmscli.DocumentProperty.Name, 0));

                    AddSubNode(ref xmlGeneral, "GeneralAttribute", "Name", "Address", sURL);

                    xmlDoc.DocumentElement.AppendChild(xmlGeneral);

                    // added per Hans' request 2007-12-04
                    //int iRichProjectPropertyCount = GetRichProjectPropertiesCount(iProjectNo);

                    SortedList<string, string> slProps =
                        PWWrapper.GetProjectPropertyValuesInList(iProjectNo);

                    // if (iRichProjectPropertyCount > 0)
                    if (true)
                    {
                        XmlElement xmlProject = xmlDoc.CreateElement("Project");

                        //int iRichProjectId = 0, iClassId = 0, iInstanceId = 0;
                        //System.Text.StringBuilder sbProjectName = new System.Text.StringBuilder(1024);
                        //System.Text.StringBuilder sbClassName = new System.Text.StringBuilder(1024);

                        //GetRichProjectGeneralProperties(iProjectNo, ref iRichProjectId,
                        //    sbProjectName, 1024, ref iClassId, sbClassName, 1024,
                        //    ref iInstanceId);

                        //xmlProject.SetAttribute("RichProjectId", iRichProjectId.ToString());
                        //xmlProject.SetAttribute("RichProjectName", sbProjectName.ToString());
                        //xmlProject.SetAttribute("RichProjectClassId", iClassId.ToString());
                        //xmlProject.SetAttribute("RichProjectClassName", sbClassName.ToString());
                        //xmlProject.SetAttribute("RichProjectInstanceId", iInstanceId.ToString());

                        // for (int iPropIndex = 0; iPropIndex < iRichProjectPropertyCount; iPropIndex++)
                        foreach (KeyValuePair<string, string> kvp in slProps)
                        {
                            //System.Text.StringBuilder sbPropertyName =
                            //    new System.Text.StringBuilder(512);
                            //System.Text.StringBuilder sbPropertyValue =
                            //    new System.Text.StringBuilder(512);

                            //if (GetRichProjectProperty(iProjectNo, iPropIndex, sbPropertyName,
                            //    sbPropertyName.Capacity, sbPropertyValue, sbPropertyValue.Capacity))
                            //{
                                AddSubNode(ref xmlProject, "ProjectAttribute", "Name",
                                    kvp.Key, kvp.Value);
                            //}
                        } // for each property

                        xmlDoc.DocumentElement.AppendChild(xmlProject);
                    }
                    // added above per Hans' request 2007-12-04

                    int iEnvId = 0, iTableId = 0, iColumnId = 0;

                    if (aaApi_GetEnvTableInfoByProject(iProjectNo,
                        ref iEnvId, ref iTableId, ref iColumnId))
                    {
                        int lNumberOfColumns = 0;

                        int lNumLinks = PwApiWrapper.dmscli.aaApi_SelectLinkDataByObject(iTableId,
                            PwApiWrapper.dmscli.ObjectTypeForLinkData.Document,
                            iProjectNo,
                            iDocumentNo,
                            null,
                            ref lNumberOfColumns,
                            null,
                            0);

                        for (int iRow = 0; iRow < lNumLinks; iRow++)
                        {
                            XmlElement xmlCustom = xmlDoc.CreateElement("Custom");

                            for (int iCol = 0; iCol < lNumberOfColumns; iCol++)
                            {
                                string sColumnName =
                                    PwApiWrapper.dmscli.aaApi_GetLinkDataColumnStringProperty(PwApiWrapper.dmscli.LinkDataProperty.ColumnName, iCol);

                                string sColumnValue =
                                    PwApiWrapper.dmscli.aaApi_GetLinkDataColumnValue(iRow, iCol);

                                if (sColumnName.Length > 0)
                                {
                                    AddSubNode(ref xmlCustom, "CustomAttribute", "Name",
                                        sColumnName, sColumnValue);
                                }
                            } // for each column

                            xmlDoc.DocumentElement.AppendChild(xmlCustom);
                        } // for each link
                    } // if environment selected
                } // if document selected OK

                xmlDoc.Save(sXMLFile);

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return false;
        }

        public override bool DoMessageProcessing(ASContext asContext, ProcessingInstruction currentProcessingInstruction)
        {
            ProcessingInstruction pi = new ProcessingInstruction();
            pi.ProcessorType = ASConstants.DelegateProcessorTypeID;
            pi.DocumentProcessorName = DocumentProcessorName;
            pi.DocumentProcessorGuid = DocumentProcessorGuid.ToString();
            pi.Step = Constants.Steps.ReplaceFileStep;

            try
            {
                string p = PwApiWrapper.Util.GetProjectWisePath();
                PwApiWrapper.Util.AppendProjectWiseDllPathToEnvironmentPath();

                PwApiWrapper.dmscli.aaApi_Initialize(0);

                if (PwApiWrapper.dmscli.aaApi_Login(
                    (PwApiWrapper.dmawin.DataSourceType)asContext.JobDefinition.ProjectWiseDataSourceNativeType,
                    asContext.JobDefinition.ProjectWiseDataSource,
                    asContext.JobDefinition.ProjectWiseUserName,
                    asContext.JobDefinition.ProjectWisePassword,
                    null, true))
                {
                    // Get the GUID and the local path of the document
                    Guid guid = new System.Guid(asContext.WorkingDocumentInfo.DocumentGuid);
                    String fileName = asContext.WorkingDocumentInfo.FilePath;

                    // Retrieving custom data on a job definition as an XmlElement.
                    XmlNode xmlNode = (XmlNode)asContext.JobDefinition.GetCustomData(DocumentProcessorGuid);

                    if (xmlNode != null)
                    {
                        ConfigData myDocProcConfigData = new ConfigData((XmlElement)xmlNode);

                        String sJustFileName = System.IO.Path.GetFileName(fileName);

                        string sOutputPath =
                            myDocProcConfigData.MDLAppName +
                            GetRichProjectName(asContext.WorkingDocumentInfo.VaultID);

                        if (!System.IO.Directory.Exists(sOutputPath))
                            System.IO.Directory.CreateDirectory(sOutputPath);

                        String sTargetFileName = System.IO.Path.Combine(sOutputPath, sJustFileName);

                        if (System.IO.File.Exists(sTargetFileName))
                        {
                            try
                            {
                                System.IO.File.Delete(sTargetFileName);
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine(ex.Message);
                            }
                        }

                        System.Text.StringBuilder filePathBuilder =
                            new System.Text.StringBuilder(1024);


                        // this copies out references, too
                        bool bCopiedOK = PwApiWrapper.dmscli.aaApi_CopyOutDocument(
                            asContext.WorkingDocumentInfo.VaultID,
                            asContext.WorkingDocumentInfo.DocumentID,
                            sOutputPath,
                            filePathBuilder, 1024);

                        if (!bCopiedOK)
                        {
                            System.Diagnostics.Debug.WriteLine(PwApiWrapper.dmsgen.aaApi_GetLastErrorMessage());
                            // System.Diagnostics.Debugger.Break();
                            System.IO.File.Copy(fileName, sTargetFileName, true);
                            if (filePathBuilder.Length == 0)
                                filePathBuilder.Append(sTargetFileName);

                            bCopiedOK = true;
                        }

                        if (bCopiedOK)
                        {
                            WriteAttributesToXML(asContext.WorkingDocumentInfo.VaultID,
                                asContext.WorkingDocumentInfo.DocumentID,
                                filePathBuilder.ToString() + ".xml",
                                filePathBuilder.ToString(),
                                asContext.JobDefinition.ProjectWiseDataSource);

                            System.Diagnostics.ProcessStartInfo startInfo =
                                new System.Diagnostics.ProcessStartInfo(myDocProcConfigData.AppKeyin);

                            //LogServer.Log(ASConstants.AutomationServicesLoggerNamespace,
                            //    ASConstants.LAYER_DelegateProcessor,
                            //    LogServer.LEVEL_Warning,
                            //    String.Format("Copied out file to: {0}",
                            //    filePathBuilder.ToString()));

                            if (GetActiveBatch(myDocProcConfigData, asContext.JobDefinition))
                            {
                                System.IO.StreamWriter sw =
                                    new System.IO.StreamWriter(this.m_sLogFileName, true);
                                sw.WriteLine(filePathBuilder.ToString());
                                sw.Close();
                            }

                            startInfo.CreateNoWindow = true;
                            startInfo.UseShellExecute = false;
                            startInfo.Arguments = String.Format("\"{0}\" {1}",
                                filePathBuilder.ToString(),
                                myDocProcConfigData.PWLoginCMD);

                            try
                            {
                                //								LogServer.Log (ASConstants.AutomationServicesLoggerNamespace, 
                                //									ASConstants.LAYER_DelegateProcessor,
                                //									LogServer.LEVEL_Warning,
                                //									String.Format("Attempting to execute {0} {1}",
                                //									startInfo.FileName, startInfo.Arguments));

                                System.Diagnostics.Process.Start(startInfo);

                                pi.AddProcessingResults(new ProcessingResults(true,
                                    "File processed OK"));
                            }
                            catch (Exception startEx)
                            {
                                //LogServer.Log(ASConstants.AutomationServicesLoggerNamespace,
                                //    ASConstants.LAYER_DelegateProcessor,
                                //    LogServer.LEVEL_Error,
                                //    startEx.Message + "\n" + startEx.StackTrace);

                                System.Diagnostics.Debug.WriteLine(startEx.Message);

                                pi.AddProcessingResults(new ProcessingResults(true,
                                    "Error executing command"));
                            }
                        }
                    }
                    else
                    {
                        //LogServer.Log(ASConstants.AutomationServicesLoggerNamespace,
                        //    ASConstants.LAYER_DelegateProcessor,
                        //    LogServer.LEVEL_Error,
                        //    "Couldn't read Job Configuration");

                        pi.AddProcessingResults(new ProcessingResults(false, "Couldn't read job configuration"));
                    }

                    // PwApiWrapper.dmscli.aaApi_Logout(asContext.JobDefinition.ProjectWiseDataSource);
                }
                else
                {
                    //LogServer.Log(ASConstants.AutomationServicesLoggerNamespace,
                    //    ASConstants.LAYER_DelegateProcessor,
                    //    LogServer.LEVEL_Error,
                    //    String.Format("Error logging in to PW: {0}, {1}, {2}, {3}",
                    //    PwApiWrapper.dmsgen.aaApi_GetLastErrorId(),
                    //    PwApiWrapper.dmsgen.aaApi_GetLastErrorMessage(),
                    //    asContext.JobDefinition.ProjectWiseDataSource,
                    //    asContext.JobDefinition.ProjectWiseUserName));

                    pi.AddProcessingResults(new ProcessingResults(false, "Error logging in to PW"));
                }
            }
            catch (Exception ex)
            {
                //pi.AddProcessingResults(new ProcessingResults(false, ex.Message));
                //LogServer.Log(ASConstants.AutomationServicesLoggerNamespace,
                //    ASConstants.LAYER_DelegateProcessor,
                //    LogServer.LEVEL_Error,
                //    String.Format("Error: {0}", ex.StackTrace));

                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                asContext.ReplaceProcessingInstructions(new ProcessingInstruction[] { pi });
            }

            return true;
        }

        public override void OnSendToNextQueue(ASContext asContext)
        {
            JobDefinition jd = asContext.JobDefinition;

            string[] sCurrDocCount = jd.DataSource.GetConfigVar("CopyOutDocumentCount_" + jd.ID.ToString());

            if (sCurrDocCount.Length > 0)
            {
                int iDocCount = Int32.Parse(sCurrDocCount[0]);

                if (iDocCount > 1)
                {
                    iDocCount--;
                    jd.DataSource.DeleteConfigVar("CopyOutDocumentCount_" + jd.ID.ToString());
                    jd.DataSource.SetConfigVar("CopyOutDocumentCount_" + jd.ID.ToString(), iDocCount.ToString());
                }
                else
                {
                    // clean up all the variables
                    jd.DataSource.DeleteConfigVar("CopyOutDocumentCount_" + jd.ID.ToString());
                    jd.DataSource.DeleteConfigVar("CopyOutOutputFile_" + jd.ID.ToString());
                }
            }
            else
            {
                // clean up all the variables
                jd.DataSource.DeleteConfigVar("CopyOutDocumentCount_" + jd.ID.ToString());
                jd.DataSource.DeleteConfigVar("CopyOutOutputFile_" + jd.ID.ToString());
            }

            base.OnSendToNextQueue(asContext);
        }

    }
#if false
    public class DelegateExtension2 : ASDelegateProcessorExtension
    {
        private string m_sLogFileName = "";

        /// <summary>
        /// Constructor
        /// </summary>
        public DelegateExtension2()
            : base(Constants.DocumentProcessorName,
            new Guid(Constants.DocumentProcessorGuid),
            Constants.DocumentProcessorDescription)
        {
            // The smart dispatcher extension is only used by pasSmartDispatcher.exe, so only pasSmartDispatcher should load it.
            System.Diagnostics.Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            if (0 == String.Compare(currentProcess.ProcessName, "pasDelegateProcessor", true))
            {
                //LogServer.Log(ASConstants.AutomationServicesLoggerNamespace,
                //    ASConstants.LAYER_JobBuilder,
                //    LogServer.LEVEL_Warning,
                //    "*********  HPE.Automation.Extensions.XMCopyOutAndExec.DelegateExtension loaded  by pasDelegateProcessor *************");
            }
            else
            {
            }
        }


        /// <summary>
        /// Called when the processor is started
        /// </summary>
        public override void OnStart()
        {
            PwApiWrapper.dmscli.aaApi_Initialize(0);

            //			LogServer.Log (ASConstants.AutomationServicesLoggerNamespace, 
            //				ASConstants.LAYER_DelegateProcessor,
            //				LogServer.LEVEL_Info,
            //				"HPE.Automation.Extensions.XMCopyOutAndExec.DelegateExtension successfully loaded and initialized ProjectWise in the delegate processor.");
        }

        /// <summary>
        /// Called when the processor is shut down
        /// </summary>
        public override void OnShutdown()
        {
            // AAODSAPI.Uninitialize ();
            //			PwApiWrapper.dmscli.aaApi_Uninitialize ();
            //
            //			LogServer.Log (ASConstants.AutomationServicesLoggerNamespace, 
            //				ASConstants.LAYER_DelegateProcessor,
            //				LogServer.LEVEL_Info,
            //				"HPE.Automation.Extensions.XMCopyOutAndExec.DelegateExtension successfully unloaded in the delegate processor.");
        }

        /// <summary>
        /// Init ODS context
        /// </summary>
        /// <param name="csm"></param>
        private void InitODS(ASContext asContext)
        {
            try
            {
                JobDefinition jd = asContext.JobDefinition;
                // Log into to ProjectWise
                Bentley.Automation.Runtime runtime = new Bentley.Automation.Runtime(jd);
                if (!runtime.ValidateLogin())
                {
                    //throw new Exception("InitODS: Failed to log into ProjectWise.");
                }

                // cache ODS classes
                //AAODSAPI.LoadAllClasses (); 
            }
            catch (Exception ex)
            {
                //LogServer.Log(ASConstants.AutomationServicesLoggerNamespace,
                //    ASConstants.LAYER_DelegateProcessor,
                //    LogServer.LEVEL_Error,
                //    "InitODS: Failed to log into ProjectWise.");
                throw ex;
            }
        }

        private bool SendMail(string sTo, string sFrom, string sSubject, string sBody, string sServer)
        {
            try
            {
                MailMessage Message = new MailMessage();
                Message.To = sTo;
                Message.From = sFrom;
                Message.Subject = sSubject;
                Message.Body = sBody;

                Message.BodyEncoding = System.Text.Encoding.ASCII;

                Message.BodyFormat = System.Web.Mail.MailFormat.Html;

                try
                {
                    // SmtpMail.SmtpServer = sServer;
                    SmtpMail.SmtpServer.Insert(0, sServer);
                    SmtpMail.Send(Message);
                }
                catch (System.Web.HttpException ehttp)
                {
                    System.Diagnostics.Debug.WriteLine("{0}", ehttp.Message);
                    System.Diagnostics.Debug.WriteLine("Here is the full error message output");
                    System.Diagnostics.Debug.Write("{0}", ehttp.ToString());

                    System.Exception ex = (System.Exception)ehttp;

                    while (ex.InnerException != null)
                    {
                        System.Diagnostics.Debug.WriteLine("--------------------------------");
                        System.Diagnostics.Debug.WriteLine("The following InnerException reported: " + ex.InnerException.ToString());
                        ex = ex.InnerException;
                    }

                    return false;
                }
            }
            catch (System.Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Unknown Exception occurred {0}", e.Message);
                System.Diagnostics.Debug.WriteLine("Here is the Full Message output");
                System.Diagnostics.Debug.WriteLine("{0}", e.ToString());

                System.Exception ex = (System.Exception)e;

                while (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine("--------------------------------");
                    System.Diagnostics.Debug.WriteLine("The following InnerException reported: " + ex.InnerException.ToString());
                    ex = ex.InnerException;
                }

                return false;
            }

            return true;
        } // SendMessage

        private int GetCurrentDocCount(JobDefinition jd)
        {
            Bentley.Automation.Runtime runtime = new Bentley.Automation.Runtime(jd);

            // System.Diagnostics.Debugger.Break();

            Bentley.Automation.JobConfiguration.ProjectWiseDocument[] failedDocs =
                new Bentley.Automation.JobConfiguration.ProjectWiseDocument[1];

            Bentley.Automation.JobConfiguration.ProjectWiseDocument[] pwDocs =
                runtime.GetExpandedInputSet(ref failedDocs);

            PwApiWrapper.dmawin.AaDocItem docItem;
            System.Guid[] docGuids = new System.Guid[2];
            String sDocGuid;

            for (int i = 0; i < pwDocs.Length; i++)
            {
                System.Diagnostics.Debug.WriteLine("Document: " + pwDocs[i].pwVaultID + ", " + pwDocs[i].pwDocID);
                docItem.lProjectId = pwDocs[i].pwVaultID;
                docItem.lDocumentId = pwDocs[i].pwDocID;

                if (PwApiWrapper.dmscli.aaApi_GetDocumentGUIDsByIds(1, ref docItem, docGuids))
                {
                    sDocGuid = docGuids[0].ToString();
                    System.Diagnostics.Debug.WriteLine(sDocGuid);
                }
            }

            return pwDocs.Length;
        }

        private bool GetActiveBatch(ConfigData cfgData, JobDefinition jd)
        {
            XmlDocument xmlDoc = new XmlDocument();

            string[] sOutputFile = jd.DataSource.GetConfigVar("CopyOutOutputFile_" + jd.ID.ToString());
            string[] sCurrDocCount = jd.DataSource.GetConfigVar("CopyOutDocumentCount_" + jd.ID.ToString());

            if (sCurrDocCount.Length == 0)
            {
                // this should be a new job because CopyOutDocumentCount_XXX is not set
                int iDocCount = GetCurrentDocCount(jd);

                jd.DataSource.DeleteConfigVar("CopyOutDocumentCount_" + jd.ID.ToString());
                jd.DataSource.SetConfigVar("CopyOutDocumentCount_" + jd.ID.ToString(), iDocCount.ToString());

                this.m_sLogFileName = String.Format("{0}\\Job_{1}.log", cfgData.MSKeyin1, jd.ID.ToString());

                if (System.IO.File.Exists(m_sLogFileName))
                    System.IO.File.Delete(m_sLogFileName);

                jd.DataSource.DeleteConfigVar("CopyOutOutputFile_" + jd.ID.ToString());
                jd.DataSource.SetConfigVar("CopyOutOutputFile_" + jd.ID.ToString(), this.m_sLogFileName);

                return true;
            }
            else if (sOutputFile.Length > 0 && sCurrDocCount.Length > 0) // old job
            {
                m_sLogFileName = sOutputFile[sOutputFile.Length - 1];

                if (m_sLogFileName.Length > 0)
                {
                    return true;
                }
            }
            else
            {
                // if we get to here, some kind of problem, delete all vars and start again
                jd.DataSource.DeleteConfigVar("CopyOutDocumentCount_" + jd.ID.ToString());
                jd.DataSource.DeleteConfigVar("CopyOutOutputFile_" + jd.ID.ToString());
            }

            return false;
        }

        [DllImport("dmscli.dll", EntryPoint = "aaApi_SelectRichProjectOfFolder", CharSet = CharSet.Unicode)]
        private static extern IntPtr aaApi_SelectRichProjectOfFolder(int iProjectId);

        [DllImport("dmscli.dll", EntryPoint = "aaApi_DmsDataBufferGetCount", CharSet = CharSet.Unicode)]
        private static extern int aaApi_DmsDataBufferGetCount(IntPtr targetBuf);

        [DllImport("dmscli.dll", EntryPoint = "aaApi_DmsDataBufferFree", CharSet = CharSet.Unicode)]
        private static extern void aaApi_DmsDataBufferFree(IntPtr targetBuf);

        [DllImport("dmscli.dll", EntryPoint = "aaApi_DmsDataBufferGetStringProperty", CharSet = CharSet.Unicode)]
        private static extern IntPtr unsafe_aaApi_DmsDataBufferGetStringProperty(IntPtr dataBuf,
            PwApiWrapper.dmscli.ProjectProperty PropertyId, int lIndex);

        private static string aaApi_DmsDataBufferGetStringProperty(IntPtr dataBuf,
            PwApiWrapper.dmscli.ProjectProperty PropertyId, int lIndex)
        {
            return ConvertIntPtrToStringUnicode(unsafe_aaApi_DmsDataBufferGetStringProperty(dataBuf,
                PropertyId, lIndex));
        }

        [DllImport("dmscli.dll", EntryPoint = "aaApi_DmsDataBufferGetNumericProperty", CharSet = CharSet.Unicode)]
        private static extern int aaApi_DmsDataBufferGetNumericProperty(IntPtr dataBuf,
            PwApiWrapper.dmscli.ProjectProperty PropertyId, int lIndex);

        [DllImport("RichProjectInterface.dll", EntryPoint = "GetRichProjectPropertiesCount", CharSet = CharSet.Unicode)]
        private static extern int GetRichProjectPropertiesCount(int iProjectId);

        [DllImport("RichProjectInterface.dll", EntryPoint = "GetRichProjectProperty", CharSet = CharSet.Unicode)]
        private static extern bool GetRichProjectProperty(int iProjectId, int iPropertyIndex,
            System.Text.StringBuilder sbPropertyName, int iNameSize,
            System.Text.StringBuilder sbPropertyValue, int iValueSize);

        [DllImport("RichProjectInterface.dll", EntryPoint = "GetRichProjectGeneralProperties",
             CharSet = CharSet.Unicode)]
        private static extern bool GetRichProjectGeneralProperties(int iProjectId,
            ref int iRichProjectIdP,
            System.Text.StringBuilder sbRichProjectName,
            int iNameSize,
            ref int iClassIdP,
            System.Text.StringBuilder sbClassName,
            int iClassNameSize,
            ref int iInstanceIdP);

        // have to "allow unsafe code blocks" in project properties
        private static unsafe string ConvertIntPtrToStringUnicode(IntPtr ptr)
        {
            if (ptr.ToInt32() == 0)
                return null;

            sbyte[] val = new sbyte[2048];
            val[0] = 0;

            fixed (sbyte* pDest = val)
            {
                sbyte* pSrc = (sbyte*)ptr.ToPointer();
                int index;
                for (index = 0; (pSrc[index] != '\0') || (pSrc[index + 1] != '\0'); index += 2)
                {
                    pDest[index] = pSrc[index];
                    pDest[index + 1] = pSrc[index + 1];
                }
                string ret = new String(pDest, 0, index + 1, System.Text.Encoding.Unicode);
                int l = ret.Length;
                return ret;
            }
        }

        private string GetRichProjectName(int iProjectId)
        {
            string sRichProjectName = "";

            IntPtr dataBuf = aaApi_SelectRichProjectOfFolder(iProjectId);

            if (dataBuf != IntPtr.Zero)
            {
                if (1 == aaApi_DmsDataBufferGetCount(dataBuf))
                {
                    sRichProjectName = String.Format("\\{0}",
                        aaApi_DmsDataBufferGetStringProperty(dataBuf,
                            PwApiWrapper.dmscli.ProjectProperty.Name, 0));
                }

                aaApi_DmsDataBufferFree(dataBuf);
            }

            return sRichProjectName;
        }

        private int GetRichProjectId(int iProjectId)
        {
            int iRichProjectId = 0;

            IntPtr dataBuf = aaApi_SelectRichProjectOfFolder(iProjectId);

            if (dataBuf != IntPtr.Zero)
            {
                if (1 == aaApi_DmsDataBufferGetCount(dataBuf))
                {
                    iRichProjectId = aaApi_DmsDataBufferGetNumericProperty(dataBuf,
                        PwApiWrapper.dmscli.ProjectProperty.ID, 0);
                }

                aaApi_DmsDataBufferFree(dataBuf);
            }

            return iRichProjectId;
        }

        [DllImport("dmscli.dll", EntryPoint = "aaApi_GetEnvTableInfoByProject", CharSet = CharSet.Unicode)]
        public static extern bool aaApi_GetEnvTableInfoByProject
            (
            int lProjectId,         /* i  Project  id                    */
            ref int lplEnvironmentId,   /* o  Environment id (or NULL)       */
            ref int lplTableId,         /* o  Table id (or NULL)             */
            ref int lplIdColumnId       /* o  Identifier column id (or NULL) */
            );

        private string CleanUpString(string sInString)
        {
            string sOutString = (string)sInString.Clone();
            string sWorkString = "";

            sWorkString = sOutString.Replace("&", "&amp;");
            sOutString = sWorkString.Replace(">", "&gt;");
            sWorkString = sOutString.Replace("<", "&lt;");
            sOutString = sWorkString.Replace("\"", "&quot;");
            sWorkString = sOutString.Replace("#", "");
            sOutString = sWorkString.Replace("$", "");
            sWorkString = sOutString.Replace("%", "");
            sOutString = sWorkString.Replace("@", "");
            // sWorkString = sOutString.Replace("/", "");
            // sOutString = sWorkString.Replace("\\", "");
            sWorkString = sOutString.Replace("\t", "");
            sOutString = sWorkString.Replace("\n", "");

            // return sOutString.ToUpper();
            return sOutString;
        }

        private void AddSubNode
            (
            ref XmlElement parentElement,
            string sNodeName,
            string sAttributeName,
            string sAttributeValue,
            string sValue
            )
        {
            try
            {
                XmlElement childNode = parentElement.OwnerDocument.CreateElement(sNodeName);

                if (sAttributeName != null && sAttributeName.Length > 0)
                {
                    childNode.SetAttribute(sAttributeName, sAttributeValue);
                }

                childNode.InnerText = CleanUpString(sValue);

                parentElement.AppendChild(childNode);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private bool WriteAttributesToXML(int iProjectNo, int iDocumentNo,
            string sXMLFile, string sCopiedOutFile, string sDatasourceName)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.LoadXml("<Document></Document>");

                xmlDoc.DocumentElement.SetAttribute("LocalFile", sCopiedOutFile);
                xmlDoc.DocumentElement.SetAttribute("ProjectNumber", string.Format("{0}", iProjectNo));
                xmlDoc.DocumentElement.SetAttribute("DocumentNumber", string.Format("{0}", iDocumentNo));
                xmlDoc.DocumentElement.SetAttribute("DatasourceName", sDatasourceName);

                System.Text.StringBuilder vaultPathBuilder =
                    new System.Text.StringBuilder(2048);

                if (PwApiWrapper.dmscli.aaApi_GetProjectNamePath(iProjectNo,
                    false, '/', vaultPathBuilder, 2048))
                {
                    xmlDoc.DocumentElement.SetAttribute("ProjectPath", vaultPathBuilder.ToString());
                }

                if (1 == PwApiWrapper.dmscli.aaApi_SelectDocument(iProjectNo,
                    iDocumentNo))
                {
                    XmlElement xmlGeneral = xmlDoc.CreateElement("General");

                    AddSubNode(ref xmlGeneral, "GeneralAttribute", "Name", "DocumentName",
                        PwApiWrapper.dmscli.aaApi_GetDocumentStringProperty(PwApiWrapper.dmscli.DocumentProperty.Name, 0));
                    AddSubNode(ref xmlGeneral, "GeneralAttribute", "Name", "Description",
                        PwApiWrapper.dmscli.aaApi_GetDocumentStringProperty(PwApiWrapper.dmscli.DocumentProperty.Desc, 0));
                    AddSubNode(ref xmlGeneral, "GeneralAttribute", "Name", "FileName",
                        PwApiWrapper.dmscli.aaApi_GetDocumentStringProperty(PwApiWrapper.dmscli.DocumentProperty.FileName, 0));
                    AddSubNode(ref xmlGeneral, "GeneralAttribute", "Name", "Version",
                        PwApiWrapper.dmscli.aaApi_GetDocumentStringProperty(PwApiWrapper.dmscli.DocumentProperty.Version, 0));
                    AddSubNode(ref xmlGeneral, "GeneralAttribute", "Name", "CreateTime",
                        PwApiWrapper.dmscli.aaApi_GetDocumentStringProperty(PwApiWrapper.dmscli.DocumentProperty.CreateTime, 0));
                    AddSubNode(ref xmlGeneral, "GeneralAttribute", "Name", "DMSDate",
                        PwApiWrapper.dmscli.aaApi_GetDocumentStringProperty(PwApiWrapper.dmscli.DocumentProperty.DMSDate, 0));
                    AddSubNode(ref xmlGeneral, "GeneralAttribute", "Name", "FileUpdateTime",
                        PwApiWrapper.dmscli.aaApi_GetDocumentStringProperty(PwApiWrapper.dmscli.DocumentProperty.FileUpdateTime, 0));
                    AddSubNode(ref xmlGeneral, "GeneralAttribute", "Name", "UpdateTime",
                        PwApiWrapper.dmscli.aaApi_GetDocumentStringProperty(PwApiWrapper.dmscli.DocumentProperty.UpdateTime, 0));

                    string sURL = String.Format("pw://{0}/Documents/{1}/{2}", sDatasourceName,
                        vaultPathBuilder.ToString(),
                        PwApiWrapper.dmscli.aaApi_GetDocumentStringProperty(PwApiWrapper.dmscli.DocumentProperty.Name, 0));

                    AddSubNode(ref xmlGeneral, "GeneralAttribute", "Name", "Address", sURL);

                    xmlDoc.DocumentElement.AppendChild(xmlGeneral);

                    // added per Hans' request 2007-12-04
                    int iRichProjectPropertyCount = GetRichProjectPropertiesCount(iProjectNo);

                    // if (iRichProjectPropertyCount > 0)
                    if (true)
                    {
                        XmlElement xmlProject = xmlDoc.CreateElement("Project");

                        int iRichProjectId = 0, iClassId = 0, iInstanceId = 0;
                        System.Text.StringBuilder sbProjectName = new System.Text.StringBuilder(1024);
                        System.Text.StringBuilder sbClassName = new System.Text.StringBuilder(1024);

                        GetRichProjectGeneralProperties(iProjectNo, ref iRichProjectId,
                            sbProjectName, 1024, ref iClassId, sbClassName, 1024,
                            ref iInstanceId);

                        xmlProject.SetAttribute("RichProjectId", iRichProjectId.ToString());
                        xmlProject.SetAttribute("RichProjectName", sbProjectName.ToString());
                        xmlProject.SetAttribute("RichProjectClassId", iClassId.ToString());
                        xmlProject.SetAttribute("RichProjectClassName", sbClassName.ToString());
                        xmlProject.SetAttribute("RichProjectInstanceId", iInstanceId.ToString());

                        for (int iPropIndex = 0; iPropIndex < iRichProjectPropertyCount; iPropIndex++)
                        {
                            System.Text.StringBuilder sbPropertyName =
                                new System.Text.StringBuilder(512);
                            System.Text.StringBuilder sbPropertyValue =
                                new System.Text.StringBuilder(512);

                            if (GetRichProjectProperty(iProjectNo, iPropIndex, sbPropertyName,
                                sbPropertyName.Capacity, sbPropertyValue, sbPropertyValue.Capacity))
                            {
                                AddSubNode(ref xmlProject, "ProjectAttribute", "Name",
                                    sbPropertyName.ToString(), sbPropertyValue.ToString());
                            }
                        } // for each property

                        xmlDoc.DocumentElement.AppendChild(xmlProject);
                    }
                    // added above per Hans' request 2007-12-04

                    int iEnvId = 0, iTableId = 0, iColumnId = 0;

                    if (aaApi_GetEnvTableInfoByProject(iProjectNo,
                        ref iEnvId, ref iTableId, ref iColumnId))
                    {
                        int lNumberOfColumns = 0;

                        int lNumLinks = PwApiWrapper.dmscli.aaApi_SelectLinkDataByObject(iTableId,
                            PwApiWrapper.dmscli.ObjectTypeForLinkData.Document,
                            iProjectNo,
                            iDocumentNo,
                            null,
                            ref lNumberOfColumns,
                            null,
                            0);

                        for (int iRow = 0; iRow < lNumLinks; iRow++)
                        {
                            XmlElement xmlCustom = xmlDoc.CreateElement("Custom");

                            for (int iCol = 0; iCol < lNumberOfColumns; iCol++)
                            {
                                string sColumnName =
                                    PwApiWrapper.dmscli.aaApi_GetLinkDataColumnStringProperty(PwApiWrapper.dmscli.LinkDataProperty.ColumnName, iCol);

                                string sColumnValue =
                                    PwApiWrapper.dmscli.aaApi_GetLinkDataColumnValue(iRow, iCol);

                                if (sColumnName.Length > 0)
                                {
                                    AddSubNode(ref xmlCustom, "CustomAttribute", "Name",
                                        sColumnName, sColumnValue);
                                }
                            } // for each column

                            xmlDoc.DocumentElement.AppendChild(xmlCustom);
                        } // for each link
                    } // if environment selected
                } // if document selected OK

                xmlDoc.Save(sXMLFile);

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return false;
        }

        /// <summary>
        /// Processes MyDocumentProcessor message anyway you see fit, just no modal dialog boxes.
        /// </summary>
        /// <param name="asContext">Automation Services context object</param>
        /// <param name="currentProcessingInstruction">Currently active processing instruction.</param>
        /// <returns>true - message sucessfully processed.</returns>
        public override bool DoMessageProcessing(ASContext asContext, ProcessingInstruction currentProcessingInstruction)
        {
            ProcessingInstruction pi = new ProcessingInstruction();
            pi.ProcessorType = ASConstants.DelegateProcessorTypeID;
            pi.DocumentProcessorName = DocumentProcessorName;
            pi.DocumentProcessorGuid = DocumentProcessorGuid.ToString();
            pi.Step = Constants.Steps.IndexingWidgetAttributes;

            try
            {
                string p = PwApiWrapper.Util.GetProjectWisePath();
                PwApiWrapper.Util.AppendProjectWiseDllPathToEnvironmentPath();

                PwApiWrapper.dmscli.aaApi_Initialize(0);

                if (PwApiWrapper.dmscli.aaApi_Login(
                    (PwApiWrapper.dmawin.DataSourceType)asContext.JobDefinition.ProjectWiseDataSourceNativeType,
                    asContext.JobDefinition.ProjectWiseDataSource,
                    asContext.JobDefinition.ProjectWiseUserName,
                    asContext.JobDefinition.ProjectWisePassword,
                    null, true))
                {
                    // Get the GUID and the local path of the document
                    Guid guid = new System.Guid(asContext.WorkingDocumentInfo.DocumentGuid);
                    String fileName = asContext.WorkingDocumentInfo.FilePath;

                    // Retrieving custom data on a job definition as an XmlElement.
                    XmlNode xmlNode = (XmlNode)asContext.JobDefinition.GetCustomData(DocumentProcessorGuid);

                    if (xmlNode != null)
                    {
                        ConfigData myDocProcConfigData = new ConfigData((XmlElement)xmlNode);

                        String sJustFileName = System.IO.Path.GetFileName(fileName);

                        string sOutputPath =
                            myDocProcConfigData.MSKeyin1 +
                            GetRichProjectName(asContext.WorkingDocumentInfo.VaultID);

                        if (!System.IO.Directory.Exists(sOutputPath))
                            System.IO.Directory.CreateDirectory(sOutputPath);

                        String sTargetFileName = System.IO.Path.Combine(sOutputPath, sJustFileName);

                        if (System.IO.File.Exists(sTargetFileName))
                        {
                            try
                            {
                                System.IO.File.Delete(sTargetFileName);
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine(ex.Message);
                            }
                        }

                        System.Text.StringBuilder filePathBuilder =
                            new System.Text.StringBuilder(1024);


                        // this copies out references, too
                        bool bCopiedOK = PwApiWrapper.dmscli.aaApi_CopyOutDocument(
                            asContext.WorkingDocumentInfo.VaultID,
                            asContext.WorkingDocumentInfo.DocumentID,
                            sOutputPath,
                            filePathBuilder, 1024);

                        if (!bCopiedOK)
                        {
                            System.Diagnostics.Debug.WriteLine(PwApiWrapper.dmsgen.aaApi_GetLastErrorMessage());
                            // System.Diagnostics.Debugger.Break();
                            System.IO.File.Copy(fileName, sTargetFileName, true);
                            if (filePathBuilder.Length == 0)
                                filePathBuilder.Append(sTargetFileName);

                            bCopiedOK = true;
                        }

                        if (bCopiedOK)
                        {
                            WriteAttributesToXML(asContext.WorkingDocumentInfo.VaultID,
                                asContext.WorkingDocumentInfo.DocumentID,
                                filePathBuilder.ToString() + ".xml",
                                filePathBuilder.ToString(),
                                asContext.JobDefinition.ProjectWiseDataSource);

                            System.Diagnostics.ProcessStartInfo startInfo =
                                new System.Diagnostics.ProcessStartInfo(myDocProcConfigData.MSKeyin2);

                            LogServer.Log(LogServer.LogASConstants.AutomationServicesLoggerNamespace,
                                LogServer.LogASConstants.LAYER_DelegateProcessor,
                                LogServer.LEVEL_Warning,
                                String.Format("Copied out file to: {0}",
                                filePathBuilder.ToString()));

                            if (GetActiveBatch(myDocProcConfigData, asContext.JobDefinition))
                            {
                                System.IO.StreamWriter sw =
                                    new System.IO.StreamWriter(this.m_sLogFileName, true);
                                sw.WriteLine(filePathBuilder.ToString());
                                sw.Close();
                            }

                            startInfo.CreateNoWindow = true;
                            startInfo.UseShellExecute = false;
                            startInfo.Arguments = String.Format("\"{0}\" {1}",
                                filePathBuilder.ToString(),
                                myDocProcConfigData.MSKeyin3);

                            try
                            {
                                LogServer.Log(LogServer.LogASConstants.AutomationServicesLoggerNamespace,
                                    LogServer.LogASConstants.LAYER_DelegateProcessor,
                                    LogServer.LEVEL_Warning,
                                    String.Format("Attempting to execute {0} {1}",
                                    startInfo.FileName, startInfo.Arguments));

                                System.Diagnostics.Process.Start(startInfo);

                                pi.AddProcessingResults(new ProcessingResults(true,
                                    "File processed OK"));
                            }
                            catch (Exception startEx)
                            {
                                LogServer.Log(LogServer.LogASConstants.AutomationServicesLoggerNamespace,
                                    LogServer.LogASConstants.LAYER_DelegateProcessor,
                                    LogServer.LEVEL_Error,
                                    startEx.Message + "\n" + startEx.StackTrace);

                                pi.AddProcessingResults(new ProcessingResults(true,
                                    "Error executing command"));
                            }
                        }
                    }
                    else
                    {
                        LogServer.Log(LogServer.LogASConstants.AutomationServicesLoggerNamespace,
                            LogServer.LogASConstants.LAYER_DelegateProcessor,
                            LogServer.LEVEL_Error,
                            "Couldn't read Job Configuration");

                        pi.AddProcessingResults(new ProcessingResults(false, "Couldn't read job configuration"));
                    }

                    // PwApiWrapper.dmscli.aaApi_Logout(asContext.JobDefinition.ProjectWiseDataSource);
                }
                else
                {
                    LogServer.Log(LogServer.LogASConstants.AutomationServicesLoggerNamespace,
                        LogServer.LogASConstants.LAYER_DelegateProcessor,
                        LogServer.LEVEL_Error,
                        String.Format("Error logging in to PW: {0}, {1}, {2}, {3}",
                        PwApiWrapper.dmsgen.aaApi_GetLastErrorId(),
                        PwApiWrapper.dmsgen.aaApi_GetLastErrorMessage(),
                        asContext.JobDefinition.ProjectWiseDataSource,
                        asContext.JobDefinition.ProjectWiseUserName));

                    pi.AddProcessingResults(new ProcessingResults(false, "Error logging in to PW"));
                }
            }
            catch (Exception ex)
            {
                pi.AddProcessingResults(new ProcessingResults(false, ex.Message));
                LogServer.Log(LogServer.LogASConstants.AutomationServicesLoggerNamespace,
                    LogServer.LogASConstants.LAYER_DelegateProcessor,
                    LogServer.LEVEL_Error,
                    String.Format("Error: {0}\n{1}", ex.Message, ex.StackTrace));
            }
            finally
            {
                asContext.ReplaceProcessingInstructions(new ProcessingInstruction[] { pi });
            }

            return true;
        }

        public override void OnSendToNextQueue(ASContext asContext)
        {
            JobDefinition jd = asContext.JobDefinition;

            string[] sCurrDocCount = jd.DataSource.GetConfigVar("CopyOutDocumentCount_" + jd.ID.ToString());

            if (sCurrDocCount.Length > 0)
            {
                int iDocCount = Int32.Parse(sCurrDocCount[0]);

                if (iDocCount > 1)
                {
                    iDocCount--;
                    jd.DataSource.DeleteConfigVar("CopyOutDocumentCount_" + jd.ID.ToString());
                    jd.DataSource.SetConfigVar("CopyOutDocumentCount_" + jd.ID.ToString(), iDocCount.ToString());
                }
                else
                {
                    // clean up all the variables
                    jd.DataSource.DeleteConfigVar("CopyOutDocumentCount_" + jd.ID.ToString());
                    jd.DataSource.DeleteConfigVar("CopyOutOutputFile_" + jd.ID.ToString());
                }
            }
            else
            {
                // clean up all the variables
                jd.DataSource.DeleteConfigVar("CopyOutDocumentCount_" + jd.ID.ToString());
                jd.DataSource.DeleteConfigVar("CopyOutOutputFile_" + jd.ID.ToString());
            }

            base.OnSendToNextQueue(asContext);
        }
    }
#endif
}