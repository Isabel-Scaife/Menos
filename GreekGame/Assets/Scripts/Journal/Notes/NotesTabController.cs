using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NotesTabController : MonoBehaviour
{
    public static NotesTabController Instance;

    public NoteEntryDatabase database;

    [Header("Placeholder for Notes Display")]
    public TMP_Text[] slots;

    public Image inputPopup;

    public TMP_Text currentInput;

    public int notesPerPage = 18;
    public int currentPage = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void OnEnable()
    {
        inputPopup.gameObject.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickOpenArea()
    {
        Debug.Log("Write your stuff here!");

        inputPopup.gameObject.SetActive(true);
    }

    // Progres page left (Updates the note page with 18 entries on left)

    // Progress page right (Updates the note page with 18 entries on right)
}
