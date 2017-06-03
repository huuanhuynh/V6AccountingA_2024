namespace V6Soft.Models.Accounting.DTO
{
    public class LoginRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Dvcs { get; set; }
        public string SelectedLanguage { get; set; }
        public string SelectedModuleId { get; set; }
    }
}
