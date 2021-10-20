using System.Collections;
using UnityEngine;

public class Portals : MonoBehaviour 
{
    [Header("Settings")]
	public GameObject PortalOutput;

    private Collider2D objectCollider;

	private void OnTriggerEnter2D(Collider2D other)
    {
        objectCollider = other;
        StartCoroutine(Teleport());
    }

    private IEnumerator Teleport()
    {
        yield return new WaitForSeconds(1f);
        objectCollider.transform.position = new Vector2(PortalOutput.transform.position.x, PortalOutput.transform.position.y);
    }
}
