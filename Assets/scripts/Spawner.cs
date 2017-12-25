﻿using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	public float spawnTime = 15f;		// The amount of time between each spawn.
	public float spawnDelay = 10f;		// The amount of time before spawning starts.
	public int enemyLimit = 5;
	public GameObject[] enemyCount;
	public GameObject[] enemies;		// Array of enemy prefabs.
	
	void Start ()
	{
		// Start calling the Spawn function repeatedly after a delay .
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}
	
	void FixedUpdate() {
		enemyCount = GameObject.FindGameObjectsWithTag( "Enemy" );
	}
	
	void Spawn ()
	{
		if( enemyCount.Length < enemyLimit ){
		// Instantiate a random enemy.
		int enemyIndex = Random.Range(0, enemies.Length);
		Instantiate(enemies[enemyIndex], transform.position, transform.rotation);
		}
		// Play the spawning effect from all of the particle systems.
		foreach(ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
		{
			p.Play();
		}
	}
}
