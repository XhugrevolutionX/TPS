using UnityEngine;

public class Hints : MonoBehaviour
{
    private bool _hintActive;
    
    [SerializeField] private AlienInputController inputs;
    [SerializeField] private GameObject aimHint;
    [SerializeField] private GameObject shootHint;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _hintActive = false;
        
        aimHint.SetActive(false);
        shootHint.SetActive(false);
    }
    
    public void ShowHint()
    {
       _hintActive = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_hintActive)
        {
            if (inputs.IsAiming)
            {
                if (inputs.IsShooting)
                {
                    shootHint.SetActive(false);
                    _hintActive = false;
                }
                else
                {
                    aimHint.SetActive(false);
                    shootHint.SetActive(true);
                }
            }
            else
            {
                shootHint.SetActive(false);
                aimHint.SetActive(true);
            }
        }
    }
}
