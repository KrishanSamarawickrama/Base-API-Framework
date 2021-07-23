using Base.Application.Common;
using Base.Application.Common.Interfaces;
using Base.Domain.Common;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Base.Infrastructure.DataAccess
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    { 
        readonly IMongoCollection<TDocument> Collection;

        public MongoRepository(IMongoDbSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            Collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }        

        private string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                typeof(BsonCollectionAttribute),
                true)
            .FirstOrDefault())?.CollectionName;
        }

        public async virtual Task<GetAdvanceResult<TDocument>> GetAdvance<T>(T PagingAndSorting) where T : PaginationAndSort
        {
            var query = Collection.Find(new BsonDocument());

            SortDefinition<TDocument> sortFilter;
            if (PagingAndSorting.Sorting.SortOrder == SortOrder.Desc)
                sortFilter = Builders<TDocument>.Sort.Descending(PagingAndSorting.Sorting.ColumnName);
            else
                sortFilter = Builders<TDocument>.Sort.Ascending(PagingAndSorting.Sorting.ColumnName);

            query.Sort(sortFilter).Skip((PagingAndSorting.Paging.Page - 1) * PagingAndSorting.Paging.RecordsPerPage)
                .Limit(PagingAndSorting.Paging.RecordsPerPage);

            long count = await Collection.CountDocumentsAsync(new BsonDocument());
            var result = await query.ToListAsync();

            GetAdvanceResult<TDocument> output = new()
            {
                DataCollection = result,
                Pagination = new()
                {
                    RecordCount = count,
                    NoOfPages = (count / PagingAndSorting.Paging.RecordsPerPage),
                    PageNo = PagingAndSorting.Paging.Page,
                    RecordsPerPage = PagingAndSorting.Paging.RecordsPerPage
                }
            };

            return output;
        }

        public async virtual Task<List<TDocument>> GetSimple()
        {
            return await Collection.Find(new BsonDocument()).SortBy(x => x.Id).ToListAsync();
        }

        public virtual IQueryable<TDocument> AsQueryable()
        {
            return Collection.AsQueryable();
        }

        public virtual IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Collection.Find(filterExpression).ToEnumerable();
        }

        public virtual IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression, 
            Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            return Collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        public virtual TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Collection.Find(filterExpression).FirstOrDefault();
        }

        public virtual Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => Collection.Find(filterExpression).FirstOrDefaultAsync());
        }

        public virtual TDocument FindById(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            return Collection.Find(filter).SingleOrDefault();
        }

        public virtual Task<TDocument> FindByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
                return Collection.Find(filter).SingleOrDefaultAsync();
            });
        }

        public virtual void InsertOne(TDocument document)
        {
            Collection.InsertOne(document);
        }

        public virtual Task InsertOneAsync(TDocument document)
        {
            return Task.Run(() => Collection.InsertOneAsync(document));
        }

        public void InsertMany(ICollection<TDocument> documents)
        {
            Collection.InsertMany(documents);
        }


        public virtual async Task InsertManyAsync(ICollection<TDocument> documents)
        {
            await Collection.InsertManyAsync(documents);
        }

        public void ReplaceOne(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            Collection.FindOneAndReplace(filter, document);
        }

        public virtual async Task ReplaceOneAsync(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            await Collection.FindOneAndReplaceAsync(filter, document);
        }

        public void DeleteOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            Collection.FindOneAndDelete(filterExpression);
        }

        public Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => Collection.FindOneAndDeleteAsync(filterExpression));
        }

        public void DeleteById(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            Collection.FindOneAndDelete(filter);
        }

        public Task DeleteByIdAsync(string id)
        {
            return Task.Run(() =>
            {
                var objectId = new ObjectId(id);
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
                Collection.FindOneAndDeleteAsync(filter);
            });
        }

        public void DeleteMany(Expression<Func<TDocument, bool>> filterExpression)
        {
            Collection.DeleteMany(filterExpression);
        }

        public Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => Collection.DeleteManyAsync(filterExpression));
        }

    }
}
