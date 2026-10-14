using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Ui : MonoBehaviour
{
    public Slider Slider1; // volume
    public Slider Slider2; // sensitivity
    public Slider Slider3; // fov

    public TextMeshProUGUI Volume;
    public TextMeshProUGUI Sensitivity;
    public TextMeshProUGUI fov;

    void Update()
    {
        Volume.text = $"Volume: {(int)(Slider1.value * 100)}%";
        Sensitivity.text = $"Sensitivity: {Slider2.value * 10}";
        fov.text = $"FoV: {Slider3.value}";
    }
}
