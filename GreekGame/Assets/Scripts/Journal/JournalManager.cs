using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class JournalManager : MonoBehaviour
{
    // A public static instance of itself to give access to functions of journal manager
    public static JournalManager Instance;
    // public Canvas journalMain;              // Instance.gameObject

    // Canvas component of Journal Object (This will be turned off and on)
    public Canvas mainCanvas;

    // State of the Journal. This will be used to change the action map from other things to journal map
    // If journal gameobject is active, this should be tagged as 'InUse'
    public static bool InUse;

    public EvidenceDatabase eDatabase;
    public RelationshipsDatabase rDatabase;

    // ** THIS SHOULD NOT BE SAVED. CHECK DISCOVERED STATE OF OBJECTS!!!
    // ** DATA CAN HAVE ITS STATUS SAVED AND
    //    EVERYTIME GAME LOADS DISCOVERED THINGS CAN BE POPPED BACK IN
    // Check whether evidence | relationships | map are discovered
    private static List<EvidenceData> discoveredEvidence = new List<EvidenceData>();
    private static List<RelationshipsData> unlockedRelations = new List<RelationshipsData>();

    // Public property to access data inside list
    public List<EvidenceData> DiscoveredEvidence { get { return discoveredEvidence; } }
    public List<RelationshipsData> UnlockedRelations { get { return unlockedRelations; } }

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

    // NOT NEEDED
    // Event used to open journal. Invokes the event and sets journal canvas active
    public static event Setup Open;
    public delegate void Setup();

    public bool escapeHandledThisFrame = false; 

    // Store Child Canvases
    // ** Don't store in child canvases because it cannot scale the objects in it separate from the parent canvas
    [SerializeField]
    private GameObject p_notes;
    [SerializeField]
    private GameObject p_evidence;
    [SerializeField]
    private GameObject p_e_popup;
    [SerializeField]
    private GameObject p_relationships;
    [SerializeField]
    private GameObject p_r_popup;
    [SerializeField]
    private GameObject p_maps;
    [SerializeField]
    private GameObject p_settings;

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

    public PlayerInput playerInput;   // Need to switch back to player map
   
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Reset all canvas/button references here
        ResetReferences();
    }

    private void ResetReferences()
    {
        // Re-assign all Canvas and Button references, especially if they're in the new scene
        p_notes = GameObject.Find("Notes Section");
        p_evidence = GameObject.Find("Evidence Section");
        p_e_popup = GameObject.Find("Evidence Popup");
        p_relationships = GameObject.Find("Relationship Section");
        p_r_popup = GameObject.Find("Relationships Popup");
        p_maps = GameObject.Find("Maps Section");
        p_settings = GameObject.Find("Settings Section");

        b_notes = GameObject.Find("Button_Notes")?.GetComponent<Button>();
        b_evidence = GameObject.Find("Button_Evidence")?.GetComponent<Button>();
        b_relationships = GameObject.Find("Button_Relationships")?.GetComponent<Button>();
        b_maps = GameObject.Find("Button_Maps")?.GetComponent<Button>();
        b_settings = GameObject.Find("Button_Settings")?.GetComponent<Button>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Subscribe to button or controller triggered events (Also Unsubscribe to previous methods)
        // ** THIS IS SUBSCRIBED WHEN JOURNAL IS OPEN | COMMENT THIS OUT WHEN TESTING IN THE GAME SCENES
        ProgressL -= UpdateIndex_L;
        ProgressL -= UpdateSection;
        ProgressL += UpdateIndex_L;
        ProgressL += UpdateSection;

        ProgressR -= UpdateIndex_R;
        ProgressR -= UpdateSection;
        ProgressR += UpdateIndex_R;
        ProgressR += UpdateSection;

        // Subscribe to journal open event (MIGHT NOT BE NEEDED, but FOR OTHER COMPONENTS DISABLE EVENT TRIGGER)
        Open += OpenJournal;

        ClickEvidenceButton();

        playerInput.SwitchCurrentActionMap("Journal/UI");

        RetrieveDiscoveredEvidence();
    }

    // Update is called once per frame
    void Update()
    {
        // Reset escape performed every frame
        escapeHandledThisFrame = false;
    }

   
    // ** JOURNAL MANAGER SHOULD NOT BE ENABLED AND DISABLED. THEREFORE, THESE CODE WERE MOVED TO OPEN/CLOSE JOURNAL
    private void OnEnable()
    {
    }

    // This can later be called when it is integrated within the main overlay screen
    // Just simply disable the journal UI
    private void OnDisable()
    {
    }

    // Disable Player Input component (Used when player is taking notes | Can be used in other instances such as when UI shouldn't change)
    public void DisableJournalUIInput()
    {
        playerInput.enabled = false;
    }

    public void EnableJournalUIInput()
    {
        playerInput.enabled = true;
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
            currentIndex -= 1;
        }
        else
        {
            currentIndex = 4;
        }

        Debug.Log("Progress Left: " + currentIndex);
    }

    void UpdateIndex_R()
    {
        if(currentIndex < 4)
        {
            currentIndex += 1;
        }
        else
        {
            currentIndex = 0;
        }

        Debug.Log("Progress Right: " + currentIndex);
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
        p_notes.gameObject.SetActive(true);
        p_evidence.gameObject.SetActive(false);
        p_e_popup.gameObject.SetActive(false);
        p_relationships.gameObject.SetActive(false);
        p_r_popup.gameObject.SetActive(false);
        p_maps.gameObject.SetActive(false);
        p_settings.gameObject.SetActive(false);
        
        //Vector3 mediumPos = b_notes.transform.position;
        //mediumPos.y = 1021;
        //b_notes.transform.position = mediumPos;

        //mediumPos = b_evidence.transform.position;
        //mediumPos.y = 1007;
        //b_evidence.transform.position = mediumPos;

        //mediumPos = b_relationships.transform.position;
        //mediumPos.y = 1007;
        //b_relationships.transform.position = mediumPos;

        //mediumPos = b_maps.transform.position;
        //mediumPos.y = 1007;
        //b_maps.transform.position = mediumPos;

        //mediumPos = b_settings.transform.position;
        //mediumPos.y = 1007;
        //b_settings.transform.position = mediumPos;

        currentIndex = 0;
    }

    public void ClickEvidenceButton()
    {
        p_notes.gameObject.SetActive(false);
        p_evidence.gameObject.SetActive(true);
        p_e_popup.gameObject.SetActive(true);
        p_relationships.gameObject.SetActive(false);
        p_r_popup.gameObject.SetActive(false);
        p_maps.gameObject.SetActive(false);
        p_settings.gameObject.SetActive(false);

        p_e_popup.SetActive(false);

        //Vector3 mediumPos = b_notes.transform.position;
        //mediumPos.y = 1007;
        //b_notes.transform.position = mediumPos;

        //mediumPos = b_evidence.transform.position;
        //mediumPos.y = 1021;
        //b_evidence.transform.position = mediumPos;

        //mediumPos = b_relationships.transform.position;
        //mediumPos.y = 1007;
        //b_relationships.transform.position = mediumPos;

        //mediumPos = b_maps.transform.position;
        //mediumPos.y = 1007;
        //b_maps.transform.position = mediumPos;

        //mediumPos = b_settings.transform.position;
        //mediumPos.y = 1007;
        //b_settings.transform.position = mediumPos;

        currentIndex = 1;
    }

    public void ClickRelationshipsButton()
    {
        p_notes.gameObject.SetActive(false);
        p_evidence.gameObject.SetActive(false);
        p_e_popup.gameObject.SetActive(false);
        p_relationships.gameObject.SetActive(true);
        p_r_popup.gameObject.SetActive(true);
        p_maps.gameObject.SetActive(false);
        p_settings.gameObject.SetActive(false);

        p_r_popup.SetActive(false);

        //Vector3 mediumPos = b_notes.transform.position;
        //mediumPos.y = 1007;
        //b_notes.transform.position = mediumPos;

        //mediumPos = b_evidence.transform.position;
        //mediumPos.y = 1007;
        //b_evidence.transform.position = mediumPos;

        //mediumPos = b_relationships.transform.position;
        //mediumPos.y = 1021;
        //b_relationships.transform.position = mediumPos;

        //mediumPos = b_maps.transform.position;
        //mediumPos.y = 1007;
        //b_maps.transform.position = mediumPos;

        //mediumPos = b_settings.transform.position;
        //mediumPos.y = 1007;
        //b_settings.transform.position = mediumPos;

        currentIndex = 2;
    }

    public void ClickMapsButton()
    {
        p_notes.gameObject.SetActive(false);
        p_evidence.gameObject.SetActive(false);
        p_e_popup.gameObject.SetActive(false);
        p_relationships.gameObject.SetActive(false);
        p_r_popup.gameObject.SetActive(false);
        p_maps.gameObject.SetActive(true);
        p_settings.gameObject.SetActive(false);

        //Vector3 mediumPos = b_notes.transform.position;
        //mediumPos.y = 1007;
        //b_notes.transform.position = mediumPos;

        //mediumPos = b_evidence.transform.position;
        //mediumPos.y = 1007;
        //b_evidence.transform.position = mediumPos;

        //mediumPos = b_relationships.transform.position;
        //mediumPos.y = 1007;
        //b_relationships.transform.position = mediumPos;

        //mediumPos = b_maps.transform.position;
        //mediumPos.y = 1021;
        //b_maps.transform.position = mediumPos;

        //mediumPos = b_settings.transform.position;
        //mediumPos.y = 1007;
        //b_settings.transform.position = mediumPos;

        currentIndex = 3;
    }

    public void ClickSettingsButton()
    {
        p_notes.gameObject.SetActive(false);
        p_evidence.gameObject.SetActive(false);
        p_e_popup.gameObject.SetActive(false);
        p_relationships.gameObject.SetActive(false);
        p_r_popup.gameObject.SetActive(false);
        p_maps.gameObject.SetActive(false);
        p_settings.gameObject.SetActive(true);

        //Vector3 mediumPos = b_notes.transform.position;
        //mediumPos.y = 1007;
        //b_notes.transform.position = mediumPos;

        //mediumPos = b_evidence.transform.position;
        //mediumPos.y = 1007;
        //b_evidence.transform.position = mediumPos;

        //mediumPos = b_relationships.transform.position;
        //mediumPos.y = 1007;
        //b_relationships.transform.position = mediumPos;

        //mediumPos = b_maps.transform.position;
        //mediumPos.y = 1007;
        //b_maps.transform.position = mediumPos;

        //mediumPos = b_settings.transform.position;
        //mediumPos.y = 1021;
        //b_settings.transform.position = mediumPos;

        currentIndex = 4;
    }


    // Opening Evidence / Relationship popup windows
    public void OpenEvidencePopup()
    {
        p_e_popup.SetActive(true);
    }

    public void OpenRelationshipsPopup()
    {
        p_r_popup.SetActive(true);
    }

    // Closing Evidence / Relationship popup windows
    public void CloseEvidencePopup()
    {
        Debug.Log("Close Evidence Popup");
        p_e_popup.SetActive(false);
    }

    public void CloseRelationshipsPopup()
    {
        Debug.Log("Close Relationships Popup");
        p_r_popup.SetActive(false);
    }


    // This entire part might not be even needed
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
        if (!discoveredEvidence.Contains(evidence))
        {
            discoveredEvidence.Add(evidence);
            Debug.Log("New Evidence Discovered: " + evidence.name);
            EvidenceTabController.Instance.RefreshPage();
        }
    }

    // Retrieve Saved Evidence 
    public void RetrieveDiscoveredEvidence()
    {
        for (int i = 0; i < eDatabase.Evidences.Length; i++)
        {
            if (eDatabase.Evidences[i].discovered)
            {
                JournalManager.Instance.UnlockEvidence(eDatabase.Evidences[i]);
            }
        }
    }


    // Relationships
    public void UnlockRelation(RelationshipsData relationship)
    {
        if(!unlockedRelations.Contains(relationship))
        {
            unlockedRelations.Add(relationship);
            Debug.Log("New Relationship Discovered: " + relationship.name);
            RelationshipsTabController.Instance.RefreshPage();
        }
    }
    
    public bool IsDiscovered(RelationshipsData relationship)
    {
        return unlockedRelations.Contains(relationship);
    }


    // Open and Close Journal
    // Open Journal
    public void OpenJournal()
    {
        // Set the canvas component to true to display everything
        mainCanvas.enabled = true;

        playerInput.SwitchCurrentActionMap("Journal/UI");

        ProgressL -= UpdateIndex_L;
        ProgressL -= UpdateSection;
        ProgressL += UpdateIndex_L;
        ProgressL += UpdateSection;

        ProgressR -= UpdateIndex_R;
        ProgressR -= UpdateSection;
        ProgressR += UpdateIndex_R;
        ProgressR += UpdateSection;

        // Subscribe to journal open event (MIGHT NOT BE NEEDED, but FOR OTHER COMPONENTS DISABLE EVENT TRIGGER)
        // Open += OpenJournal;

        // After Journal Instance has been set active and open journal was triggered, this runs
        // First section to see is notes
        ClickEvidenceButton();

        // Change the action map to Journal (Not needed because Journal is in a SEPARATE scene)
        // playerInput.SwitchCurrentActionMap("Journal/UI");

        InUse = true;
    }

    // NOT NEEDED FOR NOW - UNLESS JOURNAL WILL BE IN THE SAME SCENE WITH OTHER THINGS
    public void InvokeOpen()
    {
        Open?.Invoke();
    }


    // Resetting pages when closing journal 
    public void CloseJournal()
    {
        // Change the input action map
        playerInput.SwitchCurrentActionMap("Player");
        
        // Unsubscribe to events
        ProgressL -= UpdateIndex_L;
        ProgressL -= UpdateSection;

        ProgressR -= UpdateIndex_R;
        ProgressR -= UpdateSection;

        // Subscribe to journal open event (MIGHT NOT BE NEEDED, but FOR OTHER COMPONENTS DISABLE EVENT TRIGGER)
        Open -= OpenJournal;

        InUse = false;

        // Reset pages of sections
        EvidenceTabController.Instance.currentPage = 0;
        RelationshipsTabController.Instance.currentPage = 0;

        // Set the canvas inactive
        mainCanvas.enabled = false;

        // Move to previous scene (Not needed unless Journal is in a separate scene)
        // SceneHistory.Instance.GoBack();
    }

    
    // ** Loading saved journal data
    //  --> DISCOVERED BOOLEAN FIELD IN DATA(EVIDENCE & RELATIONSHIP) MIGHT NOT BE NECESSARY

    /// <summary>
    /// Load all the relationship unlocked from the previous session
    /// </summary>
    /// <param name="relationshipData">Relationship Data that has been saved from previous session</param>
    public void LoadData(DiscoveredRelationshipData relationshipData)
    {
        for(int i = 0; i < relationshipData.discoveredRelations.Length; i++)
        {
            unlockedRelations.Add(rDatabase.FindRelationshipByID(relationshipData.discoveredRelations[i]));
        }
    }

    /// <summary>
    /// Load all evidence unlocked from the previous session
    /// </summary>
    /// <param name="evidenceData">Evidence Data that has been saved from previous session</param>
    public void LoadData(DiscoveredEvidenceData evidenceData)
    {
        for(int i = 0; i < evidenceData.savedEvidence.Length; i++)
        {
            EvidenceDataData data = evidenceData.savedEvidence[i];
            EvidenceData evidence = eDatabase.FindEvidenceByID(data.evidenceID);
            
            foreach(string relation in data.relatedRelationID)
            {
                evidence.possibleRelations.Add(rDatabase.FindRelationshipByID(relation));
            }
        }
    }
}
