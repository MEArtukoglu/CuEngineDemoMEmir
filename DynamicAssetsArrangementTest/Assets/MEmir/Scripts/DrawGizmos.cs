using UnityEngine;


public class DrawGizmos : MonoBehaviour
{
    [SerializeField] private Color gizmosColor = Color.white;
    [SerializeField] private float size;


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawSphere(transform.position, size);
    }
#endif
}
