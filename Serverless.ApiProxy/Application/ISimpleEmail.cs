﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serverless.ApiProxy.Application
{
    public interface ISimpleEmail
    {
        public Task SendEmailAsync();

        public Task SendEmailAttachAsync();
    }
}
