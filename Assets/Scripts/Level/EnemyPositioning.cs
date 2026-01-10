using UnityEngine;

public static class EnemyPositioning
{
    public static Vector3 GetCirclePosition(
        Transform player,
        int index,
        int total,
        float radius
    )
    {
        float angle = (360f / total) * index;
        float rad = angle * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(
            Mathf.Cos(rad),
            0,
            Mathf.Sin(rad)
        ) * radius;

        return player.position + offset;
    }
}
