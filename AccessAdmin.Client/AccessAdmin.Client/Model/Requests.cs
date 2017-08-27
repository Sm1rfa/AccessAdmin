using Newtonsoft.Json;

namespace AccessAdmin.Client.Model
{
    public class Requests
    {
        [JsonProperty("requestId")]
        public int RequestId { get; set; }

        [JsonProperty("requestReason")]
        public string RequestReason { get; set; }

        [JsonProperty("decisionReason")]
        public string DecisionReason { get; set; }

        [JsonProperty("userId")]
        public User UserId { get; set; }

        [JsonProperty("systemId")]
        public Systems SystemId { get; set; }

        [JsonProperty("roleId")]
        public Roles RoleId { get; set; }

        [JsonProperty("requestStateId")]
        public RequestStates RequestStateId { get; set; }
    }
}
