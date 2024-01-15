using MessageSender;
using System;

Console.WriteLine("Welcome to the WhatsApp messenger from Aron (& Marcel)");

SeleniumUser user = new SeleniumUser();

Console.WriteLine("Your Number:");
string fromTel = Console.ReadLine() ?? ""; // Providing an empty string as a default value

Console.WriteLine("The Number you want to send the message to:");
string toTel = Console.ReadLine() ?? "";

Console.WriteLine("Your Message:");
string message = Console.ReadLine() ?? "";

user.SetFromTel(fromTel);
user.SetToTel(toTel);
user.SetMessage(message);
user.Start();