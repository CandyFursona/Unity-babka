using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject Player;
    public killing killingscript;
    public Game game; 
    public GameObject pausecanvas;
    public Button resumebtn;
    public Button exitbtn;
    public bool pause = false;

    void Start()
    {
        resumebtn.onClick.AddListener(resumegame);
        exitbtn.onClick.AddListener(exitgame);
    }


    void exitgame()
    {
        game.exit();
    }
    void resumegame()
    {
        pause = false;
    }

    void Update()
    {
        if (game.gamete)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pause = !pause;
            }
            if (pause && game.gamete)
            {
                if (!killingscript.killingnow)
                {
                    Player.GetComponent<PlayerController>().enabled = false;
                    Cursor.lockState = CursorLockMode.None;
                }
                Cursor.visible = true;
                pausecanvas.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                if (!killingscript.killingnow)
                {
                    Player.GetComponent<PlayerController>().enabled = true;
                    Cursor.lockState = CursorLockMode.Locked;
                }

                Cursor.visible = false;
                pausecanvas.SetActive(false);
                Time.timeScale = 1;
            }
        }
        else
        {
            Cursor.visible = true;
            pausecanvas.SetActive(false);
            Time.timeScale = 1;
        }
    }

}
