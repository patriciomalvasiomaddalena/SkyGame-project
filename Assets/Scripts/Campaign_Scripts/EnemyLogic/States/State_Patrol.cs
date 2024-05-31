using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class State_Patrol : State
{
    IA_StateMachine _STMachine;

    [SerializeField] Transform _NodeTarg;
    private void Start()
    {
        _STMachine= GetComponentInParent<IA_StateMachine>();
        if (_STMachine.Nodes.Count > 0 && _NodeTarg == null)
        {
            _NodeTarg = _STMachine.Nodes[0];
        }

    }

    public override void EnterState()
    {

    }


    public override void ExitState()
    {

    }

    float dist;
    float pulse;
    public override void UpdateState()
    {
        if(_NodeTarg == null)
        {
            FindNewTarget();
            return;
        }
        if(dist < _STMachine._PatrolDist)
        {
            if(pulse <=_STMachine._PatrolRest)
            {
                pulse = pulse + (1 * Time.deltaTime);
                return;
            }
            else
            {
                FindNewTarget();
                pulse = 0;
                return;
            }
        }

        dist = Vector3.Distance(_NodeTarg.transform.position, _STMachine.transform.position);
        _STMachine.transform.position = Vector3.MoveTowards(_STMachine.transform.position, _NodeTarg.transform.position, _STMachine._PatrolVel * Time.deltaTime);
    }

    float _nextNode;
    private void FindNewTarget()
    {
        if(_nextNode >= _STMachine.Nodes.Count)
        {
            _NodeTarg = _STMachine.Nodes[0];
            _nextNode = 0;
        }
        else
        {
            _NodeTarg = _STMachine.Nodes[(int)_nextNode];
            _nextNode++;
        }
        dist = Vector3.Distance(_NodeTarg.transform.position, _STMachine.transform.position);
    }
}
