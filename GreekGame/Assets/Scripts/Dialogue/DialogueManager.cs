using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    private GameObject dialogueBox;
    [SerializeField]
    private float textDelay;    // seconds before showing the next char

    // dialogue choices
    [SerializeField]
    private List<TextMeshProUGUI> choiceTMPs;
    [SerializeField]
    private List<GameObject> choiceBoxes;
    private bool choicesShowing;
    private DialogueChoice chosen;

    // talking sprites
    [SerializeField]
    private Image speakerLeft;
    [SerializeField]
    private Image speakerRight;

    public bool DialogueIsHappening { get; private set; }

    // functions
    private void Awake()
    {
        // destroy duplicate instance if one of this singleton already exists
        if (Instance != null && Instance != this)
        {
            Debug.Log("Destroyed duplicate DialogueManager object");
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

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
                // close dialogue and switch input controls back to player
                DialogueIsHappening = false;
                dialogueBox.SetActive(false);
                dialogueTMP.text = "";
                player.SwitchActionMaps(false);
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
            // TODO: do something with choice's outcome object, e.g. log something in journal
            if (chosen.outcome != null)
            {

            }
            
            // hides choice boxes then displays next piece
            choicesShowing = false;
            int len = currentNode.choices.Count;
            for (int i = 0; i < len; i++)
            {
                choiceBoxes[i].SetActive(false);
                choiceTMPs[i].text = "";
            }
            currentNode = nodes[chosen.nextNodeID];
            chosen = null;
            DisplayDialogue();
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

        // displays dialogue in UI
        dialogueBox.SetActive(true);
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
            // shows the current node's choices
            int len = currentNode.choices.Count;
            for (int i = 0; i < len; i++)
            {
                choiceTMPs[i].text = currentNode.choices[i].text;
                choiceBoxes[i].SetActive(true);                
            }
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
}
