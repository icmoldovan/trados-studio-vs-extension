﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;
using TemplatesVSIX.MsBuild;
using TemplatesVSIX.Trados.Patches;

namespace TemplatesVSIXUnitTest
{
    [TestClass]
    public class HintPathPatchTest
    {
        [TestMethod]
        public void UpdateHintPath_RelativePathGiven_PathChanged()
        {
            var service = new HintPathPatch("16");

            var reference = Substitute.For<IReference>();
            reference.HintPath.Returns(@"$(ProgramFiles)\SDL\SDL Trados Studio\Studio15\Sdl.Desktop.IntegrationApi.dll");

            var project = Substitute.For<IProject>();
            project.References.Returns(new List<IReference>() { reference });

            service.PatchProject(project);

            Assert.AreEqual(@"$(ProgramFiles)\SDL\SDL Trados Studio\Studio16\Sdl.Desktop.IntegrationApi.dll", reference.HintPath);
        }

        [TestMethod]
        public void UpdateHintPath_MacroPathGiven_PathChanged()
        {
            var service = new HintPathPatch("16");

            var reference = Substitute.For<IReference>();
            reference.HintPath.Returns(@"$(ProgramFiles)\SDL\SDL Trados Studio\Studio15\Sdl.Desktop.IntegrationApi.dll");

            var project = Substitute.For<IProject>();
            project.References.Returns(new List<IReference>() { reference });

            service.PatchProject(project);

            Assert.AreEqual(@"$(ProgramFiles)\SDL\SDL Trados Studio\Studio16\Sdl.Desktop.IntegrationApi.dll", reference.HintPath);
        }

        [TestMethod]
        public void UpdateHintPath_OldTwoDigitVersionNumber_PathChanged()
        {
            var service = new HintPathPatch("16");

            var reference = Substitute.For<IReference>();
            reference.HintPath.Returns(@"$(ProgramFiles)\SDL\SDL Trados Studio\Studio20\Sdl.Desktop.IntegrationApi.dll");

            var project = Substitute.For<IProject>();
            project.References.Returns(new List<IReference>() { reference });

            service.PatchProject(project);

            Assert.AreEqual(@"$(ProgramFiles)\SDL\SDL Trados Studio\Studio16\Sdl.Desktop.IntegrationApi.dll", reference.HintPath);
        }

        [TestMethod]
        public void UpdateHintPath_OldOneDigitVersionNumber_PathChanged()
        {
            var service = new HintPathPatch("16");

            var reference = Substitute.For<IReference>();
            reference.HintPath.Returns(@"$(ProgramFiles)\SDL\SDL Trados Studio\Studio2\Sdl.Desktop.IntegrationApi.dll");

            var project = Substitute.For<IProject>();
            project.References.Returns(new List<IReference>() { reference });

            service.PatchProject(project);

            Assert.AreEqual(@"$(ProgramFiles)\SDL\SDL Trados Studio\Studio16\Sdl.Desktop.IntegrationApi.dll", reference.HintPath);
        }

        [TestMethod]
        public void UpdateHintPath_ProjectIsNull_ExceptionIsNotThrown()
        {
            var service = new HintPathPatch("16");

            service.PatchProject(null);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void UpdateHintPath_ReferebcesIsNull_ExceptionIsNotThrown()
        {
            var service = new HintPathPatch("15");
            var project = Substitute.For<IProject>();

            service.PatchProject(null);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void UpdateHintPath_ReferebcesEmptyList_ExceptionIsNotThrown()
        {
            var service = new HintPathPatch("16");
            var project = Substitute.For<IProject>();
            project.References.Returns(new List<IReference>());

            service.PatchProject(null);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void UpdateHintPath_MultipleReferences_VersionChangedForAll()
        {
            var service = new HintPathPatch("16");

            var reference1 = Substitute.For<IReference>();
            reference1.HintPath.Returns(@"$(ProgramFiles)\SDL\SDL Trados Studio\Studio20\Sdl.Desktop.IntegrationApi.dll");

            var reference2 = Substitute.For<IReference>();
            reference2.HintPath.Returns(@"$(ProgramFiles)\SDL\SDL Trados Studio\Studio20\Sdl.Desktop.dll");

            var project = Substitute.For<IProject>();
            project.References.Returns(new List<IReference>() { reference1, reference2 });

            service.PatchProject(project);

            Assert.AreEqual(@"$(ProgramFiles)\SDL\SDL Trados Studio\Studio16\Sdl.Desktop.IntegrationApi.dll", reference1.HintPath, "Hint path for reference 1 not valid");
            Assert.AreEqual(@"$(ProgramFiles)\SDL\SDL Trados Studio\Studio16\Sdl.Desktop.dll", reference2.HintPath, "Hint path for reference 2 not valid");
        }
    }
}
