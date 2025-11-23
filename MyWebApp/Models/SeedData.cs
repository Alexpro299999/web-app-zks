using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyWebApp.Data;
using System;
using System.Linq;

namespace MyWebApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                context.Database.EnsureCreated();

                if (context.Wards.Any() || context.Doctors.Any())
                {
                    return;
                }

                var wards = new Ward[]
                {
                    new Ward { Number = "101", Floor = 1, Capacity = 4, Type = "Общая" },
                    new Ward { Number = "102", Floor = 1, Capacity = 2, Type = "Изолятор" },
                    new Ward { Number = "201", Floor = 2, Capacity = 1, Type = "VIP" },
                    new Ward { Number = "202", Floor = 2, Capacity = 6, Type = "Интенсивная терапия" }
                };
                context.Wards.AddRange(wards);
                context.SaveChanges();

                var doctors = new Doctor[]
                {
                    new Doctor { LastName = "Преображенский", FirstName = "Филипп", MiddleName = "Филиппович", Specialization = "Хирург", CabinetNumber = "305", Schedule = "Пн-Чт: 10:00-16:00" },
                    new Doctor { LastName = "Борменталь", FirstName = "Иван", MiddleName = "Арнольдович", Specialization = "Терапевт", CabinetNumber = "306", Schedule = "Пн-Пт: 09:00-18:00" },
                    new Doctor { LastName = "Ватсон", FirstName = "Джон", MiddleName = "Хэмиш", Specialization = "Военный врач", CabinetNumber = "101", Schedule = "Вт, Чт: 14:00-20:00" },
                    new Doctor { LastName = "Хаус", FirstName = "Грегори", MiddleName = "", Specialization = "Диагност", CabinetNumber = "404", Schedule = "По настроению" }
                };
                context.Doctors.AddRange(doctors);
                context.SaveChanges();

                var patients = new Patient[]
                {
                    new Patient { LastName = "Иванов", FirstName = "Иван", MiddleName = "Иванович", InsuranceNumber = "1234 5678 9012 3456", Phone = "+7 (900) 111-22-33", Address = "ул. Ленина, д. 1", BirthDate = DateTime.Parse("1980-01-01"), WardId = wards[0].Id },
                    new Patient { LastName = "Петров", FirstName = "Петр", MiddleName = "Петрович", InsuranceNumber = "9876 5432 1098 7654", Phone = "+7 (900) 555-44-33", Address = "ул. Гагарина, д. 10", BirthDate = DateTime.Parse("1995-05-15"), WardId = wards[2].Id }
                };
                context.Patients.AddRange(patients);
                context.SaveChanges();
            }
        }
    }
}