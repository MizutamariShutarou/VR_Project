using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CatcherController : MonoBehaviour
{
    [SerializeField] private GameObject _catchPoint;
    
    private bool _canChatch;

    private BallController _ball;

    private void Start()
    {
        _canChatch = false;
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            if (_canChatch && _ball != null)
            {
                _ball.Cacthed(_catchPoint.transform);
                
                OVRInput.SetControllerVibration(0.01f, 1f, OVRInput.Controller.LTouch);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            OVRInput.SetControllerVibration(0.01f, 0.01f, OVRInput.Controller.LTouch);
            _ball = other.gameObject.GetComponent<BallController>();
            Debug.Log(_ball);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            _ball = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            _ball = other.gameObject.GetComponent<BallController>();

            _canChatch = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            _canChatch = false;
        }
    }
}