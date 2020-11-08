using JsonRPC;
using Newtonsoft.Json;

namespace KerioConnect
{
    public class TypedResponse<TType> : Response where TType : class
    {
        /// <summary>The result if no error occurred.</summary>
        [JsonProperty("result", Required = Required.Default)]
        public TType Result;

        public bool ShouldSerializeResult()
        {
            return this.Result != null;
        }
    }
}
