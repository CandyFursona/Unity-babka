using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using JetBrains.Annotations;
using Unity.VisualScripting;
public class StartGame : MonoBehaviour
{
    public Game Game;
    public Button btn;
    public Button StartGameRl;
    public GameObject camera1;
    public GameObject camera2;
    public GameObject canvas;
    public GameObject canvas2;
    public GameObject player;
    public GameObject granny;
    public GameObject grannyreal;
    public GameObject MainMenu;
    public GameObject Settmenu;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        btn.onClick.AddListener(Swap);
        StartGameRl.onClick.AddListener(Stgame);

    }
    public void Swap()
    {
        Settmenu.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void Stgame()
    {
        Settmenu.SetActive(false);
        MainMenu.SetActive(true);
        Game.startgame();
    }

    void Update()
    {
        
    }
}
