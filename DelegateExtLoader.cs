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
