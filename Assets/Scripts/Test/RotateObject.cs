using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float speed = -50f;

    void Update()
    {
        transform.Rotate(speed * Time.deltaTime, 0f, 0f);
    }
}