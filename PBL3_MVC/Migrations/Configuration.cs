﻿namespace PBL3_MVC.Migrations
{
    using PBL3_MVC.Data.Tables;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PBL3_MVC.Data.Db>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PBL3_MVC.Data.Db context)
        {
            
            context.Roles.AddRange(new Role[]
            {
                new Role() {RoleID = 1, RoleName = "Admin"},
                new Role() {RoleID = 2, RoleName = "BusStation"},
            });

            context.Locations.AddRange(new Location[]
            {
                new Location() { LocationID = 1, LocationName = "Hà Nội" },
                new Location() { LocationID = 2, LocationName = "Hồ Chí Minh" },
                new Location() { LocationID = 3, LocationName = "Hải Phòng" },
                new Location() { LocationID = 4, LocationName = "Đà Nẵng" },
                new Location() { LocationID = 5, LocationName = "Cần Thơ" },
                new Location() { LocationID = 6, LocationName = "An Giang" },
                new Location() { LocationID = 7, LocationName = "Bà Rịa - Vũng Tàu" },
                new Location() { LocationID = 8, LocationName = "Bạc Liêu" },
                new Location() { LocationID = 9, LocationName = "Bắc Giang" },
                new Location() { LocationID = 10, LocationName = "Bắc Kạn" },
                new Location() { LocationID = 11, LocationName = "Bắc Ninh" },
                new Location() { LocationID = 12, LocationName = "Bến Tre" },
                new Location() { LocationID = 13, LocationName = "Bình Định" },
                new Location() { LocationID = 14, LocationName = "Bình Dương" },
                new Location() { LocationID = 15, LocationName = "Bình Phước" },
                new Location() { LocationID = 16, LocationName = "Bình Thuận" },
                new Location() { LocationID = 17, LocationName = "Cà Mau" },
                new Location() { LocationID = 18, LocationName = "Cao Bằng" },
                new Location() { LocationID = 19, LocationName = "Đắk Lắk" },
                new Location() { LocationID = 20, LocationName = "Đắk Nông" },
                new Location() { LocationID = 21, LocationName = "Điện Biên" },
                new Location() { LocationID = 22, LocationName = "Đồng Nai" },
                new Location() { LocationID = 23, LocationName = "Đồng Tháp" },
                new Location() { LocationID = 24, LocationName = "Gia Lai" },
                new Location() { LocationID = 25, LocationName = "Hà Giang" },
                new Location() { LocationID = 26, LocationName = "Hà Nam" },
                new Location() { LocationID = 27, LocationName = "Hà Tĩnh" },
                new Location() { LocationID = 28, LocationName = "Hải Dương" },
                new Location() { LocationID = 29, LocationName = "Hậu Giang" },
                new Location() { LocationID = 30, LocationName = "Hòa Bình" },
                new Location() { LocationID = 31, LocationName = "Hưng Yên" },
                new Location() { LocationID = 32, LocationName = "Khánh Hòa" },
                new Location() { LocationID = 33, LocationName = "Kiên Giang" },
                new Location() { LocationID = 34, LocationName = "Kon Tum" },
                new Location() { LocationID = 35, LocationName = "Lai Châu" },
                new Location() { LocationID = 36, LocationName = "Lâm Đồng" },
                new Location() { LocationID = 37, LocationName = "Lạng Sơn" },
                new Location() { LocationID = 38, LocationName = "Lào Cai" },
                new Location() { LocationID = 39, LocationName = "Long An" },
                new Location() { LocationID = 40, LocationName = "Nam Định" },
                new Location() { LocationID = 41, LocationName = "Nghệ An" },
                new Location() { LocationID = 42, LocationName = "Ninh Bình" },
                new Location() { LocationID = 43, LocationName = "Ninh Thuận" },
                new Location() { LocationID = 44, LocationName = "Phú Thọ" },
                new Location() { LocationID = 45, LocationName = "Quảng Bình" },
                new Location() { LocationID = 46, LocationName = "Quảng Nam" },
                new Location() { LocationID = 47, LocationName = "Quảng Ngãi" },
                new Location() { LocationID = 48, LocationName = "Quảng Ninh" },
                new Location() { LocationID = 49, LocationName = "Quảng Trị" },
                new Location() { LocationID = 50, LocationName = "Sóc Trăng" },
                new Location() { LocationID = 51, LocationName = "Sơn La" },
                new Location() { LocationID = 52, LocationName = "Tây Ninh" },
                new Location() { LocationID = 53, LocationName = "Thái Bình" },
                new Location() { LocationID = 54, LocationName = "Thái Nguyên" },
                new Location() { LocationID = 55, LocationName = "Thanh Hóa" },
                new Location() { LocationID = 56, LocationName = "Thừa Thiên Huế" },
                new Location() { LocationID = 57, LocationName = "Tiền Giang" },
                new Location() { LocationID = 58, LocationName = "Trà Vinh" },
                new Location() { LocationID = 59, LocationName = "Tuyên Quang" },
                new Location() { LocationID = 60, LocationName = "Vĩnh Long" },
                new Location() { LocationID = 61, LocationName = "Vĩnh Phúc" },
                new Location() { LocationID = 62, LocationName = "Yên Bái" },
                new Location() { LocationID = 63, LocationName = "Phú Yên" }
            });

            context.Accounts.Add(new Account() {UserName = "admin", Password = "c4ca4238a0b923820dcc509a6f75849b", RoleID = 1 });

            context.SaveChanges();
            
        }
    }
}
