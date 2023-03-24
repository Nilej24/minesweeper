using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequestManager : MonoBehaviour
{

    public void PostNewDeck() {
        StartCoroutine(Test());
    }

    private IEnumerator Test() {

        var data = new DeckData() { name = "LMAO I MADE THIS FROM INSIDE A UNITY GAME" };
        var req = CreateRequest("https://thoughtscape-y2ub.onrender.com/api/decks", "POST", data);
        string token = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjYyYzZhNGUyM2Q0NDMzM2E4Y2MwZTVhNCIsImlhdCI6MTY3OTIxMTczOCwiZXhwIjoxNjgxODAzNzM4fQ.gBnlaVAj0j77RNHQRY0pvMUWIoWAfSw5mvHi_v9MChc";
        req.SetRequestHeader("Authorization", token);

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success) Debug.Log("app it worked");
        else Debug.Log(req.error);

    }

    private UnityWebRequest CreateRequest(string url, string method="GET", object data=null) {

        var request = new UnityWebRequest(url, method);

        if (data != null) {

            var dataByteArray = Encoding.UTF8.GetBytes(JsonUtility.ToJson(data));
            request.uploadHandler = new UploadHandlerRaw(dataByteArray);
            
        }

        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        return request;

    }

    public void PostNewDeck2() {
        StartCoroutine(Test2());
    }

    private IEnumerator Test2() {

        var form = new WWWForm();
        form.AddField("name", "wow this is way easier");

        var req = UnityWebRequest.Post("https://thoughtscape-y2ub.onrender.com/api/decks", form);
        string token = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjYyYzZhNGUyM2Q0NDMzM2E4Y2MwZTVhNCIsImlhdCI6MTY3OTIxMTczOCwiZXhwIjoxNjgxODAzNzM4fQ.gBnlaVAj0j77RNHQRY0pvMUWIoWAfSw5mvHi_v9MChc";
        req.SetRequestHeader("Authorization", token);

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success) Debug.Log("it works yet again");
        else Debug.Log(req.error);

    }

}

public class DeckData {
    public string name;
}
