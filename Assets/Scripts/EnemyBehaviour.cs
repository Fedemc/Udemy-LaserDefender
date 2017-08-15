using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    public float health = 150;
    public GameObject enemy_laser;
    public float enemy_laserSpeed = 2.5f;
    public float shotsPerSecond = 0.5f;
    public GameObject playerShip;
    public int enemyPoints=150;
    private ScoreKeeper scorekeeper;

    private void Start()
    {
        scorekeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Laser misil = collider.gameObject.GetComponent<Laser>();
        if (misil)
        {
            health -= misil.GetDamage();
            misil.Hit();

            if (health <= 0)
            {
                scorekeeper.Score(enemyPoints);
                Destroy(gameObject);
            }
                
        }
    }

    private void Update()
    {
        float probability = Time.deltaTime * shotsPerSecond;
        if (Random.value < probability)
            EnemyShoot();

    }


    void EnemyShoot()
    {
        Vector3 startPosition = transform.position + new Vector3(0, -1, 0);
        GameObject enemy_beam = Instantiate(enemy_laser, startPosition, Quaternion.identity) as GameObject;
        enemy_beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0,-enemy_laserSpeed,0f);
    }
}
