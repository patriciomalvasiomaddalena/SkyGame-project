using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet_Enemy : Fleet_Base
{
    [SerializeField] float MoveSpeed;
    [SerializeField] IA_StateMachine _StateMachine;


    SpriteRenderer _SpRenderer;
    private void Start()
    {
        _StateMachine = GetComponent<IA_StateMachine>();
    }

    private void Update()
    {
        RunLogic();
    }

    protected override void RunLogic()
    {

    }


}
