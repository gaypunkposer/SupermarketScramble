using UnityEngine;

namespace StateMachine
{
    public class SMRunner : MonoBehaviour
    {
        public SMInput LastInput => _stateMachine.LastInput;
        
        public bool running;
        public GameObject stateContainer;
        public int initialStateIndex;

        private Machine _stateMachine;
        
        public void RefreshMachine()
        {
            ISMInputProducer producer = GetComponent<ISMInputProducer>();
            ISMOutputConsumer[] consumers = GetComponents<ISMOutputConsumer>();
            State[] states = stateContainer.GetComponents<State>();

            _stateMachine = new Machine(producer, states[initialStateIndex]);
            
            foreach (ISMOutputConsumer c in consumers)
                _stateMachine.RegisterOutput(c);
            foreach (State s in states)
                _stateMachine.RegisterState(s);
        }
        
        private void Start()
        {
            RefreshMachine();
        }

        private void Update()
        {
            if (!running) return;

            _stateMachine.Run(Time.deltaTime);
        }
    }
}
