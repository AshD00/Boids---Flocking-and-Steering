using UnityEngine;

public class BoidV1 : MonoBehaviour {

    private BoidManager manager;

    private Vector3 separationForce;
    private Vector3 cohesionForce;
    private Vector3 alignmentForce;

    private Vector3 avoidWallsForce;

    private void Start() {
        manager = BoidManager.Instance;
    }

    /// <summary>
    /// The basic update method, this loops each frame and calls the calculation for each boids next movement and then moves them
    /// </summary>
    private void Update() {
        calculateForces();
        moveForward();
    }

    /// <summary>
    /// The calculateForces method calculates each boids next movement based on the positions of each boid in the scene as well as the cage they're in
    /// </summary>
    private void calculateForces() {

        Vector3 seperationSum = Vector3.zero;
        Vector3 positionSum = Vector3.zero;
        Vector3 headingSum = Vector3.zero;

        int boidsNearby = 0;

        for (int i = 0; i < manager.boids.Count; i++) {

            if (this != manager.boids[i]) {

                Vector3 otherBoidPosition = manager.boids[i].transform.position;
                float distToOtherBoid = (transform.position - otherBoidPosition).magnitude;

                if (distToOtherBoid < manager.boidPerceptionRadius) {

                    seperationSum += -(otherBoidPosition - transform.position) * (1f / Mathf.Max(distToOtherBoid, .0001f));
                    positionSum += otherBoidPosition;
                    headingSum += manager.boids[i].transform.forward;

                    boidsNearby++;
                }
            }
        }

        if (boidsNearby > 0) {
            separationForce = seperationSum / boidsNearby;
            cohesionForce   = (positionSum / boidsNearby) - transform.position;
            alignmentForce  = headingSum / boidsNearby;
        }
        else {
            separationForce = Vector3.zero;
            cohesionForce   = Vector3.zero;
            alignmentForce  = Vector3.zero;
        }

    	if (minDistToBorder(transform.position, manager.cageSize) < manager.avoidWallsTurnDist) {
            // Back to center of cage
            avoidWallsForce = -transform.position.normalized;
        }
        else {
            avoidWallsForce = Vector3.zero;
        }
    }
    
    /// <summary>
    /// The moveForward method implements the movement decided by the calculate forces method to travel the correct distance from its current position
    /// </summary>
    private void moveForward() {
        Vector3 force = 
            separationForce * manager.separationWeight +
            cohesionForce   * manager.cohesionWeight +
            alignmentForce  * manager.alignmentWeight +
            avoidWallsForce * manager.avoidWallsWeight;

        Vector3 velocity = transform.forward * manager.boidSpeed;
        velocity += force * Time.deltaTime;
        velocity = velocity.normalized * manager.boidSpeed;

        transform.position += velocity * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(velocity);
    }

    private float minDistToBorder(Vector3 pos, float cageSize) {
        float halfCageSize = cageSize / 2f;
        return Mathf.Min(Mathf.Min(
            halfCageSize - Mathf.Abs(pos.x),
            halfCageSize - Mathf.Abs(pos.y)),
            halfCageSize - Mathf.Abs(pos.z)
        );
    }
}
