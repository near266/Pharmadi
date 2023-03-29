// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Redis.Configurations
{
    public class RedisConfig
    {
        public string Configuration { get; set; }
        public string InstanceName { get; set; }
        public bool RedisEnabled { get; set; }
        public string RedisKey { get; set; }
    }
}
