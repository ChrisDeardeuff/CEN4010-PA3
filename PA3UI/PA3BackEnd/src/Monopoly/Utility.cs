using System;
using System.Diagnostics;

namespace PA3BackEnd.src.Monopoly
{
    public class Utility: Property
    {
        public Utility(int location, Group group, int price, string name):base(location, group, price, name){
        }
        
        public override bool CanBeMortaged() {
            return false;
        }
        
        public override int GetRent() {
            Debug.Fail("GetRent(int role) should be called instead of this");
            return 0;
        }

        public int GetRent(int role)
        {
            return role * 12;
        }
    }
}