using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialScript : MonoBehaviour
{
    static int counter = 0;
    SpriteRenderer sr2D;
    public Material wall1, wall2; // raczej od wywalenia chyba ¿e chcemy zmieniaæ kolory tekstur 
    public Sprite wallSprite; // przerobiæ na listê lub przekazywaæ przez osobn¹ metodê 
    
    // Start is called before the first frame update
    void Start()
    {
        sr2D = GetComponent<SpriteRenderer>();
        if (counter % 2 == 0)
            sr2D.material = wall1;
        else
            sr2D.material = wall2;
        sr2D.sprite = wallSprite;
        counter++;
    }
}
