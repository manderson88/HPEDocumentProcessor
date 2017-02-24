/*--------------------------------------------------------------------------------------+
//----------------------------------------------------------------------------
// DOCUMENT ID:   
// LIBRARY:       
// CREATOR:       Mark Anderson
// DATE:          02-15-2017
//
// NAME:          Constants.cs
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
 * $Archive: /ProjectWise/ASFramework/HPEDocumentProcessor/Constants.cs $
 * $Revision: 1 $
 * $Modtime: 2/15/17 7:18a $
 * $History: Constants.cs $
 * 
 * *****************  Version 1  *****************
 * User: Mark.anderson Date: 2/15/17    Time: 7:43a
 * Created in $/ProjectWise/ASFramework/HPEDocumentProcessor
 * A General purpose document processor.  This will  use an application
 * name and command line to load in to the msprocessor
 * 
*/
using System;

namespace HPE.Automation.Extensions.HPEGeneralProcessor
{
    /// <summary>
    /// The constants for the  class. Most important on here is the 
    /// document processor GUID.  This GUID has no {} on the ends.
    /// </summary>
    public abstract class Constants
    {
        public static readonly string DocumentProcessorName =
                "HPEGeneralProcessor";
        public static readonly string DocumentProcessorDescription =
                "Document Processor";
        //generated 2/6/2017 MFA
        public static readonly string DocumentProcessorGuid =
                "04F801C5-2A91-4ADE-BB0B-0BB2A0A1054C";

        public abstract class Steps
        {
            public static readonly string ReplaceFileStep
                = "Replace Document File";
            public static readonly string KeyInStep
                = "Process MicroStation Keyins";
        }
    }
}
