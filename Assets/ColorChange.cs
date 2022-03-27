using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChange : MonoBehaviour
{
    public Text scr;
    public Image cap;
    public Text capText;
    public Text username;
    public Text nameinput;
    public bool started;

    string[] capTexts = { "A", "B", "C", "D", "E", "F", "G", "H" };

    Color tilecolor;
    float[] row = { 0.001f, 0.0015f, 0.002f, 0.0025f, 0.003f, 0.0035f, 0.004f, 0.0045f };
    float[] col = { 0.001f, 0.0015f, 0.002f, 0.0025f, 0.003f, 0.0035f, 0.004f, 0.0045f };
    int score = 0;
    int startframe;
    int timescale = 1;
    int frame = 0;
    int counter = 0;
    int totalScore = 0;

    public bool chose;
    // Start is called before the first frame update
    void Start()
    {
        ShuffleArray(row);
        ShuffleArray(col);
    }

    public void startgame()
    {
        username.text = nameinput.text;
        started = true;
    }

    private T[] ShuffleArray<T>(T[] array)
    {
        int random1, random2;
        T temp;

        for (int i = 0; i < array.Length; ++i)
        {
            random1 = Random.Range(0, array.Length);
            random2 = Random.Range(0, array.Length);

            temp = array[random1];
            array[random1] = array[random2];
            array[random2] = temp;
        }

        return array;
    }

    public void reset()
    {
        counter++;
        if (counter == 6)
        {
            end();
            return;
        }
        score = 0;
        frame = 0;
        timescale = 1;
        startframe = Time.frameCount;
        ShuffleArray(row);
        ShuffleArray(col);
        for (int i = 0; i < 64; i++)
        {
            transform.GetChild(i).GetComponent<Image>().color = Color.blue;
            transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
        }
        chose = false;
    }

    public GameObject endtext;
    void end()
    {
        scr.text = "끝,누적<size=30>" + totalScore + "</size>개";
        endtext.SetActive(true);
    }

    public Color[] colors;
    public void clicked(int index)
    {
        chose = true;
        timescale = 0;
        int rowi = index / 8;
        int coli = index % 8;
        int time = Time.frameCount - startframe; //500쯤이 0점
        int selltime = (int)(1000 - time - col[coli] * 100000);

        score = selltime * (int)((row[rowi] * 10000) + 100) / 4;
        totalScore += score;
        scr.text = "<size=20>" + selltime + "일</size> 동안\n" + "<size=20>" + score + "개</size> 팔림!\n누적 " + totalScore + "개";
        cap.color = colors[coli];
        capText.text = capTexts[rowi];
    }

    // Update is called once per frame
    void Update()
    {
        if (frame >= 1000)
            reset();
        if (frame > 30 && counter < 6)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int n = 8 * i + j;
                    tilecolor = transform.GetChild(n).GetComponent<Image>().color;
                    transform.GetChild(n).GetComponent<Image>().color = new Color(tilecolor.r, tilecolor.g, tilecolor.b, tilecolor.a - row[i] * timescale);
                }
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int n = i + 8 * j;
                    tilecolor = transform.GetChild(n).GetComponent<Image>().color;
                    transform.GetChild(n).GetComponent<Image>().color = new Color(tilecolor.r + col[i], tilecolor.g, tilecolor.b - col[i] * timescale, tilecolor.a);
                }
            }
        }
        if (started)
        {
            frame += timescale;
        }
    }
}
