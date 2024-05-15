using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        GameManager.Instance.PlayerShip = this.gameObject;
    }


    //ideas, cuando el jugador no deja inputs, incrementar la gravedad para hacer que caiga mas rapido

    private void MovementLogic()
    {
        _Moveaxis = _MovementInput.RunLogic();

            _2DRb.AddForce(_Moveaxis * _Speed, ForceMode2D.Force);
        _MovSpeed = _2DRb.velocity;
    }

    private void TotalPlayerDeath(Color rmf)
    {
    }

    private void ResetComp()
    {
    }

    public void AddSpeed(float Speed)
    {
        _Speed += Speed;
    }
}
