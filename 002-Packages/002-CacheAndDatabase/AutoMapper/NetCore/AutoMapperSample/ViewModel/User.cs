namespace AutoMapperSample.ViewModel
{
    public class User
    {
        public int Id { get; set; }

        public string LoginId { get; set; } = string.Empty;

        public string CreatedDate { get; set; } = string.Empty;

        public string UpdatedDate { get; set; } = string.Empty;

        public bool IsEnable { get; set; }
    }
}
