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

    private ParticleSystem flashFX;
    public static event Action OnCollectableGet;

    private void Start()
    {
        startHeight = transform.localPosition.y;
        flashFX = GetComponentInChildren<ParticleSystem>();
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
            flashFX.Stop();
            StartCoroutine(GetCollected(other));
            Debug.Log("Collectable Get");
        }
    }

    public IEnumerator GetCollected(Collider player)
    {
        float time = 0;
        transform.SetParent(player.transform.parent.transform);
        startHeight = 0;
        transform.position = Vector3.zero;

        while (time < collectDuration)
        {
            Vector3 shrink = Vector3.Lerp(player.transform.localScale, Vector3.zero, time / collectDuration);

            transform.localScale = shrink;
            time += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
