using MessageSender;
Console.WriteLine("Hello, Enter a Number");
SeleniumUser user = new SeleniumUser();
string tel = "+41 77 522 72 20";
user.setTel(tel);
string message = "Hallo Marcel";
user.setMessage(message);
user.sendMessage();