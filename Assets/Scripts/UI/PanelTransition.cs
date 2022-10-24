using System.Collections;
using System.Linq;
using UnityEngine;

public class PanelTransition : MonoBehaviour
{
    [SerializeField] private RectTransform[] panels;

    private RectTransform _currentPanel;

    private void Awake()
    {
        // Ensure only one panel in switch group is active (first by default).
        // First deactivate all panels.
        foreach (var panel in this.panels)
            panel.gameObject.SetActive(false);

        // Then set first to the active panel (if applicable).
        SwitchTo(this.panels.Length > 0 ? this.panels[0] : null);
        Time.timeScale = 0f;
    }

    public void SwitchTo(RectTransform panel)
    {
        if (!this.panels.Contains(panel))
        {
            Debug.LogWarning("Cannot switch to untracked panel.");
            return;
        }

        if (this._currentPanel)
            this._currentPanel.gameObject.SetActive(false);
        this._currentPanel = panel;
        this._currentPanel.gameObject.SetActive(true);
    }
}
