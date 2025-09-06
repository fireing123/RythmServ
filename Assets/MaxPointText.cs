using TMPro;
using UnityEngine;

public class MaxPointText : MonoBehaviour
{
    TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        text.text = PointCountor.ReadPoint().ToString();
    }
}
