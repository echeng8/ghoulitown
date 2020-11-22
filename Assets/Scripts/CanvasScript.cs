using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    public static CanvasScript instance;
    
    [SerializeField]
    private Text statusText;

    [SerializeField]
    private GameObject[] menus;

    public enum MenuStage
    {
        Main,
        Lobby
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetMenuStage(MenuStage.Main);
    }

    public void SetStatus(string text)
    {
        //Debug.Log("Status Update: " + text);
        statusText.text = text;
    }

    public void SetMenuStage(MenuStage stage)
    {
        foreach(GameObject menu in menus)
        {
            menu.SetActive(false);
        }
        menus[(int)stage].SetActive(true);
    }
}
