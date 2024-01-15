using MessageSenderConsole;
using OpenQA.Selenium;

Console.WriteLine("Welcome to the WhatsApp messenger from Aron (& Marcel)");

var messageSender = new MessageSenderConsole.MessageSender();

Console.WriteLine("Your Number:");
var fromTel = GetValidPhoneNumber();
messageSender.FromTel = fromTel;
messageSender.InitializeWebDriver();
messageSender.Start();

do
{
    Console.Clear();
    SendMessage(messageSender, elementFinder);
    Console.WriteLine("Message sent successfully");

    Console.WriteLine("Do you want to send another message? (y/n)");
} while (Console.ReadLine()?.ToLower() == "y");

messageSender.End();


static void SendMessage(MessageSenderConsole.MessageSender sender, ElementFinder elementFinder)
{
    Console.WriteLine("The Number you want to send the message to:");
    var toTel = GetValidPhoneNumber();

    Console.WriteLine("Your Message:");
    var message = Console.ReadLine() ?? "";

    sender.ToTel = toTel;
    sender.Message = message;
    sender.Continue(sender.ToTel, sender.Message);
}

static string GetValidPhoneNumber()
{
    string phoneNumber;
    do
    {
        Console.Write("Enter a valid phone number (digits only): ");
        phoneNumber = Console.ReadLine() ?? "";
    } while (!IsValidPhoneNumber(phoneNumber));

    return phoneNumber;
}

static bool IsValidPhoneNumber(string phoneNumber)
{
    return !string.IsNullOrEmpty(phoneNumber) && phoneNumber.All(char.IsDigit);
}