using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float movSpeed = 10f;
    float minX, maxX;
    public float padding = 1;
    public GameObject laser;
    public float laserSpeed=5f, firingRate=0.2f;
    public float health=250f;
    private ScoreKeeper scorekeeper;


    private void Start()
    {
        //Me creo 2 vectores que me van a indicar las posiciones en 3D del punto mas a la izq y del de mas a la derecha
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost= Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        minX = leftMost.x + padding;
        maxX = rightMost.x - padding;

        scorekeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire",0.000001f,firingRate);
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //this.transform.position += new Vector3(-movSpeed * Time.deltaTime, 0f, 0f);
            transform.position += Vector3.left * movSpeed * Time.deltaTime;
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += Vector3.right * movSpeed * Time.deltaTime;
            }
        }

        //Restrict the player to the gamespace
        float newX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);	
	}

    void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0, 0.6f, 0);
        GameObject beam = Instantiate(laser, startPosition, Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, laserSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        Laser misil = collider.gameObject.GetComponent<Laser>();
        if (misil)
        {
            Debug.Log("Player collided with enemy laser");
            health -= misil.GetDamage();
            misil.Hit();

            if (health <= 0)
            {
                scorekeeper.ResetScore();
                Destroy(gameObject);
            }
                
        }
    }
}
