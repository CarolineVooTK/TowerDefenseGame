using UnityEngine;

// Class to turn off all notifications
public class NotificationTransition : MonoBehaviour
{
    [SerializeField] private RectTransform[] panels;

    // Only set first panel as active
    public void TurnOffNotif()
    {
        // Deactivate all panels
        foreach (var panel in panels)
            panel.gameObject.SetActive(false);

    }
}
