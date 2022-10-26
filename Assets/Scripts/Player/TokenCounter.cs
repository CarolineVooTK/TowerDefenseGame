using UnityEngine;

// Class as an initial value of token
public class TokenCounter : MonoBehaviour
{
    private int _token;
    public int startingToken;

    // Reset values when start new scene
    private void Start()
    {
        _token = startingToken;
        GameManager.ResetToken(_token);
    }

    // Reset values when called
    public void ResetToken()
    {
        _token = startingToken;
        GameManager.ResetToken(_token);
    }
}
