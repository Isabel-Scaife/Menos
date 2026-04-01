using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RelationshipsPopup : MonoBehaviour
{
    public static RelationshipsPopup Instance;
    public static bool               open;

    public Image characterSprite;
    public TMP_Text name;
    public TMP_Text description;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        open = false;
    }

    public void Show(RelationshipsData relationship)
    {
        JournalManager.Instance.OpenRelationshipsPopup();

        name.text = relationship.name;
        description.text = relationship.description;
        characterSprite.sprite = relationship.icon;

        open = true;
    }

    public void ExitRelationshipsPopup()
    {
        if(open)
        {
            JournalManager.Instance.CloseRelationshipsPopup();
            open = false;
        }
    }
}
