using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuizzeResponse
{
    public string id;
    public string question;
    public bool isHard;
    public string imageURL;
    public List<Answer> answers;
}

[Serializable]
public class Answer
{
    public string text;
    public bool correct;
}