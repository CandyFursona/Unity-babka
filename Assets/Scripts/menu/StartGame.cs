using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using JetBrains.Annotations;
public class StartGame : MonoBehaviour
{
    public Game Game;
    public Button btn;
    public GameObject camera1;
    public GameObject camera2;
    public GameObject canvas;
    public GameObject canvas2;
    public GameObject player;
    public GameObject granny;
    public GameObject grannyreal;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        btn.onClick.AddListener(StartingGame);
    }
    public void StartingGame()
    {
        Game.startgame();
    }

    void Update()
    {
        
    }
}
