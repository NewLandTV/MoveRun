using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject playerAttackArea;

    private bool isAttack;

    private Coroutine attackCoroutine;

    public void OnAttackButtonClick()
    {
        if (!isAttack)
        {
            isAttack = true;

            attackCoroutine = StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        float timer = 0f;

        playerAttackArea.SetActive(true);

        yield return new WaitForSeconds(0.25f);

        Vector3 moveDirection = Vector3.right * Random.Range(-1, 2) + Vector3.up * Random.Range(-1, 2);

        while (isAttack && timer < 1f)
        {
            playerAttackArea.transform.position += moveDirection * Random.Range(0.5f, 5f) * Time.deltaTime;

            timer += Time.deltaTime;
        }

        playerAttackArea.SetActive(false);

        playerAttackArea.transform.localPosition = Vector3.zero;

        yield return new WaitForSeconds(4f);

        isAttack = false;
    }

    public void Stop()
    {
        StopCoroutine(attackCoroutine);

        playerAttackArea.SetActive(false);

        playerAttackArea.transform.localPosition = Vector3.zero;

        isAttack = false;
    }
}
