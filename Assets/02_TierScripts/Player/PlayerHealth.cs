using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("플레이어 기본 설정")]
    [SerializeField]
    private float CurrentHp;
    [SerializeField]
    private float MaxHp;
    public bool IsFullHpStart;
    [Header("UI 연결")]
    public Slider HealthBar;
    [Header("테스트 데미지 받기")]
    public InputAction Damageable;
    bool IsDead => CurrentHp <= 0;
    AniController animator;
    private void Awake()
    {
        
        if(IsFullHpStart)
        {
            CurrentHp = MaxHp;
        }
        if(CurrentHp <= 0)
        {
            CurrentHp = 1;
        }
        HealthBar.maxValue = MaxHp;
        HealthBar.value = CurrentHp;
        animator = GetComponentInChildren<AniController>();
    }
    private void OnValidate()
    {
        if (CurrentHp > MaxHp)
        {
            CurrentHp = MaxHp;
        }

        if (MaxHp < 1)
        {
            MaxHp = 1;
        }

        if (CurrentHp < 0)
        {
            CurrentHp = 0;
        }
    }
    private void OnEnable()
    {
        Damageable.Enable();
    }
    private void OnDisable()
    {
        Damageable.Disable();
    }
    private void Update()
    {
        HealthBar.value = CurrentHp;
        if(Damageable.WasPerformedThisFrame())
        {
            TakeDamage(10);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        HeartObject Heart = other.GetComponent<HeartObject>();
        if (Heart == null) return;
        if (Heart != null)
        {
            float Heal = Heart.GetHeal();
            if (Heal <= 0) return;
            CurrentHp = Mathf.Min(CurrentHp + Heal, MaxHp);
        }
        other.gameObject.SetActive(false);
    }

    public void TakeDamage(float amount)
    {
        CurrentHp = Mathf.Max(0, CurrentHp - amount);
        if(animator != null)animator.Hit();
    }
}
