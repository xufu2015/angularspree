using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public abstract class Resource : Link
    {
        [JsonIgnore]
        public Link Self { get; set; }
    }
}
