using UnityEngine;
using UnityEngine.SceneManagement;

public class Phantoms : MonoBehaviour
{
    public Phantom[] prefabs;

    public int rows = 5;
    public int columns = 11;
    public AnimationCurve speed;
    public Projectile missilePrefab;
    public float missileAttackRate = 1.0f;

    public int amountKilled { get; private set; }
    public int amountAlive => this.totalPhantoms - this.amountKilled;
    public int totalPhantoms => this.rows * this.columns;
    public float percentKilled => this.amountKilled / this.totalPhantoms;

    private Vector3 _direction = Vector2.right;
  
    private void Awake()
    {
        for (int row = 0; row < this.rows; row++)
        {
            float width = 2.0f * (this.columns - 1);
            float height = 2.0f * (this.rows - 1);
            Vector2 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 2.0f), 0.0f);

            for (int col = 0; col < this.columns; col++)
            {
                Phantom phantom = Instantiate(this.prefabs[row], this.transform);
                phantom.killed += PhantomKilled;

                Vector3 position = rowPosition;
                position.x += col * 2.0f;
                phantom.transform.localPosition = position;
            }
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(MissileAtack), this.missileAttackRate, this.missileAttackRate);
    }
    private void Update()
    {
        this.transform.position += _direction * this.speed.Evaluate(this.percentKilled) * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);


        foreach (Transform phantom in this.transform)
        {
            if (phantom.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (_direction == Vector3.right && phantom.position.x >= (rightEdge.x - 1.0f))
            {
                AdvanceRow();
            }
            else if (_direction == Vector3.left && phantom.position.x <= (leftEdge.x))
            {
                AdvanceRow();
            }
        }
    }

    private void AdvanceRow()
    {
        _direction.x *= -1.0f;

        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
    }

    private void MissileAtack()
    {
        foreach (Transform phantom in this.transform)
        {
            if (!phantom.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (Random.value < (1.0f / (float)this.amountAlive))
            {
                Instantiate(this.missilePrefab, phantom.position, Quaternion.identity);
                break;
            }
        }
    }

    private void PhantomKilled()
    {
        this.amountKilled++;

        if (this.amountKilled >= this.totalPhantoms)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

  
}
