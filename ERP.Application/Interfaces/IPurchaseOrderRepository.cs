using ERP.Domain.Entities;

namespace ERP.Application.Interfaces
{
    public interface IPurchaseOrderRepository
    {
        Task CreateAsync(PurchaseOrder po);

        Task<List<PurchaseOrder>> GetAllAsync();

        Task<PurchaseOrder?> GetByIdAsync(string id);

        Task UpdateAsync(PurchaseOrder po);

        Task<List<PurchaseOrder>> GetFilteredAsync(
    string? status,
    string? search);
    }
}
