using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CScript
{
    private int id;
    private int type;
    private int choose;
    private int text;
    private int change;
    private bool marker;

    public CScript(int _id, int _type, int _choose, int _text, int _change, bool _marker)
    {
        this.id = _id;
        this.type = _type;
        this.choose = _choose;
        this.text = _text;
        this.change = _change;
        this.marker = _marker;
    }
}

