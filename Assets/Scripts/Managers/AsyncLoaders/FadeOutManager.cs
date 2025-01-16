using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutManager : MonoBehaviour
{
    [SerializeField] private Animator _FadeAnimator;
    [SerializeField] private float _FadeDuration;
    [SerializeField] private GameObject _FadeGM;

    public event Action FadeComplete = delegate { };

    public void StartFadeOut()
    {
        _FadeGM.SetActive(true);
        StartCoroutine(FadeOutSFX());
    }

    IEnumerator FadeOutSFX()
    {
        _FadeAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(_FadeDuration);

        Debug.Log("fade out complete");
        FadeComplete();
        _FadeGM.SetActive(false);
        _FadeAnimator.SetTrigger("Start");
    }

}
