using System.Collections.Generic;
using UnityEngine;

public class DiceContestant : MonoBehaviour
{
    // fields
    [SerializeField] private List<Dice> dice;
    private int rolls;

    // properties
    /*
    /// <summary>
    /// whether or not this player can currently do anything 
    /// </summary>
    public bool Ready 
    {
        get
        {
            // unready if this player has no dice
            if (dice == null || dice.Count == 0) return false;

            // unready if dice are rolling
            for (int i = 0; i < dice.Count; i++)
            {
                if (dice[i].IsRolling) return false;
            }

            return true;
        }
    }
    */

    /// <summary>
    /// gets how many times this player rolled their dice
    /// </summary>
    /// <returns>number of rolls used by this player</returns>
    public int GetRollsUsed()
    {
        return rolls;
    }

    /// <summary>
    /// gets the sum of this player's dice's values
    /// </summary>
    /// <returns>sum of this player's dice's values</returns>
    public int GetTotalDiceValue()
    {
        if (dice == null || dice.Count == 0) return -1;
        int sum = 0;
        for (int i = 0; i < dice.Count; i++)
        {
            sum += dice[i].Value;
        }
        return sum;
    }

    /// <summary>
    /// gets this player's dice's values
    /// </summary>
    /// <returns>this player's dice's values as an array</returns>
    public int[] GetDiceValues()
    {
        if (dice == null || dice.Count == 0) return null;
        int size = dice.Count;
        int[] results = new int[size];
        for (int i = 0; i < size; i++)
        {
            results[i] = dice[i].Value;
        }
        return results;
    }

    /// <summary>
    /// rolls all of this player's dice that are not locked or selected
    /// </summary>
    public void RollAll()
    {
        if (dice == null || dice.Count == 0) return;
        for (int i = 0; i < dice.Count; i++)
        {
            if (dice[i].Selected) dice[i].Locked = true;    // lock selected dice
            else if (!dice[i].Locked) dice[i].Roll();       // roll unlocked dice
        }
        rolls++;
    }

    /// <summary>
    /// resets this player's states and dice
    /// </summary>
    public void Reset()
    {
        if (dice == null || dice.Count == 0) return;
        for (int i = 0; i < dice.Count; i++)
        {
            dice[i].Reset();
        }
        rolls = 0;
    }
}
