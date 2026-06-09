using UnityEngine;
using UnityEngine.InputSystem;

public class FollowMouse : MonoBehaviour
{
    public Vector2 MousePostion { get; private set; }
    void Update()
    {
        MousePostion = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.position = MousePostion;
    }

}
