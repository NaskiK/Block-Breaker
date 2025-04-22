using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float paddleY = -4f; // Fixed vertical position

    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mouseWorldPos.x, paddleY, 0f);
    }
}
