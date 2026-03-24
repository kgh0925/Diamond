using UnityEngine;

public class HeartObject : MonoBehaviour
{
    [Header("회복 수치 설정")]
    [SerializeField]
    private float Heal_Stats;

    public float GetHeal()
    {
        return Heal_Stats;
    }
}
