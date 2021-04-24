using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    private static DialogueManager _instance;

    public static DialogueManager Instance {
        get {
            if (_instance == null) {
                Debug.Log("here");
                // The double loop is to avoid calling find object when not needed
                _instance = FindObjectOfType<DialogueManager>();
                if (_instance == null) {
                    _instance = new DialogueManager();
                }
            }

            Debug.Log(_instance);
            return _instance;
        }
    }

    public Queue<string> sentences;
    void Start() {
        sentences = new Queue<string>();   
    }

    public void startDialogue(Dialogue dialogue) {
        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue() {
        return;
    }
}
