using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet_Enemy : Fleet_Base
{
    [SerializeField] float MoveSpeed, DetectDistance;
    [SerializeField] IA_StateMachine _StateMachine;
    [SerializeField] bool _PlayerDetected;
    public Transform _PlayerTransform;


    SpriteRenderer _SpRenderer;
    private void Start()
    {
        _StateMachine = GetComponent<IA_StateMachine>();
        CampaignManager.Instance._EnemyFleets.Add(this);
    }

    private void Update()
    {
        RunLogic();
    }

    protected override void RunLogic()
    {
        _StateMachine.RunStateMachine();
        if(_PlayerDetected == false)
        {
            CheckNearbyEnemy();
        }
        else
        {
            LostPlayer();
        }
    }
    float dist;
    private void LostPlayer()
    {
       dist = Vector3.Distance(_PlayerTransform.transform.position, this.transform.position);
        if(dist > DetectDistance)
        {
            _PlayerDetected = false;
            _PlayerTransform = null;
            _StateMachine.SwitchState(_StateMachine._PatrolState);
        }
    }


    private void CheckNearbyEnemy()
    {
        foreach(Fleet_Player player in CampaignManager.Instance._PlayerFleets)
        {
           dist = Vector3.Distance(player.transform.position,this.transform.position);

            if(dist <  DetectDistance)
            {
                _PlayerDetected = true;
                _PlayerTransform = player.transform;
                _StateMachine.SwitchState(_StateMachine._InterceptState);
                break;
            }
            else
            {
                _PlayerDetected = false;
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, DetectDistance);
    }
}
