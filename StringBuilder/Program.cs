using Encryption;

Console.WriteLine("Password:");

SecurityController _security = new();
string AESPassword = Console.ReadLine();

Console.WriteLine("String:");
string ProtectedString = _security.Encrypt(AESPassword, Console.ReadLine());

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine($"\n\n{ProtectedString}");
Console.ResetColor();
Console.ReadLine();