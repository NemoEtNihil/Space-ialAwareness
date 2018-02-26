using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Utils;

public class GameControl : MonoBehaviour {


    public static GameControl control;
    public static List<List<GameObject>> enemies = new List<List<GameObject>>();
    public static List<Stack<Transform>> enemyPaths = new List<Stack<Transform>>();
    public static Dictionary<int, Level> levels = new Dictionary<int, Level>();
    public static int currentLevel = 0;
    public static Level currentLevelInfo;
    public static Transform playerReference;
    public static int score = 0;
    [Space]
    [Header("References")]
    public GameObject enemyPrefab;
    public Transform [] waypoints = new Transform[4];
    public NavMeshSurface s;

    private static float spawnTimer;
    private static int currentWave;
    private static int spawnedEnemies = 0;
    private NavMeshHit hit;

    void Awake()
    {
        //Cursor.visible = false;
        FindObjectOfType<MapGenerator>().InitializeMapGenerator();
        FindObjectOfType<DungeonDisplay>().GenerateMap();
        if(GameControl.control != null)
            GameControl.control.s.BuildNavMesh();

        //make walls transparent
        /*GameObject[] gameObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject g in gameObjects)
        {
            Debug.Log(g.transform.eulerAngles.z);
            if (g.layer == LayerMask.GetMask("Walls") && ((g.transform.eulerAngles.z > 50 && g.transform.eulerAngles.z < 130) || (g.transform.eulerAngles.z > 230 && g.transform.eulerAngles.z < 310)))
            {
                Debug.Log("new trans");
                g.layer = LayerMask.GetMask("TransparentWalls");
            }

        }*/

        playerReference = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 playerPos = GameObject.FindGameObjectWithTag("RoomCenter").transform.position;
        playerPos.y = 1;
        playerReference.transform.position = playerPos;

        if (control == null)
        {
            control = this;
            DontDestroyOnLoad(gameObject);
            LoadLevelInfo();
            currentLevelInfo = levels[currentLevel];
            spawnTimer = currentLevelInfo.spawnTimer;
            currentWave = 0;
            GameControl.control.s.BuildNavMesh();
        }
        else
            Destroy(gameObject);

    }

    void Update()
    {
        if (playerReference != null)
        {
            //NavMesh.FindClosestEdge(playerReference.position + new Vector3(50, 0, 0), out hit, NavMesh.AllAreas);
            waypoints[0].position = playerReference.position + new Vector3(40, 0, 0);
            //NavMesh.FindClosestEdge(playerReference.position + new Vector3(-50, 0, 0), out hit, NavMesh.AllAreas);
            waypoints[1].position = playerReference.position + new Vector3(-40, 0, 0);
            //NavMesh.FindClosestEdge(playerReference.position + new Vector3(0, 0, 50), out hit, NavMesh.AllAreas);
            waypoints[2].position = playerReference.position + new Vector3(0, 0, 40);
            //NavMesh.FindClosestEdge(playerReference.position + new Vector3(0, 0, -50), out hit, NavMesh.AllAreas);
            waypoints[3].position = playerReference.position + new Vector3(0, 0, -40);
            spawnTimer -= Time.deltaTime;
            if (spawnTimer < 0 && spawnedEnemies < currentLevelInfo.sizesOfWaves[currentWave])
            {
                if (NavMesh.FindClosestEdge(waypoints[Random.Range(0, waypoints.Length - 1)].position, out hit, NavMesh.AllAreas))
                {
                    Debug.Log(hit.position);
                    GameObject enemy = Instantiate(enemyPrefab, hit.position, new Quaternion());
                    enemy.GetComponent<EnemyScript>().player = playerReference;
                    spawnTimer = currentLevelInfo.spawnTimer;
                    spawnedEnemies++;
                }
            }
        }
        //Debug.Log("Spawned enemies: " + spawnedEnemies + " wave size: " + currentLevelInfo.sizesOfWaves[currentWave] + " current level: " + currentLevel + " enemies count: " + enemies.Count);

        if (spawnedEnemies >= currentLevelInfo.sizesOfWaves[currentWave])
        {
            if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                enemies.Clear();
            if(enemies.Count == 0)
                NextWave();
        }  
    }

    //finds closest AI to specified position
    public static GameObject FindClosestAI(Vector3 position)
    {
        GameObject closestAI = null;
        float smallestDistance = -1;
        float distance;

        foreach (List<GameObject> group in enemies)
        {
            foreach (GameObject ai in group)
            {
                distance = Vector3.Distance(ai.transform.position, position);
                if (smallestDistance < 0)
                {
                    smallestDistance = distance;
                    closestAI = ai;
                }

                if (distance < smallestDistance)
                {
                    smallestDistance = distance;
                    closestAI = ai;
                }
            }
        }
        //returns null if no AI was found
        return closestAI;
    }

    //finds closest AI to specified GameObject(AI)/agent exclusive of itself
    public static GameObject FindClosestAI(GameObject agent)
    {
        GameObject closestAI = null;
        float smallestDistance = -1;
        float distance;

        foreach (List<GameObject> group in enemies)
        {
            foreach (GameObject ai in group)
            {
                if (agent != null && ai!=null)
                {
                    distance = Vector3.Distance(ai.transform.position, agent.transform.position);
                    if (ai != agent)
                    {
                        if (smallestDistance < 0)
                        {
                            smallestDistance = distance;
                            closestAI = ai;
                        }

                        if (distance < smallestDistance)
                        {
                            smallestDistance = distance;
                            closestAI = ai;
                        }
                    }
                }
            }
        }
        //returns null if no AI was found
        return closestAI;
    }

    public static void LoadLevelInfo()
    {
        Level l = new Level();
        int levelNumber = 0;

        //level 0
        l.numOfWaves = 3;
        l.sizesOfWaves = new int[]{10, 15, 20};
        l.squadSize = 1;
        l.spawnTimer = 2f;
        levels.Add(levelNumber++, l);

        //level 1
        l.numOfWaves = 3;
        l.sizesOfWaves = new int[] { 25, 30, 35 };
        l.squadSize = 6;
        l.spawnTimer = 1f;
        levels.Add(levelNumber++, l);

        //level 2
        l.numOfWaves = 3;
        l.sizesOfWaves = new int[] { 40, 45, 50 };
        l.squadSize = 6;
        l.spawnTimer = 0.9f;
        levels.Add(levelNumber++, l);

        //level 3
        l.numOfWaves = 3;
        l.sizesOfWaves = new int[] { 50, 55, 60 };
        l.squadSize = 6;
        l.spawnTimer = 0.7f;
        levels.Add(levelNumber++, l);

        //level 4
        l.numOfWaves = 3;
        l.sizesOfWaves = new int[] { 50, 55, 60 };
        l.squadSize = 6;
        l.spawnTimer = 0.5f;
        levels.Add(levelNumber++, l);

        //level 5
        l.numOfWaves = 3;
        l.sizesOfWaves = new int[] { 60, 65, 70 };
        l.squadSize = 6;
        l.spawnTimer = 0.3f;
        levels.Add(levelNumber++, l);
    }

    public static void LoadNextLevel()
    {
        GameControl.currentLevel++;
        currentLevelInfo = levels[currentLevel];
        currentWave = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void LoadLevel(int index)
    {
        currentLevel = index;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void NextWave()
    {
        spawnedEnemies = 0;
        currentWave++;
        if (currentWave == currentLevelInfo.numOfWaves)
        {
            score = ScoreController.score;
            LoadNextLevel();
        }
    }

}
