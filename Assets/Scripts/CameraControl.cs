using UnityEngine;


public class CameraControl : MonoBehaviour
{
    public float vitDéfilement = 10f;
    private bool peutSeDéplacer = true;
    public SpriteRenderer limiteGauche;
    public SpriteRenderer limiteDroite;
    

    void Start()
    {

        
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("a") && pos.x > limiteGauche.transform.position.x)
        { pos.x -= vitDéfilement * Time.deltaTime;}

        if (Input.GetKey("d") && pos.x < limiteDroite.transform.position.x)
        { pos.x += vitDéfilement * Time.deltaTime;}

        transform.position = pos;

        
    }


}
