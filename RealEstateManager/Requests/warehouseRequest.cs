using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateManager.Models.Requests
{
    public class warehouseRequest
    {
        public int EstateID { get; set; }
        public string Location { get; set; }

        public Enums.RentorSale RentorSale { get; set; }

        public int Price { get; set; }
    }
}
