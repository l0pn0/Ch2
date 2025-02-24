using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private Transform _player;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _moveStep;
    private bool facingRight = true;
    private bool _isPause = false;
    [SerializeField] private GameObject targetObject;
    private bool isVisible = false;
    [SerializeField] private HealthManager healthManager;
    void Awake()
    {
        _player = GetComponent<Transform>();
    }
    void Update()
    {
        float moveStepSpeed = _moveSpeed * Time.deltaTime;
        if (_isPause == false)
        {
            if (Input.GetKey(KeyCode.A))
            {
                _player.position = Vector2.MoveTowards(_player.position, new Vector2(_player.position.x - _moveStep, _player.position.y), moveStepSpeed);
                if (!facingRight)
                {
                    Flip();
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                _player.position = Vector2.MoveTowards(_player.position, new Vector2(_player.position.x + _moveStep, _player.position.y), moveStepSpeed);
                if (facingRight)
                {
                    Flip();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (healthManager != null && healthManager.IsAlive())
            {
                _isPause = !_isPause;
                Time.timeScale = _isPause ? 0 : 1;
                isVisible = !isVisible;
                targetObject.SetActive(isVisible);
            }
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }


}
