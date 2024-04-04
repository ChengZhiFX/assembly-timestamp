using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace WindowsFormsApp4.Properties;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
internal class Resources
{
	private static ResourceManager resourceMan;

	private static CultureInfo resourceCulture;

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static ResourceManager ResourceManager
	{
		get
		{
			if (resourceMan == null)
			{
				ResourceManager resourceManager = new ResourceManager("WindowsFormsApp4.Properties.Resources", typeof(Resources).Assembly);
				resourceMan = resourceManager;
			}
			return resourceMan;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static CultureInfo Culture
	{
		get
		{
			return resourceCulture;
		}
		set
		{
			resourceCulture = value;
		}
	}

	internal static Bitmap _789
	{
		get
		{
			object @object = ResourceManager.GetObject("_789", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap BackgroundImage
	{
		get
		{
			object @object = ResourceManager.GetObject("BackgroundImage", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Icon clock
	{
		get
		{
			object @object = ResourceManager.GetObject("clock", resourceCulture);
			return (Icon)@object;
		}
	}

	internal Resources()
	{
	}
}
