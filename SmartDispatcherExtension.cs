using System;
using System.Xml;
using Bentley.Automation;
using Bentley.Automation.Extensions;
using Bentley.Automation.Messaging;
using Bentley.Orchestration.MSProcessor;
using Bentley.Orchestration.Extensibility;
using PwApiWrapper;

namespace HPE.Automation.Extensions.HPEGeneralProcessor
{
    /// <summary>
    /// Summary description for SmartDispatcherExtension.
    /// </summary>
    public class SmartDispatcherExtension : ASSmartDispatcherExtension
    {
        public SmartDispatcherExtension()
            : base(Constants.DocumentProcessorName,
                new Guid(Constants.DocumentProcessorGuid),
                Constants.DocumentProcessorDescription)
        {
            // The smart dispatcher extension is only used by pasSmartDispatcher.exe, so only pasSmartDispatcher should load it.
            System.Diagnostics.Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            if (0 == String.Compare(currentProcess.ProcessName, "pasSmartDispatcher", true))
            {
                LogServer.Log(LogServer.LogASConstants.AutomationServicesLoggerNamespace,
                    LogServer.LogASConstants.LAYER_JobBuilder,
                    LogServer.LEVEL_Info,
                    "***** HPE.Automation.Extensions.HPEGeneralProcessor.SmartDispatcherExtension loaded *********");
            }
            else
            {
            }
        }

        private bool isCADFile(string filePath)
        {
            if (filePath.ToLower().EndsWith("dgn") || filePath.ToLower().EndsWith("dwg"))
                return true;
            return false;
        }

        private ProcessingInstruction GenerateFirstCADIndexInstruction(ASContext asContext)
        {
            MSProcessorInstruction pi = new MSProcessorInstruction();
            pi.DocumentProcessorName = DocumentProcessorName;
            pi.DocumentProcessorGuid = DocumentProcessorGuid.ToString();
            pi.Step = Constants.Steps.KeyInStep;
            pi.MicroStationMessage = GenerateMicroStationInstructions(asContext);
            return pi;
        }

        private void ParseKeyinString(string sKeyin, MicroStationMessage msm)
        {
            string[] split = sKeyin.Split(new Char[] { ';' });

            foreach (string s in split)
            {
                if (s.Trim() != "")
                    msm.AddKeyin(s);
            }
        }
        /// <summary>
        /// this will load the mdl application and login to PW (Optional)
        /// then queue the command to execute on the "current" file.
        /// </summary>
        /// <param name="asContext"></param>
        /// <returns></returns>
        private MicroStationMessage GenerateMicroStationInstructions(ASContext asContext)
        {
            MicroStationMessage msm = new MicroStationMessage();
            msm.AddKeyin("rd=" + asContext.WorkingDocumentInfo.FilePath);

            // Retrieving custom data on a job definition as an XmlElement.
            XmlNode xmlNode = (XmlNode)asContext.JobDefinition.GetCustomData(DocumentProcessorGuid);

            if (xmlNode != null)
            {
                ConfigData myDocProcConfigData = new ConfigData((XmlElement)xmlNode);

                // by convention use this as then name of your mdl application
                if (0 < myDocProcConfigData.MDLAppName.Length)
                {
                    string sMDLPath;/* = PwApiWrapper.Util.GetProjectWisePath();
                    sMDLPath += @"bin\";*/
                    sMDLPath = myDocProcConfigData.MDLAppName;

                    string loadKeyin = "mdl load \"" + sMDLPath + "\"";

                    msm.AddKeyin(loadKeyin);
                }

                // by convention use this to log in to PW from MS
                if (0 < myDocProcConfigData.MSKeyin2.Length)
                {
                    if (null != myDocProcConfigData.PWUser &&
                        null != myDocProcConfigData.PWPassword)
                    {
                        string sLoginCmd = myDocProcConfigData.MSKeyin2 + " " +
                            asContext.JobDefinition.ProjectWiseDataSource + "," +
                            myDocProcConfigData.PWUser + "," +
                            myDocProcConfigData.PWPassword + "," +
                            asContext.WorkingDocumentInfo.VaultID.ToString() + "," +
                            asContext.WorkingDocumentInfo.DocumentID.ToString();
                        msm.AddKeyin(sLoginCmd);
                    }
                    else
                    {
                        ParseKeyinString(myDocProcConfigData.MSKeyin2, msm);
                    }
                }

                if (0 < myDocProcConfigData.MSKeyin3.Length)
                    ParseKeyinString(myDocProcConfigData.MSKeyin3, msm);

                if (0 < myDocProcConfigData.MSKeyin4.Length)
                    ParseKeyinString(myDocProcConfigData.MSKeyin4, msm);

                if (0 < myDocProcConfigData.MSKeyin5.Length)
                    ParseKeyinString(myDocProcConfigData.MSKeyin5, msm);
                //turn off the save design feature
                //msm.AddKeyin("SAVE DESIGN");
            }

            return msm;
        }

