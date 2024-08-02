namespace Backend.ViewModels
{
    public class AccountViewModel
    {
        public int AccountId {  get; set; }
        public string? Email { get; set; }

        public string? FullName { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Picture_url{ get; set; }
        public string? GoogleId { get; set; }
        
        public string? AccessToken { get; set; }

    }
}
