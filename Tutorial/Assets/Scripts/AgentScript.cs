using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class AgentScript : Agent
{
    [Header("Agent properties")]
    Rigidbody rb;
    [SerializeField] GameObject goal;
    GoalScript goalScript;
    public float speed;

    [Header("Environment properties")]
    [Range(-1, 0)]
    public float distRwd;
    public float deathPenalty = -1f;
    public float winReward = 1f;
    public float timeScale = 20f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        goalScript = goal.GetComponent<GoalScript>();

        Time.timeScale = timeScale;
    }

    public override void OnEpisodeBegin()
    {
        transform.position = new Vector3(0f, 1f, 0f);
        rb.velocity = Vector3.zero;
        goalScript.OnEpisodeBegin();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position.x);
        sensor.AddObservation(transform.position.z);
        sensor.AddObservation(goal.transform.position.x);
        sensor.AddObservation(goal.transform.position.z);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float distToGoal = distRwd * Vector3.Distance(transform.position, goal.transform.position);
        AddReward(distToGoal);

        int action = actions.DiscreteActions[0];
        Move(action);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        if (Input.GetAxisRaw("Vertical") > 0.1f) { discreteActions[0] = 0; }
        else if (Input.GetAxisRaw("Vertical") < -0.1f) { discreteActions[0] = 1; }
        else if (Input.GetAxisRaw("Horizontal") > 0.1f) { discreteActions[0] = 2; }
        else if (Input.GetAxisRaw("Horizontal") < -0.1f) { discreteActions[0] = 3; }
        else { discreteActions[0] = 4; }
    }

    private void Move(int action)
    {
        Vector3 direction = new Vector3();
        if (action == 0) { direction = new Vector3(0f, 0f, 1f); }
        else if (action == 1) { direction = new Vector3(0f, 0f, -1f); }
        else if (action == 2) { direction = new Vector3(1f, 0f, 0f); }
        else if (action == 3) { direction = new Vector3(-1f, 0f, 0f); }

        rb.velocity = speed * direction;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Goal"))
        {
            AddReward(winReward);
            EndEpisode();
        }
        if (other.transform.CompareTag("Edge"))
        {
            AddReward(deathPenalty);
            EndEpisode();
        }
    }
}
