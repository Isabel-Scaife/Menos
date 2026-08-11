using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable, CreateAssetMenu(fileName = "ChangeScene", menuName = "Events/ChangeScene")]
public class ChangeScene : QuestEvent
{
    [SerializeField] private string sceneName;

    public override void PlayEvent()
    {
        if (sceneName != null)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

}
