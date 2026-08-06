using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Shows dialogue in UI, manages dialgoue choices (branching logic), etc.
/// </summary>
public class DialogueManager : MonoBehaviour
{
    // singleton
    public static DialogueManager Instance { get; private set; }

    // fields
    private DialogueSO currentDialogue;
    private DialogueNode currentNode;
    private Dictionary<string, DialogueNode> nodes;
    private bool textIsScrolling;
    private float scrollTimer;
    private Queue<char> scrollTextRemaining;
    private bool wantsToAdvance;
    private PlayerControlled player;

    // display text stuff
    [SerializeField]
    private TextMeshProUGUI speakerTMP;
    [SerializeField]
    private TextMeshProUGUI dialogueTMP;
    [SerializeField]
    private GameObject dialoguePanel;
    [SerializeField]
    private float textDelay;    // seconds before showing the next char

    // dialogue choices
    [SerializeField]
    private List<TextMeshProUGUI> choiceTMPs;
    [SerializeField]
    private List<GameObject> choiceBoxes;
    private GameObject previousSelectedChoiceBox;
    private bool choicesShowing;
    private DialogueChoice chosen;

    // talking sprites
    [SerializeField]
    private Image speakerLeft;
    [SerializeField]
    private Image speakerRight;

    public bool DialogueIsHappening { get; private set; }

    public event Action OnDialogueEnd;

    // functions
    private void Awake()
    {
        Instance = this;

        // reset fields to defaults
        textIsScrolling = false;
        wantsToAdvance = false;
        DialogueIsHappening = false;
        scrollTimer = 0.0f;
        scrollTextRemaining = new Queue<char>();
        chosen = null;
        choicesShowing = false;
    }

