using Core.Entities;
using Core.Interfaces;
using Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MongoData
{
    public class MongoTransactionsRepository : IMongoTransactionsRepository
    {

        private readonly IMongoCollection<TransactionDetail> _collection;
        public MongoTransactionsRepository(IOptions<MongoSettings> config)
        {

            var mongoURL = config.Value.ConnectionString;
            var database = config.Value.DatabaseName;
            var client = new MongoClient(mongoURL);
            var mongoDatabase = client.GetDatabase(database);
            _collection = mongoDatabase.GetCollection<TransactionDetail>(config.Value.CollectionName);

        }
        public async Task Delete(int Id)
        {
            await _collection.DeleteOneAsync(x => x.Id == Id);
        }

        public async Task<IReadOnlyList<TransactionDetail>> GetAll()
        {
            var list = new List<TransactionDetail>();
            var coll = await _collection.FindAsync(x => true);
            await coll.ForEachAsync(x => list.Add(x));
            return list;
        }
        public async Task<TransactionDetail> GetEntityById(int Id)
        {
            var item = await _collection.Find(x => x.Id == Id).FirstOrDefaultAsync();
            return item;
        }

        public async Task<TransactionDetail> Save(TransactionDetail transaction)
        {
            await _collection.InsertOneAsync(transaction);
            return transaction;
        }

        public async Task<List<TransactionDetail>> SaveMany(List<TransactionDetail> transactionDetails)
        {
            await _collection.InsertManyAsync(transactionDetails);
            return transactionDetails;
        }

        public async Task Update(int Id, TransactionDetail transaction)
        {
            await _collection.ReplaceOneAsync(x => x.Id == Id, transaction);
        }
    }
}
