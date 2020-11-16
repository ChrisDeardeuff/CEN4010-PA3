using System;
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
            propertiesOwned.Remove(property);
        }

        public void addProperty(Property property)
        {
            propertiesOwned.Add(property);
            propertiesOwned.Sort((Property l1,Property l2) => l1.GetLocation().CompareTo(l2.GetLocation()));
            
            List<Property> tempList = new List<Property>();
            List<Property> tempList2 = new List<Property>();
            
            foreach (var p in propertiesOwned)
            {
                if (p.GetLocation() == 5 || p.GetLocation() == 15 || p.GetLocation() == 25 || p.GetLocation() == 35 )
                {
                    tempList.Add(p);
                    
                }else if (p.GetLocation() == 12 || p.GetLocation() == 28)
                {
                    tempList2.Add(p);
                    
                }
            }

            foreach (var p in tempList)
            {
                propertiesOwned.Remove(p);
                propertiesOwned.Add(p);
            }
            foreach (var p in tempList2)
            {
                propertiesOwned.Remove(p);
                propertiesOwned.Add(p);
            }
            
        }

        public List<Property> getPropertiesOwned() {
            return propertiesOwned;
        }

        public bool HasEnoughMoney(int amount)
        {
            if (balance >= amount)
            {
                return true;
            }
            return false;
        }

        public int CalculateScore()
        {
            int score = balance;

            foreach (var property in propertiesOwned)
            {
                //if developed
                if (property.developmentValue > 0)
                {
                    for (int i = property.developmentValue; i > 0; i--)
                    {
                        score += property.group.priceToBuild;
                    }
                }else if (property.developmentValue < 0)
                {
                    score -= property.price / 2;
                    
                    continue;
                }

                score += property.price / 2;
            }
            
            return score;
        }
    }
}