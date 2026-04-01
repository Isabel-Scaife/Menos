using UnityEngine;
using UnityEngine.UI;

public class SuspectTags : MonoBehaviour
{
    [SerializeField]
    public Image[] suspectImgHolder;

    public void HideAllTag()
    {
        for(int i = 0; i < suspectImgHolder.Length; i++)
        {
            suspectImgHolder[i].gameObject.SetActive(false);

            // Don't forget to hide the background!! and set it active in evidence tab controller
            suspectImgHolder[i].transform.parent.gameObject.SetActive(false);

        }
    }
}
