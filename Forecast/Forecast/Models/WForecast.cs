using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forecast.Models
{
    public partial class WForecast
    {
        
        public int CityId { get; set; }
        public DateTime Date { get; set; }
        [Key]
        public string City { get; set; }
        public int HighTemp { get; set; }
        public int LowTemp { get; set; }
        public string ForCast { get; set; }
    }
}
