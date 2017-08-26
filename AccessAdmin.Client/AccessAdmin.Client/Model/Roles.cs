using Newtonsoft.Json;

namespace AccessAdmin.Client.Model
{
    public class Roles
    {
        [JsonProperty("roleId")]
        public int RoleId { get; set; }

        [JsonProperty("roleName")]
        public string RoleName { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }
    }
}
