using System;
using System.Collections.Generic;
using System.Text;

namespace PA3BackEnd.src.Monopoly
{
	public enum Actions
	{ 
		go,
		none,
		canBuy,
		payRent,
		payTax100,
		payTax200,
		goToPrison,
	}

	public abstract class Field
	{
		int location;

		public Field(int location)
		{
			this.location = location;
		}

		public int GetLocation()
		{
			return location;
		}

		public abstract Actions GetAction();
	}
}
