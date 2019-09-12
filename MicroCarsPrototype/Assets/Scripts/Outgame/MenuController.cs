using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    public GameObject spawnedCars;

    private void Start()
    {
        StartCoroutine(spawnCars());
    }

    public IEnumerator spawnCars()
    {

        while(true)
        {
            Vector3 randomSpawnPoint = Vector3.zero;

            int side = Random.Range(0, 4);
            switch (side)
            {
                case 0:     // Set spawnPoint on left wall
                    randomSpawnPoint = new Vector3(-6.0f, Random.Range(-12.0f, 12.0f), 0.0f);
                    break;
                case 1:     // Set spawnPoint on top wall
                    randomSpawnPoint = new Vector3(Random.Range(-12.0f, 6.0f), 12.0f, 0.0f);
                    break;
                case 2:     // Set spawnPoint on right wall
                    randomSpawnPoint = new Vector3(6.0f, Random.Range(-12.0f, 12.0f), 0.0f);
                    break;
                case 3:     // Set spawnPoint on bottom wall
                    randomSpawnPoint = new Vector3(Random.Range(-12.0f, 12.0f), -12.0f, 0.0f);
                    break;
            }
            Vector2 direction = randomSpawnPoint - Vector3.zero;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            float randomWait = Random.Range(0, 4);
            Instantiate(spawnedCars, randomSpawnPoint, rotation);
            yield return new WaitForSeconds(randomWait);
        }
        
    }

}
