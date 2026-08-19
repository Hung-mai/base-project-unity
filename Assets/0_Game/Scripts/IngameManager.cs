using UnityEngine;

public class IngameManager : MonoBehaviour
{
    public static IngameManager ins;

    private void Awake()
    {
        ins = this;
    }
}
