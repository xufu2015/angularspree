﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class Collection<T> : Resource
    {
        public T[] Value { get; set; }
    }
}
