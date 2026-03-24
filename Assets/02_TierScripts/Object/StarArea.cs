using UnityEngine;

public class StarArea : MonoBehaviour
{
    [SerializeField]
    private float Speed = 5.0f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMove3D Player = other.gameObject.GetComponent<PlayerMove3D>();
        if (Player != null )
        {
            Player.moveSpeed += Speed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerMove3D Player = other.gameObject.GetComponent<PlayerMove3D>();
        if (Player != null)
        {
            Player.moveSpeed -= Speed;
        }
    }
}
