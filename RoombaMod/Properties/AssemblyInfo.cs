using MelonLoader;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle(Thicket.BuildInfo.Name)]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(Thicket.BuildInfo.Company)]
[assembly: AssemblyProduct(Thicket.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + Thicket.BuildInfo.Author)]
[assembly: AssemblyTrademark(Thicket.BuildInfo.Company)]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
//[assembly: Guid("")]
[assembly: AssemblyVersion(Thicket.BuildInfo.Version)]
[assembly: AssemblyFileVersion(Thicket.BuildInfo.Version)]
[assembly: NeutralResourcesLanguage("en")]
[assembly: MelonInfo(typeof(Thicket.Thicket), Thicket.BuildInfo.Name, Thicket.BuildInfo.Version, Thicket.BuildInfo.Author, Thicket.BuildInfo.DownloadLink)]


// Create and Setup a MelonModGame to mark a Mod as Universal or Compatible with specific Games.
// If no MelonModGameAttribute is found or any of the Values for any MelonModGame on the Mod is null or empty it will be assumed the Mod is Universal.
// Values for MelonModGame can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame("Hakita", "ULTRAKILL")]