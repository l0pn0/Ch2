using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;
    [SerializeField] private GameObject specialObject;
    [SerializeField] private List<GameObject> objectsToDestroy;
    private int damageCounter = 0;

    void Start()
    {
        currentHealth = maxHealth;

        if (specialObject != null)
        {
            specialObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        damageCounter++;
        Debug.Log("�������� ������� ����! ������� ��������: " + currentHealth);

        if (currentHealth <= 0)
        {
            StopGame();
        }
        else if (currentHealth == 1 && specialObject != null)
        {
            ShowSpecialObject();
        }
        RemoveObjects();
    }

    private void StopGame()
    {
        Debug.Log("���� �����������! �������� ������ 1.");
        Time.timeScale = 0;
    }

    private void ShowSpecialObject()
    {
        Debug.Log("�������� ����������� ������! �������� = 1.");
        specialObject.SetActive(true);
    }
    private void RemoveObjects()
    {
        if (damageCounter <= objectsToDestroy.Count)
        {
            GameObject objToDestroy = objectsToDestroy[damageCounter - 1];
            if (objToDestroy != null)
            {
                Destroy(objToDestroy);
                Debug.Log("������ ������: " + objToDestroy.name);
            }
        }
    }
    public bool IsAlive()
    {
        return currentHealth > 0;
    }
}
