using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SpawnGameObject : MonoBehaviour
{
    private GameObject player, planet;
    
    [SerializeField] 
    private GameObject gameObjectToSpawn;
    
    [SerializeField] 
    private bool spawnOnOtherSidePlanet, spawnOnDeathPosition;
    
    private bool spawnedPositionSet = false;
    
    public UnityEvent onSpawnObject;

    [SerializeField] 
    private LayerMask includeLayer;

    [Tooltip("The number set is the max and min distances from the striktly otherside of the planet that the object will be randomized to be able to be spawned at, in all directions along the planet")]
    
    [SerializeField] 
    private float minSpawnOffset = -80, maxSpawnOffset = 80;

    Vector3 playerPos, planetPos, diffBetweenPlayerAndPlanetPos, diameterSpawnOffset, spawnPosMinOffset;
    
    void Awake()
    {
        planet = GameObject.FindWithTag("Planet");
        player = GameObject.FindWithTag("Player");
    }

    public void SpawnObject() {
        SpawnAGameObject(gameObjectToSpawn, spawnOnOtherSidePlanet, spawnOnDeathPosition);
    }
    
    public void SpawnAGameObject(GameObject ToSpawn, bool SpawnOtherSide, bool SpawnDeathLocation)
    {

        if (spawnOnDeathPosition == true && spawnOnOtherSidePlanet == true)
        {
            Debug.Log("cant spawn the same object in two places at once");
        }
        else if (spawnOnOtherSidePlanet == true)
        {

            playerPos = player.transform.position;

            planetPos = planet.transform.position;

            diffBetweenPlayerAndPlanetPos = playerPos - planetPos;

            diameterSpawnOffset = new Vector3(Random.Range(minSpawnOffset, maxSpawnOffset), 0f , Random.Range(minSpawnOffset, maxSpawnOffset));
 
            GameObject spawnedGameObject = Instantiate(gameObjectToSpawn, playerPos - (diffBetweenPlayerAndPlanetPos * 3) + diameterSpawnOffset, Quaternion.identity);
            spawnedGameObject.name = "SpawnedGameObject"; 
            spawnedPositionSet = true; 

            // SpawnGameObject script = spawnedGameObject.GetComponent<SpawnGameObject>();
            
            // script.spawnOnOtherSidePlanet = spawnOnOtherSidePlanet;
            // script.spawnOnDeathPosition = spawnOnDeathPosition;
            // script.minSpawnOffset = minSpawnOffset;
            // script.maxSpawnOffset = maxSpawnOffset;
            // script.gameObjectToSpawn = gameObjectToSpawn;


        }
        else if (spawnOnDeathPosition == true)
        {
            GameObject SpawnedGameObject = Instantiate(gameObjectToSpawn, transform.position, Quaternion.identity);
            SpawnedGameObject.name = "SpawnedGameObject";
        }
        onSpawnObject.Invoke();
        
        
    }
}