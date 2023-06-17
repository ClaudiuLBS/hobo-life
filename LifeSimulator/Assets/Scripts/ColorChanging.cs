using UnityEngine;
using System.Threading.Tasks;

public class ColorChange : MonoBehaviour
{
    Renderer lr;
    Color col;
    // Start is called before the first frame update
    async void Start()
    {
        lr=GetComponent<Renderer>();
        await Task.Delay(3000);
        col.r=Random.Range(0f,1f);
        col.g=Random.Range(0f,1f);
        col.b=Random.Range(0f,1f);
        lr.material.color=col;
    }
   

    // Update is called once per frame
    void Update()
    {
        col_rend.material.color= Color.Lerp(Color.black, Color.white,timer/5);
        timer+=Time.deltaTime;
    }

}