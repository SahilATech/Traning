using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace custom_api_out_box
{
    public class API_Custom : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {

            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            try
            {
                var AES_IV = context.InputParameters["hrms_vi"].ToString();
                var AES_Key = context.InputParameters["hrms_Key"].ToString();
                var Input = context.InputParameters["hrms_String"].ToString();

                RijndaelManaged aes = new RijndaelManaged();
                aes.KeySize = 128;
                aes.BlockSize = 128;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = Convert.FromBase64String(AES_Key);
                aes.IV = Convert.FromBase64String(AES_IV);
                var decrypt = aes.CreateDecryptor();
                // Decrypt Input
                byte[] xBuff = null;
                using (var ms = new MemoryStream())
                {
                    // Convert from base64 string to byte array, write to memory stream and decrypt, then convert to byte array.
                    using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
                    {
                        byte[] xXml = Convert.FromBase64String(Input);
                        cs.Write(xXml, 0, xXml.Length);
                    }
                    xBuff = ms.ToArray();
                }
                                // Convert from byte array to UTF-8 string then return
                String Output = Encoding.UTF8.GetString(xBuff);
                var json = Output.Split('&').Select(it => it.Split('='));
                context.OutputParameters["hrms_Response"] = Output;
                tracingService.Trace(Output);
            }
            catch (Exception ex)
            {
                tracingService.Trace(ex.Message);
            }
        }
    }
}
