
namespace PA3BackEnd.src.Monopoly
{
	public class Group
	{
		private Property[] properties;
		public int priceToBuild { get; private set; }
		private int currentAmount;

		public Group(int size)
		{
			properties = new Property[size];
		}

		public Group(int size, int priceToBuild) : this(size)
		{
			this.priceToBuild = priceToBuild;
		}

		public void AddProperty(Property property)
		{
			properties[currentAmount] = property;
			currentAmount++;
		}

		public bool CanPlayerBuild(int playerid)
		{
			for (int i = 0; i < properties.Length; i++)
			{
				if (properties[i].owner != playerid)
				{
					return false;
				}
			}
			return true;
		}

		public int GetAmountPlayerOwns(int playerid)
		{
			int amount = 0;
			foreach (var prop in properties)
			{
				if (prop.owner == playerid)
				{
					amount++;
				}
			}
			return amount;
		}
	}
}
