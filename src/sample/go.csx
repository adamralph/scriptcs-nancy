// to run this sample, execute:
//   scriptcs -install
//   scriptcs
//   #load "go.csx"
// or, just copy and paste the line of code into REPL :-)

var n = Require<NancyPack>().Get("/", _ => "Hello world").Go().Browse();
