using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public delegate void MovementDelegate(Color SetColor);
    public static  event MovementDelegate _movementDelegate;

    [SerializeField]float _Speed;
    Vector2 _Moveaxis;

    bool _TriggeredEvent;

    Rigidbody2D _2DRb;

    private void Awake()
    {
        _2DRb= GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MovementLogic();
    }

    private void Start()
    {
        LifeComp._TotalPlayerDeath += TotalPlayerDeath;
        LifeComp._ResetAll += ResetComp;
    }

    private void MovementLogic()
    {
        _Moveaxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _2DRb.velocity = _Moveaxis * (_Speed * Time.fixedDeltaTime);

        if (_2DRb.velocity != Vector2.zero && _TriggeredEvent == false)
        {
            _movementDelegate?.Invoke(Random.ColorHSV());
            _TriggeredEvent= true;
        }

        if(_2DRb.velocity == Vector2.zero )
        {
            _TriggeredEvent = false;
        }
    }

    private void TotalPlayerDeath(Color rmf)
    {
        LifeComp._TotalPlayerDeath -= TotalPlayerDeath;
        this.enabled = false;
    }

    private void ResetComp()
    {
        this.enabled = true;
        LifeComp._TotalPlayerDeath += TotalPlayerDeath;
    }
}
