using UnityEngine;
using System.Collections;

public class UIWarning: MonoBehaviour
{
    public GameObject panel;

    private void OnEnable()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        panel.SetActive(false);
    }
}
