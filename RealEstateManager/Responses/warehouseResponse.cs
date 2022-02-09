using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateManager.Models.Responses
{
    public class warehouseResponse
    {
        public int EstateID { get; set; }
        public string Location { get; set; }

        public Enums.RentorSale RentorSale { get; set; }

        public int Price { get; set; }
    }
}
