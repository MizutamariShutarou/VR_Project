using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpawnerController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _balls = default;
    
    [SerializeField]
    private GameObject _muzzle = default;

    private void Update()
    {
        if (Input.GetButtonDown("Jump") || OVRInput.GetDown(OVRInput.Button.One))
        {
            var num = Random.Range(0, _balls.Count);
            var ballObj = Instantiate(_balls[num]);

            ballObj.transform.position = _muzzle.transform.position;
        }
    }
}
