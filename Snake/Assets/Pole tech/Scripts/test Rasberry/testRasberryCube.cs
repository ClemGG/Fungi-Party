using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testRasberryCube : MonoBehaviour
{
    public int id;
    public Joystick j;
    public KeyCode[] numPad;
    public Color[] colors;
    bool useIA;

    Transform t;



    float angle;
    float speed = (2 * Mathf.PI) / 5; //2*PI in degress is 360, so you get 5 seconds to complete a circle
    public float radius = 5;



    // Start is called before the first frame update
    void Start()
    {
        t = transform;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < numPad.Length; i++)
        {
            if (Input.GetKeyDown(numPad[i]))
            {
                ChangeColor(i);
            }
        }


        if (useIA)
        {
            angle += speed * Time.deltaTime; //if you want to switch direction, use -= instead of +=
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            t.position += new Vector3(x, y);

        }
        else
        {
            t.position += new Vector3(j.Horizontal * Time.deltaTime, j.Vertical * Time.deltaTime);
            t.position += new Vector3(Input.GetAxis($"Joystick{id}Horizontal") * Time.deltaTime, Input.GetAxis($"Joystick{id}Vertical") * Time.deltaTime);
        }
    }

    public void ChangeColor(int i)
    {
        GetComponent<SpriteRenderer>().color = colors[i];
    }

    public void ActiverIA()
    {
        useIA = !useIA;
    }
}
