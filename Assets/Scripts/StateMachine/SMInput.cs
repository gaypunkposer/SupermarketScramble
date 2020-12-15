namespace StateMachine
{
    public class SMInput
    {
        public SMInput previous;
        public State state;
        public float timeInState;

        protected SMInput(State start)
        {
            state = start;
        }

        protected SMInput(SMInput prevInput) : this(prevInput.state)
        {
            previous = prevInput;
            previous.previous = null;
        }
    }
}
