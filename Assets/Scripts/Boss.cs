using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    // zone
    public GameObject redZoneObject;
    public GameObject yellowZoneObject;
    public GameObject blackZoneObject;
    public GameObject orangeZoneObject;
    public GameObject magentaZoneObject;
    public GameObject cyanZoneObject;

    // private require component from zone
    private Rigidbody2D yellowZoneRigidbody2D;
    private BoxCollider2D redZoneCollider;
    private SpriteRenderer redZoneSpriteRenderer;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Text healthText;

    private Color originColor;

    private Vector2 yellowZoneDirection;

    private bool[,] map = new bool[5, 5]
    {
        { false, false, false, false, false },
        { false, false, false, false, false },
        { false, false, false, false, false },
        { false, false, false, false, false },
        { false, false, false, false, false }
    };

    private float respawnTimer = 0.75f;

    private Vector3 originBossPosition;
    private Vector3 originBossScale;

    private float currentBossSpeed = 0.9f;

    [SerializeField]
    private PlayerAttack playerAttack;

    private int health;
    [SerializeField]
    private int maxHealth;

    // 플레이어가 이동하는 중인지 검사하기 위한 스크립트
    [SerializeField]
    private JoyStickHandle joyStickHandle;

    private void Awake()
    {
        redZoneCollider = redZoneObject.GetComponent<BoxCollider2D>();
        redZoneSpriteRenderer = redZoneObject.GetComponent<SpriteRenderer>();
        yellowZoneRigidbody2D = yellowZoneObject.GetComponent<Rigidbody2D>();

        originBossPosition = transform.position;
        originBossScale = transform.localScale;

        health = maxHealth;

        originColor = spriteRenderer.color;
    }

    private IEnumerator Start()
    {
        StartCoroutine(YellowZoneMovement());
        StartCoroutine(BlackZoneProcess());
        StartCoroutine(OrangeZoneRotation());
        StartCoroutine(MagentaZoneMovement());
        StartCoroutine(CyanZoneProcess());
        StartCoroutine(BossRoutine());

        while (true)
        {
            if (GameManager.instance.isGameStart)
            {
                redZoneSpriteRenderer.color = Color.gray;
                redZoneCollider.enabled = false;

                SpawnRedZone();
            }

            yield return new WaitForSeconds(respawnTimer * 0.5f);

            redZoneSpriteRenderer.color = Color.red;
            redZoneCollider.enabled = true;

            yield return new WaitForSeconds(respawnTimer * 0.5f);
        }
    }

    #region private IEnumerator - Zone

    // 가장자리에서 움직이기
    private IEnumerator YellowZoneMovement()
    {
        float timer = 0f;

        while (true)
        {
            if (GameManager.instance.isGameStart)
            {
                if (timer > 1.75f)
                {
                    yellowZoneDirection = Vector2.right * Random.Range(-0.5f, 0.5f) + Vector2.up * Random.Range(-0.5f, 0.5f);

                    timer = 0f;
                }

                yellowZoneRigidbody2D.AddForce(yellowZoneDirection, ForceMode2D.Impulse);

                timer += Time.deltaTime;
            }

            yield return null;
        }
    }

    // 왼쪽 위부터 오른쪽 아래에서 9로 나눈 위치 중 랜덤으로 이동과 보임 / 안보임
    private IEnumerator BlackZoneProcess()
    {
        // Left Top ~ Right Bottom
        Vector3[] movePositions = new Vector3[9]
        {
            Vector3.up * 4f,
            Vector3.right * 2f + Vector3.up * 4f,
            Vector3.one * 4f,
            Vector3.up * 2f,
            Vector3.one * 2f,
            Vector3.right * 4f + Vector3.up * 2f,
            Vector3.zero,
            Vector3.right * 2f,
            Vector3.right * 4f
        };

        while (true)
        {
            if (GameManager.instance.isGameStart)
            {
                blackZoneObject.transform.position = movePositions[Random.Range(0, movePositions.Length)];

                blackZoneObject.SetActive(Random.Range(0, 2) == 1);

                yield return new WaitForSeconds(Random.Range(0.75f, 4.75f));
            }

            yield return null;
        }
    }

    // 움직이지 않고, 회전을 함
    private IEnumerator OrangeZoneRotation()
    {
        float multiplyValue = 1.5f;
        float timer = 0f;

        while (true)
        {
            if (GameManager.instance.isGameStart)
            {
                if (timer >= 1.5f)
                {
                    timer -= 1.5f;

                    multiplyValue = Random.Range(-10f, 10f);
                }

                orangeZoneObject.transform.eulerAngles += Vector3.forward * multiplyValue;
            }

            yield return null;
        }
    }

    // 플레이어가 이동하지 않으면 플레이어한테 따라감
    private IEnumerator MagentaZoneMovement()
    {
        Vector3 start = transform.position;

        Vector3 end = Player.GetPosition();

        float t = 0f;

        while (true)
        {
            if (GameManager.instance.isGameStart)
            {
                if (joyStickHandle.Direction == Vector3.zero)
                {
                    start = transform.position;

                    end = Player.GetPosition();

                    while (joyStickHandle.Direction == Vector3.zero && t >= 1f)
                    {
                        transform.position = Vector3.Lerp(start, end, t);

                        t += Time.deltaTime * 0.1f;

                        yield return null;
                    }
                }
                else
                {
                    t = 0f;
                }
            }

            yield return null;
        }
    }

    // 그린존 + 오렌지존
    private IEnumerator CyanZoneProcess()
    {
        float multiplyValue = 1.5f;
        float timer = 0f;

        while (true)
        {
            if (GameManager.instance.isGameStart)
            {
                if (timer >= 1.5f)
                {
                    timer -= 1.5f;

                    multiplyValue = Random.Range(-10f, 10f);

                    cyanZoneObject.SetActive(Random.Range(0, 2) == 0);
                }

                cyanZoneObject.transform.eulerAngles += Vector3.forward * multiplyValue;

                timer += Time.deltaTime;
            }

            yield return null;
        }
    }

    #endregion

    private IEnumerator BossRoutine()
    {
        while (true)
        {
            if (GameManager.instance.isGameStart)
            {
                healthText.text = $"BOSS HEALTH : {health}";

                transform.position += Vector3.down * currentBossSpeed * Time.deltaTime;
                transform.eulerAngles += Vector3.forward * currentBossSpeed;

                if (transform.position.y < 0f)
                {
                    currentBossSpeed += 0.25f;

                    if (currentBossSpeed > 5f)
                    {
                        currentBossSpeed = 0.9f;

                        yield return new WaitForSeconds(0.5f);

                        transform.position = Vector3.right * 2f;
                        transform.localScale = Vector3.one * 0.25f;

                        yield return new WaitForSeconds(3f);

                        transform.localScale = originBossScale;


                        yield return new WaitForSeconds(0.5f);
                    }

                    transform.position = originBossPosition;

                    yield return new WaitForSeconds(3.25f);
                }
            }

            yield return null;
        }
    }

    private IEnumerator TakeDamage(int damage)
    {
        playerAttack.Stop();

        spriteRenderer.color = Color.red;

        health -= damage;

        yield return new WaitForSeconds(1.25f);

        spriteRenderer.color = originColor;
    }

    private void SpawnRedZone()
    {
        if (!redZoneObject.activeSelf)
        {
            redZoneObject.SetActive(true);
        }

        int ranX = Random.Range(0, 5);
        int ranY = Random.Range(0, 5);

        if (GetAllRedZone())
        {
            if (respawnTimer <= 0.5f)
            {
                respawnTimer = 3f;

                return;
            }

            respawnTimer -= 0.1f;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    map[i, j] = false;
                }
            }

            return;
        }
        else if (map[ranX, ranY])
        {
            SpawnRedZone();
        }
        else
        {
            map[ranX, ranY] = true;

            redZoneObject.transform.position = new Vector3(ranX, ranY, 0f);
        }
    }

    // 모든 곳이 전부 한 번씩 레드 존이 되면 true
    private bool GetAllRedZone()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (!map[i, j])
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // PlayerAttack Layer
        if (collision.gameObject.layer == 8)
        {
            StartCoroutine(TakeDamage(GameManager.instance.player.damage));
        }
    }
}
