using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Fleet_Player : Fleet_Base
{
    public static List<Fleet_Player> MovablePlayerFleet= new List<Fleet_Player>();

    [SerializeField] float MoveSpeed;
    [SerializeField] Campaign_Input_Base MoveInput;
    [SerializeField] LineRendererController _LRC;
    [SerializeField] CircleLineDrawer _FuelRend;
    Vector3 Dire;
    private bool Selected,_HasFuel;


    public float FuelAmount;
    public float FuelEff;

    SpriteRenderer _SpRenderer;
    private void Start()
    {
        _SpRenderer = GetComponent<SpriteRenderer>();

        MovablePlayerFleet.Add(this);
        _LRC = GetComponent<LineRendererController>();
        _LRC.Points[0] = this.transform.position;
        CampaignManager.Instance._PlayerFleets.Add(this);
    }

    private void Update()
    {
        RunLogic();
    }

    protected override void RunLogic()
    {
        if (Selected || Dire != Vector3.zero)
        {
            if(FuelAmount > 0)
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
        }
        else
        {
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
            TotalDist = Dist;
        }

        float TotalFuelCons = (TotalDist * (10 * FuelEff)) / 10; // primer 10 = consumo base de combustible, hacer variable, segundo es constante
        if(TotalFuelCons > _InitialFuel)
        {
            FinalFuel = 0;
        }
        else
        {
            FinalFuel = TotalFuelCons;
        }

        float _NewFuel = Mathf.Lerp(FuelAmount, FinalFuel,(Dist/TotalDist)*Time.deltaTime);
        FuelAmount -= FuelAmount - _NewFuel;
        /*while(FuelAmount > FinalFuel)
        {
            FuelAmount -= ((10 * FuelEff) / 10);
        }*/
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
}
