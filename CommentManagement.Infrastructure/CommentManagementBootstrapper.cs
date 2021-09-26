using CommentManagement.Domain.CourseCommentAgg;
using Microsoft.Extensions.DependencyInjection;
using System;
using _0_FrameWork.Domain.Infrastructure;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Infrastructure.EfCore;
using CommentManagement.Infrastructure.EfCore.Repository;
using CommentManagement.Application;
using CommentManagement.Application.Contract.HomePhoto;
using CommentManagement.Domain.HomePageDetailsAgg;
using CommentManagement.Domain.Notification.Agg;
using CommentManagement.Domain.VisitAgg;
using CommentManagement.Infrastructure.Permissions;
using Microsoft.EntityFrameworkCore;
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

            service.AddTransient<IHomePhotoApplication, HomePhotoApplication>();
            service.AddTransient<IHomePhotoRepository, HomePhotoRepository>();

            service.AddDbContext<CommentContext>(option =>
            {
                option.UseSqlServer(connectionString);
            });
        }
    }
}
