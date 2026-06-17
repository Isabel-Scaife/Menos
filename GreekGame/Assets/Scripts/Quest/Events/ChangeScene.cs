using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour, IEvent
{
    [SerializeField] private string sceneName;

    public void OnQuestComplete()
    {
        if (sceneName != null)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

}
