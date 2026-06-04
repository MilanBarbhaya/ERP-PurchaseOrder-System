using ERP.Application.Interfaces;
using ERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Seed
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher)
        {
            var procurementUser =
                await userRepository.GetByUsernameAsync(
                    "procurement");

            if (procurementUser == null)
            {
                await userRepository.CreateAsync(
                    new AppUser
                    {
                        Username = "procurement",
                        PasswordHash =
                            passwordHasher.Hash(
                                "Procurement@123"),
                        Role = "ProcurementOfficer"
                    });
            }

            var financeUser =
                await userRepository.GetByUsernameAsync(
                    "finance");

            if (financeUser == null)
            {
                await userRepository.CreateAsync(
                    new AppUser
                    {
                        Username = "finance",
                        PasswordHash =
                            passwordHasher.Hash(
                                "Finance@123"),
                        Role = "FinanceManager"
                    });
            }
        }
    }
}
