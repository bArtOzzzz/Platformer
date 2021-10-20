using System.Collections;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [Header("Settings")]
    public float damage;

    // Other
    private IDamagable damagable;
    private bool isCanDamaged;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        damagable = collision.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(damage);
            isCanDamaged = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        damagable = null;
        isCanDamaged = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        damagable = collision.GetComponent<IDamagable>();
        if (isCanDamaged)
            StartCoroutine(LoopDamage());
    }

    private IEnumerator LoopDamage()
    {
        isCanDamaged = false;
        yield return new WaitForSeconds(1f);
        if(damagable != null)
        {
            damagable.TakeDamage(damage);
            isCanDamaged = true;
        }
        else
        {
            damagable = null;
            isCanDamaged = false;
        }
    }
}
