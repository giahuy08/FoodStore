namespace FoodStoreAPI.ViewModel.Mail
{
    public class EmailSettingViewModel
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
    }
}
