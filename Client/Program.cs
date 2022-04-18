using SimpleTcp;
using System.Text;

string ServerAddress = "ServerAddress.txt";

if (!File.Exists(ServerAddress))
{
    // Assign default example IP & port.
    using StreamWriter writer = new(ServerAddress);
    writer.WriteLine("127.0.0.1:9000");
    writer.Close();
}

SimpleTcpClient client = new(File.ReadAllText(ServerAddress));

try
{
    // Establish the connection to the server.
    client.Events.DataReceived += DataReceived;
    client.Connect();

    if (client.IsConnected == true)
    {
        Console.WriteLine("Enter the string you would like the server to decrypt.\n\n");

        while (true)
        {
            SendPacket();
        }
    }
}
catch (Exception Ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Write(Ex.Message);
    Console.ReadLine();
}

void DataReceived(object sender, DataReceivedEventArgs e)
{
    // Incoming packet
    string Packet = PacketXOR(Encoding.UTF8.GetString(e.Data));

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"{Packet}\n\n");
    Console.ResetColor();
}

void SendPacket()
{
    client.Send(PacketXOR(Console.ReadLine()));
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
