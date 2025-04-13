using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public string playerTag = "Player"; // Assign this tag to the local player object
    public Vector3 offset = new Vector3(0, 2, -5);
    public float smoothSpeed = 5f;

    private Transform target;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogWarning("FollowPlayer: No player found with tag '" + playerTag + "'");
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
            transform.LookAt(target); // optional, camera looks at the player
        }
    }
}
