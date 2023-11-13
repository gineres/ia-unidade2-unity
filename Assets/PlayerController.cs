using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;

    private int fullHp = 100;
    private int hp;
    private int bombAmount = 3;

    [SerializeField] private Slider hpSlider;
    [SerializeField] private Text bombText;

    private float attackRadius = 2f;
    public LayerMask enemyLayer;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hp = fullHp;
        UpdateHealthBar();
        UpdateBombText();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        movement.Normalize();
        rb.velocity = new Vector2(movement.x * speed, movement.y * speed);
        DrawCircle(.01f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PerformAttack();
            bombAmount--;
            UpdateBombText();
        }
    }

    void PerformAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius, enemyLayer);
        

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Destroy(collider.gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Collectible")
        {
            Collectible collectible = other.gameObject.GetComponent<Collectible>();
            if (collectible.collectibleType == CollectibleType.HEAL)
            {
                hp += 20;
                if (hp > fullHp)
                {
                    hp = fullHp;
                }
                UpdateHealthBar();
            } else
            {
                bombAmount++;
                UpdateBombText();
            }

            Destroy(other.gameObject);
        } else if (other.tag == "Enemy")
        {
            hp -= 20;
            if (hp <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            UpdateHealthBar();
        } else if (other.tag == "Exit")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void UpdateHealthBar()
    {
        if (hpSlider != null)
        {
            hpSlider.value = hp;
        }
    }

    void UpdateBombText(){
        bombText.text = "Bomb: " + bombAmount.ToString();
    }

    public void DrawCircle(float lineWidth)
    {
        var segments = 360;
        var line = GetComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.positionCount = segments + 1;

        var pointCount = segments + 1; 
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * attackRadius, Mathf.Cos(rad) * attackRadius, 0);
        }

        line.SetPositions(points);
    }
}
