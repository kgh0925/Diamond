/*using System.Collections.Generic;
using UnityEngine;

public class WeaponHitBox : MonoBehaviour
{
    [Header("히트박스 생명주기")]
    [Tooltip("히트박스가 살아있는 시간")]
    [SerializeField] private float LifeTime = 0.1f;

    [Header("타격 대상 필터")]
    [Tooltip("이 레이어에 속한 대상만 공격 가능")]
    [SerializeField] private LayerMask TargetMask;

    [Header("방향별 히트박스 크기")]
    [Tooltip("좌우 공격일 때 사용할 히트박스 크기")]
    [SerializeField] private Vector2 HorizontalSize = new Vector2(1.2f, 0.8f);

    [Tooltip("상하 공격일 때 사용할 히트박스 크기")]
    [SerializeField] private Vector2 VerticalSize = new Vector2(0.8f, 1.2f);

    [Header("디버그")]
    [SerializeField] private bool ShowDebug = true;

    private int Damage;

    private GameObject Owner;

    private Vector2 AttackDirection;

    private HashSet<Collider2D> HitTargets = new HashSet<Collider2D>();
    private BoxCollider2D boxCollider2D;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;
    }

    private void Start()
    {
        Destroy(gameObject, LifeTime);
    }

    public void Initialize(int damage, Vector2 attackdirection, GameObject owner)
    {
        this.Damage = damage;
        this.AttackDirection = attackdirection;
        this.Owner = owner;
        if (ShowDebug)
        {
            Debug.Log($"MeleeHitBox 초기화" + $"| damage : {damage}," + $" dir : {attackdirection}");
        }

    }

    private void ApplyHitBoxShapeByDirection()
    {
        if (Mathf.Abs(AttackDirection.x) > Mathf.Abs(AttackDirection.y))
        {
            boxCollider2D.size = HorizontalSize;
        }
        else
        {
            boxCollider2D.size = VerticalSize;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Owner) return;
        if (((1 << collision.gameObject.layer) & TargetMask) == 0)
        {
            return;
        }

        if (HitTargets.Contains(collision))
        {
            return;
        }

        HitTargets.Add(collision);

        HealthPointManager Hp = collision.gameObject.GetComponent<HealthPointManager>();
        if (Hp != null && !Hp.IsDead)
        {
            Hp.TakeDamage(Damage);

            if (ShowDebug)
            {
                Debug.Log($"{collision.gameObject.name} 타격 성공! 데미지 {Damage}");
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector2 SizeToDraw = HorizontalSize;

        if (Mathf.Abs(AttackDirection.y) >= Mathf.Abs(AttackDirection.x))
        {
            SizeToDraw = VerticalSize;
        }

        Gizmos.DrawWireCube(transform.position, SizeToDraw);
    }
}
*/