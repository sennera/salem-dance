namespace SalemDance.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using SalemDance.Models;
    using System.Collections.Generic;


    internal sealed class Configuration : DbMigrationsConfiguration<SalemDance.DAL.DanceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SalemDance.DAL.DanceContext";
        }

        protected override void Seed(SalemDance.DAL.DanceContext context)
        {
            if (context.DaysOpen.Count() == 0 )
            {
                var days = new List<DayOpen>
                {
                    new DayOpen { DayOfWeek = DayOfWeek.Sunday },
                    new DayOpen { DayOfWeek = DayOfWeek.Monday },
                    new DayOpen { DayOfWeek = DayOfWeek.Tuesday },
                    new DayOpen { DayOfWeek = DayOfWeek.Wednesday },
                    new DayOpen { DayOfWeek = DayOfWeek.Thursday },
                    new DayOpen { DayOfWeek = DayOfWeek.Friday },
                    new DayOpen { DayOfWeek = DayOfWeek.Saturday }
                };

                days.ForEach(s => context.DaysOpen.AddOrUpdate(p => p.ID, s));
                context.SaveChanges();
            }
            
        }
    }
}
