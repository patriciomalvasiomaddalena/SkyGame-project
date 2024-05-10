using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public delegate void MovementDelegate(Color SetColor);
    public static  event MovementDelegate _movementDelegate;

    [SerializeField] Move_Base _MovementInput;

    [SerializeField] InsiderManager _InsiderManagerScript;

    [SerializeField] float _Speed; // velocidad final; observacion, parece que cada 50 velocidad x 1 masa es velocidad rapida.
    [SerializeField] float _Weight; // peso final
    [SerializeField] Vector2 _MovSpeed;

    Vector2 _Moveaxis;

    bool _TriggeredEvent;

    Rigidbody2D _2DRb;

    private void Awake()
    {
        _2DRb= GetComponent<Rigidbody2D>();

        _InsiderManagerScript = GetComponentInParent<InsiderManager>();
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


    //ideas, cuando el jugador no deja inputs, incrementar la gravedad para hacer que caiga mas rapido

    private void MovementLogic()
    {
        _Moveaxis = _MovementInput.RunLogic();
        _2DRb.AddForce(_Moveaxis * _Speed, ForceMode2D.Force);

        if (_2DRb.velocity != Vector2.zero && _TriggeredEvent == false)
        {
            _movementDelegate?.Invoke(Random.ColorHSV());
            _TriggeredEvent= true;
        }

        if(_2DRb.velocity == Vector2.zero )
        {
            _TriggeredEvent = false;
        }

        _MovSpeed = _2DRb.velocity;
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

    public void AddSpeed(float Speed)
    {
        _Speed += Speed;
    }
}
