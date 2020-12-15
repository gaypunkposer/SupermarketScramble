using System.Collections.Generic;
using System.Linq;

namespace StateMachine
{
    public class Machine
    {
        public SMInput LastInput { get; private set; }
        
        private readonly ISMInputProducer _producer;
        private readonly List<ISMOutputConsumer> _outputs;
        private readonly List<State> _states;
        
        public Machine(ISMInputProducer prod, State initial)
        {
            _producer = prod;
            _outputs = new List<ISMOutputConsumer>();
            _states = new List<State> {initial};
            
            LastInput = _producer.GetInput(_states[0]);
        }

        public void RegisterState(State st)
        {
            if (_states.Contains(st)) return;
            _states.Add(st);
            _states.Sort((a, b) =>
                -a.Priority.CompareTo(b.Priority));
        }
        
        public void RegisterOutput(ISMOutputConsumer cons)
        {
            if (_outputs.Contains(cons)) return;
            _outputs.Add(cons);
            _outputs.Sort((a, b) =>
                -a.GetPriority().CompareTo(b.GetPriority()));    
        }
        
        public void Run(float deltaTime)
        {
            SMInput currentInput = _producer.GetInput(LastInput);
            State chosenState = _states.FirstOrDefault(s => s.ShouldRunThisState(currentInput));

            if (chosenState == null)
                return;
            
            currentInput.state = chosenState;
            //Effectively: if previous isn't null and current state is equal to previous state, add to the time
            currentInput.timeInState = 
                currentInput.state == currentInput.previous?.state ? currentInput.previous.timeInState + deltaTime : 0;
            
            SMOutput output = chosenState.Run(currentInput);

            foreach (ISMOutputConsumer r in _outputs)
            {
                r.ReceiveOutput(output);
            }

            LastInput = currentInput;
        }
    }
}
