using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class OliveMinigame : MonoBehaviour
{
    public event Action OnComplete;

    [SerializeField] private FollowMouse birdFollow;
    private Vector2 mousePosition;

    private void Update()
    {
        mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        // mouse first enters top half, turn on follow
        if (mousePosition.y - transform.position.y >= 1 && !birdFollow.enabled)
        {
            birdFollow.enabled = true;
        }
        // mouse first enters bottom hallf, turn off follow
        else if (mousePosition.y - transform.position.y < 1 && birdFollow.enabled)
        {
            birdFollow.enabled = false;
        }
    }

    public void Complete()
    {
        // play animation or sound effect 

        // run listening methods 
        if (OnComplete != null) OnComplete();
    }
}
