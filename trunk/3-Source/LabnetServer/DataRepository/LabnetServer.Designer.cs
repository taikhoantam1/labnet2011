﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]

namespace DataRepository
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class LabnetServerContainer : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new LabnetServerContainer object using the connection string found in the 'LabnetServerContainer' section of the application configuration file.
        /// </summary>
        public LabnetServerContainer() : base("name=LabnetServerContainer", "LabnetServerContainer")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new LabnetServerContainer object.
        /// </summary>
        public LabnetServerContainer(string connectionString) : base(connectionString, "LabnetServerContainer")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new LabnetServerContainer object.
        /// </summary>
        public LabnetServerContainer(EntityConnection connection) : base(connection, "LabnetServerContainer")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
    }
    

    #endregion
    
    
}