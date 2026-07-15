
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingScene : MonoBehaviour
{
    [SerializeField] ScenePart startingScene;

    public static StartingScene Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        // check starting scene
        if (startingScene == null && SpawnManager.Instance == null) return;
        {
            // run starting scene if not run in the past
            if (!SpawnManager.Instance.SceneLoadedInPast(SceneManager.GetActiveScene().name))
            {
                startingScene.gameObject.SetActive(true);
            }
        }
    }

    //public void Pause()
    //{
    //    if (timelineDirector.state == PlayState.Playing)
    //    {
    //        timelineDirector.Pause();
    //    }
    //}

    //public void Resume(string flagName)
    //{
    //    if (GameStateManager.Instance == null) { Debug.Log("Missing GameState manger."); return; }

    //    if (timelineDirector.state == PlayState.Paused)
    //    {
    //        if (GameStateManager.Instance.ClearFlag(flagName)) 
    //            timelineDirector.Play();
    //    }
    //}

    //public void PlayCutscene()
    //{
    //    timelineDirector.time = 0;
    //    timelineDirector.Play();
    //}

}
