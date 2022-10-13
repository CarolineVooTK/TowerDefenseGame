using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TokenCounter : MonoBehaviour
{
    private int _token;
    public int startingToken;

    [SerializeField] private Text textDisplay;
    [SerializeField] private string prefix;
    [SerializeField] private int lerpSpeed;

    // Reset values when start
    private void Start()
    {
        _token = startingToken;
        UpdateText();
    }

    public int Token
    {
        get => _token;
        set
        {
            _token = value;
        }
    }

    private void UpdateText()
    {
        textDisplay.text = this.prefix + Token;
    }
}
