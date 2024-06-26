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
        FoundTargetBool = false;
    }

    public override void ExitState()
    {
   
    }

    bool FoundTargetBool;
    public override void UpdateState()
    {
        _STMachine.transform.position = Vector3.MoveTowards(_STMachine.transform.position, _NodeTarg.transform.position, _STMachine._PatrolVel * Time.deltaTime);
        if(Vector3.Distance(_NodeTarg.transform.position,_STMachine._EnemyCore.transform.position) < 0.1f)
        {
            //ScreenManager.Instance.PushScreen("IDFight");
            if (FoundTargetBool == false)
            {
                FoundTarget();
                FoundTargetBool = true;
            }
        }
    }

    private void FoundTarget()
    {
        FightSetup._Instance.DragAllFleets(_STMachine._EnemyCore.transform);
    }
}
