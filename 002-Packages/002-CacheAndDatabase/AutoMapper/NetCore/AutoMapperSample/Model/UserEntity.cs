namespace AutoMapperSample.Model
{
    public class UserEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public string IsEnable { get; set; } = EnableTypes.Enabled;

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }

    public static class EnableTypes
    {
        public const string Enabled = "Y";

        public const string Disabled = "N";
    }
}
