using UnityEngine;
using System.Collections;

public class LightBallSpawner : MonoBehaviour {

	private const float RANGE = 16f;
	private const float SPAWN_DISTANCE = 42f;

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
		// Declare construction parameters
		Direction direction;
		Vector2 spawnPosition = new Vector2 ();
		int speed = (int)(Random.value * 15f + 1);

		// used variables
		float directionRandomizer = Random.value;
		float spawnRandomizer = Random.Range (-RANGE, RANGE);

		// initialize parameters
		if (directionRandomizer < .25f) {
			direction = Direction.north;
			spawnPosition.Set (spawnRandomizer, -SPAWN_DISTANCE);
		}
		if (directionRandomizer < .5f) {
			direction = Direction.south;
			spawnPosition.Set (spawnRandomizer, SPAWN_DISTANCE);
		}
		if (directionRandomizer < .75f){
			direction = Direction.east;
			spawnPosition.Set (-SPAWN_DISTANCE, spawnRandomizer);
		}
		else{
			direction = Direction.west;
			spawnPosition.Set (SPAWN_DISTANCE, spawnRandomizer);
		}

		// Instantiate
		LightBall lightball = Instantiate (objectToSpawn, spawnPosition, Quaternion.identity) as LightBall;

		// Set params
		lightball.SetUp(direction, speed);
	}
}
