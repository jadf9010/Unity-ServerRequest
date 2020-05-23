using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizzeAnswered
{
    public QuizzeResponse quizzeCompleted;
    public bool _correct;

    public void AddQuizzeCompleted(QuizzeResponse quizzeResponse, bool correct)
    {
        quizzeCompleted = quizzeResponse;   
        _correct = correct;
    }
}
