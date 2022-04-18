using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colisiones : MonoBehaviour
{
    public int num = 0;
    //int i = 0;
    private float distancias = 0.0f;
    private float angulochoque = 0.0f;
    private float v1p = 0.0f, v1n = 0.0f, v2p = 0.0f, v2n = 0.0f, v1xp = 0.0f, v1yp = 0.0f, v2xp = 0.0f, v2yp = 0.0f, vp1new = 0.0f, vp2new = 0.0f, v1xpnew = 0.0f, v1ypnew = 0.0f, v2xpnew = 0.0f, v2ypnew = 0.0f;
    private float vxpnew = 0.0f, vypnew = 0.0f, vp = 0.0f, vn = 0.0f;
    private float vxp, vyp;
    private float h = 0.1f, e = 0.9f;
    public class Esfera
    {

        public Esfera(float x, float y, float vel, float r, float m, float ang)
        {
            this.Posicion_x = x;
            this.Posicion_y = y;
            this.Velocidad = vel;
            this.Radio = r;
            this.Masa = m;
            this.Angulo_traslacion = ang;
        }

        public float Posicion_x { get; set; }
        public float Posicion_y { get; set; }
        public float Velocidad { get; set; }
        public float Radio { get; set; }
        public float Masa { get; set; }
        public float Angulo_traslacion { get; set; }
        public GameObject obj1;
        public float vx;
        public float vy;
        float hh = 0.1f;

        public void crearesfera()
        {
            obj1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            obj1.transform.localScale = new Vector3(2 * Radio, 2 * Radio, 2 * Radio);
            obj1.transform.position = new Vector3(Posicion_x, Posicion_y, 0f);
            Angulo_traslacion = (Angulo_traslacion * Mathf.PI) / 180.0f;
            vx = Mathf.Cos(Angulo_traslacion) * Velocidad;
            vy = Mathf.Sin(Angulo_traslacion) * Velocidad;
        }

        public void movimiento()
        {
            Posicion_x = vx * hh + Posicion_x;
            Posicion_y = vy * hh + Posicion_y;
            obj1.transform.position = new Vector3(Posicion_x, Posicion_y, 0f);
        }

    }

    Esfera[] esferitas = new Esfera[100];
    public void creandoesferas()
    {
        //List<Esfera> lisesferitas = new List<Esfera>{rx,ry,rv,rr,rm,ra};
        for (int i = 0; i < num; i++)
        {
            float rx = Random.Range(-100.0f, 100.0f);
            float ry = Random.Range(-100.0f, 100.0f);
            float rv = Random.Range(-1.0f, 1.0f);
            float rr = Random.Range(2.0f, 3.0f);
            float rm = Random.Range(1.0f, 2.0f);
            float ra = Random.Range(0.0f, 360.0f);
            //lisesferitas[i].Esfera(rx,ry, rv, rr, rm, ra);
            //lisesferitas[i].Add(new Esfera(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), rv, rr, rm, ra));
            //lisesferitas[i].Add(new Esfera(rx,ry, rv, rr, rm, ra));
            //parts.Add(new Part() { PartName = "crank arm", PartId = 1234 });
            //lisesferitas[i].crearesfera();
            esferitas[i] = new Esfera(rx, ry, rv, rr, rm, ra);
            esferitas[i].crearesfera();

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        creandoesferas();
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < num; i++)
        {
            for (int j = 0; j < num; j++)
            {
                distancias = Mathf.Sqrt(Mathf.Pow((esferitas[i].Posicion_x - esferitas[j].Posicion_x), 2) + Mathf.Pow((esferitas[i].Posicion_y - esferitas[j].Posicion_y), 2));
                if (distancias > esferitas[i].Radio + esferitas[j].Radio || distancias == 0)
                {
                    //movimiento();
                    esferitas[i].movimiento();
                    esferitas[j].movimiento();
                    //Debug.Log(esferitas[i].vx);
                }
                else if (distancias <= esferitas[i].Radio + esferitas[j].Radio)
                {
                    Debug.Log("colisioooonn");
                    calculos(esferitas[i].Posicion_y, esferitas[j].Posicion_y, esferitas[i].Posicion_x, esferitas[j].Posicion_x, esferitas[i].vx, esferitas[i].vy, esferitas[j].vx, esferitas[j].vy, esferitas[i].Masa, esferitas[j].Masa, i, j);
                    colision(esferitas[i].Posicion_x, esferitas[i].Posicion_y, esferitas[j].Posicion_x, esferitas[j].Posicion_y);

                        esferitas[i].Posicion_x = esferitas[i].Posicion_x + 0.1f;
                        esferitas[i].Posicion_y = esferitas[i].Posicion_y + 0.1f;

                    esferitas[i].Posicion_x = esferitas[i].Posicion_x + h;
                    esferitas[i].Posicion_y = esferitas[i].Posicion_y + h;
                    esferitas[j].Posicion_x = esferitas[j].Posicion_x + h;
                    esferitas[j].Posicion_y = esferitas[j].Posicion_y + h;
                    esferitas[i].vx = v1xpnew;
                    esferitas[i].vy = v1ypnew;
                    esferitas[j].vx = v2xpnew;
                    esferitas[j].vy = v2ypnew;

                    Debug.Log("esferitas i");
                    Debug.Log(esferitas[i].vx);
                    Debug.Log("esferitas j");
                    Debug.Log(esferitas[j].vx);
                }

            }

        }

    }

    void calculos(float y1, float y2, float x1, float x2, float v1x, float v1y, float v2x, float v2y, float Masa_1, float Masa_2, int i, int j)
    {

        angulochoque = Mathf.Atan((y2 - y1) / (x2 - x1));
        v1p = v1x * Mathf.Cos(angulochoque) + v1y * Mathf.Sin(angulochoque);
        v1n = -v1x * Mathf.Sin(angulochoque) + v1y * Mathf.Cos(angulochoque);
        v2p = v2x * Mathf.Cos(angulochoque) + v2y * Mathf.Sin(angulochoque);
        v2n = -v2x * Mathf.Sin(angulochoque) + v2y * Mathf.Cos(angulochoque);
        //Velocidades rotadas
        v1xp = Mathf.Cos(angulochoque) * v1p;
        v1yp = Mathf.Sin(angulochoque) * v1p;
        v2xp = Mathf.Cos(angulochoque) * v2p;
        v2yp = Mathf.Sin(angulochoque) * v2p;
        //Velocidad post colision
        vp1new = ((Masa_1 - (e) * (Masa_2) * v1p) / (Masa_1 + Masa_2)) + ((1 + e) * (Masa_2) * v2p) / (Masa_1 + Masa_2);
        vp2new = (((1 + e) * (Masa_1) * v1p) / (Masa_1 + Masa_2)) + (((Masa_2 - (e) * (Masa_1) * v2p) / (Masa_1 + Masa_2)));
        //Rotacion inversa, velocidades nuevas
        v1xpnew = Mathf.Cos(angulochoque) * vp1new - Mathf.Sin(angulochoque) * v1n;
        v1ypnew = Mathf.Sin(angulochoque) * vp1new + Mathf.Cos(angulochoque) * v1n;
        v2xpnew = Mathf.Cos(angulochoque) * vp2new - Mathf.Sin(angulochoque) * v2n;
        v2ypnew = Mathf.Sin(angulochoque) * vp2new + Mathf.Cos(angulochoque) * v2n;

    }

    void colision(float x1, float y1, float x2, float y2)
    {
        x1 = v1xpnew * h + x1;
        y1 = v1ypnew * h + y1;
        x2 = v2xpnew * h + x2;
        y2 = v2ypnew * h + y2;

    }


}
