using System;
using Bentley.Orchestration.Extensibility;

namespace HPE.Automation.Extensions.HPEGeneralProcessor
{
    /// <summary>
    /// Summary description for SmartDispatcherExtLoader.
    /// </summary>
    public class SmartDispatcherExtLoader : IExtensionLoader
    {
        public SmartDispatcherExtLoader()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region IExtensionLoader Members

        public void Initialize()
        {//Updated 2/6/2017 MFA
            ExtensionsDirectory.RegisterExtensionObject
                (new Guid("{29604AFD-B4F1-4719-893C-ABFA011FEED0}"),
                new SmartDispatcherExtension());
        }

        #endregion
    }
}
