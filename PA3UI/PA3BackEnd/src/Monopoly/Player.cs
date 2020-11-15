using System.Collections.Generic;

namespace PA3BackEnd.src.Monopoly
{
    public class Player
    {
        List<Property> propertiesOwned;
        public int balance { private set; get; }
        public bool inPrison { private set; get; }
        public int position { private set; get; }

        public int inPrisonCounter { private set; get; }

        public Player(){

            this.propertiesOwned = new List<Property>();
            this.balance = 1500;
            this.inPrison = false;
            this.position = 0;
            this.inPrisonCounter = 0;

        }

        public void movePlayerForward(int position){
            this.position += position;
            if (this.position >= 40)
            {
                this.position -= 40;
                balance += 200;
            }
        }

        public void goToJail(){
            this.inPrison = true;
            this.position = 10;
        }

        public void UpdateInPrisonCounter()
        {
            inPrisonCounter++;
        }

        public void GetOutOfJail()
        {
            this.inPrison = false;
            this.inPrisonCounter = 0;
        }

        public void addBalance(int money){
            this.balance = this.balance + money;
        }

        public void subtractBalance(int money){
            this.balance = this.balance - money;
        }

        public void removeProperty(Property property){
            propertiesOwned.Add(property);
        }
        public void addProperty(Property property){
            propertiesOwned.Add(property);
        }

        public List<Property> getPropertiesOwned() {
            return propertiesOwned;
        }

        public void setPosition(int position) {
            this.position = position;
        }

        public bool HasEnoughMoney(int amount)
        {
            if (balance >= amount)
            {
                return true;
            }
            return false;
        }

    }
}