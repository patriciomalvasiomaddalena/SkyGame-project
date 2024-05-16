using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Aim_Turret_Joystick : AimBase ,IDragHandler,IEndDragHandler
{
    public override event OnPlayerShooting PlayerShoot;

    Vector3 _initialPos;
    Vector3 _moveDir;
    [SerializeField, Range(75, 150)] float _maxMagnitude = 125f;


    void Start()
    {
        _initialPos = transform.position;
    }

    public void ShootMethod()
    {
        PlayerShoot.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _moveDir = Vector3.ClampMagnitude((Vector3)eventData.position - _initialPos, _maxMagnitude);
        transform.position = _initialPos + _moveDir;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = _initialPos;
        _moveDir = Vector3.zero;
    }

    public override Vector3 RunLogic(Transform _GunTransform)
    {
        Vector3 modifiedDir = new Vector3(_moveDir.x, _moveDir.y, 0);
        modifiedDir /= _maxMagnitude;
        return modifiedDir;
    }
}
