using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Vfx_Exp_Script : MonoBehaviour
{
    [SerializeField] ParticleSystem[] _ExplosionVFX = new ParticleSystem[5];
    [SerializeField] float _pulse, _timer;
    private void Awake()
    {
        _ExplosionVFX = GetComponentsInChildren<ParticleSystem>();
        this.gameObject.SetActive(false);
    }
    public void PlayVFX()
    {
        this.transform.parent = null;
        foreach(ParticleSystem p in _ExplosionVFX)
        {
            p.Play();
        }
    }

    public void ActivateGMOB()
    {
        this.gameObject.SetActive(true);
    }

    private void Update()
    {
        if(_timer > _pulse)
        {
            _pulse = _pulse + 1 * Time.deltaTime;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
