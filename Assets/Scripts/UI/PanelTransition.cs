using System.Linq;
using UnityEngine;

// Class to set active child panels aside from the first onee
public class PanelTransition : MonoBehaviour
{
    [SerializeField] private RectTransform[] panels;
    private RectTransform _currentPanel;

    // Only set first panel as active
    private void Awake()
    {
        // Deactivate all panels
        foreach (var panel in panels)
            panel.gameObject.SetActive(false);

        // Set first to the active panel
        SwitchTo(panels.Length > 0 ? panels[0] : null);
        Time.timeScale = 0f;
    }

    // Switch to other panel
    public void SwitchTo(RectTransform panel)
    {
        if (!panels.Contains(panel))
        {
            Debug.LogWarning("Cannot switch to untracked panel.");
            return;
        }

        if (_currentPanel)
            _currentPanel.gameObject.SetActive(false);
        _currentPanel = panel;
        _currentPanel.gameObject.SetActive(true);
    }
}
