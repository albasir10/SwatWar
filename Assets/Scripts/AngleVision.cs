using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AngleVision
{
    float viewAngle; // Угол обзора противника
    float viewDistance; // Максимальная дистанция обзора

    LayerMask targetMask; // Слои объектов, которые будут считаться целями
    LayerMask obstacleMask; // Слои объектов, которые могут закрывать обзор противника
    GameObject head;
    Unit unit;
    // Метод для поиска всех видимых целей

    public AngleVision(Unit unit, LayerMask targetMask, LayerMask obstacleMask)
    {
        this.unit = unit;
        this.head = unit.head;
        viewAngle = unit.viewAngle;
        viewDistance = unit.viewDistance;
        this.targetMask = targetMask;
        this.obstacleMask = obstacleMask;
    }


    public bool CheckTarget()
    {
        Vector3 directionToTarget = (unit.currentTarget.transform.position - head.transform.position).normalized;
        float distanceToTarget = Vector3.Distance(head.transform.position, unit.currentTarget.transform.position);
        if (!Physics.Raycast(head.transform.position, directionToTarget, distanceToTarget, obstacleMask))
        {
            return true;
        }
        return false;
    }

    public List<Unit> FindVisibleTargets()
    {
        
        List<Unit> visibleTargets = new ();
        Collider[] targetsInViewRadius = new Collider[50];
        Physics.OverlapSphereNonAlloc(head.transform.position, viewDistance, targetsInViewRadius, targetMask);
        foreach (Collider targetCollider in targetsInViewRadius)
        {
            if (targetCollider == null) continue;
            Unit target = targetCollider.GetComponentInParent<Unit>();
            if (target == null) continue;
            else if (unit.isEnemy == target.isEnemy) continue;
            else if (target.GetStateUnit().GetType() == typeof(StateInjury)) continue;
            Vector3 directionToTarget = (target.transform.position - head.transform.position).normalized;
            Transform headTransformnew = head.transform;
            Vector3 headRotationNew = head.transform.rotation.eulerAngles;
            headRotationNew.x = 0;
            headTransformnew.rotation = Quaternion.Euler(headRotationNew);
            if (Vector3.Angle(headTransformnew.forward, directionToTarget) < viewAngle / 2f)
            {
                float distanceToTarget = Vector3.Distance(headTransformnew.position, target.transform.position);
                if (!Physics.Raycast(headTransformnew.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    if (visibleTargets.IndexOf(target) == -1) 
                    {
                        visibleTargets.Add(target);
                    }
                    
                }
            }
        }

        return visibleTargets;
    }


    // Отрисовка области обзора противника (только для наглядности в редакторе Unity)
    public void CheckLines()
    {
        Transform headTransformnew = head.transform;
        Vector3 headRotationNew = head.transform.rotation.eulerAngles;
        headRotationNew.x = 0;
        headTransformnew.rotation = Quaternion.Euler(headRotationNew);
        Vector3 rightDir = Quaternion.Euler(0, viewAngle / 2f, 0) * headTransformnew.forward;
        Vector3 leftDir = Quaternion.Euler(0, -viewAngle / 2f, 0) * headTransformnew.forward;

        Debug.DrawLine(headTransformnew.position, headTransformnew.position + rightDir * viewDistance);
        Debug.DrawLine(headTransformnew.position, headTransformnew.position + leftDir * viewDistance);
    }
}
