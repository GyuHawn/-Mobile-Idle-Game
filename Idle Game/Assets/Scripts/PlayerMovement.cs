using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // ü��

    // ���� ������

    public float spd;
    public Vector2 inputVec;

    private Rigidbody2D rigib;

    public Transform pos;
    public Vector2 BoxSize;

    private void Awake()
    {
        rigib = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        spd = 0.2f;
    }

    private void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        //���ݹ߻�

        //�ڽ��ȿ� ���� �߽߰� ���� ������ ����
    }

    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * spd;
        rigib.MovePosition(rigib.position + nextVec);
    }

    // ���� ������ ������

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, BoxSize);
    }
}
