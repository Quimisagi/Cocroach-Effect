using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerButton : MonoBehaviour
{
    public Option Option { get; set; }
    public DialogueSystem DialogueSystem {get; set;}

    public void SelectAnswer()
    {
        this.DialogueSystem.SelectAnswer(this.Option);
    }
}
