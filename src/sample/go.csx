// to run this sample, execute:
// scriptcs -install
// scriptcs go.csx

var n = Require<NancyPack>();
n.Go();

public class IndexModule : NancyModule
{
    public IndexModule()
    {
        Get["/"] = _ =>	View["index"]; // located in views folder
    }
}

public class HelloModule : NancyModule
{
    public HelloModule()
    {
        Get["/hello"] = _ => "Hello World!";
    }
}
