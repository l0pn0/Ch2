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
        // Если мандарин пойман игроком
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Игрок поймал мандарин!");
            isCaught = true;
            ScoreManager.Instance.AddPlayerScore(1);
            Destroy(gameObject);
        }
        // Если мандарин пойман ботом
        else if (collision.CompareTag("Bot"))
        {
            Debug.Log("Бот поймал мандарин!");
            isCaught = true;
            ScoreManager.Instance.AddBotScore(1);
            Destroy(gameObject);
        }
    }

    // Если требуется реализовать дополнительную логику (например, нанесение урона при пропуске),
    // можно использовать OnBecameInvisible() как в предыдущем варианте
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
                    Debug.Log("Урон нанесён игроку, мандарин пропущен");
                    damageApplied = true;
                }
            }
            // При необходимости можно не уничтожать объект, если хотите сохранить его в сцене
            // Например, отключить SpriteRenderer и Collider:
            // GetComponent<SpriteRenderer>().enabled = false;
            // GetComponent<Collider2D>().enabled = false;
        }
    }
}
