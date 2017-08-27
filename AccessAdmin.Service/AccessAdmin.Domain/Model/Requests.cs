namespace AccessAdmin.Domain.Model
{
    public class Requests
    {
        public int RequestId { get; set; }
        public string RequestReason { get; set; }
        public string DecisionReason { get; set; }
        public User UserId { get; set; }
        public Systems SystemId { get; set; }
        public Roles RoleId { get; set; }
        public RequestStates RequestStateId { get; set; }
    }
}
