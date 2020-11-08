using System.Text;
using JsonRPC;
using Newtonsoft.Json;

namespace KerioConnect
{
    internal class KerioJsonClient : Client
    {
        public KerioJsonClient(string baseUrl)
            : base(baseUrl)
        {
        }

        public string Token { get; private set; }

        /// <summary>Perform a remote procedure call</summary>
        public override TResponse Rpc<TResponse>(Request request)
        {
            string requestSerialized = JsonConvert.SerializeObject(request);
            byte[] requestBinary = Encoding.UTF8.GetBytes(requestSerialized);
            byte[] resultBinary;

            lock (this.webClient)
            {
                if (!string.IsNullOrWhiteSpace(this.Token))
                {
                    this.webClient.Headers["X-Token"] = this.Token;
                }
                else
                {
                    this.webClient.Headers.Remove("X-Token");
                }

                resultBinary = this.webClient.UploadData(this.url, "POST", requestBinary);
            }

            string resultSerialized = Encoding.UTF8.GetString(resultBinary);
            var response = JsonConvert.DeserializeObject<TResponse>(resultSerialized);

            var genericResponse = response as GenericResponse;
            if (genericResponse != null && genericResponse.Result != null && genericResponse.Result.Value<string>("token") != null)
            {
                this.Token = genericResponse.Result.Value<string>("token");
            }

            return response;
        }

        public GenericResponse Upload(string url, string type, byte[] data)
        {
            byte[] resultBinary;

            lock (this.webClient)
            {
                if (!string.IsNullOrWhiteSpace(this.Token))
                {
                    this.webClient.Headers["X-Token"] = this.Token;
                }
                else
                {
                    this.webClient.Headers.Remove("X-Token");
                }

                var cType = this.webClient.Headers["Content-Type"];
                this.webClient.Headers["Content-Type"] = type;
                resultBinary = this.webClient.UploadData(this.url + url, "POST", data);
                this.webClient.Headers["Content-Type"] = cType;
            }

            string resultSerialized = Encoding.UTF8.GetString(resultBinary);
            var response = JsonConvert.DeserializeObject<GenericResponse>(resultSerialized);

            if (response != null && response.Result != null && response.Result.Value<string>("token") != null)
            {
                this.Token = response.Result.Value<string>("token");
            }

            return response;
        }
    }
}
