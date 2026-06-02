using System.IO;
using UnityEngine;

public class Key : Item
{
    [SerializeField]
    private Door door;

    [SerializeField]
    public EvidenceData key;

    public override void Interact(PlayerControlled player)
    {
        // place object in bird inventory
        if (player is Bird)
        {
            Bird bird = (Bird)player;

            if (bird.Pickup(this.gameObject))
            {
                this.transform.SetParent(bird.transform);
            }
        }
        // destory key if it's not currently held
        else if (player is Player && transform.parent == null)
        {
            Debug.Log("player picks up key");

            key.discovered = true;
            string keyJSON = JsonUtility.ToJson(key);
            File.WriteAllText(Application.persistentDataPath + "/saveData.json", keyJSON);

            Debug.Log("Key Discovered: " + key.discovered);

            SpawnManager.Instance.RemoveItem(itemID);
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Unlock door that corresponds
    /// </summary>
    private void OnDestroy()
    {
        door.Unlock();
    }
}
