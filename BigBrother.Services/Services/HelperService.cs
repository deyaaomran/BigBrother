﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBrother.Services.Services
{
    public static class HelperService
    {
        public static string GenerateCode()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString(); // كود مكون من 6 أرقام
        }
    }
}
