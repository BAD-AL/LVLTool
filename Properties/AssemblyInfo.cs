using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("LVLTool")]
[assembly: AssemblyDescription("Replace stuff in LVL files")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("LVLTool")]
[assembly: AssemblyCopyright("Copyright ©  2025")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("53f8bb46-8502-4f5e-bad7-7f571623c11f")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
//[assembly: AssemblyVersion("0.9.0.1")]      //0.9.0.0 initial release
//[assembly: AssemblyFileVersion("0.9.0.1")]
//[assembly: AssemblyVersion("0.9.0.2")]      //0.9.0.2 fixed bug in coremerge for SWBF1
//[assembly: AssemblyFileVersion("0.9.0.2")]
//[assembly: AssemblyVersion("0.9.0.3")]
//[assembly: AssemblyFileVersion("0.9.0.3")]
//[assembly: AssemblyVersion("0.9.0.4")]
//[assembly: AssemblyFileVersion("0.9.0.4")]

//[assembly: AssemblyVersion("0.9.0.5")]   // use .gz compression on dict; expand command line replacement
//[assembly: AssemblyFileVersion("0.9.0.5")]
//////**************************************************************************************************************************
[assembly: AssemblyVersion("0.9.0.6")]
[assembly: AssemblyFileVersion("0.9.0.6")]
//==== Changes ===
//0. Added Explode/Assemble to the Unmunge UI Form
//1. Fix For Bugs:
//   https://github.com/BAD-AL/LVLTool/issues/2
// - BF1 CC common.lvl (TGA)      [error popup message]
// - BF1 CC common.lvl (.texture) [out of memory]
//   [LVLTool.csproj, Munger.cs, CreateLvlForm.cs]
//2. Added Functionality to get embedded strings from command line
//    CoreMerge.cs, Program.cs
//3. Added 'info:' to informational print statements
//    [CoreMerge.cs, HashHelper.cs, LocHelper.cs, LuaCodeHelper.cs,
//    UcfbHelper.cs, MainForm.cs, Program.cs, UnmungeForm.cs]
//4. Re-ordered Platform selections for PC first (File->Create LVL file).
//    [CreateLvlForm.Designer.cs]
//5. Add/Replace file (ConfigMunge) Support for FileTypes:[
//         ".mcfg", ".fx",  ".prp", ".bnd", ".snd", ".mus", 
//         ".combo",".sanm",".hud", ".cfg", ".pth", ".sky",
//         ".lgt",  ".pvs", ".tsr"]
//    [Munger.cs]
//6. Fixed Help message error (for specifying mod tools folder)
//    [Program.cs]
//////**************************************************************************************************************************
