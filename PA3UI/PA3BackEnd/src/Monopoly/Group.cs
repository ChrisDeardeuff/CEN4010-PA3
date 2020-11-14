
namespace PA3BackEnd.src.Monopoly
{
	public class Group
	{
		private Property[] properties;
		private int priceToBuild;
		private int currentAmount;

		public Group(int size)
		{
			properties = new Property[size];
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
				if (properties[i].GetOwner() != playerid)
				{
					return false;
				}
			}
			return true;
		}
	}
}
