using Newtonsoft.Json.Schema;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System;

namespace NUnitJsonValidator
{
    public class JsonSchemaValidator
    {
   
        public ValidateResponse Validate(ValidateRequest request)
        {
            // load schema
            JSchema schema = JSchema.Parse(request.Schema);
            JToken json = JToken.Parse(request.Json);
            string errors = "";
            // validate json
            IList<ValidationError> validationerrors;
            IList<string> validations = new List<string>(); ;
            bool valid = json.IsValid(schema, out validationerrors);
            if (!valid)
            {
               
                foreach (ValidationError error in validationerrors)
                {
                    string err= $"{error.Message} Line: {error.LineNumber} Position: {error.LinePosition}";
                    errors += $"{err} {Environment.NewLine}";
                    validations.Add(err);
                }
            }
            // return error messages and line info to the browser
            return new ValidateResponse
            {
                Valid = valid,
                ErrorsList = validationerrors,
                Errors = errors,
                Validations = validations

            };
        }
       
    }
    public class ValidateRequest
    {
        public string Json { get; set; }
        public string Schema { get; set; }
    }

    public class ValidateResponse
    {
        public bool Valid { get; set; }
        public IList<ValidationError> ErrorsList { get; set; }
        public string Errors { get; set; }
        public IList<string> Validations { get; set; }
    }

    public class ValidationRequest
    {
        public string URL { get; set; }
        public Dictionary<string,string> Headers { get; set; }
        public string Method { get; set; }
    }
}