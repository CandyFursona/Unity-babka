using UnityEngine;
using UnityEngine.UI;

public class music : MonoBehaviour
{
    public Game game;
    public AudioSource audioSource;
    public Button btn;
    public GameObject camera2;
    public GameObject canvas;

    private bool played = false;

    private void Start()
    {
        btn.onClick.AddListener(stopmusic);
    }

    void stopmusic()
    {
        audioSource.Stop();
        played = false; 
    }

    private void Update()
    {
        if (camera2.activeSelf && !played && canvas.activeSelf)
        {
            audioSource.Play();
            played = true;
        }
    }
}