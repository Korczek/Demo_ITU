using System.Collections.Generic;
using UnityEngine;

public class SlotDecor : SpawnableObject
{
    public List<GameObject> decorsByNeighbors;

    public virtual void SetDecor(SlotRole role, BoardSlot[] thisSameNeighborsPresent = null)
    {
        switch (role)
        {
            case SlotRole.Start:
                // set start
                break;
            case SlotRole.Finish:
                // set finish
                break;
            default:

    
                // set decors by neighbors


                break;
        }
    }
}
