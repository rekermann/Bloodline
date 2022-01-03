using TMPro;
using UnityEngine;

namespace UiUtilities
{
    public class BarUiScaler : MonoBehaviour
    {
        public TextMeshProUGUI barText;
        public Transform bar;
        
        public void UpdateBarXScale(int current, int maxValue) 
        {
            if(barText != null)
                barText.text = current + "/" + maxValue;
            if (current > maxValue) current = maxValue;
            bar.localScale = new Vector3((float) current / maxValue, 1, 1);
        }
        
        public void UpdateBarYScale(int current, int maxValue) 
        {
            if(barText != null)
                barText.text = current + "/" + maxValue;
            if (current > maxValue) current = maxValue;
            bar.localScale = new Vector3(1, (float) current / maxValue, 1);
        }
    }
}
