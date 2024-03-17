using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private JoyStickHandle joyStickHandle;

    private static new Transform transform;

    public static Func<Vector3> GetPosition = () => transform.position;

    public float speed;

    private bool isDead;
    private bool isHit;
    private bool onBossParticle;

    private int hp = 6555;
    public int HP
    {
        get
        {
            return hp;
        }
    }

    private float timer;
    private float dam = 5f;

    public int damage;

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

    private IEnumerator Start()
    {
        while (true)
        {
            if (GameManager.instance.isGameStart)
            {
                CheckHealth();

                if (!isDead)
                {
                    Move();
                }
                else
                {
                    SceneManager.LoadScene(1);
                }

                timer += Time.deltaTime;

                if (timer >= 1f)
                {
                    timer = 0f;
                    dam *= 1.15f;
                }
            }

            yield return null;
        }
    }

    private void CheckHealth()
    {
        if (hp <= 0)
        {
            isDead = true;
        }
    }

    private void Move()
    {
        Vector3 dir = joyStickHandle.Direction;

        if (dir != Vector3.zero)
        {
            transform.position += dir.normalized * speed * Time.deltaTime;
        }
    }

    private IEnumerator OnDamage(int rem)
    {
        isHit = true;
        hp -= rem;

        yield return new WaitForSeconds(0.5f);

        isHit = false;
    }

    private IEnumerator OnBossParticleDamage()
    {
        hp -= UnityEngine.Random.Range(4, 7);

        yield return new WaitForSeconds(0.5f);

        onBossParticle = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            StartCoroutine(OnDamage(40));
        }

        if (!isHit)
        {
            if (collision.CompareTag("RedZone"))
            {
                StartCoroutine(OnDamage(Mathf.RoundToInt(dam)));
            }

            if (collision.CompareTag("YellowZone"))
            {
                StartCoroutine(OnDamage(3));
            }

            if (collision.CompareTag("BlackZone"))
            {
                StartCoroutine(OnDamage(hp >> 1));
            }

            if (collision.CompareTag("OrangeZone"))
            {
                StartCoroutine(OnDamage((hp >> 4) + 10));
            }

            if (collision.CompareTag("MagentaZone"))
            {
                StartCoroutine(OnDamage(Mathf.RoundToInt(dam * dam)));
            }
        }

        if (collision.CompareTag("GreenZone"))
        {
            hp += 2;
        }

        if (collision.CompareTag("CyanZone"))
        {
            hp += 3;
        }
    }

    private void OnParticleTrigger()
    {
        if (!onBossParticle)
        {
            onBossParticle = true;

            StartCoroutine(OnBossParticleDamage());
        }
    }
}
