using UnityEngine;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class PermissionsRationaleDialog : MonoBehaviour
{
    const int kDialogWidth = 3000;
    const int kDialogHeight = 1000;
    private bool windowOpen = true;

    void DoMyWindow(int windowID)
    {
        GUI.Label(new Rect(Screen.width / 3, Screen.height / 2 + 150, 1000, 30), "Please let me use the microphone.");
        GUI.Button(new Rect(Screen.width / 3, Screen.height / 2 + 150, 1000, 70), "No");
        if (GUI.Button(new Rect(Screen.width / 3, Screen.height / 2 + 200, 1000, 70), "Yes"))
        {
#if PLATFORM_ANDROID
            Permission.RequestUserPermission(Permission.FineLocation);
#endif
            windowOpen = false;
        }
    }

    void OnGUI()
    {
        if (windowOpen)
        {
            Rect rect = new Rect((Screen.width / 2) - (kDialogWidth / 2), (Screen.height / 2) - (kDialogHeight / 2), kDialogWidth, kDialogHeight);
            GUI.ModalWindow(0, rect, DoMyWindow, "Permissions Request Dialog");
        }
    }
}