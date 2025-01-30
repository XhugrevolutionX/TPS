using UnityEngine;
public class TimerController : MonoBehaviour
{
    public float timer = 0.0f;
    public bool timerActive;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(timerActive)
        {
            timer += Time.deltaTime;
        }
    }
}
