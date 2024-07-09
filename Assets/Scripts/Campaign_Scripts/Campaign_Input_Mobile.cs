using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Campaign_Input_Mobile : Campaign_Input_Base
{
    Vector3 Dire;
    [SerializeField] LineRendererController _LRC;
    private Vector2 touchStartPos;
    private Vector2 touchDeltaPos;
    private bool isDragging;
    private Vector2 _LRCTarget;

    private void Start()
    {
        _LRC.Points[1] = this.transform.position;
    }
    public override Vector3 InputMachine(Transform FleetTransform)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    isDragging = false;
                    break;

                case TouchPhase.Moved:

                    touchDeltaPos = touch.position - touchStartPos;
                    isDragging = true;
                    _LRCTarget = Camera.main.ScreenToWorldPoint (touch.position);
                    _LRC.Points[1] =_LRCTarget ;
                    break;

                case TouchPhase.Ended:
                    if (isDragging)
                    {
                        Dire = Camera.main.ScreenToWorldPoint(touch.position);
                        Dire.z = 0f;
                        _LRC.Points[1] = Dire;
                    }
                    isDragging = false;
                    break;
            }
        }
        
        return Dire;
    }
}

