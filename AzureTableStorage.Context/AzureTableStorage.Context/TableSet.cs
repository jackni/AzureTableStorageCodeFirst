using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AzureTableStorage.Context
{
    public class TableSet<T> where T : TableEntity, new()
    {
        #region Members

        private string _partitionKey;
        private string _tableName;
        private string _connectionString;
        public string TableName
        {
            get { return _tableName; }
            private set { this._tableName = value; }
        }

        public string PartitionKey
        {
            get { return _partitionKey; }
            set { this._partitionKey = value; }
        }

        internal CloudTableClient tableClient;
        internal CloudTable table;
        public CloudTable Table
        {
            get { return this.table; }
            set { table = value; }
        }

        #endregion

        #region Constructors
        public TableSet()
        {

        }

        #endregion

        #region Methods
        public virtual IEnumerable<T> GetAll()
        {
          
            try
            {
                TableQuery<T> query = new TableQuery<T>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "dorbleMVPClient_dorbleDeviceId"));
                var result = table.ExecuteQuery(query);
                //var result = table.ExecuteQuery(query, typeResolver, null, null);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> result;
            var query = table.CreateQuery<T>().AsQueryable().Where(predicate).Take(10000);
            try
            {

                result = query.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public virtual void Add(T addEntity)
        {
            try
            {
                TableOperation insertOperation = TableOperation.Insert(addEntity);
                table.Execute(insertOperation);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual void AddRange(IEnumerable<T> addEntities)
        {
            try
            {
                TableBatchOperation batchOperation = new TableBatchOperation();
                foreach (var add in addEntities)
                {
                    batchOperation.Insert(add);
                }
                // Execute the batch operation.
                table.ExecuteBatch(batchOperation);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual void Update(T updateEntity)
        {
            // Create a retrieve operation  entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<T>(updateEntity.PartitionKey, updateEntity.RowKey);
            try
            {
                // Execute the operation.
                TableResult retrievedResult = table.Execute(retrieveOperation);
                T update = retrievedResult.Result as T;
                if (update != null)
                {
                    update = updateEntity;
                    // Create the Replace TableOperation.
                    TableOperation updateOperation = TableOperation.Replace(update);

                    table.Execute(updateOperation);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public virtual void Delete(T deleteEntity)
        {
            try
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                table.Execute(deleteOperation);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual async Task AddAsync(T addEntity)
        {
            try
            {
                TableOperation insertOperation = TableOperation.Insert(addEntity);
                await table.ExecuteAsync(insertOperation);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task AddRangeAsync(IEnumerable<T> addEntities)
        {
            try
            {
                TableBatchOperation batchOperation = new TableBatchOperation();
                foreach (var add in addEntities)
                {
                    batchOperation.Insert(add);
                }
                // Execute the batch operation.
                await table.ExecuteBatchAsync(batchOperation);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual async Task UpdateAsync(T updateEntity)
        {
            // Create a retrieve operation  entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<T>(updateEntity.PartitionKey, updateEntity.RowKey);
            try
            {
                // Execute the operation.
                TableResult retrievedResult = table.Execute(retrieveOperation);
                T update = retrievedResult.Result as T;
                if (update != null)
                {
                    update = updateEntity;
                    // Create the Replace TableOperation.
                    TableOperation updateOperation = TableOperation.Replace(update);

                    await table.ExecuteAsync(updateOperation);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public virtual async Task DeleteAsync(T deleteEntity)
        {
            try
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                await table.ExecuteAsync(deleteOperation);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
