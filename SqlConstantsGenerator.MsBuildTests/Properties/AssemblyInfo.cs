using System.Reflection;
using System.Runtime.InteropServices;
using SqlConstantsGenerator.Attributes;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("SqlConstantsGenerator.MsBuildTests")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("SqlConstantsGenerator.MsBuildTests")]
[assembly: AssemblyCopyright("Copyright © 2019 Kabanov Konstantin")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("93d2f48a-432b-47a5-9b53-64508de2a1b9")]

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
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: SqlConstantsGeneratorOptions(
	PrefixSql = @"if(object_id(N'[$viewname$]', N'V') is not null) drop view [$viewname$];
go",
	PostfixSql = @"print 'View [$viewname$] created successfully.';
go"
)]
