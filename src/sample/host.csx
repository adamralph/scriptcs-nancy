// to run this sample, execute:
//   scriptcs -install
//   scriptcs host1.csx
// or, just copy and paste the line of code into REPL :-)

Require<NancyPack>().Get("/", _ => "Hello world").Host();
// Require<NancyPack>().Get(g => g["/"] = _ => "Hello world").Host();
// Require<NancyPack>().Module(m => m.Get["/"] = _ => "Hello world").Host();
