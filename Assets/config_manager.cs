using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ControllerType 
{
    KyM,
    Joystick,
    Gyro_Touch,
    Default
}

public class config_manager : MonoBehaviour
{
    public static config_manager _Instance;
    public ControllerType CurrentController;

    PlayerPrefs _PlayerPrefs;

    public static event Action OnControlKeyboardMouse;
    public static event Action OnControlJoystick;
    public static event Action OnControlGyro;

    [Header("References")]
    public Aim_Turret_Joystick JoystickAim;
    public JoystickMobile JoystickMovement;


    private void Awake()
    {
        if(_Instance == null && _Instance != this)
        {
            _Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        FindJoysticks(); 
    }

    private void FindJoysticks()
    {
        if (JoystickAim == null || JoystickMovement == null)
        {
            JoystickAim = FindObjectOfType<Aim_Turret_Joystick>(true);
            JoystickMovement = FindObjectOfType<JoystickMobile>(true);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        FindJoysticks();
    }

    public void SwitchControllers(ControllerType _NewControlScheme)
    {
        switch (_NewControlScheme)
        {
            case ControllerType.KyM:
                CurrentController = ControllerType.KyM;
                OnControlKeyboardMouse?.Invoke();
                break;

            case ControllerType.Joystick:
                CurrentController = ControllerType.Joystick;
                JoystickMovement.gameObject.SetActive(true);
                OnControlJoystick?.Invoke();
                break; 

            case ControllerType.Gyro_Touch:
                CurrentController = ControllerType.Gyro_Touch;
                JoystickMovement.gameObject.SetActive(false);
                OnControlGyro?.Invoke(); 
                                break;
        }
    }

    public void SwitchControllers(int _newControlScheme)
    {
        switch (_newControlScheme)
        {
            case 0:
                CurrentController = ControllerType.KyM;
                OnControlKeyboardMouse?.Invoke();
                break;

            case 1:
                CurrentController = ControllerType.Joystick;
                OnControlJoystick?.Invoke();
                break;

            case 2:
                CurrentController = ControllerType.Gyro_Touch;
                OnControlGyro?.Invoke();
                break;
        }
    }

}
