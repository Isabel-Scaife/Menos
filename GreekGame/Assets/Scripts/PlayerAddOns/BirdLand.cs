using UnityEngine;

public class BirdLand : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bird bird = collision.GetComponentInParent<Bird>();

        if(bird != null) bird.Land();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Bird bird = collision.GetComponentInParent<Bird>();

        if (bird != null) bird.TakeOff();
    }
}
