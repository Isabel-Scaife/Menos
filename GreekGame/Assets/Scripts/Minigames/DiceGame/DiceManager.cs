using System.Collections.Generic;
using UnityEngine;

// manages dice game logic
public class DiceManager : MonoBehaviour
{
    // fields
    // player to give input control back to after post game dialogue runs
    [SerializeField] private PlayerControlled worldPlayer;

    // flag to set after winning for first time
    [SerializeField] private string firstWinFlag;

    [SerializeField] private DialogueSO firstWinDialogue;
    [SerializeField] private DialogueSO winDialogue;
    [SerializeField] private DialogueSO lossDialogue;

    [SerializeField] private DiceContestant dicePlayer;
    [SerializeField] private List<DiceContestant> opponents;


    // singleton
    public static DiceManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        bool firstWin = false;

        // if wincon met and first win flag not yet set, firstwin = true and set flag
        if (WinConditionMet())
        {

        }

        else if (true)  // loss condition
        {

        }
    }

    /// <summary>
    /// determines if the player's dice meet their win condition
    /// </summary>
    /// <returns>true if player win condition is met, false otherwise</returns>
    private bool WinConditionMet()
    {
        return true;
    }

    /// <summary>
    /// logic to run when dice game is won or lost
    /// </summary>
    /// <param name="win">whether the player won or lost</param>
    private void Complete(bool win, bool firstWin)
    {
        // Isabel goes here :p

        // here for now for testing, maybe goes in OnComplete? should be called after switching 
        //      camera to main scene, needs this method's win param as its own win arg
        RunPostGameDialogue(win, firstWin);
    }

    // call this after switching back to main scene after completing dice game
    //      (this code is here and not on the NPC since, unlike dialogue that
    //      plays when starting dice game, it is not triggered by interacting)
    private void RunPostGameDialogue(bool win, bool firstWin)
    {
        if (DialogueManager.Instance == null) return;
        DialogueSO dialogue = lossDialogue;
        if (firstWin) dialogue = firstWinDialogue;
        else if (win) dialogue = winDialogue;
        DialogueManager.Instance.BeginDialogue(dialogue, worldPlayer);
    }
}
