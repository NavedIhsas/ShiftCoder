using AccountManagement.Domain.ProvinceAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EfCore.Mapping
{
   public class ProvinceMapping:IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.HasMany(x => x.Cities).WithOne(x => x.Province).HasForeignKey(x => x.ProvinceId);
            builder.HasData(new Province("انتخاب کنید",1));
            builder.HasData(new Province("کابل", 2));
            builder.HasData(new Province("هرات", 3));
            builder.HasData(new Province("بلخ", 4));
            builder.HasData(new Province("غزنی", 5));
            builder.HasData(new Province("بامیان", 6));
            builder.HasData(new Province("بادغیس", 7));
            builder.HasData(new Province("دایکندی", 8));
            builder.HasData(new Province("ارزگان", 9));
            builder.HasData(new Province("فاریاب", 10));
            builder.HasData(new Province("فراه", 11));
            builder.HasData(new Province("پکتیا", 12));
            builder.HasData(new Province("پکتیکا", 13));
            builder.HasData(new Province("سمنگان", 14));
            builder.HasData(new Province("نورسان", 15));
            builder.HasData(new Province("بدخشان", 16));
            builder.HasData(new Province("نمیروز", 17));
            builder.HasData(new Province("غور", 19));
            builder.HasData(new Province("هلمند", 20));
            builder.HasData(new Province("لوگر", 21));
            builder.HasData(new Province("پروان", 22));
            builder.HasData(new Province("میدان وردک", 23));
            builder.HasData(new Province("بغلان", 24));
            builder.HasData(new Province("پنجشیر", 25));
            builder.HasData(new Province("خوست", 26));
            builder.HasData(new Province("زابل", 27));
            builder.HasData(new Province("سرپل", 28));
            builder.HasData(new Province("کاپیسا", 29));
            builder.HasData(new Province("لغمان", 30));
            builder.HasData(new Province("ننگرهار", 32));
            builder.HasData(new Province("کنر", 33));
            builder.HasData(new Province("کندز", 34));
            builder.HasData(new Province("قندهار", 35));
        }
    }

   public class CityMapping:IEntityTypeConfiguration<City>
   {
       public void Configure(EntityTypeBuilder<City> builder)
       {
           builder.HasKey(x => x.Id);
           builder.Property(x => x.Name).HasMaxLength(70);
           builder.HasOne(x => x.Province).WithMany(x => x.Cities).HasForeignKey(x => x.ProvinceId);
           builder.HasMany(x => x.Accounts).WithOne(x => x.City).HasForeignKey(x => x.CityId);
           builder.HasData(new City(1,1,"انتخاب کنید"));
        }
    }
}
