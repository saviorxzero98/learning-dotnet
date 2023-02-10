namespace DatabaseManager.Entities
{
    public class FlowData
    {
        public const string TableName = "Flow";

        public string Id { get; set; }

        public int Version { get; set; }

        public string Content { get; set; }
    }
}
