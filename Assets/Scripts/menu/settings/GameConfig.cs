using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameConfig : MonoBehaviour
{
    [Header("others scripts")]
    public NavAgent granny;
    public PlayerController plrcontroller;

    [Header("Config")]
    public int Objpreset = 1;
    public int ObjSeed;

    public int Sppd;
    public int Scpd;
    public int PlrSpd = 6;
    public bool UseGlitch = false;
    public bool UseDrift = false;

    [Header("Difficulty")]
    public Slider DifficultSlider;
    public TMP_Text DifficultyText;
    public GameObject WarnText;

    [Header("Preset")]
    public Slider PresetSlider;
    public TMP_Text PresetText;

    [Header("Preset Random")]
    public GameObject SeedSettings;
    public Toggle UseARandom;
    public TMP_InputField enteraseed;

    [Header("Buttons")]
    public Button back;
    public Button Startgame;

    [Header("Menus")]
    public GameObject MainMenu;

    [Header("CustomSettings")]
    public GameObject CustomDiff;
    private int ChaseSpeed;
    private int PatrolSpeed;
    public TMP_InputField PatrolSpd;
    public TMP_InputField ChaseSpd;

    public TMP_InputField PlrSpeed;
    public TMP_InputField PlrSpeed2;
    public GameObject PracticeSetSpeed;

    [Header("BoolSettings")]
    public Toggle UseGlitches;
    public Toggle babkadrift;

    void Start()
    {
        UseARandom.onValueChanged.AddListener(OnToggleChanged);
        back.onClick.AddListener(backmenu);

        PatrolSpd.onEndEdit.AddListener(PatrolSpdChanged);
        ChaseSpd.onEndEdit.AddListener(ChasespdChanged);
        PlrSpeed.onEndEdit.AddListener(PlayerSpeedChanged);
        enteraseed.onEndEdit.AddListener(SeedChanged);
        PlrSpeed2.onEndEdit.AddListener(PlayerSpeedChangedV2);
        UseGlitches.onValueChanged.AddListener(ChangeGlitch);
        babkadrift.onValueChanged.AddListener(ChangeDrift);
    }

    void ChangeGlitch(bool Us)
    {
        if (Us)
        {
            UseGlitch = true;
        } else
        {
            UseGlitch = false;
        }

    }
    
    void ChangeDrift(bool Us)
    {
        if (Us)
        {
            UseDrift = true;
        } else
        {
            UseDrift = false;
        }
    }
    void PlayerSpeedChangedV2(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            PlrSpeed2.text = "6";
            PlrSpd = 6;
            return;
        }

        if (int.TryParse(input, out int value))
        {
            PlrSpd = value;
        }
        else
        {
            PlrSpeed2.text = "6";
            PlrSpd = 6;
        }
    }
    void backmenu()
    {
        MainMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void SeedChanged(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            enteraseed.text = "0";
            ObjSeed = 0;
            return;
        }

        if (int.TryParse(input, out int value))
        {
            ObjSeed = value;
        }
        else
        {
            enteraseed.text = "0";
            ObjSeed = 0;
        }
    }

    public void PlayerSpeedChanged(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            PlrSpeed.text = "0";
            PlrSpd = 0;
            return;
        }

        if (int.TryParse(input, out int value))
        {
            PlrSpd = value;
        }
        else
        {
            PlrSpeed.text = "0";
            PlrSpd = 0;
        }
    }

    public void PatrolSpdChanged(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            PatrolSpd.text = "0";
            Sppd = 0;
            return;
        }

        if (int.TryParse(input, out int value))
        {
            Sppd = value;
        }
        else
        {
            PatrolSpd.text = "0";
            Sppd = 0;
        }
    }

    public void ChasespdChanged(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            ChaseSpd.text = "0";
            Scpd = 0;
            return;
        }

        if (int.TryParse(input, out int value))
        {
            Scpd = value;
        }
        else
        {
            ChaseSpd.text = "0";
            Scpd = 0;
        }
    }


    void OnToggleChanged(bool Us)
    {
        if (Us)
        {
            enteraseed.interactable = false;
            ObjSeed = 0;
        }
        else
        {
            enteraseed.interactable = true;
        }
    }

    // Update is called once per frame
    //enteraseed.interactable = true;
    void Update()
    {
        WarnText.SetActive(false);
        if (DifficultSlider.value == 0)
        {
            DifficultyText.text = "Difficulty: Practice";
            CustomDiff.SetActive(false);
            PracticeSetSpeed.SetActive(true);
            granny.ChangeVars(0, 0, 0);
            plrcontroller.ChangeSpeed(PlrSpd);
            
        } 
        else if (DifficultSlider.value == 1)
        {
            DifficultyText.text = "Difficulty: Easy";
            CustomDiff.SetActive(false);
            PracticeSetSpeed.SetActive(false);
            granny.ChangeVars(1, 0, 0);
            plrcontroller.ChangeSpeed(6);

        }
        else if (DifficultSlider.value == 2)
        {
            DifficultyText.text = "Difficulty: Normal";
            CustomDiff.SetActive(false);
            PracticeSetSpeed.SetActive(false);
            granny.ChangeVars(2, 0, 0);
            plrcontroller.ChangeSpeed(6);
        }
        else if (DifficultSlider.value == 3)
        {
            DifficultyText.text = "Difficulty: Hard";
            CustomDiff.SetActive(false);
            PracticeSetSpeed.SetActive(false);
            granny.ChangeVars(3, 0, 0);
            plrcontroller.ChangeSpeed(6);
        }
        else if (DifficultSlider.value == 4)
        {
            DifficultyText.text = "Difficulty: <color=#FF2B00>EXTREME</color>";
            CustomDiff.SetActive(false);
            PracticeSetSpeed.SetActive(false);
            granny.ChangeVars(4, 0, 0);
            WarnText.SetActive(true);
            plrcontroller.ChangeSpeed(6);
        }
        /*
        else if (DifficultSlider.value == 5)
        {
            DifficultyText.text = "Difficulty: <color=#D12300>EXTREME+</color>";
            CustomDiff.SetActive(false);
            PracticeSetSpeed.SetActive(false);
            granny.ChangeVars(5, 0, 0);
            WarnText.SetActive(true);
            plrcontroller.ChangeSpeed(6);
        }
        else if (DifficultSlider.value == 6)
        {
            DifficultyText.text = "Difficulty: <color=#751400>EXTREME++</color>";
            CustomDiff.SetActive(false);
            PracticeSetSpeed.SetActive(false);
            granny.ChangeVars(6, 0, 0);
            WarnText.SetActive(true);
            plrcontroller.ChangeSpeed(6);
        }
        else if (DifficultSlider.value == 7)
        {
            DifficultyText.text = "Difficulty: <color=#470C00>IMPOSSIBLE</color>";
            CustomDiff.SetActive(false);
            PracticeSetSpeed.SetActive(false);
            granny.ChangeVars(7, 0, 0);
            WarnText.SetActive(true);
            plrcontroller.ChangeSpeed(6);
        }
        */
        else if (DifficultSlider.value == 5)
        {
            DifficultyText.text = "Difficulty: <color=yellow>Custom</color>";
            CustomDiff.SetActive(true);
            SeedSettings.SetActive(true);
            PracticeSetSpeed.SetActive(false);
            granny.ChangeVars(8, Sppd, Scpd);
            plrcontroller.ChangeSpeed(PlrSpd);
        }

        if (PresetSlider.value == 1)
        {
            PresetText.text = "Preset: 1";
            Objpreset = 1;
            SeedSettings.SetActive(false);
        }
        else if (PresetSlider.value == 2)
        {
            PresetText.text = "Preset: 2";
            Objpreset = 2;
            SeedSettings.SetActive(false);
        }
        else if (PresetSlider.value == 3)
        {
            PresetText.text = "Preset: 3";
            Objpreset = 3;
            SeedSettings.SetActive(false);
        } else if (PresetSlider.value == 4)
        {
            PresetText.text = "Preset: 4";
            Objpreset = 4;
            SeedSettings.SetActive(false);
        }
        else if (PresetSlider.value == 5)
        {
            PresetText.text = "Preset: 5";
            Objpreset = 5;
            SeedSettings.SetActive(false);
        }
        else if (PresetSlider.value == 6)
        {
            PresetText.text = "Preset: <color=yellow>Custom</color>";
            Objpreset = 6;
            SeedSettings.SetActive(true);
        }
        
    }
}
