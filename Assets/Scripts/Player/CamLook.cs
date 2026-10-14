using UnityEngine;

public class CamLook : MonoBehaviour
{
    public float sensitivity = 2f;
    public float minPitch = -80f;
    public float maxPitch = 80f;

    float yaw;
    float pitch;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        yaw = transform.eulerAngles.y;
        if (pitch > 180f) pitch -= 360f; 
    }

    void Update()
    {
        float mx = Input.GetAxis("Mouse X") * sensitivity;
        float my = Input.GetAxis("Mouse Y") * sensitivity;

        yaw += mx;
        pitch -= my; // минус = стандартно “мышь вверх = камера вверх”
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
    }
}