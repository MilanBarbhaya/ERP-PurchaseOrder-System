using ERP.Application.Interfaces;
using ERP.Domain.Entities;
using ERP.Domain.Enums;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repositories
{
    public class PurchaseOrderRepository
     : IPurchaseOrderRepository
    {
        private readonly IMongoCollection<PurchaseOrder>
            _collection;

        public PurchaseOrderRepository(
            IMongoDatabase database)
        {
            _collection =
                database.GetCollection<PurchaseOrder>(
                    "PurchaseOrders");
        }

        public async Task CreateAsync(
            PurchaseOrder po)
        {
            var indexKeys = Builders<PurchaseOrder>.IndexKeys.Ascending(x => x.PoNumber);

            await _collection.Indexes.CreateOneAsync(
                new CreateIndexModel<PurchaseOrder>(
                    indexKeys));
            await _collection.InsertOneAsync(po);
        }

        public async Task<List<PurchaseOrder>>
            GetAllAsync()
        {
            return await _collection
                .Find(_ => true)
                .ToListAsync();
        }

        public async Task<PurchaseOrder?>
            GetByIdAsync(string id)
        {
            return await _collection
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(
            PurchaseOrder po)
        {
            await _collection.ReplaceOneAsync(
                x => x.Id == po.Id,
                po);
        }

        public async Task<List<PurchaseOrder>>
    GetFilteredAsync(
        string? status,
        string? search)
        {
            var filter = Builders<PurchaseOrder>.Filter.Empty;

            if (!string.IsNullOrWhiteSpace(status))
            {
                if (Enum.TryParse<PurchaseOrderStatus>(
                    status,
                    true,
                    out var poStatus))
                {
                    filter &=
                        Builders<PurchaseOrder>.Filter
                            .Eq(x => x.Status, poStatus);
                }
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                filter &=
                    Builders<PurchaseOrder>.Filter.Or(
                        Builders<PurchaseOrder>.Filter.Regex(
                            x => x.PoNumber,
                            new MongoDB.Bson.BsonRegularExpression(search, "i")),
                        Builders<PurchaseOrder>.Filter.Regex(
                            x => x.VendorName,
                            new MongoDB.Bson.BsonRegularExpression(search, "i"))
                    );
            }

            return await _collection
                .Find(filter)
                .ToListAsync();
        }
    }
}
