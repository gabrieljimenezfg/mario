using UnityEngine;

// pasar nivel 
// main menu

public class Apuntes : MonoBehaviour
{
    // Accessibilidad, tipo de variable, nombre
    [SerializeField]
    private int numEntero = 5;
    private float numDecimalesFalsos = 5.5f;
    private double numDecimalesPartidos = 5.5;
    private string cadenaCharacters = "cinco";
    private char character = 'c';
    private bool booleano = true; // false
    private Vector3 vector3D = new Vector3(2, 6, 9); // x, y, z
    private Vector2 vector2D = new Vector2(5, 4); // x, y
    private Transform trans;
    private MeshRenderer mesh;
    private CapsuleCollider colliderObject;
    private Material material;
    private Apuntes nuestrosScripts;
    private Animator animator;
    private Rigidbody rb;

    // se ejecuta primero que todo
    void Awake()
    {
        Debug.Log("Awake");
    }

    // se ejecuta cada vez que se activa el objecto con este script, o cada vez que se active el script
    private void OnEnable()
    {
        Debug.Log("OnEnable");
    }

    void Start()
    {
        Debug.Log("Start");
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // se ejecuta cada x segundos definidos en project settings
    void FixedUpdate()
    {
        
    }

    // se ejectua al final de cada frame
    private void LateUpdate()
    {
        
    }

    // se ejecuta el primer frame que se detecte una colision
    private void OnCollisionEnter(Collision collision)
    {
        
    }

    // se ejecuta el primer frame que deje de detectar una colision
    private void OnCollisionExit(Collision collision)
    {

    }

    // se ejecuta mientras la colision esta activa (si se esta moviendo, si no no hay colisiones nuevas)
    private void OnCollisionStay(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        
    }
    private void OnTriggerStay(Collider collider)
    {
        
    }
}
