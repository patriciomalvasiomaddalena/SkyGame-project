using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class AsyncLoadManager : MonoBehaviour
{
    public static AsyncLoadManager _Instance;

    [Header("References")]
    [SerializeField] private GameObject _LoadingScreen;
    [SerializeField] private UnityEngine.UI.Slider _LoadingScreenSlider;


    private void Awake()
    {
        if(_Instance == null && _Instance != this)
        {
            _Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

    }

    public void LoadAsyncLevel(string LevelToLoad)
    {
        _LoadingScreen.SetActive(true);
        _LoadingScreenSlider.value = 0;
        StartCoroutine(LoadLevelAsync(LevelToLoad));
    }

    IEnumerator LoadLevelAsync(string LevelToLoad)
    {
        AsyncOperation LoadOperation = SceneManager.LoadSceneAsync(LevelToLoad);
        while(!LoadOperation.isDone)
        {
            float ProgressBar = Mathf.Clamp01(LoadOperation.progress / 0.9f);
            _LoadingScreenSlider.value = ProgressBar;
            yield return null;
        }
        _LoadingScreen.SetActive(false);
    }

}
