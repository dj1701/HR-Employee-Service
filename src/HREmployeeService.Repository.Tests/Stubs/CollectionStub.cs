using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HREmployeeService.Repository.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace HREmployeeService.Repository.Tests.Stubs
{
    public class AsyncCursorStub<TProjection> : IAsyncCursor<TProjection>
    {
        private readonly TProjection _projection;
        private readonly int _findAsyncRepeat;

        public AsyncCursorStub(TProjection projection, int findAsyncRepeat)
        {
            _projection = projection;
            _findAsyncRepeat = findAsyncRepeat;
        }

        public void Dispose()
        {
            
        }

        public bool MoveNext(CancellationToken cancellationToken = new CancellationToken())
        {
            return true;
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.FromResult(true);
        }

        IEnumerable<TProjection> IAsyncCursor<TProjection>.Current
        {
            get
            {
                yield return _projection;
            }
        }
    }

    public class CollectionStub : IMongoCollection<EmployeeData>
    {
        private string _id;
        private string _version;
        private object _payload;

        public bool SetFindOneAndUpdateAsyncToReturnNull { get; set; }

        public IAsyncCursor<TResult> Aggregate<TResult>(PipelineDefinition<EmployeeData, TResult> pipeline, AggregateOptions options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IAsyncCursor<TResult>> AggregateAsync<TResult>(PipelineDefinition<EmployeeData, TResult> pipeline, AggregateOptions options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public BulkWriteResult<EmployeeData> BulkWrite(IEnumerable<WriteModel<EmployeeData>> requests, BulkWriteOptions options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<BulkWriteResult<EmployeeData>> BulkWriteAsync(IEnumerable<WriteModel<EmployeeData>> requests, BulkWriteOptions options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public long Count(FilterDefinition<EmployeeData> filter, CountOptions options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync(FilterDefinition<EmployeeData> filter, CountOptions options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public DeleteResult DeleteMany(FilterDefinition<EmployeeData> filter, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<DeleteResult> DeleteManyAsync(FilterDefinition<EmployeeData> filter, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public DeleteResult DeleteOne(FilterDefinition<EmployeeData> filter, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<DeleteResult> DeleteOneAsync(FilterDefinition<EmployeeData> filter, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IAsyncCursor<TField> Distinct<TField>(FieldDefinition<EmployeeData, TField> field, FilterDefinition<EmployeeData> filter, DistinctOptions options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IAsyncCursor<TField>> DistinctAsync<TField>(FieldDefinition<EmployeeData, TField> field, FilterDefinition<EmployeeData> filter, DistinctOptions options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IAsyncCursor<TProjection> FindSync<TProjection>(FilterDefinition<EmployeeData> filter, FindOptions<EmployeeData, TProjection> options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IAsyncCursor<TProjection>> FindAsync<TProjection>(FilterDefinition<EmployeeData> filter, FindOptions<EmployeeData, TProjection> options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var obj = Activator.CreateInstance<TProjection>();
            
            typeof(EmployeeData).GetProperty("Payload").SetValue(obj, _payload.ToString());

            IAsyncCursor<TProjection> asyncCursor = new AsyncCursorStub<TProjection>(obj, 1);

            return Task.FromResult(asyncCursor);
        }

        public TProjection FindOneAndDelete<TProjection>(FilterDefinition<EmployeeData> filter, FindOneAndDeleteOptions<EmployeeData, TProjection> options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<TProjection> FindOneAndDeleteAsync<TProjection>(FilterDefinition<EmployeeData> filter, FindOneAndDeleteOptions<EmployeeData, TProjection> options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public TProjection FindOneAndReplace<TProjection>(FilterDefinition<EmployeeData> filter, EmployeeData replacement,
            FindOneAndReplaceOptions<EmployeeData, TProjection> options = null, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<TProjection> FindOneAndReplaceAsync<TProjection>(FilterDefinition<EmployeeData> filter, EmployeeData replacement,
            FindOneAndReplaceOptions<EmployeeData, TProjection> options = null, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public TProjection FindOneAndUpdate<TProjection>(FilterDefinition<EmployeeData> filter, UpdateDefinition<EmployeeData> update,
            FindOneAndUpdateOptions<EmployeeData, TProjection> options = null, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<TProjection> FindOneAndUpdateAsync<TProjection>(FilterDefinition<EmployeeData> filter, UpdateDefinition<EmployeeData> update,
            FindOneAndUpdateOptions<EmployeeData, TProjection> options = null, CancellationToken cancellationToken = new CancellationToken())
        {
            var obj = Activator.CreateInstance<TProjection>();

            if (SetFindOneAndUpdateAsyncToReturnNull) return Task.FromResult(obj);

            typeof(EmployeeData).GetProperty("Id").SetValue(obj, _id);
            typeof(EmployeeData).GetProperty("Version").SetValue(obj, _version);
            typeof(EmployeeData).GetProperty("Payload").SetValue(obj, _payload.ToString());

            return Task.FromResult(obj);
        }

        public void InsertOne(EmployeeData document, InsertOneOptions options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task InsertOneAsync(EmployeeData document, CancellationToken _cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task InsertOneAsync(EmployeeData document, InsertOneOptions options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            _id = document.Id;
            _version = document.Version;
            _payload = document.Payload;
            return Task.FromResult(0);
        }

        public void InsertMany(IEnumerable<EmployeeData> documents, InsertManyOptions options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task InsertManyAsync(IEnumerable<EmployeeData> documents, InsertManyOptions options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IAsyncCursor<TResult> MapReduce<TResult>(BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<EmployeeData, TResult> options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<IAsyncCursor<TResult>> MapReduceAsync<TResult>(BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<EmployeeData, TResult> options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IFilteredMongoCollection<TDerivedDocument> OfType<TDerivedDocument>() where TDerivedDocument : EmployeeData
        {
            throw new NotImplementedException();
        }

        public IMongoCollection<EmployeeData> WithReadConcern(ReadConcern readConcern)
        {
            throw new NotImplementedException();
        }

        public IMongoCollection<EmployeeData> WithReadPreference(ReadPreference readPreference)
        {
            throw new NotImplementedException();
        }

        public IMongoCollection<EmployeeData> WithWriteConcern(WriteConcern writeConcern)
        {
            throw new NotImplementedException();
        }

        public CollectionNamespace CollectionNamespace { get; }
        public IMongoDatabase Database { get; }
        public IBsonSerializer<EmployeeData> DocumentSerializer { get; }
        public IMongoIndexManager<EmployeeData> Indexes { get; }
        public MongoCollectionSettings Settings { get; }

        public Task<UpdateResult> UpdateOneAsync(FilterDefinition<EmployeeData> filter, UpdateDefinition<EmployeeData> update, UpdateOptions options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateOne(FilterDefinition<EmployeeData> filter, UpdateDefinition<EmployeeData> update, UpdateOptions options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<UpdateResult> UpdateManyAsync(FilterDefinition<EmployeeData> filter, UpdateDefinition<EmployeeData> update, UpdateOptions options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateMany(FilterDefinition<EmployeeData> filter, UpdateDefinition<EmployeeData> update, UpdateOptions options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<EmployeeData> filter, EmployeeData replacement, UpdateOptions options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public ReplaceOneResult ReplaceOne(FilterDefinition<EmployeeData> filter, EmployeeData replacement, UpdateOptions options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}
