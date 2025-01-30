using TMPro;
using UnityEngine;

public class TimerPanel : MonoBehaviour
{
    
    [SerializeField] private TimerController timerController;
    [SerializeField] private TextMeshProUGUI timerText;

    // Update is called once per frame
    void Update()
    {
        timerText.text = Mathf.FloorToInt(timerController.timer / 60).ToString("00") + ":" + Mathf.FloorToInt(timerController.timer % 60).ToString("00");
    }
}
