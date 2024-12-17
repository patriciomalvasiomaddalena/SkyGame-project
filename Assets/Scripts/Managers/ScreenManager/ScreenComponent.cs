using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScreenComponent : MonoBehaviour, Iscreen
{

    Transform _Root;

    [SerializedDictionary("component","Status")]
    [SerializeField] SerializedDictionary<Behaviour, bool> _BeforeDeactivation = new SerializedDictionary<Behaviour, bool>();
    Behaviour[] ChildrenBehaviours;
    [SerializeField] bool _DestroyableRoot;

    public ScreenComponent _ScreenComponent;

    public ScreenComponent ScreenComp 
    {
        get { return _ScreenComponent; }
    }

    public ScreenComponent(Transform _newRoot)
    {
        _Root = _newRoot;
        _BeforeDeactivation= new SerializedDictionary<Behaviour, bool>();
    }

    private void Start()
    {
        _Root = this.gameObject.transform;
        ChildrenBehaviours = _Root.GetComponentsInChildren<Behaviour>();
        foreach(Behaviour child in ChildrenBehaviours)
        {
            if (!_BeforeDeactivation.ContainsKey(child))
            {
                _BeforeDeactivation.Add(child, child.enabled);
            }
        }
    }

    private void OnEnable()
    {
        if (_Root == null)
        {
            _Root = this.gameObject.transform;
        }

        if(_BeforeDeactivation.Count == 0 && _Root != null)
        {
            ChildrenBehaviours = _Root.GetComponentsInChildren<Behaviour>();
            foreach (Behaviour child in ChildrenBehaviours)
            {
                if(!_BeforeDeactivation.ContainsKey(child)) 
                {
                    _BeforeDeactivation.Add(child, child.enabled);
                }
            }
        }
    }

    public void Activate()
    {
        foreach(var pair in _BeforeDeactivation)
        {
            if(pair.Key != null)
            {
                pair.Key.enabled = pair.Value;
            }
        }
    }

    public void Deactivate(bool hideScreen)
    {
        if(_BeforeDeactivation.Count > 0)
        {
            foreach (Behaviour behaviour in ChildrenBehaviours)
            {
                //_BeforeDeactivation[behaviour] = behaviour.enabled;
                if(behaviour != null)
                {
                    behaviour.enabled = false;
                }
            }
        }

        this.gameObject.SetActive(hideScreen);
    }

    public void Free()
    {
        if(_DestroyableRoot == false)
        {
            _Root.gameObject.SetActive(false);
        }
        else
        {
            Destroy(_Root.gameObject);
        }

    }
}
