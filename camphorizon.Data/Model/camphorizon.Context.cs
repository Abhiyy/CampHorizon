﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace camphorizon.Data.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class camphorizonEntities : DbContext
    {
        public camphorizonEntities()
            : base("name=camphorizonEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ApplicationSetting> ApplicationSettings { get; set; }
        public DbSet<CancellationTerm> CancellationTerms { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<ImagesCatalogue> ImagesCatalogues { get; set; }
        public DbSet<PackageCancellationTermsMapping> PackageCancellationTermsMappings { get; set; }
        public DbSet<PackageExclusion> PackageExclusions { get; set; }
        public DbSet<PackageFacilityMapping> PackageFacilityMappings { get; set; }
        public DbSet<PackageInclusion> PackageInclusions { get; set; }
        public DbSet<PackageItinerary> PackageItineraries { get; set; }
        public DbSet<Package> Packages { get; set; }
    }
}