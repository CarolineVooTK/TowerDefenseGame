using UnityEngine;

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

    public void ResetToken()
    {
        _token = startingToken;
        GameManager.ResetToken(_token);
    }
}
