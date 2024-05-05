using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GenericTest : MonoBehaviour, Ipoolable<GenericTest>
{
    [SerializeField] GameObject _thisPrefab;
    [SerializeField] GenericTest _thisScript;
    [SerializeField] int _SizeOfPool;
    [SerializeField] GameObject _FactoryBlueprint;
    GenericFactory<GenericTest> _thisFactory;
    float Health = 10;

    void Awake()
    {
        if(_thisFactory == null)
        {
            if(GenericFactory<GenericTest>.Instance == null)
            {
                GameObject obj = Instantiate(_FactoryBlueprint);
                obj.AddComponent<GenericFactory<GenericTest>>();

                GenericFactory<GenericTest> GenericPool = new GenericFactory<GenericTest>();
                GenericPool.genericScript = _thisScript;
                GenericPool._PoolPrefab = _thisPrefab;
                GenericPool._PoolCount = _SizeOfPool;
                Instantiate(_FactoryBlueprint);
                _thisFactory.Awaken();
            }
            else
            {
                _thisFactory = GenericFactory<GenericTest>.Instance;
            }
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        Health = 10;
    }

    void Update()
    {

        if(Health > 0) 
        { Health = Health - (1 * Time.deltaTime); }
        else
        {
            _thisFactory.ReturnObjectToPool(_thisScript);
            TurnOff(this);
        }
    }

    private void ResetValues()
    {
        Health = 10f;
    }

    public void TurnOff(GenericTest GenericScript)
    {
        this.gameObject.SetActive(false);
    }

    public void TurnOn(GenericTest GenericScript)
    {
        ResetValues();
        this.gameObject.SetActive(true);
    }


}
