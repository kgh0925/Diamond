using UnityEngine;

public class ShopIN : MonoBehaviour
{
    [SerializeField] Transform View_UI;
    [SerializeField] Transform Shop_UI;
    private bool InArea;
    public bool Area => InArea;
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
            View_UI.gameObject.SetActive(true);
            InArea = true;
            Debug.Log("감지완료");
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            View_UI.gameObject.SetActive(false);
            InArea = false;
        }
    }
}
