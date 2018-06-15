﻿//--------------------------------------------------
// <copyright file="MongoTestObject.cs" company="Magenic">
//  Copyright 2018 Magenic, All rights Reserved
// </copyright>
// <summary>Holds MongoDB context data</summary>
//--------------------------------------------------
using Magenic.MaqsFramework.BaseTest;
using Magenic.MaqsFramework.Utilities.Logging;

namespace Magenic.MaqsFramework.BaseMongoTest
{
    /// <summary>
    /// Mongo test context data
    /// </summary>
    /// <typeparam name="T">The Mongo collection type</typeparam>
    public class MongoTestObject<T> : BaseTestObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoTestObject{T}" /> class
        /// </summary>
        /// <param name="connectionString">Client connection string</param>
        /// <param name="databaseString">Database connection string</param>
        /// <param name="collectionString">Mongo collection string</param>
        /// <param name="logger">The test's logger</param>
        /// <param name="softAssert">The test's soft assert</param>
        /// <param name="fullyQualifiedTestName">The test's fully qualified test name</param>
        public MongoTestObject(string connectionString, string databaseString, string collectionString, Logger logger, SoftAssert softAssert, string fullyQualifiedTestName) : base(logger, softAssert, fullyQualifiedTestName)
        {
            this.DriversStore.Add(typeof(MongoDriverStore<T>).FullName, new MongoDriverStore<T>(connectionString, databaseString, collectionString, this));
        }

        /// <summary>
        /// Gets the Mongo driver
        /// </summary>
        public MongoDriverStore<T> MongoDBDriver
        {
            get
            {
                return this.DriversStore[typeof(MongoDriverStore<T>).FullName] as MongoDriverStore<T>;
            }
        }

        /// <summary>
        /// Gets the Mongo wrapper
        /// </summary>
        public MongoDBDriver<T> MongoDBWrapper
        {
            get
            {
                return this.MongoDBDriver.Get();
            }
        }

        /// <summary>
        /// Override the Mongo wrapper settings
        /// </summary>
        /// <param name="connectionString">Client connection string</param>
        /// <param name="databaseString">Database connection string</param>
        /// <param name="collectionString">Mongo collection string</param>
        public void OverrideMongoDBWrapper(string connectionString, string databaseString, string collectionString)
        {
            this.DriversStore.Remove(typeof(MongoDriverStore<T>).FullName);
            this.DriversStore.Add(typeof(MongoDriverStore<T>).FullName, new MongoDriverStore<T>(connectionString, databaseString, collectionString, this));
        }

        /// <summary>
        /// Override the Mongo wrapper settings
        /// </summary>
        /// <param name="wrapper">New Mongo wrapper</param>
        public void OverrideMongoDBWrapper(MongoDBDriver<T> wrapper)
        {
            this.MongoDBDriver.OverrideWrapper(wrapper);
        }
    }
}