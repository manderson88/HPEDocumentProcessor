/*--------------------------------------------------------------------------------------+
//----------------------------------------------------------------------------
// DOCUMENT ID:   
// LIBRARY:       
// CREATOR:       Mark Anderson
// DATE:          02-15-2017
//
// NAME:          DelegateExtLoader.cs
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
 * $Archive: /ProjectWise/ASFramework/HPEDocumentProcessor/DelegateExtLoader.cs $
 * $Revision: 1 $
 * $Modtime: 2/15/17 7:18a $
 * $History: DelegateExtLoader.cs $
 * 
 * *****************  Version 1  *****************
 * User: Mark.anderson Date: 2/15/17    Time: 7:43a
 * Created in $/ProjectWise/ASFramework/HPEDocumentProcessor
 * A General purpose document processor.  This will  use an application
 * name and command line to load in to the msprocessor
 * 
*/
using System;
using Bentley.Orchestration.Extensibility;

namespace HPE.Automation.Extensions.HPEGeneralProcessor
{
    /// <summary>
    /// Summary description for DelegateExtLoader.
    /// </summary>
    public class DelegateExtLoader : IExtensionLoader
    {
        public DelegateExtLoader()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region IExtensionLoader Members

        public void Initialize()
        {//updated 2/6/2017 MFA
            ExtensionsDirectory.RegisterExtensionObject
                (new Guid("{B39C8FB4-DF2C-46F3-B2CC-231BDD4D2A82}"),
                new DelegateExtension());
        }

        #endregion
    }
}
