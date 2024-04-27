using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeComp : MonoBehaviour
{
    public delegate void TakeDamage(Color DMG);
    public static event TakeDamage _TakeDamageEvent;

    public delegate void PlayerDeath(Color DMG);
    public static event TakeDamage _TotalPlayerDeath;

    public delegate void PlayerReset();
    public static event PlayerReset _ResetAll;
    [SerializeField] float _DMG,_Health;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            if((_Health - _DMG) > 0) 
            {
                _Health -= _DMG;
                _TakeDamageEvent?.Invoke(Color.red);
            }
            else
            {
                _TotalPlayerDeath?.Invoke(Color.black);
            }
        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            if(_Health <= 0)
            {
                _Health += _DMG;
                _ResetAll.Invoke();
            }
            else
            {
                _Health += _DMG;
            }
        }
    }
}
