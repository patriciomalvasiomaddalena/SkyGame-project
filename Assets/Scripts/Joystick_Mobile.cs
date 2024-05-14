using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickMobile : Controller, IDragHandler, IEndDragHandler
{
    
    Vector3 _initialPos;
    [SerializeField, Range(75, 150)] float _maxMagnitude = 125f;


    void Start()
    {
        _initialPos = transform.position;
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

 

    public override Vector3 GetMovementInput()
    {
        throw new System.NotImplementedException();
    }
}
