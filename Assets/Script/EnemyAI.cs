using System;
using UnityEngine;
using UnityEngine.AI;

namespace AlexzanderCowell
{

    public class EnemyAI : MonoBehaviour
    {

        [Header("Enemy Movements")]
        [SerializeField] private float enemyMovement = 6f; // Controls the enemyMovement as it patrols.
        //[SerializeField] private float enemyRunSpeed = 10f; // Controls the enemyMovement as it runs away from the player.

       // [Header("Enemy Health Value")]
       // [SerializeField] private float enemyHealth = 3f; // Enemy Health value for when the player attacks.

        [Header("Player Transform GameObject")]
        [SerializeField] private Transform thePlayer; // Transform details from a gameObject which in this case is the main character or player.

        [Header("Enemy LayerMask")]
        [SerializeField] private LayerMask whatIsPlayer, whatIsGround; // Uses Layer Mask to define what the enemy AI see's.

        [Header("Enemy Attack Range & Sight Range")]
        [SerializeField] private float attackRange, sightRange;

        private NavMeshAgent newNav; // Navmesh agent for enemy controller.

        private bool playIsInSight; // Checks if player is in sight or not.
        private bool playIsInAttack; // Checks if the enemy is in attack mode.

        private Vector3 walkPoint; // Finds the vector 3 transform postion details for where to walk.
        private bool walkPointSet; // Sets a walk method to true or false.

        //private Vector3 runPoint; // Finds the vector 3 transform postion details for where to run.
       // private bool runPointSet; // Sets a run method to true or false.

        //public static event Action<int> _HealthUpdateEventwithEnemy; // Sends out a event for HealthUpdateEventwithEnemy.


        private void Start()
        {
            newNav = GetComponent<NavMeshAgent>(); // Grabs the Nav Mesh agent on the enemy to be assigned to newNav for the script to use.
            thePlayer = GameObject.Find("PlayerObj").transform; // Uses the players transform position and assigns it to the Player.
        }


        private void Update()
        {
            playIsInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer); // Assigns play is in Sight to the Sphere that is projected out of the enemy to it's current position and sightRange value & the player layer mask.
            playIsInAttack = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer); // Assigns play is in Attack to the Sphere that is projected out of the enemy to it's current position and attackRange value & the player layer mask.

            if (!playIsInAttack && !playIsInSight) PatrolYard(); // If the play is not in attack range anmd sight range it will use the PatrolYard method.
            if (playIsInSight && playIsInAttack) ChasePlayer(); // If the play is in attack range and sight range it will use the ChasePlayer method for attack.
        }

        private void PatrolYard() // Patrolling Method and starts the code attatched to it.
        {
            if (!walkPointSet) SearchWalkPoint(); // If it does not use the walkPointSet it will start the search walk point method.

            if (walkPointSet) // If it does use the walkPointset than it will set the nav mesh agent to destination to the walk point vector 3 position value.
            {
                newNav.SetDestination(walkPoint);
            }

            Vector3 distanceToWalkPoint = transform.position - walkPoint; // Sets a new Vector 3 called distance to walk point and will equal the transform poition of the enmy minus the walk point vector 3.

            if (distanceToWalkPoint.magnitude < 1f) // Will check the distance if its less than a float of 1.
                walkPointSet = false; // Will set the walk point set to false if it does find it less than a float of 1.
        }

        private void SearchWalkPoint() // Starts the code for searching for a new walk point.
        {
            float randomZ = UnityEngine.Random.Range(-enemyMovement, enemyMovement); // Generates a z using minus the enemy movement and current enemy movement floats.
            float randomX = UnityEngine.Random.Range(-enemyMovement, enemyMovement); // Generates a x using minus the enemy movement and current enemy movement floats. 
            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ); // Walk point will be a new vector 3 for the transform position using the random values above while staying on the current y positon.

            if (Physics.Raycast(walkPoint, -transform.up, whatIsGround)) // Checks the ground to make sure it stays with in it's boundry's.
                walkPointSet = true; // If it does stay with in it's boundry's it will set the walk point to be true.
        }

        private void ChasePlayer() // Starts the chase player code.
        {
            newNav.SetDestination(thePlayer.position); // Will use the Nav mesh to set a destination to the players position.
            transform.LookAt(thePlayer); // Will make the enemy face the player as it chases after it.
        }

        ////private void TakeDamage() // Take Damage method starts with code underneth it.
        //{
        //    enemyHealth = Mathf.Clamp(enemyHealth - 1, 0, 3); // calculates the enemy health using Mathf.Clamp so it sets the 3 parameters so the computer knows what to do with -1 when it happens and 0 being the min health and 3 being the max health.

        //    if (enemyHealth <= 0) // If the enemy health equals or is less than 0 it will run the drop coin method and the kill enemy method.
        //    {
        //        DropCoins();
        //        KillEnemy();
        //    }
        //}

        //private void KillEnemy() // Starts the kill enemy method.
        //{
        //    Destroy(gameObject); // Destroys the enemy that has this script attatched to.
        //}

        private void OnDrawGizmosSelected() // Starts the On Draw Gizmos for the editor view.
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange); // attack range will be displayed in the editor as a red wire Sphere to correctly assign the attack range of the enemy.

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange); // sight range will be displayed in the editor as a yellow wire Sphere to correctly assign the sight range of the enemy.
        }

        //private void DecreaseHealth() // Starts the Decrease Health of the player method.
        //{
        //    waterTig.health = Mathf.Clamp(waterTig.health - 1, 0, 15); // Gets the health int that is already in the Water trigger script and uses the same maths to decrease the health of the player in the UI.

        //    if (_HealthUpdateEventwithEnemy != null) // If the method above starts than will ask the HealthUpdateEventwithEnemy if it does not equal nothing and if it does not than will send out the health calculations for the event to be picked up.
        //    {
        //        _HealthUpdateEventwithEnemy(waterTig.health); // Calls out using the Water trigger health value.
        //    }
        //}


    }


}