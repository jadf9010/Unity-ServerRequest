using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizzesManager : MonoBehaviour
{
    private List<QuizzeResponse> _quizzeResponses;
    private List<QuizzeAnswered> quizzeAnswereds;

    private QuizzeResponse currentQuizze;

    private QuizzeQuestion _quizzeQuestion;
    private QuizzeAnswer _quizzeAnswer;

    private void Start()
    {
        quizzeAnswereds = new List<QuizzeAnswered>();
        _quizzeResponses = new List<QuizzeResponse>();

        _quizzeQuestion = GetComponentInChildren<QuizzeQuestion>();
        _quizzeAnswer = GetComponentInChildren<QuizzeAnswer>();
    }

    public void InitQuizzesManager(List<QuizzeResponse> quizzeResponses)
    {
        _quizzeResponses = quizzeResponses;

        InjectDependencies();

        FindQuizzeToShow();
    }

    public void OnQuestionAnswerCorrect()
    {
        var quizzeAnswered = new QuizzeAnswered();
        quizzeAnswered.AddQuizzeCompleted(currentQuizze, true);

        quizzeAnswereds.Add(quizzeAnswered);

        FindQuizzeToShow();
    }

    private void InjectDependencies()
    {
        _quizzeQuestion.Inject(this);
        _quizzeAnswer.Inject(this);
    }

    public void FindQuizzeToShow()
    {
        bool found = false;
        foreach (var quizze in _quizzeResponses)
        {
            if (!quizzeAnswereds.Exists(q => String.Compare(q.quizzeCompleted.id, quizze.id) == 0 ))
            {
                currentQuizze = quizze;
                found = true;

                ShowQuizze(currentQuizze);

                break;
            }
        }

        if (!found)
        {
            //Game Finished. All questions were answered
        }
    }

    private void ShowQuizze(QuizzeResponse currentQuizze)
    {
        _quizzeQuestion.ShowQuestion(currentQuizze.question);
        _quizzeAnswer.ShowAnswers(currentQuizze.answers);
    }
}
