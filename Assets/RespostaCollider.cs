using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class RespostaCollider : MonoBehaviour
{
    public string objetoTag = "Correto";

    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == objetoTag)
        {
            GrudaNaPosicaoCentro(collision.gameObject);
            DesativaComponentes();
            AlteraTagDoObjeto(collision.gameObject, "Respondido");
        }
    }

    void GrudaNaPosicaoCentro(GameObject objeto)
    {
        Quaternion rotacaoFixa = Quaternion.Euler(0f, 90f, 0f);
        transform.rotation = rotacaoFixa;
        Vector3 posicaoCentro = objeto.transform.position;
        transform.position = posicaoCentro;
    }

    void DesativaComponentes()
    {
        if (grabInteractable != null)
        {
            grabInteractable.enabled = false;
        }

        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }
    void AlteraTagDoObjeto(GameObject objeto, string novaTag)
    {
        Debug.Log("Alterando a tag do objeto para " + novaTag + "!");
        objeto.tag = novaTag;
    }

}