using UnityEngine;

public class PropSpawner : MonoBehaviour
{
    public Rect[] locations;
    public GameObject[] props;
    public float spawnChance;
    public int spawnAttempts;
    public float yPos;
    public float yRange;
    public Vector3 rotationAxis;

    void Start()
    {
        for(int i = 0; i < spawnAttempts; i++)
        {
            if(Random.value < spawnChance)
            {
                Rect location = locations.Choose();
                GameObject prop = Instantiate(props.Choose(), transform);

                Vector3 pos = new Vector2(location.x, location.y) + new Vector2(Random.value*location.size.x - location.size.x/2f, Random.value*location.size.y - location.size.y/2f);
                pos.z = pos.y;
                pos.y = yPos + (Random.value-0.5f)*yRange;
                prop.transform.localPosition = pos;
                prop.transform.localRotation *= Quaternion.Euler(360f*Random.value*rotationAxis);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(locations != null)
        {
            Gizmos.color = Color.cyan;
            foreach(Rect location in locations)
            {
                Vector3 pos = location.position;
                pos.y = yPos;
                Gizmos.DrawWireCube(pos, new Vector3(location.width, Mathf.Max(1f, yRange*2f), location.height));
            }
        }
    }
}
