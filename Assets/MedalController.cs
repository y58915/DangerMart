using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MedalController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] List<Sprite> medalImages;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Image>().sprite = medalImages[Score.instance.GetMedal()];
    }
}
