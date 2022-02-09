using RealEstateManager.Models.DTO;
using System.Collections.Generic;


namespace RealEstateManager.DL.InMemoryDB
{
    public static class warehouseInMemoryCollection
    {
        public static List<warehouse> warehouseDb = new List<warehouse>()
        {
            new warehouse()
            {
                EstateID = 300,
                Location = "Industrial Zone",
                Price = 300000,


            },
            new warehouse()
            {
                EstateID = 301,
                Location = "Outskirts",
                Price = 200000
            },

            new warehouse()
            {
                EstateID = 102,
                Location = "Close Center",
                Price = 250000
            }


        };
    }
}

