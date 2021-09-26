using Moq;
using ShopManagement.Domain.CourseAgg;
using ShopManagement.Infrastructure.EfCore.Repository;
using Xunit;

namespace ShiftCoder.Test.Repositories
{
   public class CourseRepositoryTest
    {
        [Fact]

        public void GetAllUnitTest()
        {
            var mackRepo = new Mock<ICourseRepository>();
            mackRepo.Setup(x=>x.GetAll()).Returns(CourseRepository)
        }
    }
}
