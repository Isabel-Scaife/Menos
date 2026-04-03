using UnityEngine.SceneManagement;

public class Vase : Interactable
{
    public override void Interact(PlayerControlled player)
    {
        SceneManager.LoadScene("PotPackage");
    }
}
