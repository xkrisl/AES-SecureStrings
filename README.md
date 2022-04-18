# AES-SecureStrings (Server, Client and String Builder)
![Header](https://i.imgur.com/40W72sk.jpg)

This originally started as an old project a couple of years ago. Decided to remake this and use .NET core instead. Also, I'm using [SimpleTCP](https://www.nuget.org/packages/SuperSimpleTcp/) to handle the connection vs. UDP.

Above you can see a screenshot showing how all of this works. The client (running on Windows) sends the encrypted string to the server (running on Linux). The server will receive, parse and decrypt the string and send it back to the client. Packets are masked with a simple XOR.

I've also included a easy-to-use string builder.

# Setup
<dl>
  <dt>Server:</dt>
  <dd>Config_Port.txt: Define the port you want the server listening on.</dd>
  <dd>Config_AESPassword.txt: Define the password that will decrypt your incoming strings.</dd>
 
  <dt>Client:</dt>
  <dd>ServerAddress.txt: Define the decryption server's IP and Port.</dd>
</dl>
