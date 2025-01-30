using System.Collections;
using System.Net.Mime;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera aimCamera;
    [SerializeField] private CinemachineCamera followCamera;
    [SerializeField] private GameObject aimingPanel;
    [SerializeField] private GameObject spotter;
    [SerializeField] private LayerMask aimLayer;
    [SerializeField] private float damageRate = 10f;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] float shootCoroutineDelay = 1f;
    private bool _canShoot = true;
    private Coroutine _shootCoroutine;
    
    public bool CanShoot => _canShoot;
    
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
        followCamera.gameObject.SetActive(_inputs.IsAiming ? false : true);
        aimingPanel.SetActive(_inputs.IsAiming ? true : false);


        if (_inputs.IsAiming)
        {
            Ray ray = _mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, _mainCamera.farClipPlane));
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        
            if (Physics.Raycast(ray, out RaycastHit hit, _mainCamera.farClipPlane, aimLayer))
            {
                spotter.SetActive(true);
                spotter.transform.position = hit.point;
            }

            if (_inputs.IsShooting)
            {
                if (_canShoot)
                {
                    if (hit.collider.TryGetComponent(out Target target))
                    {
                        target.TakeDamage(damageRate);
                    }
                    
                    muzzleFlash.Play();
                    
                    _canShoot = false;    
                    
                    if (_shootCoroutine != null)
                    {
                        StopCoroutine(_shootCoroutine);
                    }
                    _shootCoroutine = StartCoroutine("ShootCoroutine");
                }
            }
        }
        else
        {
            spotter.transform.SetPositionAndRotation(_startSpotterPosition.position, _startSpotterPosition.rotation);
            spotter.SetActive(false);
        }
    }

    IEnumerator ShootCoroutine()
    {
        yield return new WaitForSeconds(shootCoroutineDelay);
        _canShoot = true;
    }
    
}
