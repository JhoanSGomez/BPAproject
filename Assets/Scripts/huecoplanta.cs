using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class huecoplanta : MonoBehaviour
{
    private bool jugadorEnContacto; // Variable para rastrear si el jugador está en contacto
    public KeyCode teclaActivacion = KeyCode.Space; // Tecla para activar la animación
    public GameObject colino;
    public GridLayoutGroup gridLayout;
    [SerializeField] GameObject plantillaItemsComprados;
    public List<GameObject> itemList = new List<GameObject>();


    void Update()
    {

        if (jugadorEnContacto && Input.GetKeyDown(teclaActivacion))
        {
            ActivarAnimacionElemento();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hice colicion OnTriggerEnter2D");

        if (collision.CompareTag("Player"))
        {
            jugadorEnContacto = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("hice colicion OnTriggerExit2D");
        if (collision.CompareTag("Player"))
        {
            jugadorEnContacto = false;
        }
    }

    private void ActivarAnimacionElemento()
    {
        if (jugadorEnContacto)
        {
            Transform parentTransform = transform.parent;
            itemBuyInformation itemAbono = GameManager.Instance.informacionBuyItems.Find(x => x.titulo == "Abono");
            itemBuyInformation itemColino = GameManager.Instance.informacionBuyItems.Find(x => x.titulo == "Colino");

            if (itemAbono != null && itemColino != null){
                if (itemAbono.cantidad >= 1 && itemColino.cantidad >= 1){
                    Destroy(gameObject);
                    GameManager.Instance.RestarCantidadPorTitulo(itemAbono.titulo, 1);
                    GameManager.Instance.RestarCantidadPorTitulo(itemColino.titulo, 1);
                    refrescarItems();
                    GameObject nuevoElemento = Instantiate(colino, transform.position+ new Vector3(0f, 1, 0f), Quaternion.identity);
                    if (parentTransform != null){
                        nuevoElemento.transform.SetParent(parentTransform);
                    }
                }else{
                    Debug.Log($"No se encontro las cantidades necesarias");
                }
            }else{
                Debug.Log($"No se encontro uno de los items");
            }
        }
    }

    public void refrescarItems()
    {
        // Encuentra todos los GameObjects con la etiqueta "objetosComprado"
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("objetosComprado");

        // Itera a través de los GameObjects encontrados
        foreach (GameObject obj in taggedObjects)
        {
            // Intenta obtener el componente GridLayoutGroup
           this.gridLayout = obj.GetComponent<GridLayoutGroup>();

            // Verifica si el componente fue encontrado
            if (this.gridLayout != null)
            {
                RefreshGridLayoutGroup();
                // Realiza acciones con el GridLayoutGroup encontrado
                Debug.Log("Se encontró el GridLayoutGroup en el GameObject con la etiqueta objetosComprado.");
                // Puedes realizar más operaciones aquí según tus necesidades
            }
            else
            {
                // El componente GridLayoutGroup no fue encontrado en este GameObject
                Debug.LogWarning("No se encontró el GridLayoutGroup en el GameObject con la etiqueta objetosComprado.");
            }
        }
    }

    public void RefreshGridLayoutGroup()
    {
        var plantillaItem = plantillaItemsComprados.GetComponent<PlantillaItemsComprados>();
 
        if (this.gridLayout != null && this.gridLayout.transform.childCount >= 0){
            foreach (var item in GameManager.Instance.testlist())
            {
                GameObject nuevoObjeto = Instantiate(plantillaItemsComprados, transform.position, Quaternion.identity);
                nuevoObjeto.transform.SetParent(transform); // Establece el objeto duplicado como hijo de este objeto

                // Agrega el objeto duplicado a la lista
                itemList.Add(nuevoObjeto);
            }
             // Borra los elementos hijos del GridLayoutGroup
            Debug.Log("testing "+gridLayout.transform.childCount);
            Debug.Log(this.gridLayout.transform);
            foreach (Transform child in this.gridLayout.transform)
            {
                Debug.Log("entree "+gridLayout.transform.childCount);

                Destroy(child.gameObject);
            }

            // Recrea los elementos en base a la lista actualizada
            foreach (GameObject item in itemList)
            {
                Instantiate(item, this.gridLayout.transform);
            }

            // Forzar la reconstrucción del layout
            LayoutRebuilder.ForceRebuildLayoutImmediate(this.gridLayout.GetComponent<RectTransform>());
        }

       
    }
}
