using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerHealth : MonoBehaviour {

    #region Event
    public delegate void AttackerDeath();
    public static event AttackerDeath OnAttackerDeath;
    #endregion

    [SerializeField]private float health;

    public void ReduceHealth(float amount) {
        health -= amount;
        if (health <= 0) {
            Die();
        }
    }

    public void Die() {
        if (OnAttackerDeath != null) {
            OnAttackerDeath();
        }
        Game.enemiesAlive--;
    }
}