using UnityEngine;

public class MyGameObject : MonoBehaviour
{
    [field: SerializeField] public MyGameObjectType Type { get; private set; }

    public void Move(Vector2 position)
    {
        transform.position = position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.28f);
    }
}
