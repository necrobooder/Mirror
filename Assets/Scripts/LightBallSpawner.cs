using UnityEngine;
using System.Collections;

public class LightBallSpawner : MonoBehaviour {

	private const float RANGE = 16f;

	public float spawnTimer;
	public LightBall objectToSpawn;

	private float counter;

	void Start()
	{
		counter = spawnTimer;
	}

	void Update () {
		ManageSpawnTimes ();
	}

	void ManageSpawnTimes()
	{
		counter -= Time.deltaTime;
		if (counter < 0) 
		{
			counter = spawnTimer;
			SpawnObject ();
		}
	}

	void SpawnObject()
	{
		// get parameters
		Direction direction = GetRandomDirection();
		int speed = (int)(Random.value * 5f);
		Vector2 spawnPosition = new Vector2 (Random.Range (-RANGE, RANGE), Random.Range (-RANGE, RANGE));

		// Instantiate
		LightBall lightball = Instantiate (objectToSpawn, spawnPosition, Quaternion.identity) as LightBall;

		// Set params
		lightball.SetUp(direction, speed);
	}

	Direction GetRandomDirection()
	{
		float x = Random.value;
		if (x < .25f)
			return Direction.north;
		if (x < .5f)
			return Direction.south;
		if (x < .75f)
			return Direction.east;
		return Direction.west;
	}
}
