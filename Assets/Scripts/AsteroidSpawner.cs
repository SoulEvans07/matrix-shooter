using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {
	public float spawnTimer;
	public GameObject[] asteroids;
	public AsteroidSpawnPoint[] spawnPoints;
	
	public float[] speeds;
	
	void Start() {
		InvokeRepeating("Spawn", spawnTimer, spawnTimer);
	}

	void Spawn() {
		if (Time.timeScale.Equals(0)) {
			return;
		}
		
		int spawnPointIndex = Random.Range(0, spawnPoints.Length);
		int asteroidIndex = Random.Range(0, asteroids.Length);
		int speedIndex = Random.Range(0, speeds.Length);
		
		GameObject asteroid = Instantiate(asteroids[asteroidIndex], 
			spawnPoints[spawnPointIndex].spawnPoint.position,
			spawnPoints[spawnPointIndex].spawnPoint.rotation);
		AsteroidController astrContr = asteroid.GetComponent<AsteroidController>();
		astrContr.SetVelocity(spawnPoints[spawnPointIndex].startVector, speeds[speedIndex]);
		asteroid.transform.position += astrContr.offset;
	}
}