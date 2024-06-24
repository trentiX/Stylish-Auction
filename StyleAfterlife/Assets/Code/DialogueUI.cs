using System.Collections;
using UnityEngine;
using TMPro;
using Image = UnityEngine.UI.Image;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    
    private TypewriterEffect typewriterEffect;
    
    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();
    }

    public void showDialogue(string label)
    {
        StartCoroutine(StepThroughDialogue(label));
    }

    private IEnumerator StepThroughDialogue(string label)
    {
        yield return RunTypingEffect(label);
        textLabel.text = label;
        yield return null;
        yield return new WaitForSeconds(4);
        textLabel.text = null;
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typewriterEffect.Run(dialogue, textLabel);

        while (typewriterEffect.IsRunning)
        {
            yield return null;
        }
    }
}