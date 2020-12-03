using System.Collections.Generic;
using UnityEngine;

public class TeleportInfo
{
    public Vector4 depart;
    public Vector4 dest;

    TeleportInfo()
    {
        depart = new Vector4(0f, 0f, 0f, 0f);
        dest = new Vector4(0f, 0f, 0f, 0f);
    }
}
