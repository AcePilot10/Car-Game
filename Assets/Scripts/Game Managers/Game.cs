using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    #region Singleton
    public static Game instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    #region Variables
    #region Game Stats
    public float pollutionLevel = 0;
    #endregion

    #region Round Stats
    //Round
    public int currentRound = 0;
    public float roundDelay = 5f;
    #endregion

    #region Spawning stats
    //Spawning
    public Attacker[] attackersToSpawn;
    public float minSpawnDelay;
    public float maxSpawnDelay;
    public int currentEnemiesToSpawn;
    public AnimationCurve enemiesToSpawn;
    public static int enemiesAlive = 0;
    public Transform attackerParent;
    #endregion

    #region Player Stats
    [SerializeField] private float energy;
    #endregion
    #endregion

    #region Round Functionality
    private void Start()
    {
        NextRound();
    }

    public IEnumerator RoundDelay() {
        yield return new WaitForSeconds(roundDelay);
        NextRound();
    }

    public void NextRound() {
        currentRound++;
        StartCoroutine(SpawnRoundAttackers());
    }

    public IEnumerator SpawnRoundAttackers() {

        float delay = Random.Range(minSpawnDelay, maxSpawnDelay);

        yield return new WaitForSeconds(delay);
        currentEnemiesToSpawn--;

        SpawnAttacker();

        if (currentEnemiesToSpawn > 0) {
            StartCoroutine(SpawnRoundAttackers());
        }
    }

    public void CalculateEnemiesToSpawn() {
        currentEnemiesToSpawn = Mathf.RoundToInt(enemiesToSpawn.Evaluate(currentRound));
    }

    public void SpawnAttacker() {
        enemiesAlive++;
        GameObject obj = Instantiate(GetAttackerToSpawn()) as GameObject;
        obj.transform.parent = attackerParent;
        Vector2 spawnPos = TrackManager.instance.GetWaypoint(0).position;
        obj.transform.position = spawnPos;
    }

    public GameObject GetAttackerToSpawn() {
        float rdm = Random.value;

        foreach (Attacker attacker in attackersToSpawn) {
            AnimationCurve spawnChanceCurve = attacker.spawnChance;
            float spawnChance = spawnChanceCurve.Evaluate(currentRound);
            if (rdm < spawnChance) {
                return attacker.prefab;
            }
        }
        return attackersToSpawn[0].prefab;
    }
    #endregion

    #region Player Functionality
    public float GetEnergy() {
        return energy;
    }

    public void AddEnergy(float amount) {
        energy += amount;
    }

    public void RemoveEnergy(float amount) {
        energy -= amount;
    }

    public bool HasEnoughEnergy(float amount) {
        return energy >= amount;
    }
    #endregion
}