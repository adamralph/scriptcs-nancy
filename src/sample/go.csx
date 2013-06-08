// to run this sample, execute:
// scriptcs -install
// scriptcs
// #load "go.csx"

var n = Require<NancyPack>();
n.Go();
System.Diagnostics.Process.Start("http://localhost:8888/");

public class IndexModule : NancyModule
{
    public IndexModule()
    {
        Get["/"] = _ =>	View["index"]; // located in views folder
    }
}

public class HelloModule : NancyModule
{
    public HelloModule(IGreeter greeter)
    {
        Get["/hello"] = _ => greeter.Greeting;
    }
}

public interface IGreeter
{
    string Greeting { get; }
}

public class Greeter : IGreeter
{
    private int count;

    public string Greeting
    {
        get { return "Hello World! We've said hello " + (++count).ToString() + " time(s)." ; }
    }
}
