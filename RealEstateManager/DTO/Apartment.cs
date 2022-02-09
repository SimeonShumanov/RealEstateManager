using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateManager.Models.DTO
{
    public class Apartment
    {
        public int EstateID { get; set; }
        public string Location { get; set; }

        public int Price { get; set;  }


        public Enums.RentorSale RentorSale { get; set; }
    }
}
