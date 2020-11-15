namespace PA3BackEnd.src.Monopoly
{
    public class Tile : Field
    {
        private Actions action;
        public Tile(int location, Actions action) : base(location)
        {
            this.action = action;
        }

        public override Actions GetAction()
        {
            return action;
        }
    }
}
