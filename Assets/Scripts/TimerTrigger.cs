using UnityEngine;
using UnityEngine.Events;

public class TimerTrigger : MonoBehaviour
{
    [SerializeField] private bool isStart;
    [SerializeField] private TimerController timerController;
    [SerializeField] private UnityEvent onTimerEnd;
    [SerializeField] private UnityEvent onTimerStart;
    private void OnTriggerEnter(Collider other)
    {
        timerController.timerActive = isStart;
        
        if (!isStart)
        {
            onTimerEnd?.Invoke();
        }
        else
        {
            onTimerStart?.Invoke();
        }
        
    }
}