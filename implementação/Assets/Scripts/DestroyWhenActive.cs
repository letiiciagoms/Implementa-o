using UnityEngine;

public class DestroyWhenActive : MonoBehaviour
{
    public GameObject alvo;

    void OnEnable()
    {
        if (alvo != null)
        {
            Destroy(alvo);
        }
    }
}

