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

    public static class Permission
    {
        //Dashboard Administration
        public const int AdministrationHomepage = 1;
        public const int AdministrationNotifications = 2;


        //Courses
        public const int ListCourses = 100;
        public const int SearchCourses = 101;
        public const int CreateCourses = 102;
        public const int EditCourses = 103;


        //CourseGroup
        public const int ListCourseGroups = 200;
        public const int SearchCourseGroups = 201;
        public const int CreateCourseGroups = 202;
        public const int EditCourseGroups = 203;
        public const int DeleteCourseGroups = 204;
        public const int RestoreCourseGroups = 205;


        //Articles
        public const int ListArticles = 400;
        public const int SearchArticles = 401;
        public const int CreateArticles = 402;
        public const int EditListArticles = 403;

        //ArticlesGroup
        public const int ListArticleCategories = 500;
        public const int SearchArticleCategories = 501;
        public const int CreateArticleCategories = 502;
        public const int EditArticleCategories = 503;

        //Course Level
        public const int CreateCourseLevel = 600;
        public const int EditCourseLevel = 601;

        //Course Status 
        public const int CreateCourseStatus = 610;
        public const int EditCourseStatus = 611;

        //CourseEpisode
        public const int ListCourseEpisodes = 620;
        public const int CreateCourseEpisodes = 621;
        public const int EditCourseEpisodes = 622;

        //CostumerDiscount
        public const int ListCostumerDiscount = 700;
        public const int SearchCostumerDiscount = 701;
        public const int CreateCostumerDiscount = 702;
        public const int EditCostumerDiscount = 703;
        public const int DeleteCostumerDiscount = 704;
        public const int RestoreCostumerDiscount = 705;


        //ColleagueDiscount
        public const int ListColleagueDiscount = 710;
        public const int SearchColleagueDiscount = 711;
        public const int CreateColleagueDiscount = 712;
        public const int EditColleagueDiscount = 713;
        public const int DeleteColleagueDiscount = 714;
        public const int RestoreColleagueDiscount = 715;

        //DiscountCode
        public const int ListDiscountCode = 720;
        public const int SearchDiscountCode = 721;
        public const int CreateDiscountCode = 722;
        public const int EditDiscountCode = 723;

        //users
        public const int ListUsers = 801;
        public const int SearchUsers = 802;
        public const int CreateUsers = 803;
        public const int EditUsers = 804;
        public const int BlockUsers = 805;
        public const int UnBlockUsers = 806;
        public const int ChangePasswordUsers = 807;
        public const int ListBlockedUsers = 808;

        //Teachers and Blogger
        public const int ListTeacherAndBlogger = 810;
        public const int EditTeacherAndBlogger = 811;
        public const int DeleteTeacherAndBlogger = 811;

        //Roles
        public const int ListRoles = 820;
        public const int CreateRoles = 821;
        public const int EditRoles = 822;

        //Comments
        public const int ListComments =900;
        public const int ApproveComments =901;
        public const int CancelComments =902;
        public const int SearchComments =903;

        //LatestNews
        public const int CreateLatestNews =1010;
        public const int EditLatestNews =1011;

        //Homephoto
        public const int ChangePhotoHomePage = 1020;
        public const int CreatePhotoHomePage = 1021;

        //System Administrator
        public const int SystemAdministratorNotification = 1000;
        public const int SystemAdministratorOrders = 1001;
        public const int SystemAdministratorActivity = 1002;
      
    }

}
