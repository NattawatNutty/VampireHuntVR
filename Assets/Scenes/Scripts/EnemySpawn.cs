using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawn : MonoBehaviour {
    public Transform player;
    public GameObject[] enemyPrefab;
    public Transform[] spawnPosition;
    private float[] lastSpawn = { 0, 0, 0, 0 };
    private int[] delay = {5, 15, 20, 30};

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // Every 5 seconds, enemy 1 is spawned
		if(Time.time > delay[0] + lastSpawn[0]) {
            GameObject newEnemy = Instantiate(enemyPrefab[0], spawnPosition[0].position, enemyPrefab[0].transform.rotation);
            newEnemy.SetActive(true);
            newEnemy.GetComponent<Enemy>().player = player;
            lastSpawn[0] = Time.time;
        }

        // Every 10 seconds, enemy 2 is spawned
        if (Time.time > delay[1] + lastSpawn[1]) {
            GameObject newEnemy = Instantiate(enemyPrefab[1], spawnPosition[1].position, enemyPrefab[1].transform.rotation);
            newEnemy.SetActive(true);
            newEnemy.GetComponent<Enemy>().player = player;
            lastSpawn[1] = Time.time;
        }

        // Every 15 seconds, enemy 3 is spawned
        if (Time.time > delay[2] + lastSpawn[2]) {
            GameObject newEnemy = Instantiate(enemyPrefab[2], spawnPosition[2].position, enemyPrefab[2].transform.rotation);
            newEnemy.SetActive(true);
            newEnemy.GetComponent<Enemy>().player = player;
            lastSpawn[2] = Time.time;
        }

        // Every 20 seconds, enemy 4 is spawned
        if (Time.time > delay[3] + lastSpawn[3]) {
            GameObject newEnemy = Instantiate(enemyPrefab[3], spawnPosition[3].position, enemyPrefab[3].transform.rotation);
            newEnemy.SetActive(true);
            newEnemy.GetComponent<Enemy>().player = player;
            lastSpawn[3] = Time.time;
        }
    }
}
