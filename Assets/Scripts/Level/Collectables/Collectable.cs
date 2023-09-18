using System;
using System.Collections;
using UnityEngine;

[SelectionBase]
public class Collectable : MonoBehaviour
{
    public float hoverMagnitude;
    public float hoverSpeed;
    private float startHeight;

    public float collectDuration;
    public static event Action OnCollectableGet;

    private void Start()
    {
        startHeight = transform.localPosition.y;
    }

    public float Hover()
    {
        return hoverMagnitude *  Mathf.Sin(Time.time * hoverSpeed);
    }

    private void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, startHeight + Hover(), transform.localPosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // OnCollectableGet?.Invoke();
            StartCoroutine(GetCollected(other));
            Debug.Log("Collectable Get");
        }
    }

    public IEnumerator GetCollected(Collider player)
    {
        float time = 0;

        while (time < collectDuration)
        {
            Vector3 shrink = Vector3.Lerp(player.transform.localScale, Vector3.zero, time / collectDuration);
            Vector3 pos = Vector3.Lerp(transform.localPosition, player.transform.localPosition, time / collectDuration);

            transform.localScale = shrink;
            transform.localPosition = pos;
            

            time += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
