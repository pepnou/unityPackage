using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
[RequireComponent(typeof(Animator))]
public class TextManager : MonoBehaviour
{
    private TMP_Text text;
    private Animator animator;

    private Queue<string> queue;

    private bool routineState = false;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        animator = GetComponent<Animator>();

        queue = new Queue<string>();
    }

    public void AddText(params string[] newText)
    {
        for(int i = 0; i < newText.Length; i++)
        {
            queue.Enqueue(newText[i]);
        }

        if(routineState == false)
        {
            routineState = true;
            StartCoroutine(DisplayText());
        }
    }

    IEnumerator DisplayText()
    {
        while(queue.Count != 0)
        {
            text.text = queue.Dequeue();
            animator.SetTrigger("start");

            yield return new WaitForSeconds(4);
        }

        routineState = false;
    }
}
