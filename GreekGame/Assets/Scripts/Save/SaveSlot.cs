using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSlot : MonoBehaviour
{
    
    [SerializeField] private string _ID = "";
    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasDataContent;

    private string sceneName;

    [SerializeField] private GameObject btn_Save;
    [SerializeField] private GameObject btn_Load;

    // UI Components
    [SerializeField] private TextMeshProUGUI chapterText;
    [SerializeField] private TextMeshProUGUI timePlayedText;
    [SerializeField] private TextMeshProUGUI collectables;

    public string ID { get => _ID;}

    /// <summary>
    /// Apply data to save slot 
    /// </summary>
    /// <param name="data"></param>
    public void SetData(ProgressionManagerData data)
    {
        if(data == null)
        {
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
        }
        else
        {
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);

            chapterText.text = data.chapter;
            timePlayedText.text = data.timePlayed + "time";
            collectables.text = data.collectables + "/ 85";

            sceneName = data.sceneName;

            // on main menu add load button, on others add save 
            if (SceneManager.GetActiveScene().name != "Starting Menu")
            {
                ApplySaveButton();
            }
            else
            {
                ApplyLoadButton();
            }
        }
    }

    /// <summary>
    /// Start a new save file
    /// </summary>
    public void OnEmptyClicked()
    {
        if( SceneManager.GetActiveScene().name == "Starting Menu" )
        {
            // start new game
            SceneManager.LoadScene(1);
        }
        else
        {
            // create new save file 
            Save();

        }
    }

    /// <summary>
    /// Load save file data 
    /// </summary>
    public void OnLoadClicked()
    {
        // load all data
        ProgressionManagerData progressionData = SaveSystem.LoadData<ProgressionManagerData>(ID, "ProgressionManager.json");
        ProgressionManager.Instance.LoadData(progressionData);
 
        SpawnManagerData spawnData = SaveSystem.LoadData<SpawnManagerData>(ID, "SpawnManager.json");
        SpawnManager.Instance.LoadData(spawnData);

        // Only holds player position which also held in spawn manager
        // so currently player data is not needed
        //PlayerData playerDatat = SaveSystem.LoadData<PlayerData>(ID, "Player.json");
        //SpawnManager.Instance.PlayerPosition = playerDatat.position;


        GameStateManagerData stateData = SaveSystem.LoadData<GameStateManagerData>(ID, "GameStateManager.json");
        GameStateManager.Instance.LoadData(stateData);

        // switch to scene
        SceneManager.LoadScene(ProgressionManager.Instance.SceneName);
    }

    /// <summary>
    /// Save game on this save slot 
    /// </summary>
    /// <exception cref="System.Exception"></exception>
    public void OnSaveClicked()
    {
        Save();
    }

    /// <summary>
    /// Save data to corresponding locations
    /// </summary>
    private void Save()
    {
        try
        {

            if (GameStateManager.Instance != null)
            {
                SaveSystem.SaveData<GameStateManager, GameStateManagerData>
                    (GameStateManager.Instance, ID, "GameStateManager.json");
            }

            if (SpawnManager.Instance != null)
            {
                SaveSystem.SaveData<SpawnManager, SpawnManagerData>
                    (SpawnManager.Instance, ID, "SpawnManager.json");
            }

            if (ProgressionManager.Instance != null)
            {
                SaveSystem.SaveData<ProgressionManager, ProgressionManagerData>
                    (ProgressionManager.Instance, ID, "ProgressionManager.json");
            }

            Debug.Log("Save Successful! Files found at: " + Application.persistentDataPath);
        }
        catch (Exception e)
        {
            throw new System.Exception("Error while saving.", e);
        }
    }

    private void ApplySaveButton()
    {
        btn_Save.SetActive(true);
        btn_Load.SetActive(false);
    }
    private void ApplyLoadButton()
    {
        btn_Save.SetActive(false);
        btn_Load.SetActive(true);
    }

}
