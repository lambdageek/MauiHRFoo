using System;
using Microsoft.Maui.Controls;
using System.Runtime.InteropServices;

namespace Foo
{
	public partial class MainPage : ContentPage
	{

		[DllImport("FooBar", EntryPoint = "Nonsense")]
		public static extern void Nonsense();

		public MainPage()
		{
			InitializeComponent();
		}

		int count = 0;
		private void OnCounterClicked(object sender, EventArgs e)
		{
			OutMethod();
			//Nonsense();
		}

		public void OutMethod()
        {
			count++;
			CounterLabel.Text = $"{GetEnCCapabilities()} is: {count}";
		}

		public string GetEE()
		{
			if (!System.Runtime.CompilerServices.RuntimeFeature.IsDynamicCodeSupported)
				return "AOT";
			if (!System.Runtime.CompilerServices.RuntimeFeature.IsDynamicCodeCompiled)
				return "Interp";
			return "JIT";
		}

		public string GetEnCCapabilities()
		{
			var ee = GetEE();
			if (ee != "Interp")
				return ee;
			if (!System.Reflection.Metadata.MetadataUpdater.IsSupported)
			{
				if (Environment.GetEnvironmentVariable("DOTNET_MODIFIABLE_ASSEMBLIES") is string s)
				{
					if (s != null && string.Equals(s, "debug", StringComparison.InvariantCultureIgnoreCase))
						return "no support";
					else
						return $"env:{s}";

				}
				else
				{
					return "no env";
				}
			}
			var mi = typeof(System.Reflection.Metadata.MetadataUpdater).GetMethod("GetCapabilities", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
			if (mi == null)
				return "no method";
			var res = mi.Invoke(null, Array.Empty<object>());
			if (res != null && res is string caps)
			{
				if (string.IsNullOrEmpty(caps))
					return "none";
				else
					return caps;
			}
			return "bad result";
		}
	}
}
