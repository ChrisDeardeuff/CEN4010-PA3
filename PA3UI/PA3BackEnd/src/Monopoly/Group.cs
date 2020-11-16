
namespace PA3BackEnd.src.Monopoly
{
	public class Group
	{
		public Property[] properties { get; private set; }
		public int priceToBuild { get; private set; }
		public int Count { get { return properties.Length; } }
		private int currentAmount;

		public Group(int size)
		{
			properties = new Property[size];
		}

		public Group(int size, int priceToBuild) : this(size)
		{
			this.priceToBuild = priceToBuild;
		}

		public bool HasAnyBuildings()
		{
			foreach (var prop in properties)
			{
				if (prop.developmentValue > 0)
				{
					return true;
				}
			}
			return false;
		}

		public void AddProperty(Property property)
		{
			properties[currentAmount] = property;
			currentAmount++;
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
