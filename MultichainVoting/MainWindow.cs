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
	MultiChainClient client;

	//Asset name to send 
	string assetName;

	//New address
	string address;

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
		client = new MultiChainClient("192.168.218.128", 6820, false, "multichainrpc", "A3u8rr7R7i8wCtcYEfMH41aCjWcapXGdxhmdHhVtMGvs", "test");

		// get server info...
		Console.WriteLine("Connect to chain");
		var info = await client.GetInfoAsync();
		info.AssertOk();
		Console.WriteLine("ChainName: {0}", info.Result.ChainName);
		Console.WriteLine();	}

	protected void OnButtonGenerateClicked(object sender, EventArgs e)
	{
		var task = Task.Run(async () =>			
		{
			await CreateAddressAsync(BlockchainPermissions.Send | BlockchainPermissions.Receive | BlockchainPermissions.Issue);

		});
		task.Wait();
			
	}

	private async Task CreateAddressAsync(BlockchainPermissions permissions)
	{
		// Create a new address
		Console.WriteLine("Create New address");
		var newAddress = await client.GetNewAddressAsync();
		newAddress.AssertOk();
		Console.WriteLine("New issue address: " + newAddress.Result);
		Console.WriteLine();

		// Give send/receive/issue permissions
		Console.WriteLine("Grant new chain permissions");
		var perms = await client.GrantAsync(new List<string>() { newAddress.Result }, permissions);
		Console.WriteLine(perms.RawJson);
		perms.AssertOk();

		// Issue a vote to the new address
		assetName = "asset_" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 24);
		Console.WriteLine("Give a new asset to the new address");
		var asset = await client.IssueAsync(newAddress.Result, assetName, 1, 1);
		Console.WriteLine(perms.RawJson);
		perms.AssertOk();

		address = newAddress.Result;
	 }

	protected void OnButtonVoteClicked(object sender, EventArgs e)
	{
		
		// Call task to connect to client blockchain
		var task = Task.Run(async () =>
		{
			await sendAsset();
		});
		task.Wait();

	}

	private async Task sendAsset()	{
		Console.WriteLine("Sending asset to address specified in text box");
		Console.WriteLine(textview1.Buffer.Text);
		var sentAsset = await client.SendAssetToAddressAsync(textview1.Buffer.Text, assetName, 1);
		Console.WriteLine(sentAsset.RawJson);
		sentAsset.AssertOk();

		Console.WriteLine("Showing current balances in address specified in text box");
		var addressBalance = await client.GetAddressBalancesAsync(textview1.Buffer.Text);
		Console.WriteLine(addressBalance.RawJson);
		sentAsset.AssertOk();


	}

}
