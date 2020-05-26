using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using System;

public class QuestionManager : MonoBehaviour
{

    private GameManager gm;

    private int maxQuestions = 10;

    private List<QuizzeResponse> _quizzeResponses;

    private QuizzesManager _quizzesManager;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        _quizzesManager = GetComponentInChildren<QuizzesManager>();

        //GetQuestionsFromServerUrl();
        GetQuestionsFromResources();
    }

    private void GetAssetBundleFromServerUrl()
    {
        Server.Instance.DownloadAssetBundleAsync("", onRequestCompleted, onRequestFaile);
    }

    private void onRequestFaile(int arg1, string arg2)
    {
        
    }

    private void onRequestCompleted(AssetBundle bundle)
    {
        GameObject engineObj = bundle.LoadAsset<GameObject>("EngineAssetBundle");
        AnimationClip anim = bundle.LoadAsset<AnimationClip>("EngineAssetBundle");

        Debug.Log("AssetBundle " + anim.name);

        Instantiate(engineObj);
    }

    /// <summary>
    /// TODO: Should get all the questions from the server
    /// </summary>
    private void GetQuestionsFromServerUrl()
    {
        Server.Instance.DoRequest("/getQuestions", null, OnQuizzesLoaded, OnQuizzesLoadFailed);
    }

    private void GetQuestionsFromResources()
    {
      StartCoroutine(Server.Instance.DoJsonUpLoadFromResourcesAsync("Examples", OnJsonUpLoadCompleted));
    }

    private void OnJsonUpLoadCompleted(string obj)
    {
        _quizzeResponses = JsonConvert.DeserializeObject<List<QuizzeResponse>>(obj);
        _quizzesManager.InitQuizzesManager(_quizzeResponses);
    }

    private void OnQuizzesLoaded(string obj)
    {
        try
        {
            _quizzeResponses = JsonConvert.DeserializeObject<List<QuizzeResponse>>(obj);
            _quizzesManager.InitQuizzesManager(_quizzeResponses);
        }
        catch (Exception ex)
        {
            Debug.LogError("Can't be loaded the Quizzes");
        }
    }

    private void OnQuizzesLoadFailed(int arg1, string arg2)
    {
        Debug.Log("Error " + arg1 + " " + arg2);
    }
}
