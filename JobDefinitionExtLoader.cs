using System;
using Bentley.Orchestration.Extensibility;

namespace HPE.Automation.Extensions.HPEGeneralProcessor
{
    /// <summary>
    /// Summary description for JobDefinitionExtLoader.
    /// </summary>
    public class JobDefinitionExtLoader : IExtensionLoader
    {
        public JobDefinitionExtLoader()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region IExtensionLoader Members

        public void Initialize()
        {//updated 2/6/2017 MFA
            ExtensionsDirectory.RegisterExtensionObject(
                new Guid("{E0B48E41-A921-4DF1-84C4-38C67A6929F7}"),
                new JobDefinitionExtension());
        }

        #endregion
    }
}
