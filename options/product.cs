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
		private Product[] warehouse = {};

		//Properties that can get and set
		public string Name { get => _name; set => _name = value; }
		public int Price { get => _price; set => _price = value; }
		public string Type 	{get=> _type; set => _type = value;}
		public int Value {get=> _value; set=> _value = value;}
		
		//Property that can get only
		public int Length {get => warehouse.Length; }
		public int Point {
			//check type of product and value to return point
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

		//You must pass parameter as real index (start from 0) plus by 1 when call this method
		//This method will get point from product in field warehouse (array)
		public int GetPoint(int i)
		{
			//If index start from 1 to last index of field warehouse plus by 1 (length)
			if (i > 0 && i <= warehouse.Length)
			{
				//Return point of product from real index (start from 0) in field warehouse
				return warehouse[i-1].Point;
			}

			//index of product is not in range
			else
			{
				return 0;
			}
		}

		//Get product from index
		public Product Get(int index)
		{
			if (index <= Length && index > 0)
			{
				return warehouse[index-1];
			}
			else 
			{
				return new Product();
			}
		}

		//Craete new product 
		public void Create(string n, int p, string t, int v)
		{
			//Create new Product (instance of product)
			Product product = new Product();

			//Assign value to property of new Product
			product.Name = n;
			product.Price = p;
			product.Type = t;
			product.Value = v;

			//Pack new Product to field warehouse
			Pack(product);
		}

		//add new object of class Product to field warehouse
		private void Pack(Product p)
		{
			int Length = this.warehouse.Length; //get length of warehouse
			int index = Length-1; //get last index of warehouse

			//create new array type Product that increase size from warehouse by 1 
			Product[] newarr = new Product[Length+1];

			//add every data from warehouse to new array, and last index of new array is null
			for (int i = 0; i < Length; i++)
			{
				newarr[i] = this.warehouse[i];
			}

			//add new product to last index of new array
			newarr[index+1] = p;

			//apply new array to warehouse
			this.warehouse = newarr;
		}

		//Show product in sell form
		public void Sell()
		{
			//loop each product in field warehouse
			for (int i = 0; i < warehouse.Length; i++)
			{
				Product p = warehouse[i];

				//format string to show details of product 
				string output = (i+1).ToString().PadRight(4);
				output += p.Name.PadRight(15);
				output += p.Type.PadRight(8);
				output += p.Value.ToString().PadRight(5) + "mL".PadRight(5);
				output += p.Price.ToString().PadRight(5) + "Baht";

				//print string that formatted
				Console.WriteLine(output);
			}
		}

		//Show product in exchange form
		public void Exchange()
		{
			//loop each product in field warehouse
			for (int i = 0; i < warehouse.Length; i++)
			{
				Product p = warehouse[i];

				//format string to show details of product
				string output = (i+1).ToString().PadRight(4);
				output += p.Name.PadRight(15);
				output += p.Type.PadRight(8);
				output += p.Value.ToString().PadRight(5) + "mL".PadRight(5);
				output += p.Point.ToString().PadRight(5) + "point";

				//print string that formatted
				Console.WriteLine(output);
			}
		}
	}
}