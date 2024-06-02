using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_StateMachine : MonoBehaviour
{
    public List<Transform> Nodes;
    public Fleet_Enemy _EnemyCore { get; private set; }

    [SerializeField] public State _CurrentState { get;private set;}
    [SerializeField] public State _PatrolState { get;private set;}
    [SerializeField] public State _InterceptState { get;private set;}
    public float _PatrolVel,_PatrolDist,_PatrolRest;
    private void Start()
    {
        _CurrentState = GetComponentInChildren<State_Patrol>();
        _PatrolState = GetComponentInChildren<State_Patrol>();
        _InterceptState = GetComponentInChildren<IAState_Intercept>();
        _EnemyCore = GetComponent<Fleet_Enemy>();
        if (_CurrentState != null)
        {
            _CurrentState.EnterState();
        }
        else
        {
            Debug.Log("current state is null" + _CurrentState.ToString());
        }
    }
    public void RunStateMachine()
    {
        //if(pausado) = > return;

        if(_CurrentState != null)
        {
            _CurrentState.UpdateState();
        }
        else
        {
            Debug.Log("current state is null" + _CurrentState.ToString());
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
