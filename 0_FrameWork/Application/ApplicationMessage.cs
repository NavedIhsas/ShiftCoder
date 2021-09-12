namespace _0_FrameWork.Application
{
    public class ApplicationMessage
    {
        public const string DuplicatedRecord = "ثبت رکورد تکراری مجاز نمیباشد، مجدداً تلاش کنید. ";
        public const string RecordNotFount = " هیچ رکوردی با این مشخصات یافت نشد.";
        public const string PasswordNotFount = "رمز عبور فعلی شما درست نمیباشد";
        public const string LoginError = "رمز عبور ویا ایمیل شما درست نمیباشد";
        public const string ExistUserCourse = "شما این دوره را قبلا خریداری کرده اید.";

    }

    public class Validate
    {
        public const string Required = "پر کردن این فیلد اجباری میباشد";
        public const string MaxLength = "کاراکتر های {0} بیشتر از حد مجاز {1} میباشد";
    }

    public class CommentType
    {
        public const int Course = 1;
        public const int Article = 2;
    }
}
