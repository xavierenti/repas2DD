using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float Speed;

    [SerializeField] Camera camera;

    private Vector2 cameraXBounds = Vector2.zero;

    private SpriteRenderer playerSpriteRenderer;

    private float playerSpriteWidth;

    private Rigidbody2D rb;

    private Vector2 horizontalmovement = Vector2.zero;

    [SerializeField] private float bulletSpawner;
    private float bulletcurrentTime = 0f;

    [SerializeField] private GameObject Bullet;

    [SerializeField] private GameObject[] Spawner;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //calculamos limites de camara
        cameraXBounds.x = camera.ViewportToWorldPoint(new Vector2(0, 1)).x;
        cameraXBounds.y = camera.ViewportToWorldPoint(new Vector2(1, 1)).x;

        playerSpriteRenderer = GetComponent<SpriteRenderer>();

        playerSpriteWidth = playerSpriteRenderer.bounds.size.x * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        //comprobacion imputs player
        horizontalmovement = new Vector2(Input.GetAxis("Horizontal"), 0f);

        //Disparamos si es necesario
        Shoot();

    }

    private void FixedUpdate()
    {
        //mover player
        moveCharacter(horizontalmovement);
    }

    private void moveCharacter(Vector2 direccion)
    {
        float finalPositionX =transform.position.x + (direccion.x * Speed * Time.fixedDeltaTime);
        finalPositionX = Mathf.Clamp(finalPositionX, cameraXBounds.x + playerSpriteWidth, cameraXBounds.y - playerSpriteWidth);

        rb.MovePosition(new Vector2(finalPositionX, rb.position.y));


    }

    private void Shoot()
    {
        bulletcurrentTime += Time.deltaTime;

        if(bulletcurrentTime > bulletSpawner)
        {
            bulletcurrentTime = 0;
            for(int i = 0; i < Spawner.Length; i++)
            {
                Instantiate(Bullet, Spawner[i].transform.position, Quaternion.identity);
            }
        }
    }
}
