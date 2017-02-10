using System;
using System.Collections.Generic;
using System.Text;

namespace HPE.Automation.Extensions.HPEGeneralProcessor
{
    class LogServer
    {
        public const int LEVEL_Warning = 1;
        public const int LEVEL_Info = 2;
        public const int LEVEL_Error = 3;
        public const int LEVEL_Debug = 4;

        public enum LogASConstants
        {
            LAYER_JobBuilder = 1,
            LAYER_DelegateProcessor = 2,
            LAYER_SmartDispatcher = 3,
            AutomationServicesLoggerNamespace = 55
        }

        public static void Log(LogASConstants asConst,
            LogASConstants asLayer,
            int iLevel,
            string sMessage)
        {
            string sNamespace = string.Empty;

            switch (asLayer)
            {
                case LogASConstants.LAYER_DelegateProcessor:
                    sNamespace = BSI.Automation.ASLog.Namespaces.DelegateProcessor;
                    break;
                case LogASConstants.LAYER_JobBuilder:
                    sNamespace = BSI.Automation.ASLog.Namespaces.JobBuilder;
                    break;
                case LogASConstants.LAYER_SmartDispatcher:
                    sNamespace = BSI.Automation.ASLog.Namespaces.SmartDispatcher;
                    break;
                default:
                    break;
            }

            switch (iLevel)
            {
                case LEVEL_Warning:
                    BSI.Orchestration.Utility.Log.Ref.Warning(sNamespace,
                        sMessage);
                    break;
                case LEVEL_Debug:
                    BSI.Orchestration.Utility.Log.Ref.Debug(sNamespace,
                        sMessage);
                    break;
                case LEVEL_Error:
                    BSI.Orchestration.Utility.Log.Ref.Error(sNamespace,
                        sMessage);
                    break;
                case LEVEL_Info:
                    BSI.Orchestration.Utility.Log.Ref.Info(sNamespace,
                        sMessage);
                    break;
                default:
                    BSI.Orchestration.Utility.Log.Ref.Debug(sNamespace,
                        sMessage);
                    break;
            }
        }
    }
}
