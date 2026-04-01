using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class JournalIconUI : MonoBehaviour  // Need to be updated
{
    public Image icon;
    private JournalEntries entry;
    public JournalEntries Entry { get { return entry; } }

    public Sprite unknownSprite;    // Undiscovered evidence or entries

    private void Start()
    {
        
    }

    public void Setup(JournalEntries data, bool discovered)
    {
        if(discovered)
        {
            entry = data;
            icon.sprite = data.icon;
            GetComponent<Button>().interactable = discovered;
        }
        else
        {
            entry = data;
            icon.sprite = unknownSprite;
            GetComponent<Button>().interactable = discovered;
        }
    }

    public void Clear()
    {
        entry = null;
        icon.sprite = null;
        GetComponent<Button>().interactable = false;
    }

    public void OnClick()
    {
        if (entry != null)
        entry.OpenPopup();
    }

    public void Display()
    {
        icon.sprite = entry.icon;
        GetComponent<Button>().interactable = true;
    }

    // PLZ IGNORE THIS - MIGHT BE USED LATER
    // Check the whether the button should be shown on the screen
    // When discovered 
    private void ShowButton(JournalIconUI entry, bool discoveredRelation)
    {
        if (discoveredRelation)
        {
            entry.Display();
        }
    }

    // Need a function - if entry type relationshipdata then don't open the popup, add image of the sprite on related suspect holder 
    public void LinkSuspect()
    {
        if(typeof(RelationshipsData) == entry.GetType())
        {
            StartCoroutine(RunTagEvents());
        }
    }

    private IEnumerator RunTagEvents()
    {
        var task = EvidencePopup.Instance.InvokeNotify((RelationshipsData)entry);
        while (!task.IsCompleted)
            yield return null;

        EvidenceTabController.Instance.InvokeReloadTag();
    }
}
