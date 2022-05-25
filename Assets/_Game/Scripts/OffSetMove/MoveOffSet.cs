using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOffSet : MonoBehaviour
{
    [SerializeField] private float speed;
    private Material currentMaterial;
    private float offSet;
    // Start is called before the first frame update
    void Start()
    {
        currentMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offSet += speed * Time.deltaTime;

        currentMaterial.SetTextureOffset("_MainTex", new Vector2(offSet, 0));
    }
}