    private void Update()
    {
        // shows text one character at a time
        if (textIsScrolling)
        {
            // fills textbox when player chooses to advance
            if (wantsToAdvance)
            {
                wantsToAdvance = false;
                textIsScrolling = false;
                dialogueTMP.text = currentNode.text;
            }

            // decrements timer until next character shows
            else if (scrollTimer > 0)
            {
                scrollTimer -= Time.deltaTime;
            }

            // shows next character
            else
            {
                scrollTimer = textDelay;
                dialogueTMP.text += scrollTextRemaining.Dequeue();
                if (scrollTextRemaining.Count < 1)
                {
                    textIsScrolling = false;
                }
            }           
        }

        // advances to next node, or quits if dialogue is over, when player advances
        else if (DialogueIsHappening && wantsToAdvance && !choicesShowing)
        {
            if (currentNode.isEndpoint)
            {
                CloseDialogue();
            }
            else
            {
                // advance to next piece of dialogue
                currentNode = nodes[currentNode.nextNodeID];
                DisplayDialogue();
            }
        }

        // displays next piece of dialogue based on option chosen
        else if (chosen != null)
        {
            // apply outcome based on choice
            if (chosen.outcome != null)
            {
                ApplyOutcome(chosen.outcome);
            }
            
            // hides choice boxes
            choicesShowing = false;
            int len = currentNode.choices.Count;
            for (int i = 0; i < len; i++)
            {
                choiceBoxes[i].SetActive(false);
                choiceTMPs[i].text = "";
            }
            string nextID = chosen.nextNodeID;
            chosen = null;

            // close dialogue if choice leads nowhere
            if (nextID == null || nextID.Length < 1)
            {
                CloseDialogue();
            }
            // otherwise display next dialogue
            else
            {
                currentNode = nodes[nextID];
                DisplayDialogue();
            }
        }

        // ensure a choice is always selected
        if (choicesShowing)
        {
            // track selected choice
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                previousSelectedChoiceBox = EventSystem.current.currentSelectedGameObject;
            }
            // reselect if missing selection
            else if (previousSelectedChoiceBox != null)
            {
                EventSystem.current.SetSelectedGameObject(previousSelectedChoiceBox);
            }
        }
    }

    /// <summary>
    /// Shows dialogue
    /// </summary>
    /// <param name="dialogue">all dialogue info for the interaction that should play</param>
    public void BeginDialogue(DialogueSO dialogue, PlayerControlled _player)
    {      
        // quits if no dialogue was given
        if (dialogue == null) return;

        // switches player input to be able to advance dialogue and not move
        player = _player;
        player.SwitchActionMaps(true);
        
        // gets all dialogue for the interaction
        currentDialogue = dialogue;
        nodes = dialogue.nodes.ToDictionary(n => n.id);
        currentNode = nodes[currentDialogue.startingNodeID];

        // trigger flag if that applies to dialogue
        if (currentDialogue.flag != null && GameStateManager.Instance != null)
        {
            // set flag
            if (currentDialogue.toggleFlagOnTalk)
            {
                GameStateManager.Instance.SetFlag(currentDialogue.flag);
            }
            // disable flag
            else
            {
                GameStateManager.Instance.ClearFlag(currentDialogue.flag);
            }
        }

        // displays dialogue in UI
        dialoguePanel.SetActive(true);
        DisplayDialogue();
        DialogueIsHappening = true;
    }

    /// <summary>
    /// Displays dialogue text and/or choices to the player
    /// </summary>
    private void DisplayDialogue()
    {       
        // quits if there is nothing to display
        if (currentNode == null) return;

        wantsToAdvance = false;
        if (currentNode.choices == null || currentNode.choices.Count < 1)
        {
            // shows current speaker's name and/or new sprites if this info should be updated
            if (currentNode.updateSpeakerInfo)
            {
                speakerTMP.text = currentNode.speakerName;
                if (currentNode.leftSprite == null)
                {
                    speakerLeft.enabled = false;
                }
                else
                {
                    speakerLeft.sprite = currentNode.leftSprite;
                    speakerLeft.enabled = true;
                }
                if (currentNode.rightSprite == null)
                {
                    speakerRight.enabled = false;
                }
                else
                {
                    speakerRight.sprite = currentNode.rightSprite;
                    speakerRight.enabled = true;
                }
            }

            // setup for showing current node's text character by character
            dialogueTMP.text = "";
            scrollTextRemaining.Clear();
            int len = currentNode.text.Length;

            // fills queue with characters that will be procedurally displayed
            for (int i = 0; i < len; i++)
            {
                scrollTextRemaining.Enqueue(currentNode.text[i]);
            }
            textIsScrolling = true;
        }
        else
        {
            // shows the current node's choices, selecting the first choice            
            int len = currentNode.choices.Count;
            for (int i = 0; i < len; i++)
            {
                choiceTMPs[i].text = currentNode.choices[i].text;
                choiceBoxes[i].SetActive(true);                
            }
            RebuildButtonNavigation();
            EventSystem.current.SetSelectedGameObject(choiceBoxes[0]);
            previousSelectedChoiceBox = choiceBoxes[0];
            choicesShowing = true;
        }
    }

    // sets dialogue to advance on next update
    public void Advance()
    {
        wantsToAdvance = true;
    }

    // advances based on option chosen (index is 0 for bottom button, 1 for next up, etc.)
    public void Choose(int index)
    {
        // unnecessary check since this is only called by button press if implemented correctly
        /*
        // exit and log error message if invalid index given
        if (index < 0 || !choicesShowing || index >= currentNode.choices.Count)
        {
            Debug.Log("invalid index given for choice, or no choices exist");
            return;
        }
        */

        chosen = currentNode.choices[index];
    }

    /// <summary>
    /// applies an outcome from dialogue to affect game states
    /// </summary>
    /// <param name="outcome">outcome data object</param>
    private void ApplyOutcome(DialogueOutcome outcome)
    {
        // make sure GameStateManager exists
        if (GameStateManager.Instance == null)
        {
            Debug.Log("No GameStateManager in scene");
            return;
        }

        if (QuestManager.Instance == null)
        {
            Debug.Log("No QuestManager in scene");
            return;
        }

        // set flags
        if (outcome.flagsToSet != null)
        {
            for (int i = 0; i < outcome.flagsToSet.Count; i++)
            {
                GameStateManager.Instance.SetFlag(outcome.flagsToSet[i]);
            }
        }

        // change stats
        if (outcome.statChanges != null)
        {
            GameStateManager.Instance.ChangeStats(outcome.statChanges);
        }

        // complete quest
        if(outcome.QuestsID != null)
        {
            ((IQuestCompleter)outcome).OnQuestComplete();
        }
    }

    /// <summary>
    /// Rebuilds navigation between buttons to include only active buttons
    /// </summary>
    private void RebuildButtonNavigation()
    {
        // get array of active buttons
        Button[] buttons = new Button[choiceBoxes.Count];
        for (int i = 0; i < buttons.Length; i++) buttons[i] = choiceBoxes[i].GetComponent<Button>();
        Button[] activeButtons = buttons.Where(b => b.gameObject.activeSelf).ToArray();

        // rebuild navigation
        for (int i = 0; i < activeButtons.Length; i++)
        {
            Navigation nav = activeButtons[i].navigation;
            nav.mode = Navigation.Mode.Explicit;
            nav.selectOnDown = i > 0 ? activeButtons[i - 1] : null;
            nav.selectOnUp = i < activeButtons.Length - 1 ? activeButtons[i + 1] : null;
            activeButtons[i].navigation = nav;
        }
    }

    /// <summary>
    /// Hides dialogue box, runs any given events, and hides dialogue UI
    /// </summary>
    private void CloseDialogue()
    {
        // close dialogue and switch input controls back to player
        DialogueIsHappening = false;
        dialoguePanel.SetActive(false);
        dialogueTMP.text = "";
        player.SwitchActionMaps(false);

        // play any events that were applied
        if (OnDialogueEnd != null)
        {
            OnDialogueEnd.Invoke();
            OnDialogueEnd = null;
        }
    }
}
