using ERP.Application.Interfaces;
using ERP.Domain.Common;
using ERP.Domain.Entities;

namespace ERP.Infrastructure.Persistence;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        // Procurement Officer

        var procurementUser =
            await userRepository.GetByUsernameAsync("procurement");

        if (procurementUser == null)
        {
            await userRepository.CreateAsync(new AppUser
            {
                Username = "procurement",
                PasswordHash = passwordHasher.Hash("Procurement@123"),
                Role = Roles.ProcurementOfficer
            });
        }

        // Finance Manager

        var financeUser =
            await userRepository.GetByUsernameAsync("finance");

        if (financeUser == null)
        {
            await userRepository.CreateAsync(new AppUser
            {
                Username = "finance",
                PasswordHash = passwordHasher.Hash("Finance@123"),
                Role = Roles.FinanceManager
            });
        }
    }
}