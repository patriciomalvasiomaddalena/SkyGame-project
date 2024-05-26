using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet_Player : Fleet_Base
{
    public static List<Fleet_Player> MovablePlayerFleet= new List<Fleet_Player>();

    [SerializeField] float MoveSpeed;
    [SerializeField] Campaign_Input_Base MoveInput;
    [SerializeField] Vector3 Dire;
    private bool Selected;

    SpriteRenderer _SpRenderer;
    private void Start()
    {
        _SpRenderer = GetComponent<SpriteRenderer>();
        MovablePlayerFleet.Add(this);
    }

    private void Update()
    {
        RunLogic();
    }

    protected override void RunLogic()
    {
        if (Selected)
        {
            MovementLogic();
        }
    }

    private void MovementLogic()
    {
        Dire = MoveInput.InputMachine(this.transform);
        transform.position = Vector3.MoveTowards(this.transform.position, Dire, MoveSpeed * Time.deltaTime);
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
        Dire = Vector3.zero;
    }
}
