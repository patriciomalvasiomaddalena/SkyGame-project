using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAState_Intercept : State
{
    IA_StateMachine _STMachine;

    [SerializeField] Transform _NodeTarg;

    private void Start()
    {
        _STMachine = GetComponentInParent<IA_StateMachine>();
    }


    public override void EnterState()
    {
        _NodeTarg = _STMachine._EnemyCore._PlayerTransform;
    }

    public override void ExitState()
    {
   
    }

    public override void UpdateState()
    {
        _STMachine.transform.position = Vector3.MoveTowards(_STMachine.transform.position, _NodeTarg.transform.position, _STMachine._PatrolVel * Time.deltaTime);
    }
}
