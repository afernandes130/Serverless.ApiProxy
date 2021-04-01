using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Serverless.ApiProxy.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using Amazon.Runtime.CredentialManagement;

namespace Serverless.ApiProxy.Application
{
    public class CrudDynamo : ICrudDynamo
    {
        private readonly IAmazonDynamoDB client;

        public CrudDynamo(IAmazonDynamoDB client)
        {
            this.client = client;
        }

        public async Task<List<string>> ScanReadingListAsync()
        {

            //All
            var retorno = new List<string>();
            //var request = new ScanRequest
            //{
            //    TableName = "Volvo3D",

            //};

            //var response = client.ScanAsync(request);
            //var result = response.Result;

            //foreach (Dictionary<string, AttributeValue> item in result.Items)
            //{

            //    // Process the result.
            //    retorno.Add(item["configjson"].S);
            //}
            //return await Task.FromResult(retorno);

            //Query
            var request = new QueryRequest
            {
                TableName = "Volvo3D",
                KeyConditions = new Dictionary<string, Condition>
                {
                    { "keyFile", new Condition()
                    {
                        ComparisonOperator = ComparisonOperator.EQ,
                        AttributeValueList = new List<AttributeValue>
                        {
                            new AttributeValue { S = "file1" }
                        }
                    }
                    }
                },
                ProjectionExpression = "configjson",
            };




            var response = await client.QueryAsync(request);
            foreach (var item in response.Items)
            {
                retorno.Add(item["configjson"].S);
            }
            
            return await Task.FromResult(retorno);

        }

        public async Task PutItemAsync(Model3d model3D)
        {
            Dictionary<string, AttributeValue> attributes = new Dictionary<string, AttributeValue>();
            attributes["keyFile"] = new AttributeValue { S = model3D.FileName };
            attributes["configjson"] = new AttributeValue { S = System.Text.Json.JsonSerializer.Serialize(model3D.ModelConfig) };
            attributes["datajson"] = new AttributeValue { S = System.Text.Json.JsonSerializer.Serialize(model3D.ModelData) };

            // Create PutItem request
            PutItemRequest request = new PutItemRequest
            {
                TableName = "Volvo3D",
                Item = attributes
            };

            // Issue PutItem request
            await client.PutItemAsync(request);
        }

    }
}
