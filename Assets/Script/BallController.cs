using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Update = UnityEngine.PlayerLoop.Update;

public class BallController : MonoBehaviour
{
    [SerializeField] private float _speed = 0f;

    /// <summary>
    /// 投球時に横にかける回転
    /// </summary>
    [SerializeField, Tooltip("投球時に横にかける回転")]
    private float _powerX = -0.0246f;

    /// <summary>
    /// 投球時に加える縦の回転
    /// </summary>
    [SerializeField, Tooltip("投球時に加える縦の回転")]
    private float _powerY = -0.0246f;

    /// <summary>
    /// プラスでフォーク方向[cm](base:-25.4f)
    /// </summary>
    [SerializeField, Tooltip("プラスでフォーク方向")]
    private float _angleX = -25.4f;

    /// <summary>
    /// プラスでシュート方向[cm](base:-6.0f)
    /// </summary>
    [SerializeField, Tooltip("プラスでシュート方向")]
    private float _angleZ = -6.0f;

    private float _power = 0f;

    private float _vel;

    private float _revf, _revr;

    private float _forcelf, _forcelr;

    private float _time = 0f;

    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        ActiveBall();
    }

    public void ActiveBall()
    {
        //球速[km/h]
        _power = 0.27862f * _speed;
        //初速がspeedになるように力を与える
        _rb.AddForce(
            (transform.forward) * _power * 0.9984f + (transform.right) * _power * _powerX +
            (transform.up) * _power * -_powerY, ForceMode.VelocityChange);
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if (_time >= 5f)
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        //マグヌス効果を考慮
        _vel = _rb.velocity.z;
        _revf = 3 * _angleX * (_vel / _speed);
        _revr = 3 * _angleZ * (_vel / _speed);
        _forcelf = -1.0f / 8.0f * Mathf.PI * Mathf.PI * 1.208f * Mathf.Pow(0.073f, 3.0f) * _vel * _revf;
        _forcelr = 1.0f / 8.0f * Mathf.PI * Mathf.PI * 1.206f * Mathf.Pow(0.073f, 3.0f) * _vel * _revr;
        _rb.AddForce(
            (transform.up) * _forcelf, ForceMode.Force);
        _rb.AddForce(
            (transform.right) * _forcelr, ForceMode.Force);
    }

    public void Cacthed(Transform transform)
    {
        _rb.constraints = RigidbodyConstraints.FreezeAll;
        gameObject.transform.parent = transform;
        gameObject.transform.position = transform.position;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
}