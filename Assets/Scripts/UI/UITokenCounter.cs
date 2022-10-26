using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Class to update UI of token  
public class UITokenCounter : MonoBehaviour
{
    [SerializeField] private Text textDisplay; 
    [SerializeField] private string prefix;

    // At the start, start coroutine to update values
    private void Start()
    {
        StartCoroutine(Animate());
    }

    // Iterate function on a certain second
    private IEnumerator Animate()
    {
        while (true)
        {
            // Constantly update text
            int current = GameManager.tokenBank;
            UpdateText(Mathf.RoundToInt(current));
            yield return new WaitForSeconds(0.05f);
        }
    }

    // Update text to the new value
    private void UpdateText(int displayValue)
    {
        textDisplay.text = prefix + displayValue;
    }
}
