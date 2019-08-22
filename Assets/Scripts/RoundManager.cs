using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
	public GameObject[] spawnPoints;
	public GameObject[] characterObjs;
    // Start is called before the first frame update
    void Start()
    {
		Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void Spawn()
	{
		int total = PlayerPrefs.GetInt("TotalPlayers");
		print("Spawn a total of " + total);
		for (int i = 0; i < total; i++) //could be totsl - 1
		{
			//character int PlayerPrefs.GetInt("CharacterPlayer" + i);
			
			GameObject character = Instantiate(characterObjs[PlayerPrefs.GetInt("CharacterPlayer" + i)], spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
			character.GetComponent<IsometricPlayerMovementController>().playerNumber = i;
			print("spawn");
		}
	}
}
