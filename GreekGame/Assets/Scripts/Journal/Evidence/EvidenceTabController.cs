using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvidenceTabController : MonoBehaviour
{
    public static EvidenceTabController Instance;

    public EvidenceDatabase database;               // Database Reference

    [Header("UI Slots (Assign 20 in Inspector)")]
    public JournalIconUI[] slots;

    [Header("SuspectTags GameObject here. Inside Image PlaceHolder")]
    public SuspectTags[] tagsParent;

    public static event UpdateTag ReloadTag;
    public delegate void UpdateTag();

    public int itemsPerPage = 20;
    private int currentPage = 0;

    private void Awake()
    {
        if(Instance == null)
        Instance = this;
    }


    // Testing purpose: Start & Update Both
    private void Start()
    {
        // Initialize all Evidence so its members can be used properly
        database.InitializeAllEvidence();

        RefreshPage();
        SetupTags();
        UpdateTags();

        ReloadTag += UpdateTags;
    }

    private void Update()
    {
        // FOR TESTING
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // int index = Random.Range(0, database.Evidences.Length);
            var random = database.Evidences[Random.Range(0, database.Evidences.Length)];
            JournalManager.Instance.UnlockEvidence(random);
            // random.buttonNum = index;
        }
    }

    // Refresh page - Call when player unlocks new evidence, or page is flipped as new evidence content needs to be displayed
    public void RefreshPage()
    {
        var allData = database.Evidences;

        int startIndex = currentPage * itemsPerPage;

        for (int i = 0; i < slots.Length; i++)
        {
            int index = startIndex + i;

            if(index < allData.Length)
            {
                var data = allData[index];
                bool discovered = JournalManager.Instance.IsDiscovered(data);

                slots[i].Setup(data, discovered);
                slots[i].gameObject.SetActive(true);
            }
            else
            {
                slots[i].Setup(null, false);
            }
        }
    }

    // Progress to next page
    public void NextPage()
    {
        if(JournalManager.Instance.CurrentSection == 1)
        {
            int total = database.Evidences.Length;
            int maxPage = Mathf.CeilToInt((float)total / itemsPerPage) - 1;

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
        if(JournalManager.Instance.CurrentSection == 1)
        {
            if (currentPage > 0)
            {
                currentPage--;
                RefreshPage();
            }
        }
    }

    // Initial tag setup
    public void SetupTags()
    {
        for(int i = 0; i < tagsParent.Length; i++)
        {
            tagsParent[i].HideAllTag();
        }
    }

    // Update Tags (suddenly not working lol)
    public void UpdateTags()
    {
        // To prevent null reference exception only check evidences that exist
        for (int i = 0; i < database.Evidences.Length; i++)
        {
            EvidenceData evidence = (EvidenceData)(slots[i].Entry);     // Maybe this doesn't return current evidence?

            // Needs to be at least one element to update tags
            if(evidence.possibleRelations.Count > 0)
            {
                var possibleRelations = evidence.possibleRelations;

                for (int j = 0; j < possibleRelations.Count; j++)
                {
                    tagsParent[i].suspectImgHolder[j].transform.parent.gameObject.SetActive(true);    // Set bg active (Later this should change to be more efficient)
                    tagsParent[i].suspectImgHolder[j].gameObject.SetActive(true);
                    tagsParent[i].suspectImgHolder[j].sprite = possibleRelations[j].icon;
                }
            }
        }
    }

    // Invoke Reload Tag outside of Evidence Tab Controller Class
    public void InvokeReloadTag()
    {
        ReloadTag?.Invoke();
    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }
}
