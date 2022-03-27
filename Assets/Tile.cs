using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile : MonoBehaviour
{
    public GameObject mark;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void clicked()
    {
        if (transform.parent.GetComponent<ColorChange>().chose) return;
        mark.SetActive(true);
        transform.parent.GetComponent<ColorChange>().clicked(transform.GetSiblingIndex());
    }
}
