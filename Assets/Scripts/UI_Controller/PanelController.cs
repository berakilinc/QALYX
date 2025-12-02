using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    public GameObject settingsPanel;
    private bool isSettingsPanelOpen;

    public AudioSource audioSourceESC;
    public AudioClip audioClipESC;

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            PanelOpen();
            audioSourceESC.PlayOneShot(audioClipESC);
        }
    }

    void PanelOpen()
    {
        isSettingsPanelOpen = !isSettingsPanelOpen;
        settingsPanel.SetActive(isSettingsPanelOpen);
    }

    public void PanelClose()
    {
        isSettingsPanelOpen = false;
        settingsPanel.SetActive(false);
    }
}
