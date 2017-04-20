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

		var client = new MultiChainClient("192.168.218.128", 6820, false, "multichainrpc", "A3u8rr7R7i8wCtcYEfMH41aCjWcapXGdxhmdHhVtMGvs", "test");

	


	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}

}
