using System;

namespace ProductOptions
{
	public class Product 
	{
		//Fields 
		//Field that have _ must be used pass property only, whether in or out of class.
		private string _name;
		private int _price;
		private string _type;
		private int _value;
		private int _point;

		//Properties that can get and set
		public string Name { get => _name; set => _name = value; }
		public int Price { get => _price; set => _price = value; }
		public string Type 	{get=> _type; set => _type = value;}
		public int Value {get=> _value; set=> _value = value;}
		public int Point {
			//check type and value to return point
			get {
				if (Type == "bottle")
				{
					_point = (_value < 600)? 50: (_value < 1200)? 100:200;
				}
				else if (Type == "can")
				{
					_point = (_value < 300)? 50: 100;
				}

				return _point;
			}
		}

		//Constructors
		public Product() {}
		public Product(string name, int price, string type, int value)
		{
			Name = name;
			Price = price;
			Type = type;
			Value = value;
		}
	}

	

	public class Warehouse
	{
		//Fields 
		private Product[] _data = {};

		//Properties
		public int Length { get => _data.Length; }

		//Craete new product 
		public void Add(string n, int p, string t, int v)
		{
			//Create new Product
			Product product = new Product(n, p, t, v);
			//Pack new Product to data
			Pack(product);
		}

		//Craete new product 
		public void Add(Product p)
		{
			Pack(p);
		}

		//Get product from index
		public bool Get(int i, out Product p)
		{
			if (i <= Length && i > 0) {
				p = _data[--i];
				return true;
			}

			p = new Product();
			return false;
		}

		//Show product
		public void Show(bool sell = true)
		{
			//loop each product in data
			for (int i = 0; i < Length; i++)
			{
				Product p = _data[i];
				//format string to show details of product
				string output = (i+1).ToString().PadRight(4);
				output += p.Name.PadRight(15);
				output += p.Type.PadRight(8);
				output += p.Value.ToString().PadRight(5) + "mL".PadRight(5);
				output += (sell)? (p.Price.ToString().PadRight(5) + "Baht"):(p.Point.ToString().PadRight(5) + "point");
				//print string that formatted
				Console.WriteLine(output);
			}
		}

		//add new object of class Product to field warehouse
		private void Pack(Product p)
		{
			//create new array
			Product[] newarr = new Product[Length+1];

			//add every Product from _data to new array
			for (int i = 0; i < Length; i++)
			{
				newarr[i] = _data[i];
			}
			newarr[Length] = p; //Add new user ass last index

			_data = newarr; //Apply new array to _data
		}
	}
}