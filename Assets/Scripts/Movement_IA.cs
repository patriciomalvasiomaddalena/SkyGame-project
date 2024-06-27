using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_IA : Move_Base
{
    [SerializeField] Transform _TargetPos;
    [SerializeField] float _MinDistance,_ArrivalDist;
    [SerializeField] float ModDist;

    float Distance;


    public override Vector3 RunLogic()
    {
        if(_TargetPos!= null)
        {
            Distance = Vector3.Distance(_TargetPos.position, transform.position);

            if(Distance < _ArrivalDist)
            {
                ModDist = -0.2f*(Distance/ _ArrivalDist);
            }
            else
            {
                ModDist = 1f;
            }

            if (Distance < _MinDistance) 
            {
                CreateNewNode();
            }
        }
        else
        {
            CreateNewNode();
        }
        Vector3 Director = (_TargetPos.transform.position - this.transform.position).normalized * ModDist;
        return Director;
    }

    private void CreateNewNode()
    {

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, _ArrivalDist);
    }

}
