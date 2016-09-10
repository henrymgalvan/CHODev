namespace cho500.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Barangays",
                c => new
                    {
                        BarangayID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.BarangayID);
            
            CreateTable(
                "dbo.BloodTypes",
                c => new
                    {
                        BloodTypeID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.BloodTypeID);
            
            CreateTable(
                "dbo.ChildBirthFollowUpVisits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AgeInWeeks = c.Int(nullable: false),
                        DateOfFollowup = c.DateTime(),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Height = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Physician = c.String(),
                        Diagnosis = c.String(),
                        Notes = c.String(),
                        PersonID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChildHealthRecords", t => t.PersonID, cascadeDelete: true)
                .Index(t => t.PersonID);
            
            CreateTable(
                "dbo.ChildHealthRecords",
                c => new
                    {
                        PersonID = c.Int(nullable: false),
                        Months = c.Int(nullable: false),
                        Weeks = c.Int(nullable: false),
                        Days = c.Int(nullable: false),
                        TypeOfDelivery = c.String(),
                        BirthWeightInPounds = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BirthLength = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HeadCircumference = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ChestCircumference = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AbdominalCircumference = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BloodTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PersonID)
                .ForeignKey("dbo.BloodTypes", t => t.BloodTypeID, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.PersonID)
                .Index(t => t.PersonID)
                .Index(t => t.BloodTypeID);
            
            CreateTable(
                "dbo.ChildImmunizationRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        First = c.DateTime(),
                        Second = c.DateTime(),
                        Third = c.DateTime(),
                        Booster1 = c.DateTime(),
                        Booster2 = c.DateTime(),
                        Booster3 = c.DateTime(),
                        Reaction = c.String(),
                        PersonID = c.Int(nullable: false),
                        VaccineID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChildHealthRecords", t => t.PersonID, cascadeDelete: true)
                .ForeignKey("dbo.Vaccines", t => t.VaccineID, cascadeDelete: true)
                .Index(t => t.PersonID)
                .Index(t => t.VaccineID);
            
            CreateTable(
                "dbo.Vaccines",
                c => new
                    {
                        VaccineID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.VaccineID);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        MiddleName = c.String(maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        DateOfBirth = c.DateTime(),
                        Sex = c.Int(nullable: false),
                        CivilStatus = c.Int(nullable: false),
                        Address = c.String(),
                        HouseholdNo = c.Int(nullable: false),
                        ContactNumber = c.String(),
                        Encoder = c.String(),
                        DateCreated = c.DateTime(),
                        Notes = c.String(),
                        BarangayID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PersonID)
                .ForeignKey("dbo.Barangays", t => t.BarangayID, cascadeDelete: true)
                .Index(t => t.BarangayID);
            
            CreateTable(
                "dbo.Consultations",
                c => new
                    {
                        ConsultationID = c.Int(nullable: false, identity: true),
                        AdmittedBy = c.String(),
                        DateOfConsult = c.DateTime(nullable: false),
                        PreviousConsultDate = c.DateTime(),
                        PreviousConsult = c.Int(nullable: false),
                        ChiefComplaint = c.String(),
                        Age = c.Int(nullable: false),
                        BPFirstReading = c.String(),
                        BPSecondReading = c.String(),
                        BPAverage = c.String(),
                        RaisedBP = c.Boolean(nullable: false),
                        PulseRate = c.Int(nullable: false),
                        RR = c.Int(nullable: false),
                        Temperature = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WeightInKgms = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HeightInCm = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WaistCircumferenceInCm = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CentralAdiposity = c.Boolean(nullable: false),
                        BMI = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BMIStatus = c.Int(nullable: false),
                        History = c.String(),
                        PhysicalExam = c.String(),
                        DiagnosisLabResult = c.String(),
                        ManagementTreatment = c.String(),
                        Recommendation = c.String(),
                        Pharmacy = c.String(),
                        Physician = c.String(),
                        PersonID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ConsultationID)
                .ForeignKey("dbo.People", t => t.PersonID, cascadeDelete: true)
                .Index(t => t.PersonID);
            
            CreateTable(
                "dbo.Physicians",
                c => new
                    {
                        PhysicianID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.PhysicianID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ChildHealthRecords", "PersonID", "dbo.People");
            DropForeignKey("dbo.Consultations", "PersonID", "dbo.People");
            DropForeignKey("dbo.People", "BarangayID", "dbo.Barangays");
            DropForeignKey("dbo.ChildImmunizationRecords", "VaccineID", "dbo.Vaccines");
            DropForeignKey("dbo.ChildImmunizationRecords", "PersonID", "dbo.ChildHealthRecords");
            DropForeignKey("dbo.ChildBirthFollowUpVisits", "PersonID", "dbo.ChildHealthRecords");
            DropForeignKey("dbo.ChildHealthRecords", "BloodTypeID", "dbo.BloodTypes");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Consultations", new[] { "PersonID" });
            DropIndex("dbo.People", new[] { "BarangayID" });
            DropIndex("dbo.ChildImmunizationRecords", new[] { "VaccineID" });
            DropIndex("dbo.ChildImmunizationRecords", new[] { "PersonID" });
            DropIndex("dbo.ChildHealthRecords", new[] { "BloodTypeID" });
            DropIndex("dbo.ChildHealthRecords", new[] { "PersonID" });
            DropIndex("dbo.ChildBirthFollowUpVisits", new[] { "PersonID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Physicians");
            DropTable("dbo.Consultations");
            DropTable("dbo.People");
            DropTable("dbo.Vaccines");
            DropTable("dbo.ChildImmunizationRecords");
            DropTable("dbo.ChildHealthRecords");
            DropTable("dbo.ChildBirthFollowUpVisits");
            DropTable("dbo.BloodTypes");
            DropTable("dbo.Barangays");
        }
    }
}
