require 'albacore'
require 'fileutils'

nuget_command = "src/.nuget/NuGet.exe"

Albacore.configure do |config|
  config.log_level = :verbose
end

desc "Execute default tasks"
task :default => [ :pack ]

desc "Clean solution"
msbuild :clean do |msb|
  FileUtils.rmtree "bin"
  msb.properties = { :configuration => :Release }
  msb.targets = [ :Clean ]
  msb.solution = "src/ScriptCs.Nancy.sln"
end

desc "Build solution"
msbuild :build => [:clean] do |msb|
  msb.properties = { :configuration => :Release }
  msb.targets = [ :Build ]
  msb.solution = "src/ScriptCs.Nancy.sln"
end

desc "create the nuget package"
exec :pack => [:build] do |cmd|
  FileUtils.mkpath "bin"
  cmd.command = nuget_command
  cmd.parameters "pack src/ScriptCs.Nancy.nuspec -OutputDirectory bin"
end
