using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizzeQuestion : MonoBehaviour
{
    public Text questionText;
    public QuizzesManager _quizzesManager;

    private void Start()
    {
        questionText = GetComponentInChildren<Text>();
    }

    public void ShowQuestion(string question)
    {
        questionText.text = question;
    }

    public void Inject(QuizzesManager quizzesManager)
    {
        _quizzesManager = quizzesManager;
    }
}
