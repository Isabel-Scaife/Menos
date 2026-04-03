using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotePopup : MonoBehaviour
{
    public static NotePopup Instance;

    public TMP_InputField input;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnConfirm()
    {
        string text = input.text;

        if(!string.IsNullOrEmpty(text))
        {
            NotesTabController.Instance.runTimeTexts.Add(text);
            NotesTabController.Instance.RefreshPage();
        }
        input.text = "";
        NotePopup.Instance.gameObject.SetActive(false);
        JournalManager.Instance.EnableJournalUIInput();
    }

    public void OnExit()
    {
        input.text = "";
        NotePopup.Instance.gameObject.SetActive(false);
        JournalManager.Instance.EnableJournalUIInput();
    }
}
