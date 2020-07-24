using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Models
{
    public static class Utility
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
