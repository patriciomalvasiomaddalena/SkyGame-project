using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_StateMachine : MonoBehaviour
{
    public List<Transform> Nodes;

    [SerializeField] State _CurrentState;
    [SerializeField] public float _PatrolVel,_PatrolDist,_PatrolRest;
    private void Start()
    {
        if(_CurrentState != null)
        {
            _CurrentState.EnterState();
        }
    }

    private void Update()
    {
        if(_CurrentState != null)
        {
            _CurrentState.UpdateState();
        }
    }


    public void SwitchState(State state)
    {
        if(state != _CurrentState) 
        {
            _CurrentState.ExitState();
        }
        _CurrentState = state;
        _CurrentState.EnterState();
    }

}
