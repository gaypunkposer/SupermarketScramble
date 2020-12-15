using Interactions;
using StateMachine;
using UnityEngine;

namespace GameRules.AI.SM
{
    public class AIInput : SMInput
    {
        public readonly bool disabled;
        public readonly bool canSeePlayer;
        public Transform latestObjectOfInterest;
        public float suspicion;
        public Item[] misplacedItems;
    
        public AIInput(State start) : base(start)
        {
        }

        public AIInput(SMInput prevInput, bool dis, bool seePlayer, Transform noiseLoc, float sus, Item[] items) : base(prevInput)
        {
            disabled = dis;
            canSeePlayer = seePlayer;
            latestObjectOfInterest = noiseLoc;
            suspicion = sus;
            misplacedItems = items;
        }
    }
}
