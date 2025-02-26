using System.Collections.Generic;
using UnityEngine;

public class Fleet_Player : Fleet_Base
{
    public static List<Fleet_Player> MovablePlayerFleet = new List<Fleet_Player>();

    [SerializeField] float MoveSpeed;
    [SerializeField] Campaign_Input_Base MoveInput;
    [SerializeField] LineRendererController _LRC;
    [SerializeField] CircleLineDrawer _FuelRend;
    [SerializeField] StaminaRegenComp _StaminaComp;
    [SerializeField]Vector3 Dire;
    [SerializeField] private bool Selected;
    bool _HasFuel;


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

        SwitchControllerType();

        //RegenerateStamina();
    }

    private void SwitchControllerType()
    {
        switch (config_manager._Instance.CurrentController)
        {
            case ControllerType.KyM:
                MoveInput = GetComponentInChildren<Campaign_Input_PC>(true);
                break;

            case ControllerType.Gyro_Touch:
            case ControllerType.Joystick:
                MoveInput = GetComponentInChildren<Campaign_Input_Mobile>(true);
                break;
        }
        if (!MoveInput.gameObject.activeSelf)
        {
            MoveInput.gameObject.SetActive(true);
        }
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
            CampaignManager.Instance._PlayerFleets.Remove(this);
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
            if (FuelAmount > 0.1f)
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
            UIManager.Instance.SetTMP("FuelTMP", "Fuel: " + fcount.ToString());
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
        if (new Vector2(Dire.x,Dire.y) != Vector2.zero && _HasFuel == true)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, Dire, MoveSpeed * Time.deltaTime);
            float dist = Vector2.Distance(Dire, this.transform.position);
            if (dist > 0.1f)
            {
                ConsumeFuel(dist, Dire);
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, 1);
        }
    }

    [SerializeField] float _InitialFuel, FinalFuel, TotalDist;
    Vector3 NewDirection = Vector3.zero;
    private void ConsumeFuel(float Dist, Vector3 Director)
    {
        if (Director != NewDirection)
        {
            _InitialFuel = FuelAmount;
            NewDirection = Director;
            TotalDist = Dist; // 5 *( 10 * 1)
        }

        float TotalFuelCons = (TotalDist * (10f * FuelEff));
        FinalFuel = TotalFuelCons;
        if (FinalFuel >= FuelAmount)
        {
            FinalFuel = 0;
        }
        else
        {
            FinalFuel = FuelAmount - FinalFuel;
        }
        //FuelAmount = Mathf.Lerp(FuelAmount, FinalFuel,Time.deltaTime);
        FuelAmount = Mathf.MoveTowards(FuelAmount, FinalFuel, TotalFuelCons * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        Selected = true;
        foreach (var Pfleet in MovablePlayerFleet)
        {
            if (Pfleet != this && Pfleet._SpRenderer != null)
            {
                Pfleet.Selected = false;
                Pfleet._SpRenderer.material.color = Color.blue;
            }
        }
        if(_SpRenderer== null)
        {
            _SpRenderer = GetComponent<SpriteRenderer>();
        }
        this._SpRenderer.material.color = Color.cyan;
        CampaignManager.Instance.SelectFleet();
    }

    public override void DestroySelf()
    {
        CampaignManager.Instance._PlayerFleets.Remove(this);
        Destroy(this.gameObject);
    }

    public void LoseSelection()
    {
        Selected = false;
        this._SpRenderer.material.color = Color.blue;
    }
}
