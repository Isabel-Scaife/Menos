using UnityEngine;
using UnityEngine.InputSystem;

public class FolllowMouse : MonoBehaviour
{
    private Vector2 mousePos;
    protected Vector2 worldPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Mouse.current.position.ReadValue();
        worldPos = (Vector2)Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = worldPos;
    }
}
