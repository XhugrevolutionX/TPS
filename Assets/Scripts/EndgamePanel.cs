using TMPro;
using UnityEngine;

public class EndgamePanel : MonoBehaviour
{
    [SerializeField] private TimerController timerController;
    [SerializeField] private TargetManager targetManager;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI targetText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowEndGamePanel()
    {
        Time.timeScale = 0f;
        timerText.text = "In " +  Mathf.FloorToInt(timerController.timer / 60).ToString("00") + ":" + Mathf.FloorToInt((timerController.timer % 60 * 100) / 100).ToString("00") ;
        targetText.text = (targetManager.targetsTotal - targetManager.targetsLeft) + "/" + targetManager.targetsTotal + " Targets destroyed ";
        gameObject.SetActive(true);
    }
}
