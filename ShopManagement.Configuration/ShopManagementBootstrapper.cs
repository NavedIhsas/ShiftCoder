using Microsoft.Extensions.DependencyInjection;
using Shop.Management.Application.Contract.CourseGroup;
using ShopManagement.Application;
using ShopManagement.Infrastructure.EfCore.Repository;
using System;
using ShopManagement.Domain.CourseGroupAgg;
using ShopManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using Shop.Management.Application.Contract.Course;
using Shop.Management.Application.Contract.CourseLevel;
using Shop.Management.Application.Contract.CourseStatus;
using ShopManagement.Domain.CourseAgg;
using ShopManagement.Domain.CourseLevelAgg;
using ShopManagement.Domain.CourseStatusAgg;
using ShiftCoderQuery.Contract.Course;
using ShiftCoderQuery.Query;

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




            service.AddDbContext<ShopContext>(option =>
            {
                option.UseSqlServer(connection);
            });
        }
    }
}
