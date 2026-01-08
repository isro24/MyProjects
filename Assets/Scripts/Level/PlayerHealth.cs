using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 100;
    int currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log("Player HP: " + currentHP);

        if (currentHP <= 0)
        {
            Debug.Log("PLAYER DEAD");
            // nanti bisa load game over
        }
    }
}
