using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Fleet_Player :Fleet_Base
{
    public static List<Fleet_Player> MovablePlayerFleet= new List<Fleet_Player>();

    [SerializeField] float MoveSpeed;
    [SerializeField] Campaign_Input_Base MoveInput;
    [SerializeField] LineRendererController _LRC;
    [SerializeField] CircleLineDrawer _FuelRend;
    [SerializeField] StaminaRegenComp _StaminaComp;
    Vector3 Dire;
    private bool Selected,_HasFuel;


    public float FuelAmount, MaxFuel, FuelRegenRate;
    public float FuelEff;

    SpriteRenderer _SpRenderer;
    private void Start()
    {
        if (_FleetComposition.Count <= 0)
        {
            Destroy(this.gameObject);
        }

        _SpRenderer = GetComponent<SpriteRenderer>();
        FuelAmount = MaxFuel;
        MovablePlayerFleet.Add(this);
        _LRC = GetComponent<LineRendererController>();
        _LRC.Points[0] = this.transform.position;
        CampaignManager.Instance._PlayerFleets.Add(this);
        _StaminaComp = GetComponent<StaminaRegenComp>();
        RegenerateStamina();
    }

    private void RegenerateStamina()
    {
        _StaminaComp._CurrentStamina = FuelAmount;
        _StaminaComp._MaxStaminaCount = MaxFuel;
        _StaminaComp._StaminaRegen = FuelRegenRate;
        FuelAmount = _StaminaComp.RechargeStamina(_StaminaComp._CurrentStamina, MaxFuel, StaminaRegenComp.TimeScale.Seconds);
    }

    private void OnEnable()
    {
        if (_FleetComposition.Count <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (!CampaignManager.ShopManagerInstance.PlayerIsInCityUI)
        {
          RunLogic();
        }
    }

    protected override void RunLogic()
    {
        if (Selected || Dire != Vector3.zero)
        {
            if(FuelAmount > 0.1f)
            {
                _HasFuel = true;
            }
            else
            {
                _HasFuel = false;
            }
             MovementLogic();
            _FuelRend.DrawCircle();
            _FuelRend._Radius = FuelAmount / 10f;
            int fcount = (int)FuelAmount;
            UIManager.Instance.SetTMP("FuelTMP","Fuel: " + fcount.ToString());
        }
        else
        {
            _FuelRend._Radius = 0;
            Dire = Vector3.zero;
        }
        _LRC.Points[0] = this.transform.position;
    }

    private void MovementLogic()
    {
        if (Selected)
        {
            Dire = MoveInput.InputMachine(this.transform);
        }
        if(Dire != Vector3.zero && _HasFuel == true)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, Dire, MoveSpeed * Time.deltaTime);
            float dist = Vector3.Distance(Dire, this.transform.position);
            if (dist > 0.1f)
            {
                ConsumeFuel(dist,Dire);
            }
        }
    }

    [SerializeField] float _InitialFuel,FinalFuel,TotalDist;
    Vector3 NewDirection = Vector3.zero;
    private void ConsumeFuel(float Dist, Vector3 Director)
    {
        if(Director != NewDirection)
        {
            _InitialFuel = FuelAmount;
            NewDirection = Director;
            TotalDist = Dist; // 5 *( 10 * 1)
        }

        float TotalFuelCons = (TotalDist * (10f * FuelEff));
        FinalFuel = TotalFuelCons;
        if(FinalFuel >= FuelAmount)
        {
            FinalFuel = 0;
        }
        else
        {
            FinalFuel = FuelAmount - FinalFuel;
        }
        //FuelAmount = Mathf.Lerp(FuelAmount, FinalFuel,Time.deltaTime);
        FuelAmount = Mathf.MoveTowards(FuelAmount, FinalFuel,TotalFuelCons * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        Selected = true;
        foreach(var Pfleet in MovablePlayerFleet)
        {
            if(Pfleet != this)
            {
                Pfleet.Selected= false;
                Pfleet._SpRenderer.material.color = Color.blue;
            }
        }
        this._SpRenderer.material.color = Color.cyan;
    }

    public override void DestroySelf()
    {
        CampaignManager.Instance._PlayerFleets.Remove(this);
        Destroy(this.gameObject);
    }
}
