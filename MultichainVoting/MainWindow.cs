using System;
using Gtk;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MultiChainLib;

public partial class MainWindow : Gtk.Window
{
	public MainWindow() : base(Gtk.WindowType.Toplevel)
	{
		Build();
	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}

	protected void OnClientConnectClicked(object sender, EventArgs e)
	{
			// Call task to connect to client blockchain
			var task = Task.Run(async () =>
			{
								await connectToChain();
			});
			task.Wait();
	}

	internal async Task connectToChain()
	{
		var client = new MultiChainClient("192.168.218.128", 6820, false, "multichainrpc", "A3u8rr7R7i8wCtcYEfMH41aCjWcapXGdxhmdHhVtMGvs", "test");

		// get server info...
		Console.WriteLine("*** getinfo ***");
		var info = await client.GetInfoAsync();
		info.AssertOk();
		Console.WriteLine("Chain: {0}, difficulty: {1}", info.Result.ChainName, info.Result.Difficulty);
		Console.WriteLine();	}
}
