using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeBot : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 2f; // Скорость движения бота
    private Transform targetOrange; // Цель — ближайший мандарин (EatObject)
    private Rigidbody2D rb;
    private bool facingRight = true; // По умолчанию бот смотрит вправо

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Поиск ближайшего мандарина
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

        // Движение к цели только по оси X
        if (targetOrange != null)
        {
            Vector2 targetPosition = new Vector2(targetOrange.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

            // Поворот бота в зависимости от целевой позиции
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

    // Метод переворота
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    // Сбор мандарина
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EatObject eatObject = collision.GetComponent<EatObject>();
        if (eatObject != null)
        {
            Debug.Log("Бот собрал мандарин!");
            // Логика начисления очков выполняется в EatObject
            Destroy(collision.gameObject);
        }
    }
}

