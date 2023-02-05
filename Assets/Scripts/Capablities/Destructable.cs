using System.Collections;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public new ParticleSystem  particleSystem;
    private SpriteRenderer boulderSprite;
    public float LerpTime=0.7f;
    bool lerpit;
    private void Start()
    {
        boulderSprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (lerpit)
        {
            boulderSprite.color = Color.Lerp(boulderSprite.color, new Color(1f, 1f, 1f, 0f), LerpTime * Time.deltaTime);
        }
    }

    public void DestroyBoulder()
    {
        particleSystem.Play();
        lerpit = true;


        StartCoroutine(DestroyObj());
    }
    IEnumerator DestroyObj()
    {

        yield return new WaitForSeconds(0.8f);

        Object.Destroy(this.gameObject);
    }
}
