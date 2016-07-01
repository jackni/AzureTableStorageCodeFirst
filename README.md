# EF Code First Style Context for Azure Table Storage

## How to use it: (I use swagger ui for quick test)
<p>
<pre><code class='language-cs'>
public class ToDoItem : TableEntity
{
    public string Name { get; set; }

    public bool IsComplete { get; set; }
}

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
</code></pre>
</p>



<p>
<pre>
<code class='language-cs'>
public class ToDoController : ApiController
    {
        [HttpPost]
        [ValidationActionFilter]
        public async Task<IHttpActionResult> AddToDoItem(ToDoItem todoItem)
        {
            try
            {
                using (var context = new ToDoContext())
                {
                    await context.ToDoItems.AddAsync(todoItem);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                throw new ApiException(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetToDoItems()
        {
            try
            {
                using (var context = new ToDoContext())
                {
                    var result = context.ToDoItems.Find(i=>i.PartitionKey== "my test partionKey");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                throw new ApiException(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
</code>
</pre>
</p>



