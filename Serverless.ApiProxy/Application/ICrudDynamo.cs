using Serverless.ApiProxy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serverless.ApiProxy.Application
{
    public interface ICrudDynamo
    {
        public Task<List<string>> ScanReadingListAsync();

        public Task PutItemAsync(Model3d model3D);
    }
}
