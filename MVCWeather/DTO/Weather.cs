using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class Weather
    {
        public int id { get; set; }
        public int degreeMorning { get; set; }
        public int degreeAfternoon { get; set; }
        public int precipitation { get; set; }
        public int humidity { get; set; }
        public int wind { get; set; }
        [DataType(DataType.Date)]
        public DateTime date { get; set; }
        public City city { get; set; }
    }
}
