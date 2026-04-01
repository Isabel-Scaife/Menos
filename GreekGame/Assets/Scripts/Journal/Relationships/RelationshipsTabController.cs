using UnityEngine;

public class RelationshipsTabController : MonoBehaviour
{
    public static RelationshipsTabController Instance;

    public RelationshipsDatabase database;

    public JournalIconUI[] family;
    public GameObject familyTreePanel;
    private bool familySetup;           // If family Setup is done, we don't have to do it again - used to ignore unlock family 
                                        // This data should also be saved through JSON

    public JournalIconUI[] otherRelation;
    public GameObject relationsGridPanel;

    public int relationPerPage = 20;
    private int currentPage = 0;
    
    // Communicates to the evidence popup whether this relationship was discovered
    public static event displayButtonInR Discovered;
    public delegate void displayButtonInR();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        familySetup = false;
        RefreshPage();
    }

    private void Update()
    {
        // FOR TESTING
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var random = database.Relationships[Random.Range(10, database.Relationships.Length)];
            JournalManager.Instance.UnlockRelation(random);
        }
    }

    // Refresh page when user flips between pages. Doesn't instantiate new UI elements
    // Also call when player unlocks/meets new relationship
    public void RefreshPage()
    {
        // First page is a family tree
        if (currentPage == 0)
        {
            familyTreePanel.SetActive(true);
            relationsGridPanel.SetActive(false);

            if(!familySetup)
            {
                UnlockFamily();
            }
            return;
        }
        // Second page and so on is other relations
        // Has a grid layout and progresses similarly to the evidence tab 
        familyTreePanel.SetActive(false);
        relationsGridPanel.SetActive(true);

        // Update page 
        RelationshipsData[] allData = database.Relationships;
        int startingIndex = 10 + 20 * (currentPage - 1);

        for(int i = 0; i < otherRelation.Length; i++)
        {
            int index = startingIndex + i;

            if (index < allData.Length)
            {
                var data = allData[index];
                bool discovered = JournalManager.Instance.IsDiscovered(data);

                otherRelation[i].Setup(data, discovered);
                otherRelation[i].gameObject.SetActive(true);

                Discovered?.Invoke();
            }
            else
            {
                otherRelation[i].Setup(null, false);
            }
        }

    }

    // Progress to next page
    public void NextPage()
    {
        if (JournalManager.Instance.CurrentSection == 2)
        {
            int total = database.Relationships.Length - 10;                 // Ignore Family Members!
            int maxPage = Mathf.CeilToInt((float)total / relationPerPage);

            if (currentPage < maxPage)
            {
                currentPage++;
                RefreshPage();
            }
        }
    }


    // Move backward to previous page
    public void PreviousPage()
    {
        if (JournalManager.Instance.CurrentSection == 2)
        {
            if (currentPage > 0)
            {
                currentPage--;
                RefreshPage();
            }
        }
    }

    // Helper function to unlock all family - this can be changed later 
    // For now, player's family relation will all be public
    public void UnlockFamily()
    {
        RelationshipsData cecrops = database.Relationships[0];
        RelationshipsData aglarausG = database.Relationships[1];
        RelationshipsData erysichthon = database.Relationships[2];
        RelationshipsData pandrosus = database.Relationships[3];
        RelationshipsData herse = database.Relationships[4];
        RelationshipsData hermes = database.Relationships[5];
        RelationshipsData aglarausM = database.Relationships[6];
        RelationshipsData ares = database.Relationships[7];
        RelationshipsData alcippe = database.Relationships[8];
        RelationshipsData ceryx = database.Relationships[9];

        JournalManager.Instance.UnlockRelation(cecrops);  // Cecrops I
        JournalManager.Instance.UnlockRelation(aglarausG);  // Aglaraus_grandmother
        JournalManager.Instance.UnlockRelation(erysichthon);  // Erysichthon
        JournalManager.Instance.UnlockRelation(pandrosus);  // Pandrosus
        JournalManager.Instance.UnlockRelation(herse);  // Herse
        JournalManager.Instance.UnlockRelation(hermes);  // Hermes
        JournalManager.Instance.UnlockRelation(aglarausM);  // aglaraus_mom
        JournalManager.Instance.UnlockRelation(ares);  // ares
        JournalManager.Instance.UnlockRelation(alcippe);  // alcippe
        JournalManager.Instance.UnlockRelation(ceryx);  // ceryx

        family[0].Setup(cecrops, true);
        family[1].Setup(aglarausG, true);
        family[2].Setup(erysichthon, true);
        family[3].Setup(pandrosus, true);
        family[4].Setup(herse, true);
        family[5].Setup(hermes, true);
        family[6].Setup(aglarausM, true);
        family[7].Setup(ares, true);
        family[8].Setup(alcippe, true);
        family[9].Setup(ceryx, true);

        familySetup = true;
    }
}
