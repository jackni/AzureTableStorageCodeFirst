using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureTableStorage.Context
{
    public class TableContext : IDisposable
    {
        #region Members

        private CloudTableClient _tableClient;
        public CloudTableClient TableClient { get { return this._tableClient; } }
        #endregion

        #region Constructor
        public TableContext()
        { }

        public TableContext(CloudStorageAccount storageaccount, StorageCredentials credentials)
        {
            _tableClient = storageaccount.CreateCloudTableClient();
            TableCreating(_tableClient);
        }

        public TableContext(string connectionString)
        {

            var connection = ConfigurationManager.ConnectionStrings[connectionString].ToString();
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connection);
            _tableClient = storageAccount.CreateCloudTableClient();
        }
        #endregion

        #region Methods
        public virtual void TableCreating(CloudTableClient tableClient)
        {
            //here like EF model builder. we create table entity init here

        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~TableContext()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
