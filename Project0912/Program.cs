// See https://aka.ms/new-console-template for more information

using Lesson2;

var bank = new DemoBank();
var ATM = bank.ATMs[0];
ATM.BigMoneyFetched += (sender, e) => { Console.WriteLine("Big money fetched!"); };
Console.WriteLine(ATM.GetBalance(1000));
var cashes = ATM.Withdraw(1000, 10000);
Console.WriteLine(ATM.GetBalance(1000));
ATM.Deposit(1000, cashes);
Console.WriteLine(ATM.GetBalance(1000));