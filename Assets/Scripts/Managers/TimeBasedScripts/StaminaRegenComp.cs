using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StaminaRegenComp : MonoBehaviour
{
    public float _MaxStaminaCount;
    public float _CurrentStamina;
    public float _StaminaRegen;

    [SerializeField] DateTime _NextStaminaTime;
    [SerializeField] DateTime _LastStaminaTime;

    string CurrentTimeKey;
    string LastTimeKey;
    string NextTimeKey;

    public void Awake()
    {
        LastTimeKey = "LastTimeKey" + this.GetInstanceID();
        NextTimeKey = "NextTimeKey" + this.GetInstanceID();
    }
    private void Start()
    {
        LoadData();
    }

    #region SaveData
    private void LoadData()
    {
        _CurrentStamina = PlayerPrefs.GetFloat(CurrentTimeKey,0);
        _NextStaminaTime = StringToDataTime(PlayerPrefs.GetString(NextTimeKey));
        _LastStaminaTime = StringToDataTime(PlayerPrefs.GetString(LastTimeKey));
    }

    private void SaveData()
    {
        PlayerPrefs.SetFloat("CurrentStaminaKey", _CurrentStamina);
        PlayerPrefs.SetString(NextTimeKey, _NextStaminaTime.ToString());
        PlayerPrefs.SetString(LastTimeKey, _LastStaminaTime.ToString());
    }

    private void DeleteAllCache()
    {
    }

    private DateTime StringToDataTime(string EnterValue)
    {
        if (string.IsNullOrEmpty(EnterValue)) return DateTime.UtcNow;
        else return DateTime.Parse(EnterValue);
    }
    #endregion
    #region:OnApplications
    private void OnApplicationPause(bool pause)
    {
        if (pause) SaveData();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus) SaveData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
    #endregion


    bool recharging;


    public float RechargeStamina(float CurrentStamina, float MaxStamina, TimeScale TimeMod)
    {
        recharging = true;
        float ReturningStamina = CurrentStamina;
        _NextStaminaTime = AddDuration(DateTime.Now, ReturningStamina,TimeMod);

        while (ReturningStamina < MaxStamina)
        {
            DateTime Current = DateTime.Now;
            DateTime NextTime = _NextStaminaTime;

            bool AddStamina = false;

            while (Current > NextTime)
            {

                if (ReturningStamina >= MaxStamina) break; // -1 por errores de redondeo de conversion entre int y float
                ReturningStamina++;

                AddStamina = true;
                // predecir siguiente suma de stamina

                DateTime TimeToAdd = NextTime;
                // chequear si el usuario cerro la app
                if (_LastStaminaTime > NextTime)
                {
                    TimeToAdd = _LastStaminaTime;
                }

                NextTime = AddDuration(TimeToAdd,_StaminaRegen,TimeMod);
            }

            if (AddStamina)
            {
                _NextStaminaTime = NextTime;
                _LastStaminaTime = DateTime.Now;
            }
            Debug.Log(_NextStaminaTime.ToString());
            SaveData();

        }
        recharging = false;
        return ReturningStamina;
    }

    public enum TimeScale
    {
        Seconds,
        minutes,
        hours,
        days,
        months
    }  

    DateTime AddDuration(DateTime TimeToAdd,float TimerToRecharge,TimeScale TimeScaleModifier)
    {
        switch (TimeScaleModifier)
        {
            case TimeScale.Seconds:
                TimeToAdd.AddSeconds(TimerToRecharge);
                break;

            case TimeScale.minutes:
                TimeToAdd.AddMinutes(TimerToRecharge);
                break;

            case TimeScale.hours:
                 TimeToAdd.AddHours(TimerToRecharge);
                break;

            case TimeScale.days:
                 TimeToAdd.AddDays(TimerToRecharge);
                break;

            case TimeScale.months:
                 TimeToAdd.AddMonths((int)TimerToRecharge);
                break;

            default:
                TimeToAdd.AddSeconds(TimerToRecharge);
                break;
        }

        return TimeToAdd;
    }

}
