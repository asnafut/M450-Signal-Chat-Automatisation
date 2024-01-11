using MessageSender;
Console.WriteLine("Welcome to the whatsapp-messenger from Aron (&Marcel)");
SeleniumUser user = new SeleniumUser();
Console.WriteLine("Your Number:");
string fromTel = Console.ReadLine();
Console.WriteLine("The Number you want to send the message to:");
string toTel = Console.ReadLine();
Console.WriteLine("Your Message:");
string message = Console.ReadLine();

user.setFromTel(fromTel);
user.setToTel(toTel);
user.setMessage(message);
user.start();
