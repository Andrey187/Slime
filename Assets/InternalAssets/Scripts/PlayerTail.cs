using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTail : MonoBehaviour
{
    [SerializeField] private GameObject _bodyPrefab;
    [SerializeField] private EndCardNavMesh endCardNav;
    [SerializeField] public List<Transform> _bodyParts = new List<Transform>();
    [SerializeField] private float _offset = 1f;
    [SerializeField] private float _speed = 5f;
    private float _dis;
    private Transform _curBodyPart;
    private Transform _prevBodyPart;
    private ParticleSystem _ps;

    public int INDEX = 1;

    private void Update()
    {
        BodyMove();
        Merge();
    }

    private void BodyMove()
    {
        float curspeed = _speed;

        for (int i = INDEX; i < _bodyParts.Count; i++)
        {
            _curBodyPart = _bodyParts[i];
            _prevBodyPart = _bodyParts[i - 1];
            _dis = Vector3.Distance(_prevBodyPart.position, _curBodyPart.position);

            Vector3 newPos = _prevBodyPart.position;

            newPos.y = _bodyParts[0].position.y;

            float T = Time.deltaTime * _dis / _offset * curspeed;
            if (T > 0.5f)
            {
                T = 0.5f;
            }

            _curBodyPart.position = Vector3.Lerp(_curBodyPart.position, newPos, T);
            _curBodyPart.rotation = Quaternion.Lerp(_curBodyPart.rotation, _prevBodyPart.rotation, T);
        }
    }

    public void AddBodyPart(Color vegeColor)
    {
        Transform newPart = Instantiate(_bodyPrefab, _bodyParts[_bodyParts.Count - 1].position,
            _bodyParts[_bodyParts.Count - 1].rotation).transform;
        newPart.SetParent(transform);
        newPart.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().materials[0].color = vegeColor;
        newPart.transform.GetComponentInChildren<CollisionPainter>().paintColor = vegeColor;
        newPart.transform.GetComponentInChildren<ParticlesController>().paintColor = vegeColor;

        var main = newPart.transform.GetComponentInChildren<ParticleSystem>().GetComponent<Renderer>();
        main.material.color = vegeColor;

        _bodyParts.Add(newPart);
    }

    
    // ToDo in one method RemoveFirst and Last body parts
    public void RemoveBodyPart()
    {
        if (_bodyParts.Count > 1)
        {
            StartCoroutine(Death(1f, _bodyParts[_bodyParts.Count - 1].gameObject));
            _bodyParts.RemoveAt(_bodyParts.Count - 1);
        }
        
    }

    public void RemoveFirstBodyPart()
    {
        StartCoroutine(DeathFirst(0f, _bodyParts[0].gameObject));
        _bodyParts.RemoveAt(0);
    }
    
    private IEnumerator DeathFirst(float time, GameObject part)
    {
        // Add Particle
        yield return new WaitForSeconds(time);
        Destroy(part);
    }
    
    private IEnumerator Death(float time, GameObject part)
    {
        part.GetComponent<Animator>().SetBool("isDead", true);
        part.transform.GetChild(2).GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(time);
        Destroy(part);
    }
    


    private void Merge()
    {
        for (int i = 1; i < _bodyParts.Count; i++)
        {
            if (_bodyParts.ElementAtOrDefault(i + 2) != null )
            {
                var IndexColor0 = _bodyParts[i].transform.GetChild(1).GetComponent<SkinnedMeshRenderer>()
                    .materials[0].color;
                var IndexColor1 = _bodyParts[i + 1].transform.GetChild(1).GetComponent<SkinnedMeshRenderer>()
                    .materials[0].color;
                var IndexColor2 = _bodyParts[i + 2].transform.GetChild(1).GetComponent<SkinnedMeshRenderer>()
                    .materials[0].color;
                if (IndexColor0 == IndexColor1 && IndexColor1 == IndexColor2)
                {
                    _bodyParts[i].gameObject.transform.localScale = new Vector3(3, 3, 3);
                    var part2 = _bodyParts[i + 2].gameObject;
                    var part1 = _bodyParts[i + 1].gameObject;
                    StartCoroutine(Merge(0, part2, part1));
                    _bodyParts.RemoveAt(i + 2);
                    _bodyParts.RemoveAt(i + 1);
                }
            }
        }
    }
    
    private IEnumerator Merge(float time, GameObject part0, GameObject part1)
    {
        yield return new WaitForSeconds(time);
        Destroy(part0);
        Destroy(part1);
    }
}
