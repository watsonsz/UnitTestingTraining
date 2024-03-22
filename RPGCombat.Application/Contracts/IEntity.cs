﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGCombat.Application.Contracts
{
    public interface IEntity: IBaseClass
    {
        public int Level { get; set; }
        public bool IsAlive { get; set; }
        
    }
}
