using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
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
