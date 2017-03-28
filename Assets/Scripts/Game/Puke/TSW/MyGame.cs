using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MyGame
{
    private List<Puke> _myPukes;
    private List<Puke> _upTimePukes;//上一次
    private TSWPrompt _prompt;

    public void Reset()
    {
        this._upTimePukes.Clear();
    }
}
