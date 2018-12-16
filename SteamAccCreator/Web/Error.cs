namespace SteamAccCreator.Web
{
    public class Error
    {
        public static string WRONG_CAPTCHA = "Wrong captcha";
        public static string HTTP_FAILED = "HTTP Request failed";
        public static string EMAIL_ERROR = "Email error";
        public static string SIMILIAR_MAIL = "This e-mail address must be different from your own.";
        public static string INVALID_MAIL = "Please enter a valid email address.";
        public static string TRASH_MAIL =
                "It appears you've entered a disposable email address, or are using an email provider that cannot be used on Steam. " +
                "Please provide a different email address.";

        public static string UNKNOWN = "An unknown error occured";
        public static string REGISTRATION = "There was an error with your registration, please try again.";
        public static string TIMEOUT = "You've waited too long to verify your email. Please try creating your account and verifying your email again.";
        public static string MAIL_UNVERIFIED = "Mail not verified";
        public static string ALIAS_UNAVAILABLE = "Alias already in use";
        public static string PASSWORD_UNSAFE = "Password not safe enough";

    }
}