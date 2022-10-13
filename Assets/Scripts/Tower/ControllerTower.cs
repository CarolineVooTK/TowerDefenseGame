﻿using UnityEngine;
using System.Collections;

public class ControllerTower : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f; // Default speed sensitivity
    [SerializeField] private GameObject projectilePrefab;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Translate(Vector3.left * (this.speed * Time.deltaTime));
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Translate(Vector3.right * (this.speed * Time.deltaTime));

        // Use the "down" variant to avoid spamming projectiles. Will only get
        // triggered on the frame where the key is initially pressed.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var projectile = Instantiate(this.projectilePrefab);
            projectile.transform.position = gameObject.transform.position + new Vector3(0f, 1.6f, 0f);
        }
    }
}
