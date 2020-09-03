using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnitJsonValidator;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Net;
using Ardalis.GuardClauses;
namespace Validations.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HALValidationsController : ControllerBase
    {
        private readonly ILogger<HALValidationsController> _logger;
        private readonly IConfiguration _cfg;

        public HALValidationsController(ILogger<HALValidationsController> logger,IConfiguration cfg)
        {
            _logger = logger;
            _cfg = cfg;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ValidationRequest request)
        {
            HttpClient client = null;
            ValidateResponse validateResponse = null;
            try
            {
                var path = request.URL;
                Guard.Against.Null(path, nameof(path));
                client = new HttpClient();
                
                string schemaPath = Path.Combine($"{AppDomain.CurrentDomain.BaseDirectory}/schema", _cfg.GetValue<string>("SchemaName"));
                client.BaseAddress = new Uri(path);
                client.DefaultRequestHeaders.Accept.Clear();
                foreach (string key in request.Headers.Keys)
                {
                    if (key.Trim() == "Authorization")
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.Headers[key]);
                    }
                    else
                    {

                        client.DefaultRequestHeaders.Add(key, request.Headers[key]);
                    }
                }
                client.Timeout = TimeSpan.FromSeconds(_cfg.GetValue<int>("TimeOut"));
                client.DefaultRequestHeaders.Accept.Add(
                   new MediaTypeWithQualityHeaderValue("application/json"));

                string jsontovalidate = "";
                HttpResponseMessage response = null;
        
                response = await client.GetAsync(path);

                if (response.IsSuccessStatusCode)
                {
                    jsontovalidate = await response.Content.ReadAsStringAsync();

                    string validjsonschema = System.IO.File.ReadAllText(schemaPath);
                    JsonSchemaValidator schemaController = new JsonSchemaValidator();
                    ValidateRequest validateRequest = new ValidateRequest();
                    validateRequest.Json = jsontovalidate;
                    validateRequest.Schema = validjsonschema;
                    validateResponse = schemaController.Validate(validateRequest);
                }
                else
                {
                    validateResponse = new ValidateResponse();
                    validateResponse.Valid = false;
                    Guard.Against.Null(response, nameof(response));
                    if (response!=null)
                    {
                        validateResponse.Validations = new List<string>();
                        validateResponse.Validations.Add("Status from API Call=>" +response.StatusCode.ToString());
                    }

                }

            }
            catch(Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
            finally
            {
                if (client != null) { client.Dispose(); }
            }
            return Ok(validateResponse);
        }

    }
}
