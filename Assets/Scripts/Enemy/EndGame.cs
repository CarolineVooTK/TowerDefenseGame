﻿using UnityEngine;
using UnityEngine.Events;

public class EndGame : MonoBehaviour
{
    private readonly string tagToDamage = "Enemy";
    [SerializeField] private UnityEvent OnEndGame;

    // Check if collide with enemy then attack
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag(this.tagToDamage))
        {
            Time.timeScale = 0f;
            OnEndGame.Invoke();
        }
    }
}
