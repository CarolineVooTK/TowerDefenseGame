using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIWaveNumber : MonoBehaviour
{
    [SerializeField] private Text textDisplay;
    [SerializeField] private Text textEndDisplay;
    [SerializeField] private string prefix;
    [SerializeField] private string prefixEnd;
    [SerializeField] private int lerpSpeed;
    private int _target;

    private void Awake()
    {
        StartCoroutine(Animate());
    }

    public void UpdateValue()
    {
        _target = GameManager.waveNum;
    }

    private IEnumerator Animate()
    {
        int current = _target = GameManager.waveNum;
        while (true)
        {
            //current = (int)Mathf.Lerp(current, this._target, this.lerpSpeed);
            current = GameManager.waveNum + 1;
            UpdateText(current);
            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdateText(int displayValue)
    {
        textDisplay.text = this.prefix + displayValue;
        textEndDisplay.text = this.prefixEnd + displayValue;
    }
}
