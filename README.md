# EF Code First Style Context for Azure Table Storage

## How to use it: (I use swagger ui for quick test)

<pre><code class='language-cs'>
public class ToDoItem : TableEntity
{
    public string Name { get; set; }

    public bool IsComplete { get; set; }
}
</code></pre>


