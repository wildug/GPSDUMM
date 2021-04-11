using UnityEngine;
using System.Collections;

public class TestLocationService : MonoBehaviour
{
    GPSPrinter pr;
    void Start()
    {
        pr = this.GetComponent<GPSPrinter>();
        StartCoroutine(LocationCoroutine());
    }
    IEnumerator LocationCoroutine()
    {
        pr.AddMessage("BAzongus", 3);
        // Uncomment if you want to test with Unity Remote
        /*#if UNITY_EDITOR
                yield return new WaitWhile(() => !UnityEditor.EditorApplication.isRemoteConnected);
                yield return new WaitForSecondsRealtime(5f);
        #endif*/
#if UNITY_EDITOR
        // No permission handling needed in Editor
#elif UNITY_ANDROID
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.CoarseLocation)) {
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.CoarseLocation);
        }

        // First, check if user has location service enabled
        if (!UnityEngine.Input.location.isEnabledByUser) {
            // TODO Failure
            pr.AddMessage("Android and Location not enabled", 5);
            yield break;
        }

#elif UNITY_IOs
        if (!UnityEngine.Input.location.isEnabledByUser) {
            // TODO Failure
            pr.AddMessage("IOS and Location not enabled", 5);
            yield break;
        }
#endif
        // Start service before querying location
        UnityEngine.Input.location.Start(500f, 500f);

        // Wait until service initializes
        int maxWait = 15;
        while (UnityEngine.Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            maxWait--;
        }

        // Editor has a bug which doesn't set the service status to Initializing. So extra wait in Editor.
#if UNITY_EDITOR
        int editorMaxWait = 15;
        while (UnityEngine.Input.location.status == LocationServiceStatus.Stopped && editorMaxWait > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            editorMaxWait--;
        }
#endif

        // Service didn't initialize in 15 seconds
        if (maxWait < 1)
        {
            // TODO Failure
            pr.AddMessage("Timed out", 5);
            yield break;
        }

        // Connection has failed
        if (UnityEngine.Input.location.status != LocationServiceStatus.Running)
        {
            // TODO Failure
            pr.AddMessage("Unable to determine device location. Failed with status , UnityEngine.Input.location.status}", 5);
            yield break;
        }
        else
        {
            pr.AddMessage($"Location service live. status {UnityEngine.Input.location.status}", 5);
            // Access granted and location value could be retrieved
            pr.AddMessage("Location: "
                + UnityEngine.Input.location.lastData.latitude + " "
                + UnityEngine.Input.location.lastData.longitude + " "
                + UnityEngine.Input.location.lastData.altitude + " "
                + UnityEngine.Input.location.lastData.horizontalAccuracy + " "
                + UnityEngine.Input.location.lastData.timestamp, 5);

            var _latitude = UnityEngine.Input.location.lastData.latitude;
            var _longitude = UnityEngine.Input.location.lastData.longitude;
            // TODO success do something with location
        }

        // Stop service if there is no need to query location updates continuously
        UnityEngine.Input.location.Stop();
    }
}