using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int tokenBank;

    // Add token to bank
    public static void AddToken(int token)
    {
        tokenBank += token;
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
    }
}
