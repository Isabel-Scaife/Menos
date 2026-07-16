using UnityEngine;

public class ShadowShift : MonoBehaviour
{
    [SerializeField] private Vector2 shiftAmt = new Vector2(0, 1);
    private void FixedUpdate()
    {
        Shift();
    }

    private void Shift()
    {
        Vector2 newPos = transform.position;
        newPos += shiftAmt;
        transform.position = newPos;
    }

    
}
