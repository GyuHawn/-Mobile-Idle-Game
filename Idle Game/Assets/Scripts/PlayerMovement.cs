using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // 체력

    // 공격 프리팹

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

        //공격발사

        //박스안에 몬스터 발견시 공격 생성후 공격
    }

    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * spd;
        rigib.MovePosition(rigib.position + nextVec);
    }

    // 공격 입을시 데미지

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, BoxSize);
    }
}
