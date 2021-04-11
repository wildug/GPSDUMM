using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GPSPrinter : MonoBehaviour
{
    public GUIStyle mystyle;
    public List<string> strs = new List<string>();
    LocationInfo locI = new LocationInfo();
    void Start()
    {
        this.GetComponent<TestLocationService>();
    }
    private void FixedUpdate()
    {
        if (Input.location.status == LocationServiceStatus.Running && locI.timestamp != Input.location.lastData.timestamp)
        {
            locI = Input.location.lastData;
            Debug.Log(Input.location.lastData);
            AddMessage($"Update: {locI.timestamp}", 5);
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 3, Screen.height / 2, 40, 40), $"L: {locI.latitude} | L: {locI.longitude}", mystyle);

        int i = 0;
        foreach (string item in strs)
        {
            i++;
            GUI.Label(new Rect(Screen.width / 3, Screen.height / 2 + i * 30, 1000, 30), item, mystyle);
        }
    }

    public void AddMessage(string m, float t)
    {
        strs.Add(m);
        StartCoroutine(WaitThenDeleteString(m, t));
    }

    IEnumerator WaitThenDeleteString(string m, float t)
    {
        yield return new WaitForSeconds(t);
        strs.Remove(m);
    }

}