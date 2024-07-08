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
    [SerializeField]TestBulletFactory TestBulletFactory;
    [SerializeField] bool DeleteAllCache;

    [SerializeField] GameObject PauseObject;

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

    private void OnLevelWasLoaded(int level)
    {
        if(PauseObject == null)
        {
            PauseObject = FindObjectOfType<Pause_Menu>().GetComponent<GameObject>();
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
        SceneManager.LoadScene(0);
    }

    public void ReturnToMenu()
    {
        Energy--;
        PlayerPrefs.SetInt("Energy", Energy);
        SceneManager.LoadScene(1);
    }

    public void TogglePauseButton(bool toggle)
    {
        if(PauseObject!= null)
        {
            PauseObject.SetActive(toggle);
        }
    }
}
