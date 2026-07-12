using UnityEngine;
using UnityEngine.InputSystem;

public class FishToSort : MonoBehaviour
{
    private bool grabbed;
    private Vector2 mousePosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        //Pickks up fih
        if (grabbed)
        {
            transform.position = new Vector3(mousePosition.x, mousePosition.y, 1);
        }
    }

    //detetcs when moused over and clicked on
    //for fih pickup
    private void OnMouseDown()
    {
        grabbed = !grabbed;

    }

}
