using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class TargetManager : MonoBehaviour
{
    
    //[SerializeField] private UnityEvent allTargetsDestroyed = new UnityEvent();
    private List<Target> _targets = new List<Target>();
    public int targetsLeft;
    public int targetsTotal;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Target[] targetsArray = GetComponentsInChildren<Target>();
        _targets = new List<Target>(targetsArray);
        foreach (Target target in _targets)
        {
            target.OnDestroyed += RemoveTarget;
        }

        targetsTotal = _targets.Count;
        targetsLeft = targetsTotal;
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    private void RemoveTarget(Target targetToRemove)
    {
        Debug.Log("Removing item");
        targetToRemove.OnDestroyed -= RemoveTarget;
        _targets.Remove(targetToRemove);
        targetsLeft--;
        
        // if (_targets.Count == 0)
        // {
        //     Debug.Log("No targets left");
        //     allTargetsDestroyed?.Invoke();
        // }
    }
    
}
