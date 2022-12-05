using UnityEngine;

public class CollisionPainter : MonoBehaviour
{
    public Color paintColor;
    
    [SerializeField] private float radius = 1;
    [SerializeField] private float strength = 1;
    [SerializeField] private float hardness = 1;
    private bool _touchGround = false;
    private ParticleSystem _particle;

    private void OnCollisionStay(Collision other)
    {
        if(_touchGround != false)
        {
            Paintable p = other.collider.GetComponent<Paintable>();
            if (p != null)
            {
                Vector3 pos = other.contacts[0].point;
                PaintManager.instance.paint(p, pos, radius, hardness, strength, paintColor);
            }
        }
    }

    private void PaintGround()
    {
        _touchGround = !_touchGround;
    }

    private void ParticleActivate()
    {
        transform.GetComponentInChildren<ParticleSystem>().Play();
    }
}
