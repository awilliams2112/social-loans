namespace SocialLoans.Domain
{
    public class PhoneConfirmResult
    {
        public bool IsSucess { get; set; }
        public bool IsExpired { get; set; }
        public bool InvalidCode { get; set; }
    }

}

