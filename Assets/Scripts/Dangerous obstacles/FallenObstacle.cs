using System.Collections;
using UnityEngine;

public class FallenObstacle : MonoBehaviour 
{
	private Rigidbody2D _rigidbody;

	private void Start () 
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}
	
	private void OnTriggerEnter2D(Collider2D collision)
    {
		if(collision.gameObject.name.Equals("Player"))
        {
			_rigidbody.isKinematic = false;
			StartCoroutine(DeleteObject());
        }
    }

	private IEnumerator DeleteObject()
    {
		yield return new WaitForSeconds(2.5f);
		Destroy(gameObject);
	}
}
