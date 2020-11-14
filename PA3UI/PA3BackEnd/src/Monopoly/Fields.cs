using System;
using System.Collections.Generic;
using System.Text;

namespace PA3BackEnd.src.Monopoly
{
	public class Fields
	{
		Group[] groups;
		Field[] fields;

		public Fields()
		{
			groups = new Group[10];
			fields = new Field[40];
		}

		public void LoadField()
		{
			// Initialize groups
			// groups for Streets
			groups[0] = new Group(2);
			groups[1] = new Group(3);
			groups[2] = new Group(3);
			groups[3] = new Group(3);
			groups[4] = new Group(3);
			groups[5] = new Group(3);
			groups[6] = new Group(3);
			groups[7] = new Group(2);
			//railroads
			groups[8] = new Group(4);
			//Utility
			groups[9] = new Group(2);

			// Initialize fields
			// street
			// utility
			// railroads
			// go
			// prison
			// freeParking
			// GoToPrison
			// taxes tiles
			// card tiles


		}

		public bool CanPlayerBuildOnProperty(int index)
		{
			return false;
		}
	}
}
