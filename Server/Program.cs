using Encryption;
using System.Text;
using System.Net;
using SimpleTcp;

// Port configuration
string Port = "Config_Port.txt";

if (!File.Exists(Port))
{
    // Assign default example port.
    using StreamWriter writer = new(Port);
    writer.WriteLine("9000");
    writer.Close();
}

// AES password used to decrypt the incoming strings.
string Password = "Config_AESPassword.txt";

if (!File.Exists(Password))
{
    // Assign default example password.
    using StreamWriter writer = new(Password);
    writer.WriteLine("Passw0rd123!");
    writer.Close();
}

SecurityController _security = new();
string AESPassword = File.ReadAllText(Password);

// Start the server.
SimpleTcpServer server = new($"{IPAddress.Any}:{File.ReadAllText(Port)}");

try
{
    server.Events.DataReceived += DataReceived;
    server.Start();
    Console.ReadKey();
}
catch (Exception Ex)
{
    Console.WriteLine(Ex.Message);
    Console.ReadKey();
}

void DataReceived(object sender, DataReceivedEventArgs e)
{
    // Incoming packet
    string Packet = _security.Decrypt(AESPassword, PacketXOR(Encoding.UTF8.GetString(e.Data)));
    Console.WriteLine($"{"["}{e.IpPort}{"]"}:{Packet}");

    // Send back to the client
    server.Send(e.IpPort, PacketXOR(Packet));
}

// Packet XOR.
static string PacketXOR(string input)
{
    byte[] Key = { 0x36, 0x71, 0x36, 0x21, 0x50, 0x5D, 0x33, 0x63, 0x4B, 0x3D, 0x29, 0x2C, 0x36, 0x7E, 0x26, 0x7D };
    char[] Output = new char[input.Length];

    for (int i = 0; i < input.Length; i++)
    {
        Output[i] = (char)(input[i] ^ Key[i % Key.Length]);
    }

    Array.Clear(Key, 0, Key.Length);
    return new string(Output);
}

