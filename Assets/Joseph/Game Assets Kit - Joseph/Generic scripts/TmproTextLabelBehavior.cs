using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


[RequireComponent(typeof(TMP_Text))]


public class TmproTextLabelBehavior : MonoBehaviour
{
    private TMP_Text label;
    public UnityEvent startEvent;
    
    private void Start()
    {
        label = GetComponent<TMP_Text>();
        startEvent.Invoke();
    }

    public void UpdateLabel(FloatData obj)
    {
        label.text = obj.value.ToString("F2", CultureInfo.InvariantCulture);

    }

    public void UpdateLabel(IntData obj)
    {
        label.text = obj.value.ToString(CultureInfo.InvariantCulture);
    }

    public void UpdateLabel(Vector3Data obj)
    {
        label.text = string.Format("({0},{1},{2})", obj.value.x.ToString("00.0"), obj.value.y.ToString("00.0"), obj.value.z.ToString("00.0"));
    }
}