using System;
using UnityEngine;

public class CONMenu
{
    public int price { get; set; }

    public CONProduct name { get; set; }

    public int number { get; set; }

    public CONMenu( CONLevelOption lv )
    {
        Array values = Enum.GetValues(typeof(CONProduct));

        name = (CONProduct)UnityEngine.Random.Range(0, 9);

       
        switch (lv.moneyRange)
        {
            case 1000:

                price = 1000 * UnityEngine.Random.Range(1, 4);

                break;

            case 500:

                price = 500 * UnityEngine.Random.Range(1, 6);

                break;

            case 100:

                price = 100 * UnityEngine.Random.Range(5, 26);

                break;

            case 50:

                price = 50 * UnityEngine.Random.Range(10, 151);

                break;

            default:

                price = 50 * UnityEngine.Random.Range(10, 151);

                break;
        }

        number = UnityEngine.Random.Range(1,lv.prodNum+1);

    }

    public override String ToString()
    {

        return name.ToString() + " : " + price + " : " + number;
    }
}
