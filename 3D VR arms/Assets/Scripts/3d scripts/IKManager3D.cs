using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IK
{
    // A typical error function to minimise
    public delegate float ErrorFunction(Vector3 target, float[] solution);

    public struct PositionRotation
    {
        Vector3 position;
        Quaternion rotation;

        public PositionRotation(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }

        // PositionRotation to Vector3
        public static implicit operator Vector3(PositionRotation pr)
        {
            return pr.position;
        }
        // PositionRotation to Quaternion
        public static implicit operator Quaternion(PositionRotation pr)
        {
            return pr.rotation;
        }
    }


    public class IKManager3D : MonoBehaviour
    {
        [Header("Joints")]
        public Transform BaseJoint;
        //[HideInInspector]
        
        public RobotJoint[] Joints;
        // The current angles
        
        public float[] Solution;

        [Header("Destination")]
        public Transform Effector;
        [Space]
        public Transform Destination;
        public float DistanceFromDestination;
        private Vector3 target;
        public Transform Destination2;
        public float DistanceFromDestination2;
        private Vector3 target2;

        [Header("Inverse Kinematics")]
        [Range(0, 1f)]
        public float DeltaGradient = 0.1f; // Used to simulate gradient (degrees)
        [Range(0, 100f)]
        public float LearningRate = 0.1f; // How much we move depending on the gradient

        [Space()]
        [Range(0, 0.25f)]
        public float StopThreshold = 0.1f; // If closer than this, it stops
        [Range(0, 10f)]
        public float SlowdownThreshold = 0.25f; // If closer than this, it linearly slows down

        public ErrorFunction ErrorFunction;

        [Space]
        [Range(0, 10)]
        public float OrientationWeight = 0.5f;
        [Range(0, 10)]
        public float TorsionWeight = 0.5f;
        public Vector3 TorsionPenality = new Vector3(1, 0, 0);

        //[Header("Debug")]
        //public bool DebugDraw = true;


        //[Header("Debug")]
        //public bool DebugDraw = true;

        ////root of the armature
        //public RobotJoint root;
        ////end effector
        //public RobotJoint end;
        ////array of joints
        //public RobotJoint[] Joints;

        //public GameObject Target;

        //public float LearningRate = 0.05f;
        //public float SamplingDistance = 0.05f;

        public float[] getAngles(RobotJoint[] Joints)
        {
            float[] angles = new float[Joints.Length];
            for (int i = 0; i < angles.Length; i++)
            {
                //angles[i] = Joints[i].transform.eulerAngles.Axis;
                if (Joints[i].Axis.y == 1)
                {
                    angles[i] = Joints[i].transform.localRotation.eulerAngles.y;
                }
                else if (Joints[i].Axis.x == 1)
                {
                    angles[i] = Joints[i].transform.localRotation.eulerAngles.x;
                }
            }
            return angles;
        }
        //Debug.Log("" + );

        public PositionRotation ForwardKinematics(float[] Solution)
        {
            Vector3 prevPoint = Joints[0].transform.position;
            //Quaternion rotation = Quaternion.identity;

            // Takes object initial rotation into account
            Quaternion rotation = transform.rotation;
            for (int i = 1; i < Joints.Length; i++)
            {
                // Rotates around a new axis
                rotation *= Quaternion.AngleAxis(Solution[i - 1], Joints[i - 1].Axis);
                Vector3 nextPoint = prevPoint + rotation * Joints[i].StartOffset;

                

                prevPoint = nextPoint;
            }

            // The end of the effector
            return new PositionRotation(prevPoint, rotation);
        }

        public float DistanceFromTarget(Vector3 target, float[] Solution)
        {
            Vector3 point = ForwardKinematics(Solution);
            return Vector3.Distance(point, target);
        }

        //public float PartialGradient(Vector3 target, float[] angles, int i)
        //{
        //    // Saves the angle,
        //    // it will be restored later
        //    float angle = angles[i];

        //    // Gradient : [F(x+SamplingDistance) - F(x)] / h
        //    float f_x = DistanceFromTarget(target, angles);

        //    angles[i] += SamplingDistance;
        //    float f_x_plus_d = DistanceFromTarget(target, angles);

        //    float gradient = (f_x_plus_d - f_x) / SamplingDistance;

        //    // Restores
        //    angles[i] = angle;

        //    return gradient;
        //}

        //public void InverseKinematics(Vector3 target, float[] angles)
        //{
        //    for (int i = 0; i < Joints.Length; i++)
        //    {
        //        // Gradient descent
        //        // Update : Solution -= LearningRate * Gradient
        //        float gradient = PartialGradient(target, angles, i);
        //        angles[i] -= LearningRate * gradient;
        //        //Joints[i].Rotate LearningRate * gradient;
        //        //Joints[i].transform.localRotation = Quaternion.Euler(0,angles[i],0);
        //    }
        //}


        // Start is called before the first frame update
        void Start()
        {
            ErrorFunction = DistanceFromTarget;
        }

        // Update is called once per frame
        void Update()
        {
            // Do we have to approach the target?
            //Vector3 direction = (Destination.position - Effector.transform.position).normalized;
            Vector3 direction = (Destination.position - transform.position).normalized;
            target = Destination.position - direction * DistanceFromDestination;
            //if (Vector3.Distance(Effector.position, target) > Threshold)
            if (ErrorFunction(target, Solution) > StopThreshold)
                ApprochTarget(target);

            //if (DebugDraw)
            //{
            //    Debug.DrawLine(Effector.transform.position, target, Color.green);
            //    Debug.DrawLine(Destination.transform.position, target, new Color(0, 0.5f, 0));
            //}
        }
        public float CalculateGradient(Vector3 target, float[] Solution, int i, float delta)
        {
            // Saves the angle,
            // it will be restored later
            float solutionAngle = Solution[i];

            // Gradient : [F(x+h) - F(x)] / h
            // Update   : Solution -= LearningRate * Gradient
            float f_x = ErrorFunction(target, Solution);

            Solution[i] += delta;
            float f_x_plus_h = ErrorFunction(target, Solution);

            float gradient = (f_x_plus_h - f_x) / delta;

            // Restores
            Solution[i] = solutionAngle;

            return gradient;
        }

        public void ApprochTarget(Vector3 target)
        {
            // Starts from the end, up to the base
            // Starts from joints[end-2]
            //  so it skips the hand that doesn't move!
            for (int i = Joints.Length - 1; i >= 0; i--)
            //for (int i = 0; i < Joints.Length - 1 - 1; i++)
            {
                // FROM:    error:      [0, StopThreshold,  SlowdownThreshold]
                // TO:      slowdown:   [0, 0,              1                ]
                float error = ErrorFunction(target, Solution);
                float slowdown = Mathf.Clamp01((error - StopThreshold) / (SlowdownThreshold - StopThreshold));

                // Gradient descent
                float gradient = CalculateGradient(target, Solution, i, DeltaGradient);
                Solution[i] -= LearningRate * gradient * slowdown;
                // Clamp
                Solution[i] = Joints[i].ClampAngle(Solution[i]);

                // Early termination
                if (ErrorFunction(target, Solution) <= StopThreshold)
                    break;
            }


            for (int i = 0; i < Joints.Length - 1; i++)
            {
                Joints[i].MoveArm(Solution[i]);
            }
        }
    }

    
}
