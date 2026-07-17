using UnityEngine;

public class ShadowShift : MonoBehaviour
{
    [SerializeField] private Vector2 shiftAmt;
    private SpriteRenderer[] shadows;

    private void Shift(Transform objectToShift, Vector2 shiftBy)
    {
        objectToShift.localPosition = shiftBy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // get shadows, turn on top, turn off bottom  
        shadows = collision.GetComponentsInChildren<SpriteRenderer>();
        Debug.Log("Shadow hit");

        shadows[0].enabled = false;

        Shift(shadows[1].transform, shiftAmt);
        shadows[1].enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        shadows[0].enabled = true;

        Shift(shadows[1].transform, shiftAmt * -1);
        shadows[1].enabled = false;
    }
}
