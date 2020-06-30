using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    public IEnumerator DownloadFromGenericResourcesAsync<T>(string fileName, Action<ResourceRequest> onRequestCompleted, Action<string> onRequestFailed) where T : UnityEngine.Object
    {
        var resourceRequest = Resources.LoadAsync<T>(fileName);

        yield return resourceRequest;

        //Simulate Delay
        yield return new WaitForSeconds(1);

        //We check if it's a mistake or a success to execute the callbacks
        if (resourceRequest == null)  //  If there is a mistake
        {
            onRequestFailed.Invoke("Resources Download Failed");
        }
        else  //if it's a success download
        {
            onRequestCompleted?.Invoke(resourceRequest);
        }

        //if(typeof(T) == typeof(TextAsset))
        //if(typeof(T) == typeof(TextAsset))
    }

    public IEnumerator DownloadAssetBundleAsync(string url, Action<AssetBundle> onRequestCompleted, Action<int, string> onRequestFailed)
    {
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(url);

        yield return request.SendWebRequest();

        var asyncOperation = request.SendWebRequest();
        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);

        //Simulate Delay
        yield return new WaitForSeconds(1);

        //  Comprobamos si es error o acierto para ejecutar los callback
        if (request.isNetworkError || request.isHttpError)  //  Si hay error
        {
            if (onRequestFailed != null)
                onRequestFailed.Invoke(Convert.ToInt32(request.responseCode), request.downloadHandler.text);
        }
        else  //    Si no hay error
        {
            if (onRequestCompleted != null)
                onRequestCompleted.Invoke(bundle);
        }
    }

    public IEnumerator DownloadFileAsync(Action<ulong> onRequestCompleted, Action<int, string> onRequestFailed)
    {
        string url = "http://dl3.webmfiles.org/big-buck-bunny_trailer.webm";

        string vidSavePath = Path.Combine(Application.persistentDataPath, "Videos");
        vidSavePath = Path.Combine(vidSavePath, "MyVideo.webm");

        //Create Directory if it does not exist
        if (!Directory.Exists(Path.GetDirectoryName(vidSavePath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(vidSavePath));
        }

        var uwr = new UnityWebRequest(url);
        uwr.method = UnityWebRequest.kHttpVerbGET;
        var dh = new DownloadHandlerFile(vidSavePath);
        dh.removeFileOnAbort = true;
        uwr.downloadHandler = dh;

        yield return uwr.SendWebRequest();

        //  Comprobamos si es error o acierto para ejecutar los callback
        if (uwr.isNetworkError || uwr.isHttpError)  //  Si hay error
        {
            if (onRequestFailed != null)
                onRequestFailed.Invoke(Convert.ToInt32(uwr.responseCode), uwr.downloadHandler.text);
        }
        else  //    Si no hay error
        {
            if (onRequestCompleted != null)
                onRequestCompleted.Invoke(uwr.downloadedBytes);
        }

        if (uwr.isNetworkError || uwr.isHttpError)
            Debug.Log(uwr.error);
        else
            Debug.Log("Download saved to: " + vidSavePath.Replace("/", "\\") + "\r\n" + uwr.error);
    }
}
