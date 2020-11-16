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
			fields[1] = new Street(1, groups[0], 60, new int[] {2, 10, 30, 90, 160, 250 }, "Mediterranean Ave");
			fields[3] = new Street(3, groups[0], 60, new int[] { 4, 20, 60, 180, 320, 450 }, "Baltic Ave");

			fields[6] = new Street(6, groups[1], 100, new int[] { 4, 20, 60, 180, 320, 450 }, "Baltic Ave");
			fields[8] = new Street(8, groups[1], 100, new int[] { 4, 20, 60, 180, 320, 450 }, "Baltic Ave");
			fields[9] = new Street(9, groups[1], 120, new int[] { 4, 20, 60, 180, 320, 450 }, "Baltic Ave");

			fields[11] = new Street(11, groups[2], 140, new int[] { 4, 20, 60, 180, 320, 450 }, "Baltic Ave");
			fields[13] = new Street(13, groups[2], 140, new int[] { 4, 20, 60, 180, 320, 450 }, "Baltic Ave");
			fields[14] = new Street(14, groups[2], 160, new int[] { 4, 20, 60, 180, 320, 450 }, "Baltic Ave");

			fields[16] = new Street(16, groups[3], 180, new int[] { 4, 20, 60, 180, 320, 450 }, "Baltic Ave");
			fields[18] = new Street(18, groups[3], 180, new int[] { 4, 20, 60, 180, 320, 450 }, "Baltic Ave");
			fields[19] = new Street(19, groups[3], 200, new int[] { 4, 20, 60, 180, 320, 450 }, "Baltic Ave");

			fields[21] = new Street(21, groups[4], 220, new int[] { 4, 20, 60, 180, 320, 450 }, "Baltic Ave");
			fields[23] = new Street(23, groups[4], 220, new int[] { 4, 20, 60, 180, 320, 450 }, "Baltic Ave");
			fields[24] = new Street(24, groups[4], 240, new int[] { 4, 20, 60, 180, 320, 450 }, "Baltic Ave");

			fields[26] = new Street(26, groups[5], 260, new int[] { 4, 20, 60, 180, 320, 450 }, "Baltic Ave");
			fields[27] = new Street(27, groups[5], 260, new int[] { 4, 20, 60, 180, 320, 450 }, "Baltic Ave");
			fields[29] = new Street(29, groups[5], 280, new int[] { 4, 20, 60, 180, 320, 450 }, "Baltic Ave");

			fields[31] = new Street(31, groups[6], 300, new int[] { 4, 20, 60, 180, 320, 450 }, "Baltic Ave");
			fields[32] = new Street(32, groups[6], 300, new int[] { 4, 20, 60, 180, 320, 450 }, "Baltic Ave");
			fields[34] = new Street(34, groups[6], 320, new int[] { 4, 20, 60, 180, 320, 450 }, "Baltic Ave");

			fields[37] = new Street(37, groups[7], 350, new int[] { 4, 20, 60, 180, 320, 450 }, "Baltic Ave");
			fields[39] = new Street(39, groups[7], 400, new int[] { 4, 20, 60, 180, 320, 450 }, "Baltic Ave");

			// railRoads
			fields[5]  = new Railroads(5, groups[8], 200, "Baltic Ave");
			fields[15] = new Railroads(15, groups[8], 200, "Baltic Ave");
			fields[25] = new Railroads(25, groups[8], 200, "Baltic Ave");
			fields[35] = new Railroads(35, groups[8], 200, "Baltic Ave");

			// utility
			fields[12] = new Utility(12, groups[9], 150, "Baltic Ave");
			fields[28] = new Utility(28, groups[9], 150, "Baltic Ave");

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
