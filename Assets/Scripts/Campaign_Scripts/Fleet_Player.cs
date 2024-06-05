using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Fleet_Player : Fleet_Base
{
    public static List<Fleet_Player> MovablePlayerFleet= new List<Fleet_Player>();

    [SerializeField] float MoveSpeed;
    [SerializeField] Campaign_Input_Base MoveInput;
    [SerializeField] LineRendererController _LRC;
    [SerializeField] CircleLineDrawer _FuelRend;
    [SerializeField] Vector3 Dire;
    private bool Selected;


    public float FuelAmount;

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
             MovementLogic();
            _FuelRend.DrawCircle();
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
        if(Dire != Vector3.zero)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, Dire, MoveSpeed * Time.deltaTime); 
        }
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
