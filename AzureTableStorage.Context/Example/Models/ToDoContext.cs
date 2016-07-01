using AzureTableStorage.Context;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Example.Models
{
    public class ToDoContext : TableContext
    {
        #region Members

        public TableSet<ToDoItem> ToDoItems { get; set; }

        #endregion

        #region Constructors
        public ToDoContext() : base("TableStorageContext")
        {
            TableCreating(base.TableClient);
        }
        #endregion

        #region Methods
        /// <summary>
        /// This is similar to the ModelBuilder
        /// </summary>
        /// <param name="tableClient"></param>
        public override void TableCreating(CloudTableClient tableClient)
        {
            ToDoItems = new TableSet<ToDoItem>();
            var todoTable = tableClient.GetTableReference("ToDoItem");
            ToDoItems.Table = todoTable;
            todoTable.CreateIfNotExists();
        }

        #endregion
    }
}