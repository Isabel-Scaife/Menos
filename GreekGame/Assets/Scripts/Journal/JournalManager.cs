using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class JournalManager : MonoBehaviour
{
    // A public static instance of itself to give access to functions of journal manager
    public static JournalManager Instance;

    // State of the Journal. This will be used to change the action map from other things to journal map
    // If journal gameobject is active, this should be tagged as 'InUse'
    public static bool InUse;

    // ** THIS SHOULD BE SAVED...!!!
    // ** DATA CAN HAVE ITS STATUS SAVED AND
    //    EVERYTIME GAME LOADS DISCOVERED THINGS CAN BE POPPED BACK IN
    // Check whether evidence | relationships | map are discovered
    private HashSet<EvidenceData> discoveredEvidence = new HashSet<EvidenceData>();
    private HashSet<RelationshipsData> unlockedRelations = new HashSet<RelationshipsData>();


    // Going to be used for section changes and button position reset
    public delegate void ChangeSection();
    public static event ChangeSection ProgressR;
    public static event ChangeSection ProgressL;

    // For optimization & quick comparing purposes
    private const byte notes           = 0b10000;   // Index 0
    private const byte evidence        = 0b01000;   // Index 1
    private const byte relationships   = 0b00100;   // Index 2
    private const byte maps            = 0b00010;   // Index 3
    private const byte settings        = 0b00001;   // Index 4
    private byte[] sections = { notes, evidence, relationships, maps, settings };
    [SerializeField]
    private int currentIndex;
    public int CurrentSection { get { return currentIndex; } }

    // Store Child Canvases
    [SerializeField]
    private Canvas c_notes;
    [SerializeField]
    private Canvas c_evidence;
    [SerializeField]
    private Canvas c_e_popup;
    [SerializeField]
    private Canvas c_relationships;
    [SerializeField]
    private Canvas c_r_popup;
    [SerializeField]
    private Canvas c_maps;
    [SerializeField]
    private Canvas c_settings;

    // Store Buttons to manipulate location
    [SerializeField]
    private Button b_notes;
    [SerializeField]
    private Button b_evidence;
    [SerializeField]
    private Button b_relationships;
    [SerializeField]
    private Button b_maps;
    [SerializeField]
    private Button b_settings;

   
    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Subscribe to button or controller triggered events
        ProgressL += UpdateIndex_L;
        ProgressL += UpdateSection;

        ProgressR += UpdateIndex_R;
        ProgressR += UpdateSection;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    private void OnEnable()
    {
        ClickNotesButton();
    }

    // This can later be called when it is integrated within the main overlay screen
    // Just simply disable the journal UI
    private void OnDisable()
    {
        
    }

    // Used to change journal section by button press or controller trigger
    public void InvokeProgressL(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;   // ignore started/canceled
        ProgressL?.Invoke();
    }
    
    public void InvokeProgressR(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;   // ignore started/canceled
        ProgressR?.Invoke();
    }

    void UpdateIndex_L()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
        }
        else
        {
            currentIndex = 4;
        }
    }

    void UpdateIndex_R()
    {
        if(currentIndex < 4)
        {
            currentIndex++;
        }
        else
        {
            currentIndex = 0;
        }
    }

    void UpdateSection()
    {
        switch(currentIndex)
        {
            case 0:
                ClickNotesButton();
                break;

            case 1:
                ClickEvidenceButton();
                break;

            case 2:
                ClickRelationshipsButton();
                break;

            case 3:
                ClickMapsButton();
                break;

            case 4:
                ClickSettingsButton();
                break;
        }
    }


    // Manually Clicking button to change sections in journal
    public void ClickNotesButton()
    {
        c_notes.gameObject.SetActive(true);
        c_evidence.gameObject.SetActive(false);
        c_e_popup.gameObject.SetActive(false);
        c_relationships.gameObject.SetActive(false);
        c_r_popup.gameObject.SetActive(false);
        c_maps.gameObject.SetActive(false);
        c_settings.gameObject.SetActive(false);
        
        Vector3 mediumPos = b_notes.transform.position;
        mediumPos.y = 1021;
        b_notes.transform.position = mediumPos;

        mediumPos = b_evidence.transform.position;
        mediumPos.y = 1007;
        b_evidence.transform.position = mediumPos;

        mediumPos = b_relationships.transform.position;
        mediumPos.y = 1007;
        b_relationships.transform.position = mediumPos;

        mediumPos = b_maps.transform.position;
        mediumPos.y = 1007;
        b_maps.transform.position = mediumPos;

        mediumPos = b_settings.transform.position;
        mediumPos.y = 1007;
        b_settings.transform.position = mediumPos;

        currentIndex = 0;
    }

    public void ClickEvidenceButton()
    {
        c_notes.gameObject.SetActive(false);
        c_evidence.gameObject.SetActive(true);
        c_e_popup.gameObject.SetActive(true);
        c_relationships.gameObject.SetActive(false);
        c_r_popup.gameObject.SetActive(false);
        c_maps.gameObject.SetActive(false);
        c_settings.gameObject.SetActive(false);

        c_e_popup.enabled = false;

        Vector3 mediumPos = b_notes.transform.position;
        mediumPos.y = 1007;
        b_notes.transform.position = mediumPos;

        mediumPos = b_evidence.transform.position;
        mediumPos.y = 1021;
        b_evidence.transform.position = mediumPos;

        mediumPos = b_relationships.transform.position;
        mediumPos.y = 1007;
        b_relationships.transform.position = mediumPos;

        mediumPos = b_maps.transform.position;
        mediumPos.y = 1007;
        b_maps.transform.position = mediumPos;

        mediumPos = b_settings.transform.position;
        mediumPos.y = 1007;
        b_settings.transform.position = mediumPos;

        currentIndex = 1;
    }

    public void ClickRelationshipsButton()
    {
        c_notes.gameObject.SetActive(false);
        c_evidence.gameObject.SetActive(false);
        c_e_popup.gameObject.SetActive(false);
        c_relationships.gameObject.SetActive(true);
        c_r_popup.gameObject.SetActive(true);
        c_maps.gameObject.SetActive(false);
        c_settings.gameObject.SetActive(false);

        c_r_popup.enabled = false;

        Vector3 mediumPos = b_notes.transform.position;
        mediumPos.y = 1007;
        b_notes.transform.position = mediumPos;

        mediumPos = b_evidence.transform.position;
        mediumPos.y = 1007;
        b_evidence.transform.position = mediumPos;

        mediumPos = b_relationships.transform.position;
        mediumPos.y = 1021;
        b_relationships.transform.position = mediumPos;

        mediumPos = b_maps.transform.position;
        mediumPos.y = 1007;
        b_maps.transform.position = mediumPos;

        mediumPos = b_settings.transform.position;
        mediumPos.y = 1007;
        b_settings.transform.position = mediumPos;

        currentIndex = 2;
    }

    public void ClickMapsButton()
    {
        c_notes.gameObject.SetActive(false);
        c_evidence.gameObject.SetActive(false);
        c_e_popup.gameObject.SetActive(false);
        c_relationships.gameObject.SetActive(false);
        c_r_popup.gameObject.SetActive(false);
        c_maps.gameObject.SetActive(true);
        c_settings.gameObject.SetActive(false);

        Vector3 mediumPos = b_notes.transform.position;
        mediumPos.y = 1007;
        b_notes.transform.position = mediumPos;

        mediumPos = b_evidence.transform.position;
        mediumPos.y = 1007;
        b_evidence.transform.position = mediumPos;

        mediumPos = b_relationships.transform.position;
        mediumPos.y = 1007;
        b_relationships.transform.position = mediumPos;

        mediumPos = b_maps.transform.position;
        mediumPos.y = 1021;
        b_maps.transform.position = mediumPos;

        mediumPos = b_settings.transform.position;
        mediumPos.y = 1007;
        b_settings.transform.position = mediumPos;

        currentIndex = 3;
    }

    public void ClickSettingsButton()
    {
        c_notes.gameObject.SetActive(false);
        c_evidence.gameObject.SetActive(false);
        c_e_popup.gameObject.SetActive(false);
        c_relationships.gameObject.SetActive(false);
        c_r_popup.gameObject.SetActive(false);
        c_maps.gameObject.SetActive(false);
        c_settings.gameObject.SetActive(true);

        Vector3 mediumPos = b_notes.transform.position;
        mediumPos.y = 1007;
        b_notes.transform.position = mediumPos;

        mediumPos = b_evidence.transform.position;
        mediumPos.y = 1007;
        b_evidence.transform.position = mediumPos;

        mediumPos = b_relationships.transform.position;
        mediumPos.y = 1007;
        b_relationships.transform.position = mediumPos;

        mediumPos = b_maps.transform.position;
        mediumPos.y = 1007;
        b_maps.transform.position = mediumPos;

        mediumPos = b_settings.transform.position;
        mediumPos.y = 1021;
        b_settings.transform.position = mediumPos;

        currentIndex = 4;
    }


    // Opening Evidence / Relationship popup windows
    public void OpenEvidencePopup()
    {
        c_e_popup.enabled = true;
    }

    public void OpenRelationshipsPopup()
    {
        c_r_popup.enabled = true;
    }

    // Closing Evidence / Relationship popup windows
    public void CloseEvidencePopup()
    {
        c_e_popup.enabled = false;
    }

    public void CloseRelationshipsPopup()
    {
        c_r_popup.enabled = false;
    }


    // Evidence
    public bool IsDiscovered(EvidenceData evidence)
    {
        return discoveredEvidence.Contains(evidence);
    }

    public List<EvidenceData> GetDiscoveredEvidence()   // Safe Getter
    {
        return new List<EvidenceData>(discoveredEvidence);
    }

    public void UnlockEvidence(EvidenceData evidence)   // Show the pre-defined evidence data
    {
        // This is possible because HashSets don't allow same entries to be added multiple times
        if (discoveredEvidence.Add(evidence))
        {
            Debug.Log("New Evidence Discovered: " + evidence.name);
            EvidenceTabController.Instance.RefreshPage();
        }
    }


    // Relationships
    public void UnlockRelation(RelationshipsData relationship)
    {
        if(unlockedRelations.Add(relationship))
        {
            Debug.Log("New Relationship Discovered: " + relationship.name);
            RelationshipsTabController.Instance.RefreshPage();
        }
    }
    
    public bool IsDiscovered(RelationshipsData relationship)
    {
        return unlockedRelations.Contains(relationship);
    }


    // Resetting pages when closing journal
}
