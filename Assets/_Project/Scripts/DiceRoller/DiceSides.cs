using UnityEngine;

[System.Serializable]
public class DiceSide {
    public Vector3 Center;
    public Vector3 Normal;
    public int Value;
}

public class DiceSides : MonoBehaviour {
    [SerializeField] public DiceSide[] Sides;

    const float k_exactMatchValue = 0.995f;
    
    public DiceSide GetDiceSide(int index) => Sides[index];

    public Quaternion GetWorldRotationFor(int index) {
        Vector3 worldNormalToMatch = transform.TransformDirection(GetDiceSide(index).Normal);
        return Quaternion.FromToRotation(worldNormalToMatch, Vector3.up) * transform.rotation;
    }

    public int GetMatch() {
        int sideCount = Sides.Length;
        
        Vector3 localVectorToMatch = transform.InverseTransformDirection(Vector3.up);

        DiceSide closestSide = null;
        float closestDot = -1f;

        for (int i = 0; i < sideCount; i++) {
            DiceSide side = Sides[i];
            float dot = Vector3.Dot(side.Normal, localVectorToMatch);
            
            if (closestSide == null || dot > closestDot) {
                closestSide = side;
                closestDot = dot;
            }
            
            if (dot > k_exactMatchValue) {
                return side.Value;
            }
        }
        
        return closestSide?.Value ?? -1;
    }
}