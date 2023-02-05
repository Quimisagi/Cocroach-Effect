using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue", order = 1)]
public class Dialogue : ScriptableObject
{
    public Sentence[] sentences;
    public Dialogue nextDialogue;
    public bool finalDialogue;

}
[System.Serializable]
public class Sentence
{
    public string text;
    public string speaker;
    public bool isQuestion;
    public Option[] options;
}

[System.Serializable]
public class Option
{
    public string answer;
    public Dialogue nextDialogue;
}
