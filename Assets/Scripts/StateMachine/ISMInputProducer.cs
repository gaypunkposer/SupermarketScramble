namespace StateMachine
{
    public interface ISMInputProducer
    {
        //Get input from an initial state
        SMInput GetInput(State availableState);
        //Get input, using a previous state to fill in gaps
        SMInput GetInput(SMInput availableState);
    }
}
