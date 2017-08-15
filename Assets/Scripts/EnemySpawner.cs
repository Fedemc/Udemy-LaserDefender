using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    public float movSpeed = 10f;
    float minX, maxX;
    private bool derecha = false;
    public float spawnDelay = 0.5f;


    // Use this for initialization
    void Start () {

        SpawnUntilFull();

        //Me creo 2 vectores que me van a indicar las posiciones en 3D del punto mas a la izq y del de mas a la derecha
        float distance = transform.position.z - Camera.main.transform.position.z;
        
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        minX = leftMost.x;
        maxX = rightMost.x;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }
    // Update is called once per frame
    void Update () {
        
        if(derecha)
        {
            transform.position += Vector3.right * movSpeed * Time.deltaTime;
        }
        else
            transform.position += Vector3.left * movSpeed * Time.deltaTime;

        //Detección de si llegué a uno de los bordes
        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float LeftEdgeOfFormation = transform.position.x - (0.5f * width);
        if(LeftEdgeOfFormation < minX || rightEdgeOfFormation > maxX)
        {
            derecha = !derecha;
        }

        if(AllMembersDead())
        {
            Debug.Log("Empty Formation");
            SpawnUntilFull();
        }
    }

    bool AllMembersDead()
    {
        foreach(Transform childPositionGameObject in transform)         //Para cada "Position" dentro de EnemyFormation
        {
            if (childPositionGameObject.childCount > 0)                 //Si ese "Position" tiene un hijo dentro (una nave enemiga)
                return false;                                           //Retorno false, porque al menos hay un enemigo vivo en pantalla
        }

        return true;                                                    //Si llego al final es porque no hay ningun enemigo vivo, estan todos muertos
    }

    void SpawnEnemies()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }

    void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if(freePosition)
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
        }
        if(NextFreePosition())
        {
            Invoke("SpawnUntilFull", spawnDelay);
        }
        
    }

    Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform)         
        {
            if (childPositionGameObject.childCount == 0)                 
                return childPositionGameObject;                                           
        }

        return null;
    }
}
