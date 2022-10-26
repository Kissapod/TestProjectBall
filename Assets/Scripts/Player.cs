using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Camera myCamera;
    [SerializeField] private float speed;
    [SerializeField] private GameObject explosion;

    private LineDraw lineDraw;
    private Vector2 direction;
    private Queue<Vector2> movesPoints = new Queue<Vector2>();
    private GameManager gameManager;
    void Start()
    {
        lineDraw = GetComponentInChildren<LineDraw>();
        gameManager = FindObjectOfType<GameManager>();
        direction = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        ClickMouseButton();
        ChangeOfPosition();
    }

    private void ChangeOfPosition()
    {
        if (movesPoints.Count > 0)
        {
            Vector2 point = movesPoints.Peek();
            if (new Vector2(transform.position.x, transform.position.y) != point)
            {
                lineDraw.DrawLine(new Vector2(transform.position.x, transform.position.y));
                transform.position = Vector2.MoveTowards(transform.position, point, speed * Time.deltaTime);
            }
            else movesPoints.Dequeue();
        }
    }

    private void ClickMouseButton()
    {
        if (Input.GetMouseButtonDown(0))
        {
            direction = myCamera.ScreenToWorldPoint(Input.mousePosition);
            movesPoints.Enqueue(direction);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("collision");
        GameObject collisionObject = collider.gameObject;
        if (collisionObject.tag == "Coin")
        {
            gameManager.CoinCounter();
            collisionObject.SetActive(false);
        }
        else if (collisionObject.tag == "Spikes")
        { 
            gameObject.SetActive(false);
            GameObject boom = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(boom, 5f);
            gameManager.ViewEndGameDisplay();
        }
    }

    public void ClearMovePoints()
    {
        movesPoints.Clear();
    }
}
