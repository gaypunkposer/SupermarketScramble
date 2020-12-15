namespace StateMachine
{
    public class SMOutput
    {
        public readonly SMInput input;

        protected SMOutput(SMInput input)
        {
            this.input = input;
        }
    }
}
