using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField] Text dialogueText;
    [SerializeField] int lettersPerSecond;

    public event Action OnShowDialogue;
    public event Action OnCloseDialogue;

    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    Dialogue dialogue;
    Action onDialogueFinished;

    int currentLine = 0;
    bool isTyping;

    public bool IsShowing { get; private set; }

    public IEnumerator ShowDialogue(Dialogue dialogue, Action OnFinished=null)
    {
        yield return new WaitForEndOfFrame();

        OnShowDialogue?.Invoke();

        IsShowing = true;
        this.dialogue = dialogue;
        onDialogueFinished = OnFinished;

        dialogueBox.SetActive(true);
        StartCoroutine(TypeDialogue(dialogue.Lines[0]));
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isTyping)
        {
            currentLine++;
            if (currentLine < dialogue.Lines.Count)
            {
                StartCoroutine(TypeDialogue(dialogue.Lines[currentLine]));
            }
            else
            {
                currentLine = 0;
                IsShowing = false;
                dialogueBox.SetActive(false);
                onDialogueFinished?.Invoke();
                OnCloseDialogue?.Invoke();
            }
        }
    }


    public IEnumerator TypeDialogue(string line)
    {
        isTyping = true;

        dialogueText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }

        isTyping = false;
    }
}
