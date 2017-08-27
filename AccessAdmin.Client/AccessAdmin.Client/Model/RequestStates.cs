using Newtonsoft.Json;

namespace AccessAdmin.Client.Model
{
    public class RequestStates
    {
        [JsonProperty("requestId")]
        public int RequestId { get; set; }

        [JsonProperty("requestName")]
        public string RequestName { get; set; }

        [JsonProperty("requestState")]
        public string RequestState { get; set; }
    }
}
