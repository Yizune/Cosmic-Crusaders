using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemySpawner : MonoBehaviour
{
    private GameObject player, planet;
    [SerializeField]
    private GameObject enemy;
    // [SerializeField]
    // private int MaxNumberOfEnemies = 5;
    // [SerializeField]
    private float PlanetDiameter;
    // , LastSpawnedTime, SpawncheckInterval = 1;
    Vector3 PlayerPos, PlanetPos, DiffBetweenPlayerAndPlanetPos;
    // [SerializeField]
    // List<GameObject> EnemiesList = new List<GameObject>();

    void Awake()
    {
        planet = GameObject.FindWithTag("Planet");
        player = GameObject.FindWithTag("Player");
    }
    void Start()
    {
        //SpawnEnemy();
    }

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Y))
    //     {
    //         SpawnEnemy();
    //     }
    //     //SpawnEnemyTime();
    // }

    public void SpawnEnemy()
    {
        // if (EnemiesList.Count < MaxNumberOfEnemies) 
        // { 
            GameObject newEnemy = Instantiate(enemy);
            newEnemy.name = "Enemy1";
            //positioning enemy compared to player and planet
            PlayerPos = player.transform.position;
            PlanetPos = planet.transform.position;  
            DiffBetweenPlayerAndPlanetPos = PlayerPos - PlanetPos;
            newEnemy.transform.position = PlayerPos - (DiffBetweenPlayerAndPlanetPos*2);
            //rotating enemy compared to player
            // newEnemy.transform.rotation = player.transform.rotation;
            // newEnemy.transform.Rotate(new Vector3(0, 180, 180));
            //Quaternion.Inverse(player.transform.rotation)

        //     //other stuff
        //     EnemiesList.Add(newEnemy);
        //     Debug.Log(PlayerPos + " " +  PlanetPos + " " + DiffBetweenPlayerAndPlanetPos + " " + newEnemy.transform.position);
        // }
    }
    // void SpawnEnemyTime()
    // {
    //     if (Time.time - LastSpawnedTime > SpawncheckInterval)
    //     {
    //         SpawnEnemy();
    //         LastSpawnedTime = Time.time;
    //     }
    // }
}
