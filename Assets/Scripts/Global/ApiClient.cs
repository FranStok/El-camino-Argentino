using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public static class ApiClient
{
    private static readonly string baseUrl = "http://127.0.0.1:8000/";

    public static IEnumerator Get(string endpoint,  System.Action<string> callback, System.Action<string> errorCallBack,Dictionary<string,string> parameters=null)
    {
        String requestUrl = baseUrl + endpoint;

        if (parameters != null)
        {
            List<string> parametersString = new List<string>();
            foreach (var (key, value) in parameters)
            {
                parametersString.Add(key + "=" + value);
            }

            requestUrl = requestUrl + "?" + string.Join("&",parametersString);
        }
        using UnityWebRequest request = UnityWebRequest.Get(requestUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
            errorCallBack.Invoke(request.error);
        else
            callback?.Invoke(request.downloadHandler.text);
    }

    public static IEnumerator Post(string endpoint, System.Action<string> callback, System.Action<string> errorCallBack,Dictionary<string,string> parameters=null,Byte[] body=null)
    {
        String requestUrl = baseUrl + endpoint;
        
        if (parameters != null)
        {
            List<string> parametersString = new List<string>();
            foreach (var (key, value) in parameters)
            {
                parametersString.Add(key + "=" + value);
            }

            requestUrl = requestUrl + "?" + string.Join("&",parametersString);

        }

        

        using UnityWebRequest request = new UnityWebRequest(requestUrl, "POST");
        if (body != null) request.uploadHandler = new UploadHandlerRaw(body);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
            errorCallBack.Invoke(request.error);
        else
            callback?.Invoke(request.downloadHandler.text);
    }
}

