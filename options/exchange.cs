using System;
using ProductOptions;

namespace ExchangeProgram
{
	public class Exchange
	{
		public static void Main(User user , Product product)
		{
			while (true)
			{
				//Clear screen and show navigation for user everytimes loop start
				Console.Clear();
				Console.WriteLine("You are now in Exchange program");
				Console.WriteLine("1.Login");
				Console.WriteLine("2.Register");
				Console.WriteLine("0.return");
				Console.WriteLine("When login or register you can type username as 0 to return.");
				Console.Write("Select : ");

				//Input menu index as string
				string Input = Console.ReadLine();

				//switch menu idex that input
				switch (Input)
				{
					case "0":

						//return to where call this method
						return;

					case "1":

						//go to login
						string login = user.Login(); //return user name or fail

						//if login not return fail
						if(login != "fail")
						{
							//go to exchange program as user that logged in
							User now = user.Get(login);
							Exchange e = new Exchange(product);
							e.Program(now);

							//after finish process from exchange program return to where call this method
							return;
						}

						//if login return fail
						break;

					case "2":

						//go to register
						user.Register();
						break;

					default:
						
						//Unknow index of menu
						break;
				}
			}
		}

		//field 
		private Product product;
		
		//constructor
		public Exchange(Product product)
		{
			//assign parameter product to field product
			this.product = product;
		}

		//main exchange program after logged in completed
		public void Program(User user)
		{
			while (true)
			{
				//Clear screen and show navigation for user everytimes loop start
				Console.Clear();
				Console.WriteLine("Select options");
				Console.WriteLine("1.Exchange product");
				Console.WriteLine("2.Exchange money");
				Console.WriteLine("0.return");
				Console.Write("Select : ");

				//Input menu index as string
				string Input = Console.ReadLine();

				//Switch menu index
				switch (Input)
				{
					case "0":

						//return to where call this method
						return;

					case "1":

						//go to GetPoint method
						GetPoint(user);
						break;	

					case "2":

						//go to GetMoney method
						GetMoney(user);
						break;

					default :

						//Unknow index of menu
						Console.WriteLine("Please enter only 1 or 2");
						break;
				}
			}
		}

		//get money from user that logged in
		public void GetMoney (User user)
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

					//If user not type 0
					if (Console.ReadLine() != "0")
					{
						//this mean confirm to go back
						return;
					}
					else
					{
						//this mean cancel to go back and go to loop again
						continue;
					}
				}

				//If amount of money that user input is not 0
				int.TryParse(Input, out int money); //if user input string, money will be 0

				//If user in put string
				if (money == 0)
				{
					Console.WriteLine("Money must be integer");
					continue;
				}
				
				//If user in put amount of money higher or lower of limit ( 0 to can_exchange )
				if (money > can_exchange || money < 0)
				{
					Console.WriteLine("You can't exchange");	
					continue;
				}

				//If amount of money not in condition before
				//Ask user to confirm amount of money that user input
				Console.WriteLine("=======================================");
				Console.WriteLine($"You want to get {money} Baht ? ");
				Console.WriteLine("> 0 to cancel | Enter to confirm ");
				Console.WriteLine("=======================================");
				Console.Write("Confirm : ");

				//If user input 0, it means cancel
				if (Console.ReadLine() == "0")
				{
					continue;
				}
				
				//if not cancel
				else
				{
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
		}

