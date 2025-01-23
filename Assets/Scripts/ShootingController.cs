using System.Net.Mime;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera aimCamera;
    [SerializeField] private GameObject aimingPanel;
    [SerializeField] private GameObject spotter;
    [SerializeField] private TextMeshProUGUI targetName;
    [SerializeField] private LayerMask aimLayer;
    
    private AlienInputController _inputs;
    private Camera _mainCamera;
    private Transform _startSpotterPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        
        _inputs = GetComponent<AlienInputController>();
        _mainCamera = Camera.main;
        _startSpotterPosition = spotter.transform;
    }

    // Update is called once per frame
    void Update()
    {
        aimCamera.Priority = _inputs.IsAiming ? 100 : 0;
        aimingPanel.SetActive(_inputs.IsAiming ? true : false);


        if (_inputs.IsAiming)
        {
            Ray ray = _mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, _mainCamera.farClipPlane));
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        
            if (Physics.Raycast(ray, out RaycastHit hit, _mainCamera.farClipPlane, aimLayer))
            {
                spotter.SetActive(true);
                spotter.transform.position = hit.point;

                if (hit.collider.CompareTag("Target"))
                {
                    targetName.gameObject.SetActive(true);
                    targetName.text = hit.collider.name;
                }
                else
                {
                    targetName.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            spotter.transform.SetPositionAndRotation(_startSpotterPosition.position, _startSpotterPosition.rotation);
            spotter.SetActive(false);
        }
    }
}
