﻿//--------------------------------------------------
// <copyright file="BaseWebServiceTestSteps.cs" company="Cognizant">
//  Copyright 2022 Cognizant, All rights Reserved
// </copyright>
// <summary>Base teststeps code for tests using web services</summary>
//--------------------------------------------------
using Maqs.BaseWebServiceTest;
using TechTalk.SpecFlow;
using MaqsWeb = Maqs.BaseWebServiceTest.BaseWebServiceTest;

namespace Maqs.SpecFlow.TestSteps
{
    /// <summary>
    /// Base for web service TestSteps classes
    /// </summary>
    [Binding, Scope(Tag = "MAQS_WebService")]
    public class BaseWebServiceTestSteps : ExtendableTestSteps<IWebServiceTestObject, MaqsWeb>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseWebServiceTestSteps" /> class
        /// </summary>
        /// <param name="context">The scenario context.</param>
        public BaseWebServiceTestSteps(ScenarioContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets the web service driver from the test object
        /// </summary>
        protected WebServiceDriver WebServiceDriver
        {
            get { return this.TestObject.WebServiceDriver; }
        }
    }
}
