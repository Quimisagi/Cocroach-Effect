using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private Dialogue _dialogue;
    private bool _isRunning;
    private int _dialogueIndex = 0;
    [SerializeField] private TextMeshProUGUI _GUItext;
    [SerializeField] private TextMeshProUGUI _speakerName;
    private float _characterGapTime = 0.025f;
    private bool _questionMode;
    [SerializeField] private GameObject _answerButtonPrefab;
    [SerializeField] private GameObject _answerButtonsLayout;
    [SerializeField] private Animator _calixtorAnimator;

    [SerializeField] private AudioSource _click;
    [SerializeField] private AudioClip _calixto;
    [SerializeField] private AudioClip _bartender;


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
                _calixtorAnimator.SetBool("isSpeaking", false);
            }
            else if (!_isRunning && (_dialogueIndex < _dialogue.sentences.Length - 1) && !_questionMode)
            {
                _click.Play();
                if (_dialogue.nextDialogue != null && _dialogueIndex == _dialogue.sentences.Length - 2)
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
                if (_dialogue.finalDialogue)
                {
                    SceneManager.LoadScene("Juego");
                }
                if (_dialogue.finalFinalDialogue)
                {
                    SceneManager.LoadScene("Juego");
                }
            }
        }
    }
    IEnumerator DisplayText(string text)
    {
        _GUItext.text = "";
        _speakerName.text = _dialogue.sentences[_dialogueIndex].speaker;
        _isRunning = true;
        if(_dialogue.sentences[_dialogueIndex].speaker == "Calixto" || _dialogue.sentences[_dialogueIndex].speaker == "???")
        {
            this.gameObject.GetComponent<AudioSource>().clip = _calixto;
            this.gameObject.GetComponent<AudioSource>().Play();
            this.gameObject.GetComponent<AudioSource>().loop = true;
            _calixtorAnimator.SetBool("isSpeaking", true);
        }
        else
        {
            this.gameObject.GetComponent<AudioSource>().clip = _bartender;
            this.gameObject.GetComponent<AudioSource>().Play();
            this.gameObject.GetComponent<AudioSource>().loop = true;
        }
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
        _calixtorAnimator.SetBool("isSpeaking", false);
        this.gameObject.GetComponent<AudioSource>().loop = false;
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
            go.transform.SetParent(_answerButtonsLayout.transform, false);
        }
    }

    public void SelectAnswer(Option option)
    {
        _click.Play();
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
