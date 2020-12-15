namespace StateMachine
{
    public interface ISMOutputConsumer
    {
        //Which consumer gets access to the output first
        int GetPriority();
        //Do something with the output
        void ReceiveOutput(SMOutput output);
    }
}
