
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(
    
    menuName = "DataTypes/Float Data",
    order = 2
)]
public class FloatData : ScriptableObject
{
    public float value;
    public float defaultValue;

    public bool minimum_value_clamp;
    public float minimum_value;
    public bool maximum_value_clamp;
    public float maximum_value;
    public void IncreaseValue(float number){
        if (value + number > maximum_value && maximum_value_clamp == true)
        {
            value = maximum_value;
        }
        else
        {
            value += number;        

        }
    }
    public void DecreaseValue(float number){
        if (value - number < minimum_value && minimum_value_clamp == true)
        {
            value = minimum_value;
        }
        else
        {
            value -= number;

        }
    }
    public void SetValue(float number){
        if (value + number > maximum_value && maximum_value_clamp == true)
        {
            return;
        }
        if (value - number < minimum_value && minimum_value_clamp == true)
        {
            return;
        }

        if (maximum_value_clamp == false && minimum_value_clamp == false)
        {
            value = number;    
        }
    }
    public void MultiplyValue(float number){
        if (value * number < minimum_value && minimum_value_clamp == true)
        {
            value = minimum_value;
        }

        if (value * number > maximum_value && maximum_value_clamp == true)
        {
            value = maximum_value;
        }

        if (minimum_value_clamp == false && maximum_value_clamp == false)
        {
            value *= number; 
        }
    }

    public void DivideValue(float number)
    {
        if (number == 0)
        {
            return;
        }

        if (value / number < minimum_value && minimum_value_clamp == true)
        {
            value = minimum_value;
        }

        if (value / number > maximum_value && maximum_value_clamp == true)
        {
            value = maximum_value;
        }

        if (minimum_value_clamp == false && maximum_value_clamp == false)
        {
            value /= number;

        }
        
    }

    public void DisplayNumber(Text text)
    {
        text.text = value.ToString(CultureInfo.InvariantCulture);//todo - Look up what inveriantculture does
    }

    public void DisplayTMPText(TMP_Text text)
    {
        text.text = value.ToString(CultureInfo.InvariantCulture);
    }

    public void ResetToDefault()
    {
        value = defaultValue;
    }

    public void DisplayAsPercent(Image percentBar)
    {
        if (percentBar != null)
        {
            if (value <0)
            {
                value = 0;
            }

            if (value > 1)
            {
                value = 1;
            }
            percentBar.fillAmount = value;    
        }
        
    }
}
