using System;
using ProductOptions;
using UserOptions;

namespace ExchangeProgram
{
	public class Exchange
	{
		public static void Main(Database user , Warehouse product)
		{
			bool error = false;
			while (true)
			{
				//Clear screen and show navigation for user everytimes loop start
				Console.Clear();
				Console.WriteLine("You are now in Exchange program");
				Console.WriteLine("1.Login");
				Console.WriteLine("2.Register");
				Console.WriteLine("0.return");
				if (error) Console.WriteLine("***Please enter only 0-2***");
				error = false;
				Console.Write("Select : ");
				
				//Switch string that user input
				switch (Console.ReadLine())
				{
					case "0": return; // return to where's called this method

					case "1":
						
						//Login from user in Database
						if (user.Login(out User login))
						{
							Exchange program = new Exchange(product);
							program.Usedby(login);
							return; // return to where's called this method
						}
						break;

					case "2":
						
						if(user.Register())
						{
							Console.WriteLine("You want to Login?");
							Console.WriteLine("> 0 to cancel | any to cofirm");
							Console.Write("Confirm : ");

							if (Console.ReadLine() != "0") goto case "1";

							return; // return to where's called this method
						}
						break;

					default: 
						error = true;
						break;
				}
			}
		}

		//field 
		private Warehouse product;
		private User user;
		
		//constructor
		public Exchange(Warehouse product) { this.product = product; }

		public void Usedby(User user)
		{
			this.user = user;
			Start();
		}

		//main exchange program after logged in completed
		private void Start()
		{
			bool error = false;
			while (true)
			{
				//Clear screen and show navigation for user everytimes loop start
				Console.Clear();
				Console.WriteLine("Select options");
				Console.WriteLine("1.Exchange product");
				Console.WriteLine("2.Exchange money");
				Console.WriteLine("3.Change password");
				Console.WriteLine("0.return");
				if (error) Console.WriteLine("***Please enter only 0-3***");
				error = false;
				Console.Write("Select : ");

				//Switch menu index from input
				switch (Console.ReadLine())
				{
					case "0": return; //return to where's called this method

					case "1":
						//go to GetPoint method
						GetPoint();
						break;	

					case "2":
						//go to GetMoney method
						GetMoney();
						break;

					case "3":
						//go to ChangePassword
						user.ChangePassword();
						break;

					default :
						//Unknow index of menu
						error = true;
						break;
				}
			}
		}

		private void GetMoney ()
		{
			//Get money that user can exchange
			int can_exchange = user.Point/50;

			//clear screen and show details of user
			Console.Clear();
			Console.WriteLine("=========Exchange money program========");
			Console.WriteLine($"User name  : {user.Name}");
			Console.WriteLine($"User point : {user.Point} point");
			Console.WriteLine("=======================================");
			Console.WriteLine("50 points can exchange 1 Baht");
			Console.WriteLine("You can withdraw  : " + can_exchange + " Baht");
			Console.WriteLine("=======================================");
			Console.WriteLine("You can type 0 to return");

			while (true)
			{
				//Input amount of money that user want to exchange
				Console.WriteLine("=======================================");
				Console.Write("Enter amount of money to exchange : ");
				string Input = Console.ReadLine();
				
				//If user input 0
				if (Input == "0")
				{
					//Ask user to confirm to return to process that call this method
					Console.WriteLine("=======================================");
					Console.WriteLine("You want to end this process?");
					Console.WriteLine("> 0 to cancel | any to end");
					Console.WriteLine("=======================================");
					Console.Write("Confirm : ");

					//Check to return
					if (Console.ReadLine() != "0") return;
					continue;
				}

				//If amount of money that user input is not 0
				bool convert = int.TryParse(Input, out int money); 

				if (convert == false)
				{
					Console.WriteLine("Money must be integer");
					continue;
				}
				
				//If user in put amount of money higher or lower of limit (0 - can_exchange)
				if (money > can_exchange || money < 0)
				{
					Console.WriteLine("You can not exchange");	
					continue;
				}

				//If amount of money not in condition before (pass all condition)
				//Ask user to confirm amount of money that user input
				Console.WriteLine("=======================================");
				Console.WriteLine($"You want to get {money} Baht ? ");
				Console.WriteLine("> 0 to cancel | Enter to confirm ");
				Console.WriteLine("=======================================");
				Console.Write("Confirm : ");

				//If user input 0, it means cancel
				if (Console.ReadLine() == "0") continue; 
				
				//show details and get money
				Console.WriteLine("=======================================");
				Console.WriteLine($"You get money : {money} Baht");
				user.Point -= money*50; //remove point thta exchanged
				Console.WriteLine($"You have {user.Point} point left"); //show point left
				Console.WriteLine("=======================================");
				//wait to confirm to continue
				Console.Write("continue...");
				Console.ReadLine();
				break;
			}
		}

		private void GetPoint ()
		{
			//clear screen and show product details
			Console.Clear();
			Console.WriteLine("====================MENU======================");
			product.Show(sell:false); //show each product from field product
			Console.WriteLine("==============================================");
			Console.WriteLine("Type 0 to return"); //navigation for return

			while (true)
			{
				//Input index of product that showed
				Console.WriteLine("==============================================");
        		Console.Write("Enter number of product : ");
            	string Input = Console.ReadLine();

				//When user input 0 it means stop exchange
				if (Input == "0") return; 

				//When data that user input is not 0 convert Input(string) to int
				int.TryParse(Input, out int index); //if convert fail, it will return 0

				//get point of product
				bool found = product.Get(index, out Product p); 

				if(found)
				{
					while (true)
					{
						//Confirm amount
						Console.Write("Enter amount of product: ");
						int.TryParse(Console.ReadLine(), out int n);

						//amount fail
						if (n <= 0)
						{
							Console.WriteLine("Amount of product must be more than 0");
							continue;
						}

						//add point after confirmed amount of product
						user.Point += n*p.Point;
						Console.WriteLine($"Now your point is {user.Point}");
						break;
					}
				}
				else Console.WriteLine("Product not found!!!");
			}
      
		}
	}

}
