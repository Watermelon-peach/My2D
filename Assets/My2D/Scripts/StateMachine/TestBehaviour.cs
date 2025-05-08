using UnityEngine;

namespace My2D
{
    //애니메이터의 SubStateMachine이나 State에 부착되는 스크립트
    public class TestBehaviour : StateMachineBehaviour
    {
        #region Variables
        public bool updateOnState;
        public bool updateOnStateMachine;
        #endregion

        // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (updateOnState)
            {
                Debug.Log("OnStateEnter 작동");
            }
        }

        // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (updateOnState)
            {
                Debug.Log("OnStateUpdate 작동");
            }
        }

        // OnStateExit is called before OnStateExit is called on any state inside this state machine
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (updateOnState)
            {
                Debug.Log("OnStateExit 작동");
            }
        }

        // OnStateMove is called before OnStateMove is called on any state inside this state machine
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateIK is called before OnStateIK is called on any state inside this state machine
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateMachineEnter is called when entering a state machine via its Entry Node
        override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            if (updateOnStateMachine)
            {
                Debug.Log("OnStateMachineEnter 작동");
            }
        }

        // OnStateMachineExit is called when exiting a state machine via its Exit Node
        override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            if (updateOnStateMachine)
            {
                Debug.Log("OnStateMachineExit 작동");
            }
        }
    }

}
