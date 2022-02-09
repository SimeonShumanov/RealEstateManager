using RealEstateManager.Models.DTO;
using System.Collections.Generic;

namespace RealEstateManager.DL.InMemoryDB
{
    public static class HouseInMemoryCollection
    {
        public static List<House> HouseDb = new List<House>()
        {
            new House()
            {
                EstateID = 100,
                Location = "Center",
                Price = 100000,


            },
            new House()
            {
                EstateID = 101,
                Location = "Outskirts",
                Price = 90000
            },

            new House()
            {
                EstateID = 102,
                Location = "Close Center",
                Price = 95000
            }


        };
    }
}
