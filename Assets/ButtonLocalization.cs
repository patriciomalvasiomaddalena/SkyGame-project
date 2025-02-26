using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLocalization : MonoBehaviour
{
    [SerializeField] private string _TextID;
    [SerializeField] private Localization_Manager localization;

    [SerializeField] private TMPro.TextMeshProUGUI _MyText;


    private void Start()
    {
        localization = Localization_Manager.Instance;
        localization.OnUpdate += ChangeText;
    }

    void ChangeText()
    {
        _MyText.text = Localization_Manager.Instance.GetLocalizationText(_TextID);
    }

    private void OnDestroy()
    {
        Localization_Manager.Instance.OnUpdate -= ChangeText;
    }

    private void OnEnable()
    {
        if(localization != null)
        {
            localization.LaunchUpdateLocalization();
        }
        else
        {
            localization = Localization_Manager.Instance;
            localization.OnUpdate += ChangeText;
            localization.LaunchUpdateLocalization();
        }
    }

}
