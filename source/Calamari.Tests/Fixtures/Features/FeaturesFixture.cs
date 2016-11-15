﻿using System.IO;
using Calamari.Deployment;
using Calamari.Integration.FileSystem;
using Calamari.Shared.Convention;
using Calamari.Tests.Helpers;
using NUnit.Framework;
using Octostache;

namespace Calamari.Tests.Fixtures.Features
{
    [TestFixture]
    [Category(TestEnvironment.ScriptingSupport.FSharp)]
    public class FeaturesFixture : CalamariFixture
    {
        [Test]
        public void ShouldErrorIfNoFeatureSpecified()
        {
            var output = InProcessInvoke(InProcessCalamari()
                .Action("run-feature")
                .Argument("script", GetFixtureResouce("Scripts", "Parameters.fsx"))
                .Argument("scriptParameters", "\"Para meter0\" Parameter1")); ;

            output.AssertFailure();
            output.AssertErrorOutput("No feature was specified. Please pass a value for the `--feature` option.");
        }


        [Test]
        public void DitIr()
        {
            var variablesFile = Path.GetTempFileName();

            var variables = new VariableDictionary();
            variables.Set(SpecialVariables.Action.Script.Path, GetFixtureResouce("../","PowerShell","Scripts", "Hello.ps1"));
            variables.Set(SpecialVariables.Action.Script.Parameters, "Cake");
            variables.Save(variablesFile);
            
            using (new TemporaryFile(variablesFile))
            {
                var output = InProcessInvoke(InProcessCalamari()
                    .Action("run-feature")
                    .Argument("feature", CommonFeatures.RunScript)
                    .Argument("variables", variablesFile));
                    //.Argument("script", GetFixtureResouce("Scripts", "Parameters.fsx"))
                    //.Argument("scriptParameters", "\"Para meter0\" Parameter1"));

                output.AssertFailure();
                output.AssertErrorOutput("No feature was specified. Please pass a value for the `--feature` option.");
            }
        }
    }
}