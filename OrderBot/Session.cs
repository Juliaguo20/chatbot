using System;

namespace OrderBot
{
    public class Session
    {
        private enum State
        {
            WELCOMING, SIZE, PROTEIN
        }

        private State nCur = State.WELCOMING;
        private Order oOrder;

        public Session(string sPhone)
        {
            this.oOrder = new Order();
            this.oOrder.Phone = sPhone;
        }

        public List<String> OnMessage(String sInMessage)
        {
            List<String> aMessages = new List<string>();
            switch (this.nCur)
            {
                case State.WELCOMING:
                    aMessages.Add("Welcome to Julia's Barbecue !");
                    aMessages.Add("Which appetite would you like?");
                    this.nCur = State.SIZE;
                    break;
                case State.SIZE:
                    this.oOrder.Size = sInMessage;
                    this.oOrder.Save();
                    aMessages.Add("What protein would you like on this  " + this.oOrder.Size + " Barbecue?");
                    this.nCur = State.PROTEIN;
                    break;
                case State.PROTEIN:
                    string sProtein = sInMessage;
                    aMessages.Add("What toppings would you like on this (1. pickles 2. Kimchi) " + this.oOrder.Size + " " + sProtein + " ?");
                    break;


            }
            aMessages.ForEach(delegate (String sMessage)
            {
                System.Diagnostics.Debug.WriteLine(sMessage);
            });
            return aMessages;
        }

    }
}
