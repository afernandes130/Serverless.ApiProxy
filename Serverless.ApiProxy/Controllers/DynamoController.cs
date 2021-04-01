using Amazon.DynamoDBv2.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serverless.ApiProxy.Models;
using Serverless.ApiProxy.Application;

namespace Serverless.ApiProxy.Controllers
{
    [Route("modelos3d/api/[controller]")]
    public class DynamoController : ControllerBase
    {
        private readonly ICrudDynamo crudDynamo;

        public DynamoController(ICrudDynamo crudDynamo)
        {
            this.crudDynamo = crudDynamo;
        }

        [HttpGet]
        public async  Task<IActionResult> Get()
        {
           return  Ok(await  crudDynamo.ScanReadingListAsync());
        }

        [HttpPost("addmodel")]
        public async Task<IActionResult> AddModel()
        {

            var config = new ModelConfig() 
            {
                ExtensoesValidas = "csv,pdf",
                FormatoArquivo = "XPTO",
                TamanhoTela = "720X480"
            };

            var data = new ModelData()
            {
                Ano = "2020",
                Modelo = "Premium",
                Tamanho = "Grande"
            };
            var model3D = new Model3d()
            {
                FileName = $"file{new Random().Next()}",
                ModelConfig = config,
                ModelData = data
            };

            await crudDynamo.PutItemAsync(model3D);
            return Ok();



        }

    }
}
