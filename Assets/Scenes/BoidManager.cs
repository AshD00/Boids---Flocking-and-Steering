using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour {
    public static BoidManager Instance;

    [SerializeField] private int boidAmount;
    [SerializeField] private GameObject boidPrefab;

    public float boidSpeed;
    public float boidPerceptionRadius;
    public float cageSize;

    public float separationWeight;
    public float cohesionWeight;
    public float alignmentWeight;

    public float avoidWallsWeight;
    public float avoidWallsTurnDist;

    public List<BoidV1> boids;

    /// <summary>
    /// The initial method called during the run cycle of the program. 
    /// The Awake method is responsible for creating the boids and putting them in a list.
    /// These boids then run through their update cycles
    /// </summary>
    private void Awake() {

        Instance = this;
        boids.Clear();

        for (int i = 0; i < boidAmount; i++) {
            Vector3 pos = new Vector3(
                Random.Range(-cageSize / 2f, cageSize / 2f),
                Random.Range(-cageSize / 2f, cageSize / 2f),
                Random.Range(-cageSize / 2f, cageSize / 2f)
            );
            Quaternion rot = Quaternion.Euler(
                Random.Range(0f, 360f),
                Random.Range(0f, 360f),
                Random.Range(0f, 360f)
            );

            BoidV1 newBoid = Instantiate(boidPrefab, pos, rot).GetComponent<BoidV1>();
            boids.Add(newBoid);
        }
    }

    public List<BoidV1> GetBoids() { return boids; }
}
