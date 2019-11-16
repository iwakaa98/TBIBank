using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TBIApp.Services.Services.Contracts;

namespace TBIApp.Services.Services
{
    public class DecodeService : IDecodeService
    {
        public async Task<string> DecodeAsync(string message)
        { 
            string codedBody = message.Replace("-", "+");
            codedBody = codedBody.Replace("_", "/");
            byte[] data = Convert.FromBase64String(codedBody);
            var result = Encoding.UTF8.GetString(data);

            return result;
        }
    }
}
