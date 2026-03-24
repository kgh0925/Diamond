using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public InputAction Attack;
    public float Damage;
    public bool IsAttack;
    private HashSet<IDamageable> Target = new HashSet<IDamageable>();
    private MeshCollider Mesh;
    public LayerMask LayerMask;
    [Header("두 Time의 합은 2.2")]
    public float TestTime1;
    public float TestTime2;
    private void Awake()
    {
        Mesh = GetComponent<MeshCollider>();
        Target.Clear();
    }
    private void OnEnable()
    {
        Attack.Enable();
        
    }

    private void OnDisable()
    {
        Attack.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (Attack.WasPerformedThisFrame() && !IsAttack)
        {
            StartCoroutine(AttackCheck());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("감지확인 오더부터" + other.name);
        if (IsAttack && (((1 << other.gameObject.layer) & LayerMask) != 0))
        {
            //Debug.Log("감지확인" + other.name);
            IDamageable target = other.GetComponent<IDamageable>();
            if (target != null && !Target.Contains(target))
            {
                Target.Add(target);
                target.TakeDamage(Damage);
            }
           
        }
    }

    IEnumerator AttackCheck()
    {
        //Animation TIme : 2.2f
        IsAttack = true;
        yield return new WaitForSeconds(TestTime1);
        Mesh.enabled = true;
        yield return new WaitForSeconds(TestTime2);
        Mesh.enabled = false;
        IsAttack = false;
        Target.Clear();
    }
}
