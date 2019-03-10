using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {
	public GameObject[] asteroids;
	public Transform[] spawnPoints;
	
	public float spawnTimer;
	public Vector3 startVector;
	
	void Start() {
		InvokeRepeating("Spawn", spawnTimer, spawnTimer);
	}

	void Spawn() {
		if (Time.timeScale.Equals(0)) {
			return;
		}
		
		int spawnPointIndex = Random.Range(0, spawnPoints.Length);
		int asteroidIndex = Random.Range(0, asteroids.Length);
		
		GameObject asteroid = Instantiate(asteroids[asteroidIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		asteroid.GetComponent<AsteroidController>().velocity = startVector;
	}
}