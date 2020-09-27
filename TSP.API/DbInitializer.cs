using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TSP.API;
using TSP.Shared;

namespace TSP.API
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.SubSystems.Any())
            {
                var root = Directory.GetCurrentDirectory();
                using (StreamReader r = File.OpenText(@$"{root}\\Data\\data.json"))
                {
                    string json = r.ReadToEnd();
                    List<SubSystem> list = JsonConvert.DeserializeObject<List<SubSystem>>(json);
                    foreach (var item in list)
                    {
                        item.Id = 0;
                        foreach (var subItem in item.SubMenuItems)
                        {
                            subItem.Id = 0;
                            foreach (var detail in subItem.SubItemDetails)
                            {
                                detail.Id = 0;
                            }
                        }
                    }
                    context.SubSystems.AddRange(list);
                }
            }
           
            context.SaveChanges();
        }
    }
}
