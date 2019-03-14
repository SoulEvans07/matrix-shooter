using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour {
	public float spawnTimer;
	[Range(0, 1)] public float powerupProbability;
	[Range(0, 1)] public float powerupAsteroidProbability;
	public List<PowerUpElement> powerups;
	[SerializeField] private List<int> powerupIndexes;
	public GameObject[] asteroids;
	public AsteroidSpawnPoint[] spawnPoints;

	public float[] speeds;

	void Start() {
		powerupIndexes = new List<int>();
		for (int i = 0; i < powerups.Count; i++) {
			for (int k = 0; k < powerups[i].spawn_weight; k++) {
				powerupIndexes.Add(i);
			}
		}

		InvokeRepeating("Spawn", spawnTimer, spawnTimer);
	}

	void Spawn() {
		if (Time.timeScale.Equals(0)) {
			return;
		}
		
		float speed = speeds[Random.Range(0, speeds.Length)];
		int spawnPointIndex = Random.Range(0, spawnPoints.Length);

		bool spawnPowerup = Random.Range(0, 1f) < powerupProbability;
		if (spawnPowerup) {
			bool spawnInAsteroid = Random.Range(0, 1f) < powerupAsteroidProbability;
			int powerupIndex = powerupIndexes[Random.Range(0, powerupIndexes.Count)];

			if (spawnInAsteroid) {
				GameObject powerup = Instantiate(powerups[powerupIndex].powerUpPrefab,
					spawnPoints[spawnPointIndex].spawnPoint.position,
					spawnPoints[spawnPointIndex].spawnPoint.rotation);
				SpawnAsteroid(spawnPointIndex, speed, powerup);
			} else {
				GameObject powerup = Instantiate(powerups[powerupIndex].powerUpPrefab,
					spawnPoints[spawnPointIndex].spawnPoint.position,
					spawnPoints[spawnPointIndex].spawnPoint.rotation);
				PowerUpBehavior puContr = powerup.GetComponent<PowerUpBehavior>();
				puContr.SetVelocity(spawnPoints[spawnPointIndex].startVector, speed);
				puContr.GetComponent<Collider2D>().enabled = true;
			}
		} else {
			SpawnAsteroid(spawnPointIndex, speed, null);
		}
	}

	void SpawnAsteroid(int spawnPointIndex, float speed, GameObject powerup) {
		int asteroidIndex = Random.Range(0, asteroids.Length);

		GameObject asteroid = Instantiate(asteroids[asteroidIndex],
			spawnPoints[spawnPointIndex].spawnPoint.position,
			spawnPoints[spawnPointIndex].spawnPoint.rotation);
		AsteroidController astrContr = asteroid.GetComponent<AsteroidController>();
		astrContr.SetVelocity(spawnPoints[spawnPointIndex].startVector, speed);
		asteroid.transform.position += astrContr.offset;

		if (powerup != null) {
			powerup.transform.parent = asteroid.transform;
//			powerup.transform.position += astrContr.offset;
			astrContr.powerUp = powerup;
		}
	}

	[Serializable]
	public class PowerUpElement {
		public GameObject powerUpPrefab;
		public int spawn_weight = 1;
	}
}