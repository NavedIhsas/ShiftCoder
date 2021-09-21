namespace _0_FrameWork.Application
{
    public class ApplicationMessage
    {
        public const string DuplicatedEmailAddress = "با این ایمیل قبلا ثبت نام کرده اید. ";
        public const string DuplicatedRecord = "ثبت رکورد تکراری مجاز نمیباشد، مجدداً تلاش کنید. ";
        public const string RecordNotFount = " هیچ رکوردی با این مشخصات یافت نشد.";
        public const string PasswordNotFount = "رمز عبور فعلی شما درست نمیباشد";
        public const string CheckEmailForExist = "با یاین ایمیل هیچ کاربری ثبت نام نکرده است.";
        public const string ExistUserCourse = "شما این دوره را قبلا خریداری کرده اید.";

        public const string ResetPasswordEmailSent =
            "ایمیل بازیابی کلمه عبور به ایمیل شما ارسال شد، لطفا جهت ادامه فرایند از طریق ایمیل خود اقدام بفرمائید.";
    }

    public class Validate
    {
        public const string Required = "پر کردن این فیلد اجباری میباشد";
        public const string MaxLength = "کاراکتر های {0} بیشتر از حد مجاز {1} میباشد";
    }

    public class ThisType
    {
        public const int Course = 1;
        public const int Article = 2;
        public const int Comment = 3;
        public const int Account = 4;
        public const int Order = 5;
        public const int Index = 6;
        public const int EnterWallet = 7;
        public const int ExistWallet = 8;
        public const int Teacher = 9;
        public const int Blogger = 10;
        public const int AdminPanelIndex = 11;

        


    }

}
