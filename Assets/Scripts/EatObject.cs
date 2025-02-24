using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EatObject : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    private bool isCaught = false;
    private bool damageApplied = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� �������� ������ �������
        if (collision.CompareTag("Player"))
        {
            Debug.Log("����� ������ ��������!");
            isCaught = true;
            ScoreManager.Instance.AddPlayerScore(1);
            Destroy(gameObject);
        }
        // ���� �������� ������ �����
        else if (collision.CompareTag("Bot"))
        {
            Debug.Log("��� ������ ��������!");
            isCaught = true;
            ScoreManager.Instance.AddBotScore(1);
            Destroy(gameObject);
        }
    }

    // ���� ��������� ����������� �������������� ������ (��������, ��������� ����� ��� ��������),
    // ����� ������������ OnBecameInvisible() ��� � ���������� ��������
    private void OnBecameInvisible()
    {
        if (!isCaught && !damageApplied)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                HealthManager healthManager = player.GetComponent<HealthManager>();
                if (healthManager != null)
                {
                    healthManager.TakeDamage(damage);
                    Debug.Log("���� ������ ������, �������� ��������");
                    damageApplied = true;
                }
            }
            // ��� ������������� ����� �� ���������� ������, ���� ������ ��������� ��� � �����
            // ��������, ��������� SpriteRenderer � Collider:
            // GetComponent<SpriteRenderer>().enabled = false;
            // GetComponent<Collider2D>().enabled = false;
        }
    }
}
