using Newtonsoft.Json;

namespace AccessAdmin.Client.Model
{
    public class Systems
    {
        [JsonProperty("systemId")]
        public int SystemId { get; set; }

        [JsonProperty("systemName")]
        public string SystemName { get; set; }

        [JsonProperty("isDeleted")]
        public string IsDeleted { get; set; }
    }
}
