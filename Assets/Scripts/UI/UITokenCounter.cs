﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITokenCounter : MonoBehaviour
{
    [SerializeField] private Text textDisplay; 
    [SerializeField] private string prefix;
    [SerializeField] private int lerpSpeed;
    private int _target;

    private void Awake()
    {
        StartCoroutine(Animate());
    }

    public void UpdateValue()
    {
        _target = GameManager.tokenBank;
    }

    private IEnumerator Animate()
    {
        int current = this._target = GameManager.tokenBank;
        while (true)
        {
            //current = (int)Mathf.Lerp(current, this._target, this.lerpSpeed);
            current = GameManager.tokenBank;
            print(current);
            UpdateText(Mathf.RoundToInt(current));

            yield return new WaitForSeconds(0.05f);
        }
    }

    private void UpdateText(int displayValue)
    {
        textDisplay.text = this.prefix + displayValue;
    }
}