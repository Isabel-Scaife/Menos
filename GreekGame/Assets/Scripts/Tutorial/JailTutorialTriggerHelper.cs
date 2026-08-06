using UnityEngine;

public class JailTutorialTriggerHelper : MonoBehaviour
{
    [SerializeField] private EscapeJailTutorial task;

    // shows task when entered by player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            task.ShowTask();
            gameObject.SetActive(false);
        }
    }
}