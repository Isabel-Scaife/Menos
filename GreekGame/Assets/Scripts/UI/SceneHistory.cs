using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

// Used to track previous scenes. Irrelevant to the build setting index. 
public class SceneHistory : MonoBehaviour
{
    public static SceneHistory Instance;

    private Stack<string> sceneStack = new Stack<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (sceneStack.Count == 0 || sceneStack.Peek() != scene.name)
            sceneStack.Push(scene.name);
    }

    public void GoBack()
    {
        if (sceneStack.Count > 1)
        {
            // Remove current scene
            sceneStack.Pop();
            // Load previous scene
            string previousScene = sceneStack.Peek();
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            Debug.Log("No previous scene to go back to.");
        }
    }

    // Open journal could be move somewhere else, but
    // for the player to be able to move to the journal scene back and forth, this should be needed (I think.)
    public void OpenJournalUI()
    {
        SceneManager.LoadScene("Journal");
    }
}