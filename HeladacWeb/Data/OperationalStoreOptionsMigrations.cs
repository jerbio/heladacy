﻿using IdentityServer4.EntityFramework.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Data
{
    public class OperationalStoreOptionsMigrations :
  IOptions<OperationalStoreOptions>
    {
        public OperationalStoreOptions Value => new OperationalStoreOptions()
        {
            DeviceFlowCodes = new TableConfiguration("DeviceCodes"),
            EnableTokenCleanup = false,
            PersistedGrants = new TableConfiguration("PersistedGrants"),
            TokenCleanupBatchSize = 100,
            TokenCleanupInterval = 3600,
        };
    }
}
