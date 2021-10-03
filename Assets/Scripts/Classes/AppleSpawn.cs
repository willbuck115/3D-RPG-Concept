using UnityEngine;

public class AppleSpawn : MonoBehaviour
{
    public GameObject apple;

    public void AppleCreation(GameObject _apple)
    {
        apple = _apple;
    }

    public bool AppleBoolean
    {
        get
        {
            if (apple == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}