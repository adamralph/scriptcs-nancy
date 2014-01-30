require 'albacore'
require 'fileutils'

version = IO.read("src/ScriptCs.Nancy/Properties/AssemblyInfo.cs").split(/AssemblyInformationalVersion\("/, 2)[1].split(/"/).first
nuget_command = "src/packages/NuGet.CommandLine.2.8.0/tools/NuGet.exe"
solution = "src/ScriptCs.Nancy.sln"
output = "bin"
nuspec = "src/ScriptCs.Nancy.nuspec"

Albacore.configure do |config|
  config.log_level = :verbose
end

desc "Execute default tasks"
task :default => [ :pack ]

desc "Restore NuGet packages"
exec :restore do |cmd|
  cmd.command = nuget_command
  cmd.parameters "restore #{solution}"
end

desc "Clean solution"
msbuild :clean do |msb|
  FileUtils.rmtree output
  msb.properties = { :configuration => :Release }
  msb.targets = [ :Clean ]
  msb.solution = solution
end

desc "Build solution"
msbuild :build => [:clean, :restore] do |msb|
  msb.properties = { :configuration => :Release }
  msb.targets = [ :Build ]
  msb.solution = solution
end

desc "create the nuget package"
exec :pack => [:build] do |cmd|
  FileUtils.mkpath output
  cmd.command = nuget_command
  cmd.parameters "pack " + nuspec + " -Version " + version + " -OutputDirectory " + output
end
