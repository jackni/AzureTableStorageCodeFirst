using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Example.Models
{
    public class ToDoItem : TableEntity
    {
        public string Name { get; set; }

        public bool IsComplete { get; set; }
    }
}