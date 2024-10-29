using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWP : MonoBehaviour
{
    public GameObject[] waypoints;
    int currentWP = 0;

    public float speed = 10.0f;
    public float rotSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Validación para asegurarse de que hay waypoints
        if (waypoints.Length == 0)
        {
            Debug.LogError("No hay waypoints asignados.");
            enabled = false; // Desactiva el script si no hay waypoints
        }

        // Inicia la coroutine para cambiar la velocidad
        StartCoroutine(ChangeSpeed());
    }

    // Coroutine para cambiar la velocidad
    IEnumerator ChangeSpeed()
    {
        while (true)
        {
            // Cambia la velocidad aleatoriamente entre 20 y 30
            speed = Random.Range(20f, 30f);
            yield return new WaitForSeconds(3f); // Espera 3 segundos antes de cambiar la velocidad nuevamente
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints.Length == 0) return; // Asegúrate de que hay waypoints disponibles

        // Comprueba la distancia y actualiza el índice de waypoint
        if (Vector3.Distance(this.transform.position, waypoints[currentWP].transform.position) < 5)
        {
            currentWP++;
            if (currentWP >= waypoints.Length) // Verifica el límite
            {
                currentWP = 0; // Resetea si llega al final
            }
        }

        // Calcular la rotación hacia el waypoint actual
        Quaternion lookatWP = Quaternion.LookRotation(waypoints[currentWP].transform.position - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookatWP, rotSpeed * Time.deltaTime);

        // Mover el objeto hacia el waypoint
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
