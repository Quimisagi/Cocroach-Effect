using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private Dialogue _dialogue;
    private bool _isRunning;
    private int _dialogueIndex = 0;
    [SerializeField] private TextMeshProUGUI _GUItext;
    [SerializeField] private TextMeshProUGUI _speakerName;
    [SerializeField] private float _characterGapTime = 0.2f;
    private bool _questionMode;
    [SerializeField] private GameObject _answerButtonPrefab;
    [SerializeField] private GameObject _answerButtonsLayout;

    public void Start()
    {
        var text = _dialogue.sentences[0].text;
        StartCoroutine(DisplayText(text));
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_isRunning)
            {
                _GUItext.text = _dialogue.sentences[_dialogueIndex].text;
                _isRunning = false;
            }
            else if (!_isRunning && (_dialogueIndex < _dialogue.sentences.Length - 1) && !_questionMode)
            {
                if(_dialogue.nextDialogue != null && _dialogueIndex == _dialogue.sentences.Length - 2)
                {
                    _dialogue = _dialogue.nextDialogue;
                    _dialogueIndex = 0;
                    _questionMode = _dialogue.sentences[0].isQuestion;
                    var text = _dialogue.sentences[0].text;
                    ClearAnswers();
                    StartCoroutine(DisplayText(text));
                }
                else {
                    _dialogueIndex++;
                    var sentence = _dialogue.sentences[_dialogueIndex];
                    if (sentence.isQuestion)
                        this._questionMode = true;
                    StartCoroutine(DisplayText(sentence.text));
                }
            }
            else if (!_isRunning && !_questionMode)
            {
                _GUItext.text = "";
            }
        }
    }
    IEnumerator DisplayText(string text)
    {
        _GUItext.text = "";
        _speakerName.text = _dialogue.sentences[_dialogueIndex].speaker;
        _isRunning = true;
        for (int i = 0; i < text.Length; i++)
        {
            if (!_isRunning)
                break;

            _GUItext.text += text[i];
            yield return new WaitForSeconds(_characterGapTime);
        }
        if (_questionMode)
        {
            this.DisplayAnswers(_dialogue.sentences[_dialogueIndex]);
        }
        _isRunning = false;
    }
    private void DisplayAnswers(Sentence question)
    {
        for (int i = 0; i < question.options.Length; i++)
        {
            var go = Instantiate(_answerButtonPrefab);
            var answerButton = go.GetComponent<AnswerButton>();
            answerButton.DialogueSystem = this;
            answerButton.Option = question.options[i];
            go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = question.options[i].answer;
            go.transform.parent = _answerButtonsLayout.transform;
        }
    }

    public void SelectAnswer(Option option)
    {
        _dialogue = option.nextDialogue;
        _dialogueIndex = 0;
        _questionMode = false;
        var text = _dialogue.sentences[0].text;
        ClearAnswers();
        StartCoroutine(DisplayText(text));
    }

    private void ClearAnswers()
    {
        foreach(Transform child in _answerButtonsLayout.transform)
        {
            Destroy(child.gameObject);
        }
    }

}
