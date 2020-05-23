using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Server : Singleton<Server>
{
    private const string SERVER_URL = "http://8e140f9d.ngrok.io";
   
    public void DoRequest(string handler, IDictionary<string, object> bodyObject, Action<string> onRequestSuccess, Action<int, string> onRequestFailed)
    {
        string url = SERVER_URL + handler;
        StartCoroutine(DoFileUploadRequest(url, bodyObject, onRequestSuccess, onRequestFailed));
    }
    private IEnumerator DoFileUploadRequest(string requestURL, IDictionary<string, object> bodyObject, Action<string> onRequestSuccess, Action<int, string> onRequestFailed)
    {
        //  Inicializamos la forma para los parametros del json
        WWWForm form = new WWWForm();

        if (bodyObject != null)
        {
            //  Asignamos cada parametro del objeto bodyObject para crear el equivalente al json que recibe el servidor
            foreach (var key in bodyObject.Keys)
            {
                form.AddField(key, bodyObject[key].ToString());
            }
        }
        Debug.Log(requestURL);

        //  Creamos la peticion de post
        UnityWebRequest www = UnityWebRequest.Post(requestURL, form);


        //  Esperamos a que termine
        yield return www.SendWebRequest();

        //  Comprobamos si es error o acierto para ejecutar los callback
        if (www.isNetworkError || www.isHttpError)  //  Si hay error
        {
            if (onRequestFailed != null) onRequestFailed.Invoke(Convert.ToInt32(www.responseCode), www.downloadHandler.text);
        }
        else  //    Si no hay error
        {
            if (onRequestSuccess != null) onRequestSuccess.Invoke(www.downloadHandler.text);
        }
    }

    public string DoJsonUpLoadFromResources(string fileName)
    {
        var jsonTextFile = Resources.Load<TextAsset>(fileName).text;

        return jsonTextFile;
    }

    public IEnumerator DoJsonUpLoadFromResourcesAsync(string fileName, Action<string> onRequestCompleted)
    {
        var resourceRequest = Resources.LoadAsync<TextAsset>(fileName);

        while (!resourceRequest.isDone)
        {
            //Work across multiple frames until resourceRequest.isDone
            //wait one frame (for rendering, etc.)
            yield return null;
        }

        //Simulate Delay
        yield return new WaitForSeconds(1);

        TextAsset textValue = resourceRequest.asset as TextAsset;

        onRequestCompleted(textValue.text);
    }
}
