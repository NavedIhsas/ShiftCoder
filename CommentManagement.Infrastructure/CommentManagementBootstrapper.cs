using _0_FrameWork.Domain.Infrastructure;
using CommentManagement.Application;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Application.Contract.HomePhoto;
using CommentManagement.Domain.CourseCommentAgg;
using CommentManagement.Domain.Notification.Agg;
using CommentManagement.Domain.SliderAgg;
using CommentManagement.Domain.VisitAgg;
using CommentManagement.Infrastructure.EfCore;
using CommentManagement.Infrastructure.EfCore.Repository;
using CommentManagement.Infrastructure.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShiftCoderQuery.Contract.Comment;
using ShiftCoderQuery.Query;

namespace CommentManagement.Infrastructure
{
    public class CommentManagementBootstrapper
    {
        public static void Configure(IServiceCollection service, string connectionString)
        {
            service.AddTransient<ICommentRepository, CommentRepository>();
            service.AddTransient<ICommentApplication, CommentApplication>();
            service.AddTransient<ICommentQuery, CommandQuery>();

            service.AddTransient<IVisitRepository, VisitRepository>();

            service.AddTransient<INotificationRepository, NotificationRepository>();

            service.AddTransient<IPermissionExposer, CommentPermissionExposer>();

            service.AddTransient<ISliderApplication, SliderApplication>();
            service.AddTransient<ISliderRepository, SliderRepository>();

            service.AddDbContext<CommentContext>(option => { option.UseSqlServer(connectionString); });
        }
    }
}