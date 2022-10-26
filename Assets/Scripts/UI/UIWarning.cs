using UnityEngine;
using System.Collections;

// Class to display notifications
public class UIWarning: MonoBehaviour
{
    public GameObject panel;

    // When it is called, do coroutine
    private void OnEnable()
    {
        StartCoroutine(Wait());
    }

    // Iterate function on a certain second
    private IEnumerator Wait()
    {
        // After 0.5 seconds, the panel will be off
        yield return new WaitForSeconds(0.5f);
        panel.SetActive(false);
    }
}
