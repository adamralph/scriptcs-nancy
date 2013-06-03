require 'albacore'
require 'fileutils'

nuget_command = "src/.nuget/NuGet.exe"
solution = "src/ScriptCs.Nancy.sln"
output = "bin"
nuspec = "src/ScriptCs.Nancy.nuspec"

Albacore.configure do |config|
  config.log_level = :verbose
end

desc "Execute default tasks"
task :default => [ :pack ]

desc "Clean solution"
msbuild :clean do |msb|
  FileUtils.rmtree output
  msb.properties = { :configuration => :Release }
  msb.targets = [ :Clean ]
  msb.solution = solution
end

desc "Build solution"
msbuild :build => [:clean] do |msb|
  msb.properties = { :configuration => :Release }
  msb.targets = [ :Build ]
  msb.solution = solution
end

desc "create the nuget package"
exec :pack => [:build] do |cmd|
  FileUtils.mkpath output
  cmd.command = nuget_command
  cmd.parameters "pack " + nuspec + " -OutputDirectory " + output
end
