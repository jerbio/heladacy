using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Models.Responses
{
    public class PaginatedResponse<T>
    {
        public int index { get; set; }
        public List<T> data { get; set; }
    }
}
