using System;
using ExchangeProgram;
using DispenserProgram;
using ProductOptions;

public class Program
{
	public static void Main()
	{
		//Create instance of User and Product
		//These object will be used for every part of program
		User Database = new User();
		Product products = new Product();

		//Create new product
		products.Create("Pepsi",12 , "can", 250);
		products.Create("Pepsi",20, "can", 325);
		products.Create("Coke",12 , "can", 250);
		products.Create("Coke",20, "can", 325);
        products.Create("Sprite",12 , "can", 250);
		products.Create("Sprite",20, "can", 325);
		products.Create("Fanta Grapes", 20, "can", 325);
		products.Create("Fanta Orange", 20, "can", 325);
		products.Create("Fanta Berry", 20, "can", 325);		
		products.Create("Soda", 20, "can", 325);
		products.Create("Orange juice", 20, "bottle", 335);
		products.Create("Lemonade", 18, "bottle", 335);
		products.Create("Apple juice", 15, "bottle", 335);
		products.Create("Tomato juice", 15, "bottle", 335);
		products.Create("Drinking water", 7, "bottle", 600);
		products.Create("Drinking water", 14, "bottle", 1500);
		products.Create("Mineral water", 10, "bottle", 600);
		products.Create("Mineral water", 22, "bottle", 1500);
		
		//loop for main program when run
		while(true)
		{
			//Clear screen and show navigation for user everytimes loop start
			Console.Clear();
			Console.WriteLine("Welcome to Water Program");
			Console.WriteLine("1. Dispenser");
			Console.WriteLine("2. Exchange");
			Console.Write("Select : ");

			//Input menu index as string
			string Input = Console.ReadLine();

			//Switch menu index that input
			switch (Input)
			{
				case "1":
					//go to method Main from class Dispenser
					Dispenser.Main(products);
					break;
				case "2":
					//go to method Main from class Exchange
					Exchange.Main(Database, products);
					break;
				default:
					//Unknow index of menu
					break;
			}
		}
	}
}

