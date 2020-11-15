namespace PA3BackEnd.src.Monopoly
{
    public class Player
    {
        Property[] propertiesOwned;
        int balance;
        bool inPrison;
        int position;

        public Player(){

            this.propertiesOwned = new Property[28];
            this.balance = 1500;
            this.inPrison = false;
            this.position = 0;

        }

        public void movePlayerTo(int position){
            //TODO
        }

        public void goToJail(){
            this.inPrison = true;
            this.position = 10;

        }

        public void addBalance(int money){
            this.balance = this.balance + money;
        }

        public void subtractBalance(int money){
            this.balance = this.balance - money;
        }

        public void removeProperty(Property property){
            //TODO
        }
        public void addProperty(Property property){
            //TODO
        }

        public Property[] getPropertiesOwned() {
            return propertiesOwned;
        }

        public void setPropertiesOwned(Property[] propertiesOwned) {
            this.propertiesOwned = propertiesOwned;
        }

        public int getBalance() {
            return balance;
        }

        public void setBalance(int balance) {
            this.balance = balance;
        }

        public bool isInPrison() {
            return inPrison;
        }

        public int getPosition() {
            return position;
        }

        public void setPosition(int position) {
            this.position = position;
        }

    }
}