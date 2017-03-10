using UnityEngine;
using System.Collections;

public class LightBallSpawner : MonoBehaviour {

	private const float MIN_SPEED = 5f;
	private const float MAX_SPEED = 12f;
	private const float RANGE = 16f;
	private const float SPAWN_DISTANCE = 42f;
	private const float SPAWN_TIMER = 5f;

	public LightBall objectToSpawn;

	private float counter;

	void Start()
	{
		counter = SPAWN_TIMER;
	}

	void Update () {
		ManageSpawnTimes ();
	}

	void ManageSpawnTimes()
	{
		counter -= Time.deltaTime;
		if (counter < 0) 
		{
			counter = SPAWN_TIMER;
			SpawnObject ();
		}
	}

	void SpawnObject()
	{
		// Declare construction parameters
		Direction direction;
		Vector2 spawnPosition = new Vector2 ();
		Color color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f, 1f, 1f);
		int speed = (int)(Random.value * (MAX_SPEED - MIN_SPEED) + MIN_SPEED);

		// used variables
		float directionRandomizer = Random.value;
		float spawnRandomizer = Random.Range (-RANGE, RANGE);

		// initialize parameters
		if (directionRandomizer < .25f) {
			direction = Direction.north;
			spawnPosition.Set (spawnRandomizer, -SPAWN_DISTANCE);
		}
		else if (directionRandomizer < .5f) {
			direction = Direction.south;
			spawnPosition.Set (spawnRandomizer, SPAWN_DISTANCE);
		}
		else if (directionRandomizer < .75f){
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
		lightball.SetUp(direction, speed, color);
	}
}
