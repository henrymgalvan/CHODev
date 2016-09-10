namespace cho500.Migrations
{
    using Entity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<cho500.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(cho500.Models.ApplicationDbContext context)
        {
            context.Vaccines.AddOrUpdate(
                     v => v.Name,
                         new Vaccine { Name = "BCG" },
                         new Vaccine { Name = "Hepatitis B" },
                         new Vaccine { Name = "DTwP/DTap" },
                         new Vaccine { Name = "OPV/IPV" },
                         new Vaccine { Name = "Measles" },
                         new Vaccine { Name = "H. infuenza type B" },
                         new Vaccine { Name = "Pneumococcal Conjugate, PCV-13" },
                         new Vaccine { Name = "Rotavirus" },
                         new Vaccine { Name = "Influenza" },
                         new Vaccine { Name = "Varicella" },
                         new Vaccine { Name = "MMR" },
                         new Vaccine { Name = "Hepatitis A" },
                         new Vaccine { Name = "Typhoid" }
                 );

            context.Physicians.AddOrUpdate(
                p => p.Name,
                    new Physician { Name = "Sheila C. Sabado, M.D." },
                    new Physician { Name = "Aurora H. Cuison, M.D." },
                    new Physician { Name = "Benjamin Marcial O. Bautista, M.D." },
                    new Physician { Name = "Ma. Julita P. De Venecia, M.D." },
                    new Physician { Name = "Lydwina Bernardo, M.D." },
                    new Physician { Name = "Doris Jovellanos, M.D." },
                    new Physician { Name = "Anne Lourdes Paragas, M.D." }
                );

            context.Classifications.AddOrUpdate(
                c => c.Classification,
                    new HouseHoldClassificationPerVisit { Classification = "N", Note = "Newborn 0-28 days" },
                    new HouseHoldClassificationPerVisit { Classification = "I", Note = "Infant 1-11 months" },
                    new HouseHoldClassificationPerVisit { Classification = "C", Note = "Child 12-59 months" },
                    new HouseHoldClassificationPerVisit { Classification =  "P", Note = "Pregnant" },
                    new HouseHoldClassificationPerVisit { Classification = "PP", Note = "Postpartum Up to 42 days after delivery" },
                    new HouseHoldClassificationPerVisit { Classification = "TBS", Note = "TB Suspect; Cough > 2 weeks" },
                    new HouseHoldClassificationPerVisit { Classification = "Ad", Note = "Adolescent(10-19 years" },
                    new HouseHoldClassificationPerVisit { Classification = "NPNP", Note = "Not pregnant and not postpartum WRA(15-49 years" },
                    new HouseHoldClassificationPerVisit { Classification = "D", Note = "Deceased" },
                    new HouseHoldClassificationPerVisit { Classification = "TO", Note = "Transfered Out" },
                    new HouseHoldClassificationPerVisit { Classification = "ON", Note = "Other Needs" }
                );

            context.BloodType.AddOrUpdate(
                b => b.Type,
                    new BloodType { Type = "O-" },
                    new BloodType { Type = "O+" },
                    new BloodType { Type = "A-" },
                    new BloodType { Type = "A+" },
                    new BloodType { Type = "B-" },
                    new BloodType { Type = "B+" },
                    new BloodType { Type = "AB-" },
                    new BloodType { Type = "AB+" }
                );

            context.Barangays.AddOrUpdate(
                    b => b.Name,
                        new Barangay { Name = "Bacayao Norte" },
                        new Barangay { Name = "Bacayao Sur" },
                        new Barangay { Name = "Barangay I" },
                        new Barangay { Name = "Barangay II & III" },
                        new Barangay { Name = "Barangay IV" },
                        new Barangay { Name = "Bolosan" },
                        new Barangay { Name = "Bonuan Binloc" },
                        new Barangay { Name = "Bonuan Boquig" },
                        new Barangay { Name = "Bonuan Gueset" },
                        new Barangay { Name = "Calmay" },
                        new Barangay { Name = "Carael" },
                        new Barangay { Name = "Caranglaan" },
                        new Barangay { Name = "Herrero Perez" },
                        new Barangay { Name = "Lasip Chico" },
                        new Barangay { Name = "Lasip Grande" },
                        new Barangay { Name = "Lomboy" },
                        new Barangay { Name = "Lucao" },
                        new Barangay { Name = "Malued" },
                        new Barangay { Name = "Mamalingling" },
                        new Barangay { Name = "Mangin" },
                        new Barangay { Name = "Mayombo" },
                        new Barangay { Name = "Pantal" },
                        new Barangay { Name = "Poblacion Oeste" },
                        new Barangay { Name = "Pogo Chico" },
                        new Barangay { Name = "Pogo Grande" },
                        new Barangay { Name = "Pugaro Suit" },
                        new Barangay { Name = "Salapingao" },
                        new Barangay { Name = "Salisay" },
                        new Barangay { Name = "Tambac" },
                        new Barangay { Name = "Tapuac" },
                        new Barangay { Name = "Tebeng" }
                );
        }
    }
}
