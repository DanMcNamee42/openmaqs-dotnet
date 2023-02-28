﻿//--------------------------------------------------
// <copyright file="AppiumIosUnitTests.cs" company="OpenMAQS">
//  Copyright 2023 OpenMAQS, All rights Reserved
// </copyright>
// <summary>Test class for ios related functions</summary>
//--------------------------------------------------
using OpenMaqs.BaseAppiumTest;
using OpenMaqs.Utilities.Helper;
using OpenMaqs.Utilities.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System.Collections.Generic;
using System.IO;

namespace AppiumUnitTests
{
    /// <summary>
    /// iOS related Appium tests
    /// </summary>
    [TestClass]
    public class AppiumIosUnitTests : BaseAppiumTest
    {
        /// <summary>
        /// Tests the creation of the Appium iOS Driver
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Appium)]
        public void AppiumIOSDriverTest()
        {
            Assert.IsNotNull(this.TestObject.AppiumDriver);
        }

        /// <summary>
        /// Tests lazy element with Appium iOS Driver
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Appium)]
        public void AppiumIOSDriverLazyTest()
        {
            Assert.IsNotNull(this.TestObject.AppiumDriver);
            this.AppiumDriver.Navigate().GoToUrl(Config.GetValueForSection(ConfigSection.AppiumMaqs, "WebSiteBase"));
            LazyMobileElement lazy = new LazyMobileElement(this.TestObject, By.XPath("//button[@class=\"navbar-toggle\"]"), "Nav toggle");

            Assert.IsTrue(lazy.Enabled, "Expect enabled");
            Assert.IsTrue(lazy.Displayed, "Expect displayed");
            Assert.IsTrue(lazy.ExistsNow, "Expect exists now");
            lazy.Click();
        }

        /// <summary>
        /// Assert function fail path
        /// </summary>
        [TestMethod]
        [TestCategory(TestCategories.Appium)]
        public void AssertFuncFailPath()
        {
            Assert.IsNotNull(this.TestObject.AppiumDriver);
            this.AppiumDriver.Navigate().GoToUrl(Config.GetValueForSection(ConfigSection.AppiumMaqs, "WebSiteBase"));

            Log = new FileLogger(string.Empty, "AssertFuncFailPath.txt", MessageType.GENERIC, true);
            AppiumSoftAssert appiumSoftAssert = new AppiumSoftAssert(TestObject);
            string logLocation = ((IFileLogger)Log).FilePath;
            string screenShotLocation = $"{logLocation.Substring(0, logLocation.LastIndexOf('.'))} assertName (1).Png";

            bool isFalse = appiumSoftAssert.Assert(() => Assert.IsTrue(false), "assertName");
            Assert.IsTrue(File.Exists(screenShotLocation), "Fail to find screenshot");
            File.Delete(screenShotLocation);
            File.Delete(logLocation);

            Assert.IsFalse(isFalse);
        }

        /// <summary>
        /// Sets capabilities for testing the iOS Driver creation
        /// </summary>
        /// <returns>iOS instance of the Appium Driver</returns>
        protected override AppiumDriver GetMobileDevice()
        {
            AppiumOptions options = new AppiumOptions
            {
                DeviceName = "iPhone 14",
                PlatformName = "iOS",
                PlatformVersion = "16",
                //BrowserName = "Safari"
            };

            var sauceOptions = AppiumConfig.GetCapabilitiesAsObjects();

            // Use Appium 1.22 for running iOS tests
            (sauceOptions["bstack:options"] as Dictionary<string, object>)["appiumVersion"] = "1.22.0";
            options.SetMobileOptions(sauceOptions);

            return AppiumDriverFactory.GetIOSDriver(AppiumConfig.GetMobileHubUrl(), options, AppiumConfig.GetMobileCommandTimeout());
        }
    }
}
