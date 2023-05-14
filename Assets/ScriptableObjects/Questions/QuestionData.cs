using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.Questions
{
    [CreateAssetMenu(fileName = "QuestionData", menuName = "Quiz/Question Data")]
    public class QuestionData : ScriptableObject
    {
        public bool random;
        public List<Question> questions;
    }
    
    [Serializable]
    public class Question
    {
        public string question;
        public Answer[] answers = new Answer[4];

    }

    [Serializable]
    public struct Answer
    {
        public bool correct;
        public string answer;
    }
}