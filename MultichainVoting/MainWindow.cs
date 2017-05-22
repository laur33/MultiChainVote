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
	//Allow all functions to make use of the MultiChainClient client
	private MultiChainClient client;
	private int x = 1;
	//testbranch
	//Asset name to send 
	//private string assetName;

	//New address
	//private string address;

	public MainWindow() : base(Gtk.WindowType.Toplevel)
	{
		Build();
		Pango.FontDescription fd = new Pango.FontDescription();
		fd.Family = "Arial"; 
		fd.Size = 15;
		this.labelTitle.ModifyFont(fd);
	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}

	protected void OnClientConnectClicked(object sender, EventArgs e)
	{


		// Call task to connect to client blockchain
		try
		{
			var task = Task.Run(async () =>
			{
				
				await connectToChain();
			});
			//task.Wait();


		}
		catch (Exception ex)
		{
			Console.WriteLine("******************");
			Console.WriteLine(ex);
		}

	}

	internal async Task connectToChain()
	{
		var timeoutCacellationTokenSource = new CancellationTokenSource();
		client = new MultiChainClient("192.168.218.128", 6820, false, "multichainrpc", "A3u8rr7R7i8wCtcYEfMH41aCjWcapXGdxhmdHhVtMGvs", "test");

		 //get server info...
		Console.WriteLine("Connect to chain");
		int timeout = 2000;
		x = 3;
	
		//var info = await client.GetInfoAsync();
	

		//Check if can connect to the chain i n <=2seconds if you can start getting data
		if (await Task.WhenAny(client.GetInfoAsync(), Task.Delay(timeout)) == client.GetInfoAsync())
		{
			var info = await client.GetInfoAsync();
			info.AssertOk();
			Console.WriteLine("ChainName: {0}", info.Result.ChainName);
		}
		else
		{
			Console.WriteLine("Timeout");

			/**MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Close, "Error");
			md.Run();
            md.Destroy();**/
		}


		/**
		info.AssertOk();
		Console.WriteLine("ChainName: {0}", info.Result.ChainName);
		Console.WriteLine();**/
	}





}
