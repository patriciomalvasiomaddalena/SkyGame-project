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

    [SerializeField] float _Speed; // velocidad final;
    [SerializeField] float _MaxSpeedClamp; // clamp de velocidad final, SOLO SE APLICA A LA IA
    [SerializeField] float _Weight; // peso final
    [SerializeField] Vector2 _MovSpeed;
    [SerializeField] bool IsPlayer;
    [SerializeField] ControllerType _CurrentController;

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
        if(IsPlayer == false)
        {
            _InsiderManagerScript.SubscribeToEvent(InsiderEventType.NPC_Event_CommandDeath, Death);
        }
        else
        {
            _InsiderManagerScript.SubscribeToEvent(InsiderEventType.Event_CommandDeath, Death);
        }
        LifeComp._ResetAll += ResetComp;
        if (IsPlayer)
        {
            GameManager.Instance.PlayerShip = this.gameObject;
            GameManager _GManager = FindObjectOfType<GameManager>();
            _GManager.PlayerShip = this.gameObject;

            SwitchControllerInput();
        }
    }

    private void  SwitchControllerInput()
    {
        _CurrentController = config_manager._Instance.CurrentController;

        switch (_CurrentController)
        {
            case ControllerType.KyM:
                _MovementInput = GetComponentInChildren<Movement_Keyboard>();
                break;

            case ControllerType.Gyro_Touch:
                _MovementInput = GetComponent<Movement_Accelerometer>();

                break;
            case ControllerType.Joystick:
                _MovementInput = config_manager._Instance.JoystickMovement;

                break;
        }
        _MovementInput.gameObject.SetActive(true);
        _MovementInput.enabled = true;
    }



    //ideas, cuando el jugador no deja inputs, incrementar la gravedad para hacer que caiga mas rapido

    private void MovementLogic()
    {
        _Moveaxis = _MovementInput.RunLogic();

        _2DRb.AddForce(_Moveaxis * _Speed, ForceMode2D.Force);
        if (!IsPlayer)
        {
            if(_2DRb.velocity.magnitude > _MaxSpeedClamp)
            {
                _2DRb.velocity = Vector2.ClampMagnitude(_2DRb.velocity, _MaxSpeedClamp);
            }
        }
        _MovSpeed = _2DRb.velocity;
        this.transform.rotation = Quaternion.identity;
    }

    private void Death(object[] a)
    {
        if (IsPlayer)
        {
            _InsiderManagerScript.UnSubscribeToEvent(InsiderEventType.Event_CommandDeath, Death);
        }
        else
        {
            _InsiderManagerScript.UnSubscribeToEvent(InsiderEventType.NPC_Event_CommandDeath, Death);
        }
          
        this.enabled = false;

    }

    private void ResetComp()
    {
    }

    public void AddSpeed(float Speed)
    {
        _Speed += Speed;
    }
}
