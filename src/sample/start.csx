// to run this sample, execute:
// scriptcs -install
// scriptcs start.csx

Require<NancyPack>().Host(typeof(IndexModule));

public class IndexModule : NancyModule
{
	public IndexModule()
	{
		Get["/"] = x =>
        {
			return View["index"]; // in views folder
		};
	}
}