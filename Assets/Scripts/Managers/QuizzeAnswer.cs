using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizzeAnswer : MonoBehaviour
{
    public List<QuestionAnswer> questionAnswers;

    public GameObject answerPrefeb;
    public QuizzesManager _quizzesManager;

    public void InstanceAnswer(Answer answer)
    {
        var button = Instantiate(answerPrefeb, this.transform);

        var answerButton = button.GetComponent<QuestionAnswer>();

        answerButton.InjectQuizzeAnswer(this);
        answerButton.InitAnswer(answer);
    }

    public void ShowAnswers(List<Answer> answers)
    {
        foreach (var answer in answers)
        {
            InstanceAnswer(answer);
        }
    }

    public void OnQuestionAnswerCorrect(Answer answer)
    {
        _quizzesManager.OnQuestionAnswerCorrect();
    }

    public void Inject(QuizzesManager quizzesManager)
    {
        _quizzesManager = quizzesManager;
    }
}
