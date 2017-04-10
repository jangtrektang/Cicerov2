﻿using Cicero.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cicero.Core.Factories
{
    public interface IClientFactory
    {
        Client CreateClient(string id, string secret, string name, ApplicationTypes applicationType, bool active, int refreshTokenLifeTime, string allowedOrigin);
    }
}
