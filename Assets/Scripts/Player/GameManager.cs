using UnityEngine;

// Class to manage static variables in game
public class GameManager : MonoBehaviour
{
    public static int totalEarned;
    public static int tokenBank;
    public static int waveNum;

    // Add token to bank
    public static void AddToken(int token)
    {
        tokenBank += token;
        totalEarned += token;
    }

    // Reduce token to bank
    public static void ReduceToken(int token)
    {
        tokenBank -= token;
    }

    // Reset the value of bank
    public static void ResetToken(int token)
    {
        tokenBank = token;
        totalEarned = token;
    }

    // Reset wave number
    public static void ResetWave()
    {
        waveNum = 0;
    }

    // Add to next wave
    public static void NextWave()
    {
        waveNum++;
    }
}