        private ProcessingInstruction GenerateFileReplaceInstruction(ProcessingInstruction previousPi)
        {
            DelegateInstruction di = new DelegateInstruction();
            LogServer.Log(LogServer.LogASConstants.AutomationServicesLoggerNamespace,
                LogServer.LogASConstants.LAYER_SmartDispatcher, LogServer.LEVEL_Info, "Creating Replace Instruction");
            di.DocumentProcessorName = DocumentProcessorName;
            di.DocumentProcessorGuid = DocumentProcessorGuid.ToString();
            di.Step = Constants.Steps.ReplaceFileStep;
            return di;
        }

        private ProcessingInstruction GenerateExitInstruction(ProcessingInstruction previousPi)
        {
            ExitInstruction ei = new ExitInstruction(true, "complete");
            LogServer.Log(LogServer.LogASConstants.AutomationServicesLoggerNamespace,
                LogServer.LogASConstants.LAYER_SmartDispatcher, LogServer.LEVEL_Info, "Creating Exit Instruction");
            ei.DocumentProcessorName = DocumentProcessorName;
            ei.DocumentProcessorGuid = DocumentProcessorGuid.ToString();
            return ei;
        }

        private ProcessingInstruction GenerateCADProcessingInstructions(ASContext asContext, ProcessingInstruction previousPi, bool isFirstRequestForProcessingInstructions)
        {
            ProcessingInstruction nextPi = null;

            if (isFirstRequestForProcessingInstructions)
            {
                // Step 1 - attribute data in xml file to delegate processor for indexing
                nextPi = GenerateFirstCADIndexInstruction(asContext);
               // nextPi = GenerateFileReplaceInstruction(previousPi);
            }
            else if (Constants.Steps.KeyInStep == previousPi.Step)
            {
                // Step 2 - replace the file
                nextPi = GenerateFileReplaceInstruction(previousPi);
            }
            else if (Constants.Steps.ReplaceFileStep == previousPi.Step)
            {
                // Step 3 - to exit for reporting and cleanup 
                nextPi = GenerateExitInstruction(previousPi);
            }
            else
            {
                // Don't understand the previous step, do nothing.
                nextPi = null;
            }
            return nextPi;
        }

        /// <summary>
        /// Gives the smart dispatcher the sequence of steps needed to index widgets.
        /// </summary>
        /// <param name="asContext">Automation Services context object</param>
        /// <param name="previousPi">The previous step</param>
        /// <param name="isFirstRequestForProcessingInstructions">First step?</param>
        /// <returns>The next processing instruction to execute</returns>
        public override ProcessingInstruction GenerateProcessingInstructions
            (
            ASContext asContext,
            ProcessingInstruction previousPi,
            bool isFirstRequestForProcessingInstructions
            )
        {
            ProcessingInstruction nextPi = GenerateCADProcessingInstructions(asContext, previousPi,
                    isFirstRequestForProcessingInstructions);

            return nextPi;
        }
    }
}
