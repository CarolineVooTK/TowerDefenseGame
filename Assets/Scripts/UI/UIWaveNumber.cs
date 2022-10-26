using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Class to update wave number as the wave adds
public class UIWaveNumber : MonoBehaviour
{
    [SerializeField] private Text textDisplay;
    [SerializeField] private Text textEndDisplay;
    [SerializeField] private string prefix;
    [SerializeField] private string prefixEnd;

    // At the start, start coroutine to update values
    private void Awake()
    {
        StartCoroutine(Animate());
    }

    // Iterate function on a certain second
    private IEnumerator Animate()
    {
        while (true)
        {
            // Constantly update text
            int current = GameManager.waveNum + 1;
            UpdateText(current);
            yield return new WaitForSeconds(1f);
        }
    }

    // Update text to the new value
    private void UpdateText(int displayValue)
    {
        textDisplay.text = this.prefix + displayValue;
        textEndDisplay.text = this.prefixEnd + displayValue;
    }
}
