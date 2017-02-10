using System;

namespace HPE.Automation.Extensions.HPEGeneralProcessor
{
    /// <summary>
    /// Summary description for Constants.
    /// </summary>
    public abstract class Constants
    {
        public static readonly string DocumentProcessorName =
                "HPEGeneralProcessor";
        public static readonly string DocumentProcessorDescription =
                "Document Processor";
        //generated 2/6/2017 MFA
        public static readonly string DocumentProcessorGuid =
                "{04F801C5-2A91-4ADE-BB0B-0BB2A0A1054C}";

        public abstract class Steps
        {
            public static readonly string IndexingWidgetAttributes
                = "Replace Document File";
            public static readonly string ScanDGNFile
                = "Process MicroStation Keyins";
        }
    }
}
