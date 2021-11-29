using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UserOptions
{
	public class Database
	{
		//fields
		private User[] userdata = {};
		private User login;
		private bool stream_db;

		//properties
		public User Current { get => login; }
		public int Length { get => userdata.Length; }
		public bool Stream { get => stream_db; }

		//Using userdata from StreamDB
		public Database(bool stream = false)
		{
			if (stream)	userdata = StreamDB.Import();
			stream_db = stream;
		}

		//add new user to userdata
		private void AddUser(User user)
		{
			List<User> data = new List<User>(userdata);
			data.Add(user);

			userdata = data.ToArray();

			if (Stream) StreamDB.Add(user);
		}

		//finding user in userdata by username
		public bool Found(string username)
		{
			foreach (User user in userdata)
			{
				if (user.Name == username) return true;
			}
			return false;
		}

		//getting user in userdata by username
		public User Get(string username)
		{
			foreach (User user in userdata)
			{
				if (user.Name == username) return user;
			}
			return new User();
		}

		public bool Login()
		{
			//declare string to catch error
			string error = "none";

			while(true)
			{
				Console.Clear();
				Console.WriteLine("Login");
				//show error if have it
				if (error != "none") Console.WriteLine(error);

				//get username and check it is 0?
				Console.Write("Username : ");
				string username = Console.ReadLine();
				if (username == "0") return false;

				//if username is not 0 get password
				Console.Write("Password : ");
				string password = Console.ReadLine();

				//check username
				if (Found(username))
				{
					login = Get(username);

					if(password != login.Password) error = "**Incorrect Password**";
					else {
						Console.WriteLine("Login completed!");
						return true; 
					}
				}
				else error = "**Username not found**";
			}
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
				//show error, if have it
				if (error != "none") Console.WriteLine(error);

				//get username and check it is 0?
				Console.Write("Username : ");
				string username = Console.ReadLine();
				if (username == "0") return false;

				//get password and confirm password
				Console.Write("Password : ");
				string password = Console.ReadLine();
				Console.Write("Confirm Password : ");
				string confirm = Console.ReadLine();

				//check username is already in database
				if(Found(username)) error = "**This username is already in used**";
				else 
				{
					//password not in condition
					if (password != confirm) error = "**Password does not match**"; 
					else if (password.Length < 4) error = "Password must be at least 4 character"; 
					
					//pass all conditions
					else
					{
						Console.WriteLine("Register completed!");
						//create new user and add to userdata
						bool s = (Stream) ? true:false;
						User u = new User(username, password, stream:s);
						AddUser(u);
						return true;
					}
				}
			}
		}

	}

	public class User
	{
		//fields
		private string _name;
		private string _password;
		private int _point;
		private bool _stream;

		//properties
		public string Name { get => _name; }

		public string Password 
		{ 
			get => _password; 
			set {
				_password = value;
				if(_stream)
				{
					User u = new User(_name, _password, _point, true);
					StreamDB.Update(u);
				}
			} 
		}

		public int Point 
		{ 
			get => _point; 
			set {
				_point = value;
				if (_stream)
				{
					User u = new User(_name, _password, _point, true);
					StreamDB.Update(u);
				}
			}
		}
		public bool Stream { get => _stream; }

		//Constructor
		public User(string name, string password, int point = 0, bool stream = false)
		{
			_name = name;
			_password = password;
			_point = point;
			_stream = stream;
		}
		public User(bool stream = false)
		{
			_stream = stream;
		}

		//change password
		public bool ChangePassword()
		{
			string error = "none";
			while (true)
			{
				Console.Clear();
				Console.WriteLine("Change Password");
				//Write error, if have it
				if (error != "none") Console.WriteLine(error);

				//Input new passeord
				Console.Write("New password : ");
				string password = Console.ReadLine();
				//if want to return
				if (password == "0") return false;
				//confirm
				Console.Write("Confirm password : ");
				string confirm = Console.ReadLine();

				//check condition of password
				if (password.Length < 4) error = "***Password must be more than 4 character";
				else {
					if (password != confirm) error = "***Password not match***";
					else 
					{
						Password = password;
						Console.WriteLine("Password changed");
						Console.Write("continue...");
						Console.ReadLine();
						return true;
					}
				}
			}
		}
	}
	
	public class StreamDB
	{
		//Local Directory of database
		public static string Path = @"Database/database.txt";

		//Adding user to database
		public static void Add(User user)
		{
			//Preparing data
			string[] UserInformation = {user.Name, user.Password, user.Point.ToString()};
			string data = String.Join("/", UserInformation);

			//Add data
			using StreamWriter db = new (Path, append:true);
			db.WriteLine(data);
		}

		//Import user from database as Array
		public static User[] Import()
		{
			List<User> db = new List<User>();

			foreach (string data in File.ReadLines(Path))
			{
				//Split data
				string[] Info = data.Split("/");
				//Create new user
				User user = new User(Info[0], Info[1], int.Parse(Info[2]), true);
				//Add user
				db.Add(user);
			}

			return db.ToArray();
		}

		public static void Update(User user)
		{
			//Prepare data for update
			string[] data = {user.Name, user.Password, user.Point.ToString()};
			string update = String.Join("/", data);

			//Update data
			string[] file = File.ReadAllLines(Path);
			file = file.Select(x => x.StartsWith(user.Name+"/") ? update : x).ToArray();
			File.WriteAllLines(Path, file);
		}
	}

}
