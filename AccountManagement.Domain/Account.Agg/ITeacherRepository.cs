using System.Collections.Generic;
using _0_FrameWork.Domain;
using AccountManagement.Application.Contract.Account;

namespace AccountManagement.Domain.Account.Agg
{
    public interface ITeacherRepository : IRepository<long, Teacher>
    {
        EditTeacherViewModel GetTeacherDetails(long id);
        List<TeacherViewModel> GetAllTeachers();
        List<TeacherViewModel> SelectList();
        List<TeacherViewModel> SelectListForArticles();
        Teacher GetTeacherBy(long id);
        Teacher GetBloggerBy(long id);
        void DeleteTeacher(long id);
    }
}