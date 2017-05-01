#addin "Cake.XdtTransform"
#addin "Cake.SemVer"
#addin "Cake.Incubator"
#addin "Cake.FileHelpers"
#tool "nuget:?package=xunit.runner.console"

var Target = Argument("target", "Default");
var BuildConfiguration = Argument("configuration", "Release");
var MakePrerelease = !HasArgument("release");
var ProjectName = Argument<string>("project");

var NuGetPackingDir = "./packed/" + ProjectName;
var SolutionFile = GetFiles("./*.sln").FirstOrDefault();
if(SolutionFile == null) throw new Exception("Solution not found");

var solution = ParseSolution(SolutionFile);
var Project = solution.Projects.FirstOrDefault(p => p.Name.Equals(ProjectName, StringComparison.OrdinalIgnoreCase));
var TestProject = solution.Projects.FirstOrDefault(p => p.Name.Equals(ProjectName+".Tests", StringComparison.OrdinalIgnoreCase));

if(Project == null) throw new Exception($"Project {ProjectName} not found");

Task("Clean")
    .Does(() => 
    {
    });

Task("CleanPackingDirectory")
    .Does(() => 
    {
        EnsureDirectoryExists(NuGetPackingDir);
        CleanDirectory(NuGetPackingDir);
    });

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() => {
        NuGetRestore(SolutionFile);
    });

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
    {
        MSBuild(Project.Path, conf => conf.SetConfiguration(BuildConfiguration));

        if(TestProject != null) 
            MSBuild(TestProject.Path, conf => conf.SetConfiguration(BuildConfiguration));
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
    {
        if(TestProject == null)
        {
            Information($"No testproject found for {Project.Name}");
            return;
        }
        
        XUnit2(ParseProject(TestProject.Path, BuildConfiguration).GetAssemblyFilePath().ToString());
    });

Task("Pack")
    .IsDependentOn("CleanPackingDirectory")
    .IsDependentOn("Build")
    .IsDependentOn("Test")
    .Does(() => {
        Information($"Packing: {Project.Name}");
        
        NuGetPack(Project.Path, new NuGetPackSettings
        {
            OutputDirectory = NuGetPackingDir,
            Properties = new Dictionary<string, string> 
            {
                { "Configuration", BuildConfiguration }
            },
            //Authors = new [] { "John Landheer" },
            //Owners =  new [] { "Studiekring B.V. / Roaring Blue" },
            //Copyright = "2010-2017 Roaring Blue",
            Version = GetPackageVersion(Project.Path)
        });
    });

Task("Push")
    .IsDependentOn("Pack")
    .Does(() => {
        var nuPushSettings = new NuGetPushSettings {
            Source = "https://mail.studiekring.nl/nuget/nuget"
        };

        var files = GetFiles(Directory(NuGetPackingDir).Path +"/*.nupkg");
        foreach(var file in files)
        {
            NuGetPush(file.ToString(), nuPushSettings);
        }

        if(!MakePrerelease)
        {
            var newVersion = IncreaseVersion(GetPackageVersion(Project.Path), VersionPart.Patch);
            var assemblyInfoFile = Project.Path.GetDirectory().CombineWithFilePath("./Properties/AssemblyInfo.cs").ToString();
            SetAssemblyInfoVersion(assemblyInfoFile, newVersion);
        }
    });

Task("Default")
    .IsDependentOn("Push")
    .Does(() => {
    });

RunTarget(Target);


string GetPackageVersion(FilePath path)
{
    var assemblyInfo = ParseAssemblyInfo(path.GetDirectory().CombineWithFilePath("./Properties/AssemblyInfo.cs"));
    var versionString = string.Join(".", assemblyInfo.AssemblyVersion.Split('.').Take(3));
    var assemblyVersion = ParseSemVer(versionString);
    var build = "r" + DateTime.Now.ToString("yyyyMMddhhmmss");
    var packageVersion = MakePrerelease ? assemblyVersion.Change(prerelease: build) : assemblyVersion;

    return packageVersion.ToString();
}

string IncreaseVersion(string version, VersionPart increasePart)
{
    var currentVersion = ParseSemVer(version);
    switch (increasePart)
    {
        case VersionPart.Major:
            return currentVersion
                .Change(major: currentVersion.Major + 1, minor: 0, patch: 0, prerelease: "", build: "")
                .ToString();
        case VersionPart.Minor:
            return currentVersion
                .Change(minor: currentVersion.Minor + 1, patch: 0, prerelease: "", build: "")
                .ToString();
        case VersionPart.Patch:
            return currentVersion
                .Change(patch: currentVersion.Patch + 1, prerelease: "", build: "")
                .ToString();
        default:
            throw new ArgumentOutOfRangeException(nameof(increasePart), increasePart, null);
    }
}

enum VersionPart {
    Major, Minor, Patch
}

public  void SetAssemblyInfoVersion(string filePath, string version)
{
    FileWriteLines(
        filePath,
        FileReadLines(filePath)
            .Select(
                i =>
                {
                    if (i.StartsWith("[assembly: AssemblyVersion(", StringComparison.OrdinalIgnoreCase))
                    {
                        return $"[assembly: AssemblyVersion(\"{version}.0\")]";
                    }

                    if (i.StartsWith("[assembly: AssemblyFileVersion(", StringComparison.OrdinalIgnoreCase))
                    {
                        return $"[assembly: AssemblyFileVersion(\"{version}.0\")]";
                    }

                    return i;
                })
                .ToArray());
}