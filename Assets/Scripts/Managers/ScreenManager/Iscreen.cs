using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Iscreen
{
    ScreenComponent ScreenComp { get;}
    void Activate();
    void Deactivate( bool hideScrren);
    void Free();
}
