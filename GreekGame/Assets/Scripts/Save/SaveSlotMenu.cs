using System.Collections.Generic;
using UnityEngine;

public class SaveSlotMenu : MonoBehaviour
{
    [SerializeField] MainMenu mainMenu;
    [SerializeField] GameObject btn_Back;
    private SaveSlot[] saveSlots;

    private void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SaveSlot>();
    }

    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
        btn_Back.SetActive(true);

        // load all save file information
        Dictionary<string, ProgressionManagerData> saveFilesData = SaveSystem.ReadSaveFolderData();

        // find matching save slat in UI and loaded data
        foreach (SaveSlot saveSlot in saveSlots)
        {
            ProgressionManagerData data = null;

            saveFilesData.TryGetValue(saveSlot.ID, out data);
            saveSlot.SetData(data);
        }


    }
    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
        btn_Back.SetActive(false);

    }

    public void OnBackClicked()
    {
        DeactivateMenu();
        if (mainMenu != null)
        {
            mainMenu.ActivateMenu();
        }
    }
}
