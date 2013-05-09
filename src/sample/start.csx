// to run this sample, execute:
// scriptcs -install
// scriptcs start.csx

Require<NancyPack>().Host();

public class IndexModule : NancyModule
{
	public IndexModule()
	{
		Get["/"] = _ =>
        {
			return View["index"]; // in views folder
		};
	}
}

public class HelloModule : NancyModule
{
	public HelloModule()
	{
		Get["/hello"] = _ =>
        {
			return "Hello World!";
		};
	}
}