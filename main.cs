using System;
using ExchangeProgram;
using DispenserProgram;
using ProductOptions;
using UserOptions;

public class Program
{
	public static void Main()
	{
		//These object will be used for every part of program
		Warehouse warehouse = new Warehouse();
		Database db = new Database(stream:true);

		//Create new product
		warehouse.Add("Pepsi",12 , "can", 250);
		warehouse.Add("Pepsi",20, "can", 325);
		warehouse.Add("Coke",12 , "can", 250);
		warehouse.Add("Coke",20, "can", 325);
        warehouse.Add("Sprite",12 , "can", 250);
		warehouse.Add("Sprite",20, "can", 325);
		warehouse.Add("Fanta Grapes", 20, "can", 325);
		warehouse.Add("Fanta Orange", 20, "can", 325);
		warehouse.Add("Fanta Berry", 20, "can", 325);		
		warehouse.Add("Soda", 20, "can", 325);
		warehouse.Add("Orange juice", 20, "bottle", 335);
		warehouse.Add("Lemonade", 18, "bottle", 335);
		warehouse.Add("Apple juice", 15, "bottle", 335);
		warehouse.Add("Tomato juice", 15, "bottle", 335);
		warehouse.Add("Drinking water", 7, "bottle", 600);
		warehouse.Add("Drinking water", 14, "bottle", 1500);
		warehouse.Add("Mineral water", 10, "bottle", 600);
		warehouse.Add("Mineral water", 22, "bottle", 1500);
		
		while(true)
		{
			//Clear screen and show navigation for user everytimes loop start
			Console.Clear();
			Console.WriteLine("Welcome to Water Program");
			Console.WriteLine("1. Dispenser");
			Console.WriteLine("2. Exchange");
			Console.Write("Select : ");

			//Switch menu index that input
			switch (Console.ReadLine())
			{
				case "1":
					//go to method Main from class Dispenser
					Dispenser.Main(warehouse);
					break;
				case "2":
					//go to method Main from class Exchange
					Exchange.Main(db, warehouse);
					break;
				case "0":
					return;
				default:
					//Unknow index of menu
					break;
			}
		}
	}
}

