using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera firstCamera;
    [SerializeField]
    private CinemachineVirtualCamera secondCamera;

    private CinemachineVirtualCamera currentCamera;

    public GameObject boss;

    private void Awake()
    {
        currentCamera = firstCamera;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            DisableCamera(firstCamera);
            EnableCamera(secondCamera);
            currentCamera = secondCamera;

            boss.SetActive(true);
        }
    }

    private void EnableCamera(CinemachineVirtualCamera cam)
    {
        cam.gameObject.SetActive(true);
    }

    private void DisableCamera(CinemachineVirtualCamera cam)
    {
        cam.gameObject.SetActive(false);
    }
}
