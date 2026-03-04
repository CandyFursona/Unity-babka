using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class fov : MonoBehaviour
{
    public Slider Slider;
    public Camera Camera2;

    private void Update()
    {
        Camera2.fieldOfView = Slider.value;
    }
}
