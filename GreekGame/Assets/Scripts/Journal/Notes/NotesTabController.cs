using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class NotesTabController : MonoBehaviour
{
    public static NotesTabController Instance;

    public NoteEntryDatabase database;

    [Header("Placeholder for Notes Display")]
    public TMP_Text[] slots;

    // Holder for where the temporary stuff will be saved
    [SerializeField]public List<string> runTimeTexts;

    public Image inputPopup;

    public int notesPerPage = 18;
    public int currentPage = 0;

    public delegate void SaveNote();
    public static event SaveNote Save;

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
        runTimeTexts = new List<string>();
        RefreshPage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickOpenArea()
    {
        Debug.Log("Write your stuff here!");

        inputPopup.gameObject.SetActive(true);

        JournalManager.Instance.DisableJournalUIInput();
    }

    // Refresh Page (Shows 18 content from the note entry section that was saved depending on the current page)
    public void RefreshPage()
    {
        // Starting index needs to be changed later when we progress through page
        // It will be used to retrieve data from 'runTimeTexts'
        int spacing = currentPage * notesPerPage;

        for (int i = 0; i < notesPerPage; i++)
        {
            int index = i + spacing;

            if(index < runTimeTexts.Count)
            {
                slots[i].text = runTimeTexts[i + spacing];
            }
        }
    }


    // Progress page left (Updates the note page with 18 entries on left)

    // Progress page right (Updates the note page with 18 entries on right)
}
