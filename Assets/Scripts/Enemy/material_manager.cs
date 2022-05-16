using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class material_manager : MonoBehaviour
{
    public float offset;
    public SpriteRenderer rend;
    private Material init_mat;
    public Material spiritMat;
    public bool animar;
    // Start is called before the first frame update
    void Start()
    {
        //spiritMat = Resources.Load<Material>("materiales/spirited.mat");
        rend = GetComponent<SpriteRenderer>();
        init_mat = rend.material;
        offset = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (animar) {
            offset += Time.unscaledDeltaTime * 2;
            if (offset > 2f)
            {
                offset = -1;
            }

            rend.material.SetFloat("_offsetY", offset);
            rend.material.SetFloat("_offsetX", -offset);
        }
    }

    public void ActivarMaterialEspiritu() {
        rend.material = spiritMat;
        spiritMat.SetTexture("_MainTex", rend.sprite.texture);
        animar = true;
        offset = -1;
    }

    public void DesActivarMaterialEspiritu()
    {
        rend.material = init_mat;
        animar = false;
    }
}
