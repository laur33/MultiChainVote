# MultiChainVote

Prototype application using multichain, monodevelop GTK 2# and a c# multichain library.

Instructions

Setup a multichain node on one machine/virtual machine.
For testing purposes in your "chainName.conf" file you should be allowing each machines IP otherwise 
just allow all ips for now by including this line rpcallowip=0.0.0.0/0.

Run and build program in monodevelop on a different machine (I was using monodevelop on windows and multichain
running on ubuntu virtual machine) and click connect to client.

Copy your chains information into the MainWindow.cs line below

    var client = new MultiChainClient("IP address", RPCPort, false, "rpc username", "rpc password", "chain name");

Should get output in monodevelops command line like so

  Connect to chain
  ChainName: "chain name"
  
 Click Generate new address and one asset button
 
 Now go back to the machine with the original multichain node onn it and create a new address and also give 
 it send/recieve permissions.
 
 Copy that address and paste it into the Address to send asset to field and click send asset
 
 In the command line you'll get a confirmation and a show of the current assets that have been sent to
 the address you have chosen
 
 Each asset sent is different so each time you send an asset you will get an additional JSON result like
 this {"result":[{"name":"asset_4a15b5a4b5264c5787997601","assetref":"442-514-17980","qty":1.00000000}
 
 Don't click buttons out of order for now it crashes.
 
 

  

