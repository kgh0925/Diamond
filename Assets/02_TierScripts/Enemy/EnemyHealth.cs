using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour, IDamageable
{
    [Header("UI 연결")]
    public Slider HealthBar;
    [Header("몹 체력 설정")]
    [SerializeField]
    private float CurrentHp;
    [SerializeField]
    private float MaxHp;

    [SerializeField]
    private GameObject X_bot;

    private BoxCollider box;
    private bool IsDead => CurrentHp <= 0;
    private bool resurrection = false;
    private EnemyController enemy;

    public void TakeDamage(float amount)
    {
        CurrentHp = Mathf.Max(0, CurrentHp - amount);
        enemy.Hit();
    }

    private void Awake()
    {
        CurrentHp = MaxHp;
        HealthBar.maxValue = MaxHp;
        HealthBar.value = CurrentHp;
        enemy = GetComponentInChildren<EnemyController>();
        box = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        CurrentHp = MaxHp;
        HealthBar.maxValue = MaxHp;
        HealthBar.value = CurrentHp;
    }

    private void Update()
    {
        HealthBar.value = CurrentHp;
        if(IsDead && !resurrection)
        {
            enemy.Death();
            StartCoroutine(Death());
        }
        /*if(HealthBar.value <= 0)
        {
            HealthBar.image = null;
        }*/
    }

    //원하는 방식으로 구현이 잘 안되어서 기능 제외 [ 오브젝트 비활성 후 일정 시간 후 다시 재생성 ]
    IEnumerator Death()
    {
        resurrection = true;
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
        /*        resurrection = true;
                box.enabled = false; 
                yield return new WaitForSeconds(3.0f);
                X_bot.SetActive(false);
                box.enabled = true; ;
                yield return new WaitForSeconds(10.0f);
                X_bot.SetActive(true);
                resurrection = false;*/
    }

}
