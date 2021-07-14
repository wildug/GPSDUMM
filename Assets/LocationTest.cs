using UnityEngine;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class LocationTest : MonoBehaviour
{
    GameObject dialog = null;
    GPSPrinter pr;

    void Start()
    {
        pr = this.GetComponent<GPSPrinter>();
#if PLATFORM_ANDROID
        pr.AddMessage(Permission.HasUserAuthorizedPermission(Permission.Microphone).ToString(), 3);
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone) )
        {
            Permission.RequestUserPermission(Permission.Microphone);
            dialog = new GameObject();
        }
#endif
    }

    void OnGUI()
    {
#if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            // The user denied permission to use the microphone.
            // Display a message explaining why you need it with Yes/No buttons.
            // If the user says yes then present the request again
            // Display a dialog here.
            dialog.AddComponent<PermissionsRationaleDialog>();
            return;
        }
        else if (dialog != null)
        {
            Destroy(dialog);
        }
#endif

        // Now you can do things with the microphone
    }
}