﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistic.ConsoleClient.Models
{
    public interface IEntity<Tid>
    {
        public Tid Id { get; set; }
    }
}
