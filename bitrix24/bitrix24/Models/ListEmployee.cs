using System.Collections.Generic;

namespace bitrix24.Models
{
    public class ListEmployee
    {
        public List<Result> result { get; set; }
        public int total { get; set; }
        public Time time { get; set; }
    }
}
