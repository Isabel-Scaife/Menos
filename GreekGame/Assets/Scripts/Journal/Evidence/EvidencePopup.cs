using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading.Tasks;

public class EvidencePopup : MonoBehaviour
{
    public static EvidencePopup Instance;
    public static bool          open;

    public EvidenceData currentEvidence;
    public Image itemImg;
    public TMP_Text name;
    public TMP_Text description;

    // Used for tagging relations, you need to know which evidence
    // opened the evidence popup 
    public static event GiveEvidence Notify;
    public delegate void GiveEvidence(RelationshipsData relationship);

    // NOTE: Should save the status of revealed button - ScriptableObjects?
    public JournalIconUI[] suspects;
    public RelationshipsData[] suspectData;

    public Image[] relatedRelations;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        open = false;
        SetupButton();
        HideRelatedRelationSlot();
        RelationshipsTabController.Discovered += UpdateButton;
        Notify += TagRelationship;
    }

    public void Show(EvidenceData evidence)
    {
        JournalManager.Instance.OpenEvidencePopup();

        currentEvidence = evidence;
        name.text = evidence.entryName;
        description.text = evidence.description;
        itemImg.sprite = evidence.icon;

        open = true;
        
        gameObject.SetActive(true); // Ensure visibility
    }

    public void ExitEvidencePopup()
    {
        if (open)
        {
            open = false;
            JournalManager.Instance.escapeHandledThisFrame = true;
            JournalManager.Instance.CloseEvidencePopup();
        }
    }

    // Initialize the suspect Button and give information setup
    private void SetupButton()
    {
        suspects[0].Setup(suspectData[0], false);   // Cranaus
        suspects[1].Setup(suspectData[1], true);    // Alcippe        
        suspects[2].Setup(suspectData[2], false);   // Poseidon
        suspects[3].Setup(suspectData[3], false);   // Ericthonius
    }

    // Call to show the unlocked relations/suspects on evidence popup (Called in discovered event in relationship tab controller)
    public void UpdateButton()
    {
        for(int i = 0; i < 4; i++)
        {
            if (JournalManager.Instance.IsDiscovered(suspectData[i]))
            {
                suspects[i].Display();
            }
        }
    }

    // Used for Setting up tagging relations in evidence popup level
    public void HideRelatedRelationSlot()
    {
        for(int i = 0; i < relatedRelations.Length; i++)
        {
            relatedRelations[i].gameObject.SetActive(false);
        }
    }
    
    // When this is called, tag this relationship and show this
    public void TagRelationship(RelationshipsData relationship)
    {
        // IF THE CURRENT POSSIBLE RELATION LIST HAS THIS CHARACTER TAG
        // break out from the function early
        if(currentEvidence.possibleRelations.Contains(relationship))
        {
            return;
        }

        // Update the evidence data to include this relation
        currentEvidence.possibleRelations.Add(relationship);

        // Show this relationship on the evidence popup
        int index = currentEvidence.possibleRelations.IndexOf(relationship);
        relatedRelations[index].gameObject.SetActive(true);
        relatedRelations[index].sprite = relationship.icon;
    }

    // Invoke the Notify event outside of this class 
    public async Task InvokeNotify(RelationshipsData relationship)
    {
        Notify?.Invoke(relationship);
        await Task.Delay(1000);
    }
}
