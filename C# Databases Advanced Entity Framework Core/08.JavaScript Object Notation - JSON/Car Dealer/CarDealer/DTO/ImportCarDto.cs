using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.DTO
{
    class ImportCarDto
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public long TravelledDistance { get; set; }
        public ICollection<int> PartsId { get; set; }
    }
}
