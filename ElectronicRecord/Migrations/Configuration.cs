namespace ElectronicRecord.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ElectronicRecord.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ElectronicRecord.DAL.ERecordContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ElectronicRecord.DAL.ERecordContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var judete = new List<Judet>
            {
                new Judet{ID="AB",Denumire="Alba"},
                new Judet{ID="AG",Denumire="Arges"},
                new Judet{ID="AR",Denumire="Arad"},
                new Judet{ID="B",Denumire="Bucuresti"},
                new Judet{ID="BC",Denumire="Bacau"},
                new Judet{ID="BH",Denumire="Bihor"},
                new Judet{ID="BN",Denumire="Bistrita-Nasaud"},
                new Judet{ID="BR",Denumire="Braila"},
                new Judet{ID="BT",Denumire="Botosani"},
                new Judet{ID="BV",Denumire="Brasov"},
                new Judet{ID="BZ",Denumire="Buzau"},
                new Judet{ID="CJ",Denumire="Cluj"},
                new Judet{ID="CL",Denumire="Calarasi"},
                new Judet{ID="CS",Denumire="Caras-Severin"},
                new Judet{ID="CT",Denumire="Constanta"},
                new Judet{ID="CV",Denumire="Covasna"},
                new Judet{ID="DB",Denumire="Dambovita"},
                new Judet{ID="DJ",Denumire="Dolj"},
                new Judet{ID="GJ",Denumire="Gorj"},
                new Judet{ID="GL",Denumire="Galati"},
                new Judet{ID="GR",Denumire="Giurgiu"},
                new Judet{ID="HD",Denumire="Hunedoara"},
                new Judet{ID="HR",Denumire="Harghita"},
                new Judet{ID="IF",Denumire="Ilfov"},
                new Judet{ID="IL",Denumire="Ialomita"},
                new Judet{ID="IS",Denumire="Iasi"},
                new Judet{ID="MH",Denumire="Mehedinti"},
                new Judet{ID="MM",Denumire="Maramures"},
                new Judet{ID="MS",Denumire="Mures"},
                new Judet{ID="NT",Denumire="Neamt"},
                new Judet{ID="OT",Denumire="Olt"},
                new Judet{ID="PH",Denumire="Prahova"},
                new Judet{ID="SB",Denumire="Sibiu"},
                new Judet{ID="SJ",Denumire="Salaj"},
                new Judet{ID="SM",Denumire="Satu Mare"},
                new Judet{ID="SV",Denumire="Suceava"},
                new Judet{ID="TL",Denumire="Tulcea"},
                new Judet{ID="TM",Denumire="Timis"},
                new Judet{ID="TR",Denumire="Teleorman"},
                new Judet{ID="VL",Denumire="Valcea"},
                new Judet{ID="VN",Denumire="Vrancea"},
                new Judet{ID="VS",Denumire="Vaslui"}
            };
            judete.ForEach(j => context.Judete.Add(j));
            context.SaveChanges();

        }
    }
}
