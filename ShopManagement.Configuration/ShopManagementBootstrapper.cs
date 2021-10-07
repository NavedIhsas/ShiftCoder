using _0_FrameWork.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShiftCoderQuery.Contract.Course;
using ShiftCoderQuery.Contract.CourseGroup;
using ShiftCoderQuery.Contract.Forum.Question;
using ShiftCoderQuery.Query;
using Shop.Management.Application.Contract.Course;
using Shop.Management.Application.Contract.CourseEpisode;
using Shop.Management.Application.Contract.CourseGroup;
using Shop.Management.Application.Contract.CourseLevel;
using Shop.Management.Application.Contract.CourseStatus;
using Shop.Management.Application.Contract.Order;
using ShopManagement.Application;
using ShopManagement.Configuration.Permission;
using ShopManagement.Domain.CourseAgg;
using ShopManagement.Domain.CourseEpisodeAgg;
using ShopManagement.Domain.CourseGroupAgg;
using ShopManagement.Domain.CourseLevelAgg;
using ShopManagement.Domain.CourseStatusAgg;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.OrderDetailAgg;
using ShopManagement.Infrastructure.EfCore;
using ShopManagement.Infrastructure.EfCore.Repository;

namespace ShopManagement.Configuration
{
    public class ShopManagementBootstrapper
    {
        public static void Configure(IServiceCollection service, string connection)
        {
            service.AddTransient<ICourseGroupApplication, CourseGroupApplication>();
            service.AddTransient<ICourseGroupRepository, CourseGroupRepository>();

            service.AddTransient<ICourseApplication, CourseApplication>();
            service.AddTransient<ICourseRepository, CourseRepository>();

            service.AddTransient<ICourseStatusApplication, CourseStatusApplication>();
            service.AddTransient<ICourseStatusRepository, CourseStatusRepository>();

            service.AddTransient<ICourseLevelApplication, CourseLevelApplication>();
            service.AddTransient<ICourseLevelRepository, CourseLevelRepository>();

            service.AddTransient<ICourseQuery, CourseQuery>();

            service.AddTransient<ICourseGroupQuery, CourseGroupQuery>();


            service.AddTransient<ICourseEpisodeApplication, CourseEpisodeApplication>();
            service.AddTransient<ICourseEpisodeRepository, CourseEpisodeRepository>();

            service.AddTransient<IOrderRepository, OrderRepository>();
            service.AddTransient<IOrderApplication, OrderApplication>();

            service.AddTransient<IOrderDetailRepository, OrderDetailRepository>();

            service.AddTransient<IPermissionExposer, ShopPermissionExposer>();
            service.AddTransient<IQuestionQuery, QuestionQuery>();


            service.AddDbContext<ShopContext>(option => { option.UseSqlServer(connection); });
        }
    }
}