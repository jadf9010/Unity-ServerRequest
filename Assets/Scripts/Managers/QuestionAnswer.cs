using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionAnswer : MonoBehaviour
{
    public Answer answer;
    public Button button;

    public QuizzeAnswer _quizzeAnswer;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void InjectQuizzeAnswer(QuizzeAnswer quizzeAnswer)
    {
        _quizzeAnswer = quizzeAnswer;
    }

    private void OnClick()
    {
        if (answer.correct)
        {
            _quizzeAnswer.OnQuestionAnswerCorrect(answer);
        }
    }

    public void InitAnswer(Answer answer)
    {
        this.answer = answer;
        GetComponentInChildren<Text>().text = answer.text;
    }
}
