namespace Project_sem_3.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Project_sem_3.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Project_sem_3.Models.ApplicationDbContext context)
        {


            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
//            context.Insurances.AddOrUpdate(x => x.Id,
//                new Models.Insurance()
//                {
//                    Id = 1,
//                    Name = "HomeInsurance",
//                    Description = string.Format(@" <h1 class=""myriad_pro_semibold"">PRIVATE HOUSE INSURANCE</h1>
//                        < div >< iframe src = ""https://www.facebook.com/plugins/like.php?href=https%3A%2F%2Fbaohiemtructuyen.com.vn%2F&width=174&layout=button_count&action=like&size=large&share=true&height=46&appId"" width = ""174"" height = ""46"" style = ""border:none;overflow:hidden"" scrolling = ""no"" frameborder = ""0"" allowTransparency = ""true"" allow = ""encrypted-media"" ></ iframe ></ div >
//                        < h2 > The insured: </ h2 >



//                    < p > The house(apartment, townhouse / villa) and properties in the house </ p >
//                    < h2 > Coverage:</ h2 >
//                    < p > BIC will indemnify the insured against damages incurred by:</ p >
//                    < p >< strong > Basic risks: </ strong ></ p >
//                    < ul >
//                        < li > Fire;</ li >


//                          < li > Lightning strike;</ li >


//                             < li > Explosive;</ li >


//                           </ ul >


//                           < p >< strong > Expansion 1:</ strong ></ p >


//                                < ul >


//                                    < li > Thunderstorms, storms, and floods(including overwater); </ li >


//                                     < li > Breakage or overflow from water tanks, water storage devices or water pipes;</ li >


//                                        < li > Bumping station with house;</ li >


//                                           < li > Theft;</ li >


//                                         </ ul >


//                                         < p >< strong > Expansion 2:</ strong ></ p >


//                                              < ul >


//                                                  < li > Field cleaning costs;</ li >


//                                                     < li > Fire fighting expenses;</ li >


//                                                        < li > Rent a house after a loss.</ li >


//                                                       </ ul >


//                                                       < h2 > Details of benefits &fee schedule:</ h2 >


//                                                         < h3 > 1.The material of the house</ h3 >


//                                                           < table border = ""0"" cellspacing = ""0"" cellpadding = ""0"" width = ""90%"" >


//                                                                      < tr >


//                                                                          < td width = ""106"" >


//                                                                               < p align = ""center"" >


//                                                                                    < strong >
//                                                                                        Limit of < br />
//                                                                                        compensation
//                                                                                    </ strong >


//                                                                                </ p >


//                                                                            </ td >


//                                                                            < td width = ""110"" >


//                                                                                 < p align = ""center"" >


//                                                                                      < strong >
//                                                                                          Time < br />
//                                                                                          to use
//                                                                                      </ strong >


//                                                                                  </ p >


//                                                                              </ td >


//                                                                              < td width = ""97"" >< p align = ""center"" >< strong > Basic fee </ strong ></ p ></ td >


//                                                                                         < td width = ""94"" >< p align = ""center"" >< strong > Expansion 1 </ strong ></ p ></ td >


//                                                                                                    < td width = ""83"" >< p align = ""center"" >< strong > Expansion 2 </ strong ></ p ></ td >


//                                                                                                               < td width = ""92"" >


//                                                                                                                    < p align = ""center"" >


//                                                                                                                         < strong >
//                                                                                                                             Total < br />
//                                                                                                                             premium
//                                                                                                                         </ strong >


//                                                                                                                     </ p >


//                                                                                                                 </ td >


//                                                                                                             </ tr >


//                                                                                                             < tr >


//                                                                                                                 < td width = ""106"" rowspan = ""2"" >< p align = ""right"" > 300.000.000 </ p ></ td >


//                                                                                                                            < td width = ""110"" >< p align = ""center"" > 01 – 15 years </ p ></ td >


//                                                                                                                                     < td width = ""97"" >< p align = ""right"" > 225.000 </ p ></ td >


//                                                                                                                                          < td width = ""94"" >< p align = ""right"" > 45.000 </ p ></ td >


//                                                                                                                                               < td width = ""83"" >< p align = ""right"" > 90.000 </ p ></ td >


//                                                                                                                                                    < td width = ""92"" >< p align = ""right"" > 360.000 </ p ></ td >


//                                                                                                                                                     </ tr >


//                                                                                                                                                     < tr >


//                                                                                                                                                         < td width = ""110"" >< p align = ""center"" > 15 – 25 years </ p ></ td >


//                                                                                                                                                                  < td width = ""97"" >< p align = ""right"" > 360.000 </ p ></ td >


//                                                                                                                                                                       < td width = ""94"">< p align = ""right"" > 60.000 </ p ></ td >


//                                                                                                                                                                            < td width = ""83"" >< p align = ""right"" > 120.000 </ p ></ td >


//                                                                                                                                                                                 < td width = ""92"" >< p align = ""right"" > 540.000 </ p ></ td >


//                                                                                                                                                                                  </ tr >


//                                                                                                                                                                                  < tr >


//                                                                                                                                                                                      < td width = ""106"" rowspan = ""2"" >< p align = ""right"" > 500.000.000 </ p ></ td >


//                                                                                                                                                                                                 < td width = ""110"" >< p align = ""center"" > 01 – 15 years </ p ></ td >


//                                                                                                                                                                                                          < td width = ""97"" >< p align = ""right"" > 375.000 </ p ></ td >


//                                                                                                                                                                                                               < td width = ""94"" >< p align = ""right"" > 75.000 </ p ></ td >


//                                                                                                                                                                                                                    < td width = ""83"" >< p align = ""right"" > 150.000 </ p ></ td >


//                                                                                                                                                                                                                         < td width = ""92"" >< p align = ""right"" > 600.000 </ p ></ td >


//                                                                                                                                                                                                                          </ tr >


//                                                                                                                                                                                                                          < tr >


//                                                                                                                                                                                                                              < td width = ""110"" >< p align = ""center"" > 15 – 25 years </ p ></ td >


//                                                                                                                                                                                                                                       < td width = ""97"" >< p align = ""right"" > 600.000 </ p ></ td >


//                                                                                                                                                                                                                                            < td width = ""94"" >< p align = ""right"" > 100.000 </ p ></ td >


//                                                                                                                                                                                                                                                 < td width = ""83"" >< p align = ""right"" > 200.000 </ p ></ td >


//                                                                                                                                                                                                                                                      < td width = ""92"" >< p align = ""right"" > 900.000 </ p ></ td >


//                                                                                                                                                                                                                                                       </ tr >


//                                                                                                                                                                                                                                                       < tr >


//                                                                                                                                                                                                                                                           < td width = ""106"" rowspan = ""2"" >< p align = ""right"" > 750.000.000 </ p ></ td >


//                                                                                                                                                                                                                                                                      < td width = ""110"" >< p align = ""center"" > 01 – 15 years </ p ></ td >


//                                                                                                                                                                                                                                                                               < td width = ""97"" >< p align = ""right"" > 562.500 </ p ></ td >


//                                                                                                                                                                                                                                                                                    < td width = ""94"" >< p align = ""right"" > 112.500 </ p ></ td >


//                                                                                                                                                                                                                                                                                         < td width = ""83"" >< p align = ""right"" > 225.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                              < td width = ""92"" >< p align = ""right"" > 900.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                               </ tr >


//                                                                                                                                                                                                                                                                                               < tr >


//                                                                                                                                                                                                                                                                                                   < td width = ""110"" >< p align = ""center"" > 15 – 25 years </ p ></ td >


//                                                                                                                                                                                                                                                                                                            < td width = ""97"" >< p align = ""right"" > 900.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                 < td width = ""94"" >< p align = ""right"" > 150.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                      < td width = ""83"" >< p align = ""right"" > 300.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                           < td width = ""92"" >< p align = ""right"" > 1.350.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                </ tr >


//                                                                                                                                                                                                                                                                                                                                < tr >


//                                                                                                                                                                                                                                                                                                                                    < td width = ""106"" rowspan = ""2"" >< p align = ""right"" > 1.000.000.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                               < td width = ""110"" >< p align = ""center"" > 01 – 15 years </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                        < td width = ""97"" >< p align = ""right"" > 750.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                             < td width = ""94"" >< p align = ""right"" > 150.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                  < td width = ""83"" >< p align = ""right"" > 300.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                       < td width = ""92"" >< p align = ""right"" > 1.200.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                            </ tr >


//                                                                                                                                                                                                                                                                                                                                                                            < tr >


//                                                                                                                                                                                                                                                                                                                                                                                < td width = ""110"" >< p align = ""center"" > 15 – 25 years </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                         < td width = ""97"" >< p align = ""right"" > 1.200.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                  < td width = ""94"" >< p align = ""right"" > 200.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                       < td width = ""83"" >< p align = ""right"" > 400.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                            < td width = ""92"" >< p align =""right"" > 1.800.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                 </ tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                 < tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                     < td width = ""106"" rowspan = ""2"" >< p align = ""right"" > 1.500.000.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                < td width = ""110"" >< p align = ""center"" > 01 – 15 years </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                         < td width = ""97"" >< p align = ""right"" > 1.125.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                  < td width = ""94"" >< p align = ""right"" > 225.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                       < td width = ""83"" >< p align =""right"" > 450.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                            < td width = ""92"" >< p align = ""right"" > 1.800.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                 </ tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                 < tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                     < td width = ""110"" >< p align = ""center"" > 15 – 25 years </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                              < td width = ""97"" >< p align = ""right"" > 1.800.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       < td width = ""94"" >< p align = ""right"" > 300.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            < td width = ""83"" >< p align = ""right"" > 600.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 < td width = ""92"" >< p align = ""right"" > 2.700.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      </ tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      < tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          < td width = ""106"" rowspan = ""2"" >< p align = ""right"" > 2.000.000.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     < td width = ""110"" >< p align = ""center"" > 01 – 15 years </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              < td width = ""97"" >< p align = ""right"" > 1.500.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       < td width = ""94"" >< p align = ""right"" > 300.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            < td width = ""83"" >< p align = ""right"" > 600.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 < td width = ""92"" >< p align = ""right"" > 2.400.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      </ tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      < tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          < td width = ""110"" >< p align = ""center"" > 15 – 25 years </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   < td width = ""97"" >< p align = ""right"" > 2.400.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            < td width = ""94"" >< p align = ""right"" > 400.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 < td width = ""83"" >< p align = ""right"" > 800.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      < td width = ""92"" >< p align = ""right"" > 3.600.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           </ tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           < tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               < td width = ""106"" rowspan = ""2"" >< p align = ""right"" > 3.000.000.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          < td width = ""110"" >< p align = ""center"" > 01 – 15 years </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   < td width = ""97"" >< p align = ""right"" > 2.250.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            < td width = ""94"" >< p align = ""right"" > 450.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 < td width = ""83"" >< p align = ""right"" > 900.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      < td width = ""92"" >< p align = ""right"" > 3.600.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           </ tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           < tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               < td width = ""110"" >< p align = ""center"" > 15 – 25 years </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        < td width = ""97"" >< p align = ""right"" > 3.600.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 < td width = ""94"" >< p align = ""right"" > 600.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      < td width = ""83"" >< p align = ""right"" > 1.200.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               < td width = ""92"" >< p align = ""right"" > 5.400.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    </ tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    < tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        < td width = ""106"" rowspan = ""2"" >< p align = ""right"" > 4.000.000.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   < td width = ""110"" >< p align = ""center"" > 01 – 15 years </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            < td width = ""97"" >< p align = ""right"" > 3.000.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     < td width = ""94"" >< p align = ""right"" > 600.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          < td width = ""83"" >< p align = ""right"" > 1.200.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   < td width = ""92"" >< p align = ""right"" > 4.800.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        </ tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        < tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            < td width = ""110"" >< p align = ""center"" > 15 – 25 years </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     < td width = ""97"" >< p align = ""right"" > 4.800.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              < td width = ""94"" >< p align = ""right"" > 800.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   < td width = ""83"" >< p align = ""right"" > 1.600.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            < td width = ""92"" >< p align = ""right"" > 7.200.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 </ tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 < tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     < td width = ""106"" rowspan = ""2"" >< p align = ""right"" > 5.000.000.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                < td width = ""110"" >< p align = ""center"" > 01 – 15 years </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         < td width = ""97"" >< p align = ""right"" > 3.750.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  < td width = ""94"" >< p align = ""right"" > 750.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       < td width = ""83"" >< p align = ""right"" > 1.500.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                < td width = ""92"" >< p align = ""right"" > 6.000.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     </ tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     < tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         < td width = ""110"" >< p align = ""center"" > 15 – 25 years </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  < td width = ""97"" >< p align = ""right"" > 6.000.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           < td width = ""94"" >< p align = ""right"" > 1.000.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    < td width = ""83"" >< p align = ""right"" > 2.000.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             < td width = ""92"" >< p align = ""right"" > 9.000.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  </ tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              </ table >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              < h3 > 2.The property part in the house</ h3 >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                < table border = ""0"" cellspacing = ""0"" cellpadding = ""0"" width = ""90%"" >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           < tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               < td width = ""127"" valign = ""top"" >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      < p align = ""center"" >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           < strong >
//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               Limit of < br />
//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               compensation
//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           </ strong >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       </ p >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   </ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   < td width = ""136"" valign = ""top"" >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          < p align = ""center"" >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               < strong >
//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   Limit compensation of<br />
//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   each item
//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               </ strong >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           </ p >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       </ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       < td width = ""85"" valign = ""top"" >< p align = ""center"" >< strong > Basic fee </ strong ></ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    < td width = ""85"" valign = ""top"" >< p align = ""center"" >< strong > Expansion 1 </ strong ></ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 < td width = ""85"" valign = ""top"" >< p align = ""center"" >< strong > Expansion 2 </ strong ></ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              < td width = ""87"" valign = ""top"" >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     < p align = ""center"" >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          < strong >
//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              Total < br />
//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              premium
//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          </ strong >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      </ p >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  </ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              </ tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              < tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  < td width = ""127"" nowrap = ""nowrap"" >< p align = ""center"" > 100.000.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             < td width = ""136"" nowrap = ""nowrap"" >< p align = ""center"" > 50.000.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        < td width = ""85"" >< p align = ""center"" > 100.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             < td width = ""85"" >< p align = ""center"" > 15.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  < td width = ""85"" >< p align = ""center"" > 35.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       < td width = ""87"" nowrap = ""nowrap"" >< p align = ""center"" > 150.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          </ tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          < tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              < td width = ""127"" nowrap = ""nowrap"" >< p align = ""center"" > 300.000.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         < td width = ""136"" nowrap = ""nowrap"" >< p align = ""center"" > 50.000.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    < td width = ""85"" >< p align = ""center"" > 300.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         < td width = ""85"" >< p align = ""center"" > 45.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              < td width = ""85"" >< p align = ""center"" > 105.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   < td width = ""87"" nowrap = ""nowrap"" >< p align = ""center"" > 450.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      </ tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      < tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          < td width = ""127"" nowrap = ""nowrap"" >< p align = ""center"" > 500.000.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     < td width = ""136"" nowrap = ""nowrap"" >< p align = ""center"" > 50.000.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                < td width = ""85"" >< p align = ""center"" > 500.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     < td width = ""85"" >< p align = ""center"" > 75.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          < td width = ""85"" >< p align = ""center"" > 175.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               < td width = ""87"" nowrap = ""nowrap"" >< p align = ""center"" > 750.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  </ tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  < tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      < td width = ""127"" nowrap = ""nowrap"" >< p align = ""center"" > 750.000.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 < td width = ""136"" nowrap = ""nowrap"" >< p align = ""center"" > 50.000.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            < td width = ""85"" >< p align = ""center"" > 750.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 < td width = ""85"" >< p align = ""center"" > 112.500 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      < td width = ""85"" >< p align = ""center"" > 262.500 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           < td width = ""87"" nowrap = ""nowrap"" >< p align = ""center"" > 1.125.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  </ tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  < tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      < td width = ""127"" nowrap = ""nowrap"" >< p align = ""center"" > 1.000.000.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 < td width = ""136"" nowrap = ""nowrap"" >< p align = ""center"" > 50.000.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            < td width = ""85"" >< p align = ""center"" > 1.000.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     < td width = ""85"" >< p align = ""center"" > 150.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          < td width = ""85"" >< p align = ""center"" > 350.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               < td width = ""87"" nowrap = ""nowrap"" >< p align = ""center"" > 1.500.000 </ p ></ td >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      </ tr >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  </ table >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  < h3 >< strong > Note:</ strong ></ h3 >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       < ul >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           < li > Basic charge applies to Fire(A) and Explosion(B) risks </ li >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            < li > Expansion fee(1) applies to risks: Thunderstorm(H), Spill(I), Collision with a house(J), Theft </ li >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           < li > Expansion fee(2) applies to risks: Site cleanup cost, Fire cost, Rent after loss</ li >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         </ ul >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         < h2 > Download documents and forms:</ h2 >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            < ul >


//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                < li >

//                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    < a href = ""BsioData/san-pham/nha-tu-nhan/1843-QTBH-toan-dien-nha-tu-nhan.pdf"" class=""normal"" target=""_blank"" style=""color: #5548C7"">Rules of comprehensive insurance for private houses</a> of BIDV Insurance Corporation.
//                        </li>
//                    </ul>
//                    <p>&nbsp;</p>
//                    <h2>See details</h2>
//                    <ul>
//                        <li><a href = ""http://baohiemtructuyen.com.vn/bao-hiem-nha-tu-nhan"" alt= "" style= ""color:#5548C7;"" > General information</a></li>
//                        <li><a href = ""http://baohiemtructuyen.com.vn/bao-hiem-nha-tu-nhan-huong-dan-boi-thuong"" alt= "" style= ""color:#5548C7;"" > Compensation guide</a></li>
//                    </ul>"),
//                    CreatedAt = DateTime.Now,
//                    UpdatedAt = DateTime.Now,
//                    DeleteAt = DateTime.Now,
//                    Status = 1


//                },
//                new Models.Insurance()
//                {
//                    Id = 2,
//                    Name = "LifeInsurance",
//                    Description = string.Format(@"
//                        <h1 style=""color:#337ab7"">WHOLE LIFE INSURANCE - MORE THAN LOVE</h1>

//                        < h3 class=""font-text"">HAPPINESS IS WHEN YOU ARE MADE SOMEBODY ELSE HAPPY</h3>

//                        <h2 class=""text-primary"">What is whole life insurance?</h2>
//                        <div style = ""margin-bottom:1%;"" >< span > As the name implies, whole life insurance covers you for your whole life, provided you continue to pay your premiums.Whole life insurance typically comes with guaranteed level premiums — the amount will never change as long as premiums are paid.</span></div>
//                        <div style = ""margin-bottom:2%;"" >< span > Whole life insurance policies pay death benefits (proceeds after death) and they may also build cash value.</span></div>
//                        <h2 class=""text-primary"" style=""margin-top: 2%;"">Life insurance packages:</h2>
//                    <p>1.<span style = ""margin-bottom:2%; color: crimson;"" > Life peace.</span></p>
//                    <p>2.<span style = ""margin-bottom:2%; color: crimson;"" > Inner peace.</span></p>
//                    <p style = ""margin-top: 2%;"" >< strong style=""color: #0f75bd;"">Life peace: </strong></p>
//                    <ul>
//                        <li>Covered up to 200% of Sum Assured for 72 cancers and critical illnesses.</li>
//                        <li>Covered up to 300% of the insured against death risk.</li>
//                        <li>Get 30% immediate cash insurance at age 65.</li>
//                        <li>Premium-free benefits in the future immediately after the first cancer / critical illness benefit is paid.</li>
//                    </ul>
//                    <p style = ""margin-top: 2%;"" >< strong style= ""color: #0f75bd;"" > Inner peace: </strong></p>
//                    <ul>
//                        <li>Diversified protection against accident risks and outside accidents.</li>
//                        <li>Guarantee of treatment expenses 24/7.</li>
//                        <li>Financial support many times before the risk of an accident.</li>
//                        <li>Refund up to 75% of closing fee at the end of the contract.</li>
//                    </ul>

//                </div>
//                @*<h2 class=""text-primary"" style=""margin-top: 2%;"">You should buy whole life insurance?</h2>
//                    <ul style = ""margin-left: 20px;"" >
//                        < li >< span style=""margin-bottom:2%"">Portable protection for life</span></li>
//                        <li><span style = ""margin-bottom:2%"" > Level premiums that stay the same each year</span></li>
//                        <li><span style = ""margin-bottom:2%""> Cash value you can use during your lifetime</span></li>
//                        <li><span style = ""margin-bottom:2%"" > Create a source of savings</span></li>
//                    </ul>*@

//                <table style = ""margin-top: 2%;"" >


//                        < tr >


//                            < th style= ""width:30%;color:#444;"" > Features </ th >


//                            < th style= ""color: #444;"" > Life peace</th>
//                            <th style = ""color: #444;"" > Inner peace</th>
//                        </tr>
//                    <tr>
//                        <td>Age of participation</td>
//                            <td><p>From 15 - 60 years old</p></td>
//                        <td><p>From 18 - 65 years old</p></td>
//                    </tr>
//                    <tr>
//                        <td>Contract term</td>
//                        <td><p>Up to 75 years old</p></td>
//                        <td><p>5 or 10 years</p></td>
//                    </tr>
//                    <tr>
//                        <td>Premium time</td>
//                        <td><p>15 years</p></td>
//                        <td><p>By the contract term</p></td>
//                    </tr>
//                    <tr>
//                        <td>Serving needs</td>
//                        <td><p>Financial protection many times against the risk of serious illness</p></td>
//                        <td><p>Insurance against accident risks and hospital guarantee 24/7</p></td>
//                    </tr>
//                </table>

//                <div class=""row"" style=""margin-top: 3%; margin-bottom: 3%;"">
//                    <div class=""col-xs-6 col-sm-6 col-md-6 col-lg-3 col-xsx-12"">
//                        <div class=""featured-number"">
//                            <span class=""featured-text"">
//                                <strong>
//                                    Life
//                                    insurance
//                                </strong>
//                            </span><p class=""featured-text1"">Your family will be protected forever.</p>
//                        </div>
//                    </div>
//                    <div class=""col-xs-6 col-sm-6 col-md-6 col-lg-3 col-xsx-12"">
//                        <div class=""featured-number""><span class=""featured-text""><strong>Flexible</strong></span><p class=""featured-text1"">Flexible fee adjustment during participation.</p></div>
//                    </div>
//                    <div class=""col-xs-6 col-sm-6 col-md-6 col-lg-3 col-xsx-12"">
//                        <div class=""featured-number""><span class=""featured-text""><strong>500 million</strong></span><p class=""featured-text1"">Advance 50% Death benefit(up to 500 million VND) for end-stage Critical Illness.</p></div>
//                    </div>
//                    <div class=""col-xs-6 col-sm-6 col-md-6 col-lg-3 col-xsx-12"">
//                        <div class=""featured-number""><span class=""featured-text""><strong>5%</strong></span><p class=""featured-text1"">Opportunity to choose an auto insurance sum of 5% / year.</p></div>
//                    </div>
//                </div>
//                <iframe style = ""margin-top:15px"" width=""560"" height=""315"" src=""https://www.youtube.com/embed/FxKb--dYdMI"" frameborder=""0"" allow=""accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture"" allowfullscreen></iframe>                    
//                <h2 style = ""margin-top: 2%; color: crimson;"" > Please contact us for a free consultation!</h2>"),
//                    CreatedAt = DateTime.Now,
//                    UpdatedAt = DateTime.Now,
//                    DeleteAt = DateTime.Now,
//                    Status = 1


//                },

//                new Models.Insurance()
//                {
//                    Id = 3,
//                    Name = "MotorInsurance",
//                    Description = string.Format(@"
// <h1>CIVIL LIABILITY INSURANCE FOR MOTORCYCLE OWNERS</h1>


//                        <h2 class=""text - primary"">The insured:</h2>
//                        < ul >
//                            < li >< span style = ""margin-bottom:2%"" > Motorcycle owners participating in traffic on the territory of the Socialist Republic of Vietnam.</ span ></ li >
//                        </ ul >
//                        < h2 class=""text-primary"" style=""margin-top: 2%;"">Coverage:</h2>
//                    <p>
//                        BIC will compensate for loss of life and property to the third party on behalf of the owner of the vehicle(the party suffered damage caused by the owner's vehicle).
//                    </p>
//                    <h2 class=""text-primary"" style=""margin-top: 2%;"">Insurance benefits:</h2>
//                    <p>BIC will compensate the third party for damage to life and property on behalf of the vehicle owner, the damage to the passengers to the maximum extent as follows: </p>
//                    <ul>
//                        <li>
//                            Regarding people: maximum 100.000.000 đ  / person / accident.Specifically:
//                        </li>
//                    </ul>
//                    <table class=""table-bordered"" border=""0"" cellpadding=""5"" cellspacing=""0"" style=""width:80%;"">
//                        <tr class="" style=""text-align:left"">
//                            <td width = ""29%"" >< strong > Insurance benefits</strong></td>
//                            <td width = ""71%"" >< strong > Maximum compensation level</strong></td>
//                        </tr>
//                        <tr>
//                            <td>

//                                      Die by accident
//                                  </td>
//                                  <td class="">Pay full 100.000.000đ / person / case</td>
//                        </tr>
//                        <tr class="">
//                            <td>
//                                Injury by accident
//                            </td>
//                            <td class="">Pay according to<a href=""BsioData/document.pdf"" class=""normal"" target=""_blank"" style=""color:#5548C7"">""Table of regulations on compensation for human damage""</a></td>
//                        </tr>
//                    </table>
//                    <ul>
//                        <li>Regarding assets: maximum of 50.000.000đ / accident.</li>
//                    </ul>

//                    <h2 class=""text-primary"">Insurance fees:</h2>
//                    <table class=""table-bordered"" width=""80%"" border=""0"" cellspacing=""10"" cellpadding=""2"">
//                        <tr>
//                            <td width = ""77%"" >
//                                < strong >
//                                    Species
//                                </ strong >
//                            </ td >
//                            < td width=""23%""><strong>Insurance fee(including VAT)</strong></td>
//                        </tr>
//                        <tr>
//                            <td>Civil liability insurance</td>
//                            <td><div align = ""right"" > 66.000 VNĐ</div></td>
//                        </tr>
//                        <tr>
//                            <td>Civil liability insurance + Accident insurance for 2 people in the car(VND 10 million / person)</td>
//                            <td><div align = ""right"" > 86.000 VNĐ</div></td>
//                        </tr>
//                    </table>
//                    <h2 class=""text-primary"">Download documents and forms:</h2>
//                    <ul>
//                        <li><a href = ""BsioData/document.pdf"" class=""normal"" target=""_blank"" style=""color:#5548C7"">Circular 22/2016 / TT-BTC of the Ministry of Finance</a> Stipulating rules, terms, fee schedule and compulsory liability of civil liability of motor vehicle owners.</li>



//                    </ul>
//                    <p>&nbsp;</p>
//                    <h2 class=""text-primary"">See details</h2>
//                    <ul>
//                        <li><a href = ""http://baohiemtructuyen.com.vn/bao-hiem-trach-nhiem-dan-su-xe-may"" alt= "" style= ""color:#5548C7;"" > General information</a></li>
//                        <li><a href = ""http://baohiemtructuyen.com.vn/bao-hiem-trach-nhiem-dan-su-xe-may-huong-dan-boi-thuong"" alt= "" style= ""color:#5548C7;"" > Compensation guide</a></li>
//                    </ul>
//                    <p>&nbsp;</p>"),
//                    CreatedAt = DateTime.Now,
//                    UpdatedAt = DateTime.Now,
//                    DeleteAt = DateTime.Now,
//                    Status = 1
//                },
//                new Models.Insurance() {
//                    Id = 4,
//                    Name = "MedicalInsurance",
//                    Description = string.Format(@" <h1 class=""myriad_pro_semibold"">HEALTH INSURANCE AND PERSONAL ACCIDENT</h1>

//                         < div style = ""display: block;  padding: 0px 10px 10px 10px;""

//< p >
//                        From February 15, 2020, BIDV Insurance Corporation issued another package of four new BIC Tam An health and personal accident insurance products.Some basic information of the new product is as follows:
//                    </ p >
//                    < ul >
//                        < p > 1.Adding 2 packages of benefits lower than the Dong program(Basic and Standard package): with the aim of meeting customers with lower incomes who want to buy insurance as a tool to prevent risks risk of illness; especially in cases of inpatient treatment or low-cost outpatient coverage.</ p >
 
//                         < p > 2.Adding 2 more high-limit maternity benefit packages(Platinum Plus and Diamond Plus) </ p >
   
//                       </ ul >
   
//                   </ div >
   
//                   < div style = ""width: 100%;"" >
    

//                        < h1 class=""myriad_pro_semibold"">
//                    </h1>
//                    <div style = ""display: block;
//    padding: 0px 10px 10px 10px;"">
//                        <p>
//                            From January 1, 2019, BIDV Insurance Corporation has issued a new package of Personal Health and Accident Insurance named BIC INSURANCE.Some basic information of the new product is as follows:
//                        </p>
//                        <ul>
//                            <p>
//                                - New benefit table and fee schedule with 5 insurance programs for customers to choose.
//                                Detailed in <a href = ""http://baohiemtructuyen.com.vn/bao-hiem-suc-khoe-bang-quyen-loi-bao-hiem"" alt="" style=""color: #5548C7;"">Table Benefit</a>  and
//                                  <a href=""http://baohiemtructuyen.com.vn/bao-hiem-suc-khoe-bieu-phi-bao-hiem"" alt="" style=""color: #5548C7;""> Fee Insurance</a>
//                              </p>
//                            <p>
//                                <span style = ""color: #ff0000;"" > -The preeminent feature of the product is the separate design of Inpatient benefits so that customers have more options when there is no need to participate in outpatient insurance and reduce insurance premiums for customers.</span>
//                            </p>
//                            <p>- Do not separate the cost limit of beds within the cost of inpatient treatment; The maximum benefit is up to VND 300 million / year, VND 7 million / day in the hospital</p>
//                            <p>- Apply payment at all hospitals and clinics nationwide; No co-payments apply for all ages</p>
//                        </ul>
//                        <p>Detailed regulations on<strong> waiting time</strong> đfor each type of disease group.Special note:</p>
//                        <ul>
//                            <p>- Respiratory diseases including bronchitis, bronchiolitis, asthma / asthma, pneumonia of all kinds: waiting time for 06 months for children from 01 to 06 years old.</p>
//                            <p>- Maternity: waiting period of 1 year for normal or caesarean birth, 280 days for maternity complications and 90 days for ectopic pregnancy.</p>
//                            <p>- Chronic diseases, available diseases: 01 year.</p>
//                            <p>- Common diseases not listed in the rule: 30 days.</p>
//                        </ul>
//                        <p>See details <a href = ""http://baohiemtructuyen.com.vn/bao-hiem-suc-khoe-huong-dan-boi-thuong"" alt= "" style= ""color: #5548C7;"" > Cases of non-compensation for the first year of insurance</a>.From the second insurance year onwards, BIC will pay normally for exclusions in the first year.</p>
//                        <p>Time to file compensation: <span style = ""color: #ff0000;"" > within 30 days</span> from the last treatment day of each treatment session.</p>
//                    </div>
//                    <div style = ""width: 100%;"" >
//                        < h2 > Benefits when buying BIC Tam An Insurance</h2>
//                        <ul>
//                            <li>Pay up <strong>to 1 billion VND </strong> for accident cases;</li>
//                            <li>Pay up<strong>to 2.5 million VND / 1 </strong> outpatient visit and treatment;</li>
//                            <li>Pay up<strong>to 9 million VND / day</strong> inpatient treatment;</li>
//                            <li>Pay up<strong>to VND 180 million per</strong>surgery;</li>
//                            <li>No medical examination is required prior to purchase;</li>
//                            <li>Guarantee of hospital fees at major hospitals / clinics nationwide;</li>
//                            <li>Compensation procedure is simple and fast.</li>
//                        </ul>
//                        <div style = ""display: block;
//        padding: 0px 10px 10px 10px;"">
//                            <p>
//                                From July 16, 2018, BIDV Insurance Corporation issued a new personal health and accident insurance product called BIC Tam An.Some basic adjustments of new products are as follows:
//                            </p>
//                            <ul>
//                                <p>
//                                    - New benefit table and fee schedule with 5 insurance programs for customers to choose.
//                                    Detailed in <a href = ""http://baohiemtructuyen.com.vn/bao-hiem-suc-khoe-bang-quyen-loi-bao-hiem""
//                                                   alt="" style=""color: #5548C7;"">Table Benefit</a> and<a href=""http://baohiemtructuyen.com.vn/bao-hiem-suc-khoe-bieu-phi-bao-hiem""
//                                                                                                            alt="" style=""color: #5548C7;""> Fee Insurance</a>
//                                </p>
//                                <p>
//                                    <span style = ""color: #ff0000;"" >
//                                        -Children between 1 and 6 years of age are required to purchase with a parent or a parent who has participated in at least 01 valid health insurance program at BIC. The children's insurance program is applied at the same or lower level than the Parent's Program.
//                                    </span>
//                                </p>
//                                <p>- Do not separate the cost limit of beds within the inpatient hospital expense limit</p>
//                            </ul>
//                            Detailed regulations on<strong> waiting time</strong> for each type of disease group.Special note:
//                            <ul>
//                                <p>
//                                    -The respiratory diseases include bronchitis, bronchiolitis, asthma / asthma, pneumonia of all kinds: waiting time for 06 months for children from 01 to 06 years old.
//                                </p>
//                                <p>- Maternity: waiting period of 1 year for normal or caesarean birth, 280 days for maternity complications and 90 days for ectopic pregnancy.</p>
//                                <p>- Chronic diseases, available diseases: 01 year.</p>
//                                <p>- Common diseases not listed in the rule: 30 days.</p>
//                            </ul>
//                            <p>See details <a href = ""http://baohiemtructuyen.com.vn/bao-hiem-suc-khoe-huong-dan-boi-thuong"" alt= "" style= ""color: #5548C7;"" > Cases of non-compensation for the first year of insurance</a>.From the second insurance year onwards, BIC will pay normally for exclusions in the first year.</p>
//                            <p>Time to file compensation: <span style = ""color: #ff0000;"" > within 30 days</span> from the last treatment day of each treatment session.</p>
//                        </div>
//                        <div style = ""width: 100%;"" >
//                            < h2 > Product information</h2>
//                            <ul>
//                                <li>
//                                    <a href = ""http://baohiemtructuyen.com.vn/bao-hiem-suc-khoe-bang-quyen-loi-bao-hiem""
//                                       alt= "" style= ""color: #5548C7;"" > Table of insurance benefits</a>
//                                </li>
//                                <li>
//                                    <a href = ""http://baohiemtructuyen.com.vn/bao-hiem-suc-khoe-bieu-phi-bao-hiem""
//                                       alt= "" style= ""color: #5548C7;"" > Premium tariff</a>
//                                </li>
//                                <li>
//                                    <a href = ""http://baohiemtructuyen.com.vn/bao-hiem-suc-khoe-huong-dan-boi-thuong""
//                                       alt= "" style= ""color: #5548C7;"" > Compensation guide</a>
//                                </li>
//                                <li>
//                                    <a href = ""http://baohiemtructuyen.com.vn/bao-hiem-suc-khoe-danh-sach-benh-vien-lien-ket""
//                                       alt= "" style= ""color: #5548C7;""> Affiliated hospital guarantees hospital fees</a>
//                                </li>
//                                <li>
//                                    <a href = ""http://baohiemtructuyen.com.vn/bao-hiem-suc-khoe-download"" alt= "" style= ""color: #5548C7;"" >
//                                        Download documents, forms
//                                    </a>
//                                </li>
//                            </ul>
//                        </div>
//                        <div style = ""width: 50%; float: left;"" >
//                        </ div > "),
//                    CreatedAt = DateTime.Now,
//                    UpdatedAt = DateTime.Now,
//                    DeleteAt = DateTime.Now,
//                    Status = 1
//                }
//                );


          //  context.InsurancePackages.AddOrUpdate(x => x.Id,
          //     new Models.InsurancePackage()
          //     {
                  
          //         InsuranceId = 27,
          //         Name = "Compulsory insurance",
          //         DurationContract = "3 year",
          //         DurationPay = "2 month",
          //         Description = "Compulsory insurance for human liability liability Insurance for personal accident in vehicles",
          //         Price = 3,
          //         CreatedAt = DateTime.Now,
          //         UpdatedAt = DateTime.Now,
          //         DeleteAt = DateTime.Now,
          //         Status = 1
          //     },
             
          //       new Models.InsurancePackage()
          //     {
                 
          //         InsuranceId = 27,
          //          DurationContract = "3 year",
          //          DurationPay= "2 month",
          //         Name = "Compulsory insurance + accident person",
          //         Description = "Compulsory employee liability insurance",
          //         Price = 4,
          //         CreatedAt = DateTime.Now,
          //         UpdatedAt = DateTime.Now,
          //         DeleteAt = DateTime.Now,
          //         Status = 1
          //     },
          //         new Models.InsurancePackage()
          //         {
                       
          //             InsuranceId = 28,
          //             DurationContract = "3 year",
          //             DurationPay = "2 month",
          //             Name = "insurance package Tam An",
          //             Description = "Pay up to 1 billion VND for accident cases;Pay up to 2.5 million dola/ 1 outpatient visit and treatment;",
          //             Price = 5,
          //             CreatedAt = DateTime.Now,
          //             UpdatedAt = DateTime.Now,
          //             DeleteAt = DateTime.Now,
          //             Status = 1
          //         },
          //          new Models.InsurancePackage()
          //          {
                        
          //              InsuranceId = 28,
          //              DurationContract = "3 year",
          //              DurationPay = "2 month",
          //              Name = " Internal insurance package",
          //              Description = "Pay up to 9 million VND / day inpatient treatment;Paying up to dola 180 million per surgery; ",
          //              Price = 5,
          //              CreatedAt = DateTime.Now,
          //              UpdatedAt = DateTime.Now,
          //              DeleteAt = DateTime.Now,
          //              Status = 1
          //          },
          //           new Models.InsurancePackage()
          //           {
                         
          //               InsuranceId = 25,
          //               Name = "Expand 1",
          //               DurationContract = "3",
          //               DurationPay = "2 month",
          //               Description = "Thunderstorm, Storm, Flood, Overflow, Collide with home, Burglar ",
          //               Price = 300,
          //               CreatedAt = DateTime.Now,
          //               UpdatedAt = DateTime.Now,
          //               DeleteAt = DateTime.Now,
          //               Status = 1
          //           },
          //            new Models.InsurancePackage()
          //            {
                         
          //                InsuranceId = 25,
          //                DurationContract = "3 year",
          //                DurationPay = "2 month",
          //                Name = "Expansion 2",
          //                Description = "Site cleanup, firefighting, rent after losses",
          //                Price = 500,
          //                CreatedAt = DateTime.Now,
          //                UpdatedAt = DateTime.Now,
          //                DeleteAt = DateTime.Now,
          //                Status = 1
          //            },
          //             new Models.InsurancePackage()
          //             {
                           
          //                 Name = "Advanced Options",
          //                 InsuranceId =26 ,
          //                 DurationContract = "3 year",
          //                 DurationPay = "2 month",
          //                 Description = "at risk of cancer,before the risk of Death",
          //                 Price = 400,
          //                 CreatedAt = DateTime.Now,
          //                 UpdatedAt = DateTime.Now,
          //                 DeleteAt = DateTime.Now,
          //                 Status = 1
          //             },
          //              new Models.InsurancePackage()
          //              {
                            
          //                  Name = "Basic Choice",
          //                  InsuranceId = 26,
          //                  DurationContract = "3 year",
          //                  DurationPay = "2 month",
          //                  Description = "at risk of cancer,before the risk of Death",
          //                  Price = 500,
          //                  CreatedAt = DateTime.Now,
          //                  UpdatedAt = DateTime.Now,
          //                  DeleteAt = DateTime.Now,
          //                  Status = 1
          //              }





          //   );



          //context.Programmes.AddOrUpdate(x => x.Id,
          //     new Models.Programme() { Id = 1, Name = "Gold", Price = 200000 },
          //     new Models.Programme() { Id = 2, Name = "Silver", Price = 180000 },
          //     new Models.Programme() { Id = 3, Name = "Standard", Price = 100000 },
          //     new Models.Programme() { Id = 4, Name = "Diamond", Price = 300000 }
          //     );

        }
    }
}
