using RealEstateManager.Models.DTO;
using System.Collections.Generic;

namespace RealEstateManager.DL.InMemoryDB
{
    public static class ApartmentInMemoryCollection
    {
        public static List<Apartment> ApartmentDb = new List<Apartment>()
        {
            new Apartment()
            {
                EstateID = 200,
                Location = "Center",
                Price = 100000,


            },
            new Apartment()
            {
                EstateID = 201,
                Location = "Outskirts",
                Price = 90000
            },

            new Apartment()
            {
                EstateID = 202,
                Location = "Close Center",
                Price = 95000
            }


        };
    }

}

