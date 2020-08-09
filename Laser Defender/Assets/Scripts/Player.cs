using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField ] float xPadding = 0.1f;
    [SerializeField] float yPadding = 0.1f;
    [SerializeField] private GameObject laserPrefab;
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;
    // Start is called before the first frame update
    void Start()
    {
        SetUpBoundries();
    }

    private void SetUpBoundries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yPadding;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {   
        
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX,xMin,xMax);
        
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY,yMin,yMax);

        transform.position = new Vector2(newXPos,newYPos);
    }
}
