﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoskusCiv2.Enums;

namespace PoskusCiv2.Techs
{
    internal class Computers : BaseTech
    {
        public Computers() : base(4, 1, TechType.Miniaturiz, TechType.MassProd, 3, 4)
        {
            Type = TechType.Computers;
            Name = "Computers";
        }
    }
}
