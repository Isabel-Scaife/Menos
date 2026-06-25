using UnityEngine;

public class CollectionBox : MonoBehaviour
{
    [SerializeField] private int oliveMax;
    [SerializeField] private OliveMinigame minigame;

    private int collected = 0;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Olive")
        {
            //adds to olive count
            collected++;

            // destory olive
            Destroy(collision.gameObject);

            if (oliveMax == collected)
            {
                minigame.Complete();
            }
        }
        print("Current Olives: " + collected);
    }
}
