using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] GameObject _CanvasVictory;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var Fplayer = collision.GetComponent<Fleet_Player>();
        if(Fplayer != null)
        {
            _CanvasVictory.SetActive(true);
            StartCoroutine(VictoryScreenLogic());
        }
    }

    IEnumerator VictoryScreenLogic()
    {
        yield return new WaitForSeconds(1f);
        AsyncLoadManager._Instance.FadeOutManager.StartFadeOut();
        yield return new WaitForSeconds(1f);
        AsyncLoadManager._Instance.LoadAsyncLevel("Menu");
        
    }

}
