using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_Projectile : MonoBehaviour
{
    public float impulseForce = 0;
    public float explodeMultiplier = 3;
    Rigidbody rb;

    float scale;
    bool explode = false;
    bool check = false;

    // Start is called before the first frame update
    void Start()
    {
        //Quaternion r = Quaternion.Euler(-65, 0, 0);
       //transform.rotation = r;

        rb = this.GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * impulseForce * 100 * Time.deltaTime, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        //Quaternion r = Quaternion.Euler(-65, 0, 0);
        //transform.rotation = r;

        if(transform.position.y <= 0 && check == false)
        {
            this.GetComponent<Rigidbody>().isKinematic = true;

            Transform tf = this.transform;
            this.transform.position = new Vector3(tf.position.x, -0.01f, tf.position.z);
            explode = true;
            check = true;
        }

        if(explode == true)
        {
            scale += Time.deltaTime * explodeMultiplier;

            transform.localScale = new Vector3(scale, scale, scale);
        }
        if(scale >= 3)
        {
            Destroy(this.gameObject);
        }
    }

}
