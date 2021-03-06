using System;
using System.Security.Principal;
using Calamari.Deployment;
using Calamari.Integration.Scripting;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Calamari.Tests.Fixtures
{
    public class RequiresAdminAttribute : TestAttribute, ITestAction
    {
        public void BeforeTest(ITest testDetails)
        {
            var isAdmin = (new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator);
            if (!isAdmin)
            {
                Assert.Ignore("Requires Admin Rights");
            }
        }

        public void AfterTest(ITest testDetails)
        {
        }

        public ActionTargets Targets { get; set; }
    }

    public class RequiresWindowsServer2012OrAboveAttribute : TestAttribute, ITestAction
    {
        public void BeforeTest(ITest testDetails)
        {
            if (!Calamari.Util.CrossPlatform.IsWindows())
            {
                Assert.Ignore("Requires Windows");
            }

#if NET40
            var decimalVersion = Environment.OSVersion.Version.Major + Environment.OSVersion.Version.Minor * 0.1;
            if(decimalVersion < 6.2)
            {
                Assert.Ignore("Requires Windows Server 2012 or above");
            }
#else
            // .NET Core will be new enough.
#endif


        }

        public void AfterTest(ITest testDetails)
        {
        }

        public ActionTargets Targets { get; set; }
    }
}