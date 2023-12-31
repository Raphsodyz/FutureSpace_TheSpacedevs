using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.DTO.Entities
{
    public class RequestLaunchDTO
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public List<LaunchDTO> Results { get; set; }
    }
}