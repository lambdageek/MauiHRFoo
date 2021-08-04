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
			count--;
			CounterLabel.Text = $"{GetEE()}: {count}";
		}

		public string GetEE()
		{
			if (!System.Runtime.CompilerServices.RuntimeFeature.IsDynamicCodeSupported)
				return "AOT";
			if (!System.Runtime.CompilerServices.RuntimeFeature.IsDynamicCodeCompiled)
				return "Interp";
			return "JIT";
		}
	}
}
