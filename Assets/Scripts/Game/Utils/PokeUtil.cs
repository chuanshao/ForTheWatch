using UnityEngine;
using System.Collections;

public class PokeUtil
{
    public static TSWPuke NumberToTSWPoke(int num) {
        var pokeColor = (PukeColor)(num % 4);
        pokeColor = num >= 53 ? PukeColor.King : pokeColor;
        TSWPuke puke = new TSWPuke(num, pokeColor);
        return puke;
    }
}
