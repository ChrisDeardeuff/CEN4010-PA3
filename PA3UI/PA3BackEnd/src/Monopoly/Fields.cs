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
			LoadFields();
		}

		public void LoadFields()
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
			fields[1] = new Street(1, groups[0], 60);
			fields[3] = new Street(3, groups[0], 60);

			fields[6] = new Street(6, groups[1], 100);
			fields[8] = new Street(8, groups[1], 100);
			fields[9] = new Street(9, groups[1], 120);

			fields[11] = new Street(11, groups[2], 140);
			fields[13] = new Street(13, groups[2], 140);
			fields[14] = new Street(14, groups[2], 160);

			fields[16] = new Street(16, groups[3], 180);
			fields[18] = new Street(18, groups[3], 180);
			fields[19] = new Street(19, groups[3], 200);

			fields[21] = new Street(21, groups[4], 220);
			fields[23] = new Street(23, groups[4], 220);
			fields[24] = new Street(24, groups[4], 240);

			fields[26] = new Street(26, groups[5], 260);
			fields[27] = new Street(27, groups[5], 260);
			fields[29] = new Street(29, groups[5], 280);

			fields[31] = new Street(31, groups[6], 300);
			fields[32] = new Street(32, groups[6], 300);
			fields[34] = new Street(34, groups[6], 320);

			fields[37] = new Street(37, groups[7], 350);
			fields[39] = new Street(39, groups[7], 400);

			// railroads
			fields[12] = new Street(12, groups[8], 150);
			fields[28] = new Street(28, groups[8], 150);

			// utility
			fields[5] = new Street(32, groups[9], 200);
			fields[15] = new Street(34, groups[9], 200);
			fields[25] = new Street(32, groups[9], 200);
			fields[35] = new Street(34, groups[9], 200);

			// corners
			fields[0] = new Tile(0, Actions.go);
			fields[10] = new Tile(10, Actions.none);
			fields[20] = new Tile(20, Actions.none);
			fields[30] = new Tile(30, Actions.goToPrison);

			// taxes tiles
			fields[4] = new Tile(4, Actions.payTax200);
			fields[38] = new Tile(38, Actions.payTax100);

			// card tiles
			fields[2] = new Tile(2, Actions.none);
			fields[7] = new Tile(7, Actions.none);
			fields[17] = new Tile(17, Actions.none);
			fields[22] = new Tile(22, Actions.none);
			fields[33] = new Tile(33, Actions.none);
			fields[36] = new Tile(36, Actions.none);
		}

		public bool CanPlayerBuildOnProperty(int index, int playerId)
		{
			if (!(fields[index] is Property))
			{
				return false;
			}

			if (!(fields[index] is Street))
			{
				return false;
			}

			//NOT YET FINISHED
			return false;
		}

		public Field GetFieldAt(int location)
		{
			return fields[location];
		}
	}
}
