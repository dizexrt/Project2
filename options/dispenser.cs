using System;
using ProductOptions;

namespace DispenserProgram
{
	public class Dispenser
	{
		public static void Main(Product product)
		{
			//Show product menu
			Console.Clear();
			Console.WriteLine("====================MENU======================");
			product.Sell();
			Console.WriteLine("==============================================");
			Console.WriteLine("*Type 0 to return*");
			
			while(true)
			{
				//Get number of product as string
				Console.WriteLine("==============================================");
				Console.Write("Input : ");
				string Input = Console.ReadLine();
				
				//If user input 0
				if (Input == "0")
				{
					//Confirm to return program
					Console.WriteLine("Are you sure to return?");
					Console.WriteLine("> Enter to return | 0 to cancel");
					Console.Write("Confirm : ");
					
					//check Confirm
					if (Console.ReadLine() == "0")
					{
						continue;
					}

					return; //return to where that call this method
				}

				//If user do not input 0
				int.TryParse(Input, out int index); //convert string to int index

				//If index in range of product
				if (index <= product.Length && index > 0)
				{
					Product selected = product.Get(index); //Get product from index
					if (Buy(selected) == false) //go to Buy method
					{
						continue;
					}
					return;
				}
				//If index not in range of product
				else
				{
					Console.WriteLine("...Program not found!!!");
				}
			}
		}

		public static bool Buy(Product buy)
		{
			//Get order from customer
			Console.WriteLine("==============================================");
			Console.WriteLine($"You selected {buy.Name} {buy.Value} mL");

			//get amount of product
			int amount = 0;
			while (amount <= 0)
			{
				Console.Write("How many? : ");
				int.TryParse(Console.ReadLine(), out amount);
				
				if (amount == 0)
				{
					return false;
				}

				if (amount < 0)
				{
					Console.WriteLine("Amount of product must be more than 0");
				}
			}

			//In put money
			Console.WriteLine("==============================================");
			InputMoney(buy.Price*amount);

			//End process
			Console.WriteLine("--Thank you for your payment--");
			Console.WriteLine(" This is your product");
			Console.WriteLine(" \\\\                   //  // ");
			Console.WriteLine(" _____________________");
			Console.WriteLine("|        |      |     |_______ _");
			Console.WriteLine("|        |      |      _______|_|");
			Console.WriteLine("|________|______|_____|");
			Console.WriteLine(" //                   \\\\  \\\\ ");
			Console.WriteLine("==============================================");
			Console.Write("continue...");
			Console.ReadLine();
			return true;
		}

		public static void InputMoney(int amount)
		{
			int limit = amount;
			//get money from customer
			while (limit > 0)
			{
				Console.Write($"Input money {limit} baht : ");
				int.TryParse(Console.ReadLine(), out int input);
				
				if (input <= 0)
				{
					Console.WriteLine("Money must be more than 0");
					continue;
				}

				limit -= input;
			}
			//show input money
			Console.WriteLine($"You get change {limit*(-1)} Baht");
		}
	}
}