using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
public class TMP_Typewriter_Behavior : MonoBehaviour
{


    public string fullstring;
    public bool trigger_on_start = true;
    public float type_speed;
    public float finish_delay;
    public UnityEvent endEvent;
    private string fullstringRef;
    private string currentstring;
    private TMP_Text text;
    private WaitForSeconds typeWfs, endWfs;
    
    /// <summary>
    /// Simple Typewriter script.
    /// Takes whatever TMP text is on the object, then stores it to be retyped when it is activated
    /// V2 Update - Take text inside TMP text, cut and paste into the fullstring box.
    /// otherwise it flashes on screen for a second, ruining the effect
    /// Set Type speed to a low number, ex
    /// 0.1 adds a letter with a 0.1 second delay between every letter.
    ///  Works best with alignment : Justified if you want the text to stay still.
    /// </summary>
    
    void Start()
    {
        typeWfs = new WaitForSeconds(type_speed);
        endWfs = new WaitForSeconds(finish_delay);
        text = GetComponent<TMP_Text>();
        if (fullstring == "")
        {
            fullstring = text.text;
            
        }
        fullstringRef = fullstring;
        if (trigger_on_start)
        {
            StartCoroutine(TypeOutText());
        }
        else
        {
            text.text = "";
            //Hide text if the text isnt supposed to be activated until later for some reason.
        }
    }

    public void TriggerTyping()
    {
        StartCoroutine(TypeOutText());
    }

    IEnumerator TypeOutText()
    {
        yield return typeWfs;
        if (currentstring != fullstring)
        {
            addLetter();
        }
        else
        {
            yield return endWfs;
            endEvent.Invoke();
        }
    }

    private void addLetter()
    {
        char firstLetter = fullstringRef[0];
        fullstringRef = fullstringRef.Substring(1);
        currentstring+= firstLetter;
        text.text = currentstring;
        StartCoroutine(TypeOutText());
    }
}