		//get point from user that logged in
		public void GetPoint (User user)
		{
			//clear screen and show product details
			Console.Clear();
			Console.WriteLine("====================MENU======================");
			product.Exchange(); //show each product from field product
			Console.WriteLine("==============================================");
			Console.WriteLine("Type 0 to return"); //navigation for return

			while (true)
			{
				//Input index of product that showed
				Console.WriteLine("==============================================");
        		Console.Write("Enter number of product : ");
            	string Input = Console.ReadLine();

				//When user input 0 it means stop exchange
				if (Input == "0")
				{
					//return to where call this method
					return;
				}

				//When data that user input is not 0 convert Input(string) to int
				int.TryParse(Input, out int index); //if convert fail, it will return 0

				//get point of product index from GetPoint method from class Product
				int point = product.GetPoint(index); //not found return 0

				//If product not found
				if (point == 0)
				{
					Console.WriteLine("Product not match!!!");
					continue;
				}

				else
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
						}
						else 
						{
							//add point after confirmed amount of product
							user.Point += n*point;
							Console.WriteLine($"Now your point is {user.Point}");
							break;
						}
					}
				}
			}
      
		}
	}

	public class User
	{
		//fields
		private User[] database = {};
		private string _name;
		private string _password;
		private int _point = 0;

		//properties
		public string Name { get => _name; set => _name = value; }
		public string Password { get => _password; set => _password = value; }
		public int Point { get => _point; set => _point = value; }

		public string Login()
		{
			//declare string to catch error
			string error = "none";

			while(true)
			{
				Console.Clear();
				Console.WriteLine("Login");

				//show error if have it
				if (error != "none")
				{
					Console.WriteLine(error);
				}

				//get username
				Console.Write("Username : ");
				string username = Console.ReadLine();

				//if username is 0 return to where call this method
				if (username == "0")
				{
					return "fail";
				}

				//if username is not 0 get password
				Console.Write("Password : ");
				string password = Console.ReadLine();

				//check username in field database
				if (Check(username) == true) //if found
				{
					User login = Get(username); //get user in field database form username

					if(password != login.Password) //check password
					{
						error = "**Incorrect Password**"; //Incorrect
					}

					else
					{
						Console.WriteLine("Login completed!"); //correct
						return username; 
					}
				}
				else //if not found
				{
					error = "**Username not found**";
				}
			}
		}

		//Check username in database
		public bool Check(string name)
		{
			foreach (User u in this.database)
			{
				if (u.Name == name) //if found
				{
					return true;
				}
			}

			return false; //not found
		}

		//get user from username
		public User Get(string name)
		{
			//loop to check user in field database
			foreach (User user in this.database)
			{
				if (user.Name == name) //if found
				{
					return user;
				}
			}

			//not found user in field database
			return new User();
		}
		
		//create new user
		public bool Register()
		{
			//declare string to catch error
			string error = "none";

			while(true)
			{
				Console.Clear();
				Console.WriteLine("Register");

				//show error if have it
				if (error != "none")
				{
					Console.WriteLine(error);
				}

				//get username
				Console.Write("Username : ");
				string username = Console.ReadLine();

				//if username is 0 return to where call this method
				if (username == "0")
				{
					return false;
				}

				//get password and confirm password
				Console.Write("Password : ");
				string password = Console.ReadLine();
				Console.Write("Confirm Password : ");
				string confirm = Console.ReadLine();

				//check username is already in database
				if(Check(username) == true)
				{
					error = "**This username is already in used**"; //username is already in used
				}

				//username is not in used
				else 
				{
					//password not match
					if (password != confirm) 
					{
						error = "**Password does not match**"; 
					}

					//password is too short
					else if (password.Length < 4)
					{
						error = "Password must be at least 4 character"; 
					}
					
					//pass all conditions
					else
					{
						Console.WriteLine("Register completed!");

						//create new user
						User new_user = new User();
						new_user.Name = username;
						new_user.Password = password;

						//add new user to database
						AddTodatabase(new_user);

						return true;
					}
				}
			}
		}

		//add new user to database
		private void AddTodatabase(User new_user)
		{
			int Length = this.database.Length; //get length of database
			int index = Length-1; //get last index of database

			//create new array type User that increase size from database by 1 
			User[] newarr = new User[Length+1];

			//add every data from database to new array, and last index of new array is null
			for (int i = 0; i < Length; i++)
			{
				newarr[i] = this.database[i];
			}

			//add new User to last index of new array
			newarr[index+1] = new_user;

			//apply new array to database
			this.database = newarr;
		}

	}
}
