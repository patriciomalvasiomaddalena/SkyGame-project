using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Vfx_Exp_Script : MonoBehaviour
{
    [SerializeField] ParticleSystem[] _ExplosionVFX = new ParticleSystem[5];
    [SerializeField] float _pulse, _timer;
    [SerializeField] Transform _Parent;
    private void Awake()
    {
        _ExplosionVFX = GetComponentsInChildren<ParticleSystem>();
        //this.gameObject.SetActive(false);
    }
    public void PlayVFX()
    {
        if(_Parent!= null)
        {
            this.transform.parent = null;
        }
        foreach(ParticleSystem p in _ExplosionVFX)
        {
            p.Play();
        }
    }

    public void ActivateGMOB()
    {
        this.gameObject.SetActive(true);
        _pulse = 0;
    }

    private void Update()
    {
        if(_timer > _pulse)
        {
            _pulse = _pulse + 1 * Time.deltaTime;
        }
        else
        {
            if (this.transform.parent == null)
            {
                this.transform.parent = _Parent;
            }
            this.gameObject.SetActive(false);
        }
    }

    public void SetParent(Transform parent)
    {
        _Parent = parent;
        this.transform.parent = parent;
        this.gameObject.SetActive(_Parent != null);
    }

    private void OnEnable()
    {
        _pulse = 0;
    }
}
