using UnityEngine;

[ExecuteAlways]
public class HpBar : MonoBehaviour
{
    public float scale;
    public Hp hp;
    public Vector3 offset;

    private void Update()
    {
        if (hp == null) return;
        float ratio = hp.GetHpPercent() * scale;
        transform.localScale = new Vector3(ratio, transform.localScale.y, 1);
        transform.position = hp.transform.position + offset;
    }
}
