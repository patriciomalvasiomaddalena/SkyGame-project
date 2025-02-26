using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int Energy;
    public static GameManager Instance { get; private set; }
    public GameObject PlayerShip;
    public TestBulletFactory TestBulletFactory;
    [SerializeField] bool DeleteAllCache;

    public GameObject PauseObject;

    public Dictionary<string, GameObject> FactoryDictionary = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            GameManager.DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        if (DeleteAllCache)
        {
            PlayerPrefs.DeleteAll();
        }
        LoadData();
    }

    private void Update()
    {
        if(TestBulletFactory == null)
        {
            TestBulletFactory = GetComponentInChildren<TestBulletFactory>();
        }
 
    }

    private void OnLevelWasLoaded(int level)
    {
        if(PauseObject == null)
        {
            var pause_Menu = FindObjectOfType<Pause_Menu>();
            if(pause_Menu != null)
            {
                PauseObject = pause_Menu.gameObject;
            }
            else
            {
                pause_Menu = null;
            }
        }
    }

    public void CreateFactory(string FactoryId, int bulletcount)
    {
        if (FactoryId == "TestPool")
        {
            TestBulletFactory.CreatePool(bulletcount);
        }
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey("Energy"))
        {
            Energy = PlayerPrefs.GetInt("Energy");
        }
        else
        {
            Energy = 10;
            PlayerPrefs.SetInt("Energy",Energy);
        }
    }
    public void MainMenu()
    {
        Energy--;
        PlayerPrefs.SetInt("Energy", Energy);
        AsyncLoadManager._Instance.LoadAsyncLevel("Menu");
    }

    public void ReturnToMenu()
    {
        Energy--;
        PlayerPrefs.SetInt("Energy", Energy);
        AsyncLoadManager._Instance.LoadAsyncLevel("Campaign Layer");
    }

    public void TogglePauseButton(bool toggle)
    {
        if(PauseObject!= null)
        {
            PauseObject.SetActive(toggle);
        }
    }
}
