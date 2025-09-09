using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public static class ApiClient
{
    private static readonly string baseUrl = "http://127.0.0.1:8000/";

    public static IEnumerator Get(string endpoint, System.Action<string> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(baseUrl + endpoint))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
                Debug.LogError("Error GET: " + request.error);
            else
                callback?.Invoke(request.downloadHandler.text);
        }
    }

    public static IEnumerator Post(string endpoint, string json, System.Action<string> callback)
    {
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        using (UnityWebRequest request = new UnityWebRequest(baseUrl + endpoint, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
                Debug.LogError("Error POST: " + request.error);
            else
                callback?.Invoke(request.downloadHandler.text);
        }
    }
}

