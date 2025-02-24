using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeBot : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 2f; // �������� �������� ����
    private Transform targetOrange; // ���� � ��������� �������� (EatObject)
    private Rigidbody2D rb;
    private bool facingRight = true; // �� ��������� ��� ������� ������

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // ����� ���������� ���������
        EatObject[] oranges = FindObjectsByType<EatObject>(FindObjectsSortMode.None);
        float closestDistance = Mathf.Infinity;
        targetOrange = null;

        foreach (var orange in oranges)
        {
            float distance = Mathf.Abs(rb.position.x - orange.transform.position.x);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                targetOrange = orange.transform;
            }
        }

        // �������� � ���� ������ �� ��� X
        if (targetOrange != null)
        {
            Vector2 targetPosition = new Vector2(targetOrange.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

            // ������� ���� � ����������� �� ������� �������
            if (targetOrange.position.x < transform.position.x && facingRight)
            {
                Flip();
            }
            else if (targetOrange.position.x > transform.position.x && !facingRight)
            {
                Flip();
            }
        }
    }

    // ����� ����������
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    // ���� ���������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EatObject eatObject = collision.GetComponent<EatObject>();
        if (eatObject != null)
        {
            Debug.Log("��� ������ ��������!");
            // ������ ���������� ����� ����������� � EatObject
            Destroy(collision.gameObject);
        }
    }
}

