/*--------------------------------------------------------------------------------------+
//----------------------------------------------------------------------------
// DOCUMENT ID:   
// LIBRARY:       
// CREATOR:       Mark Anderson
// DATE:          02-15-2017
//
// NAME:          LogServer.cs
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
 * $Archive: /ProjectWise/ASFramework/HPEDocumentProcessor/LogServer.cs $
 * $Revision: 1 $
 * $Modtime: 2/15/17 7:18a $
 * $History: LogServer.cs $
 * 
 * *****************  Version 1  *****************
 * User: Mark.anderson Date: 2/15/17    Time: 7:44a
 * Created in $/ProjectWise/ASFramework/HPEDocumentProcessor
 * A General purpose document processor.  This will  use an application
 * name and command line to load in to the msprocessor
 * 
*/
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
