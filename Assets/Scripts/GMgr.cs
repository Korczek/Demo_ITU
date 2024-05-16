using UnityEngine;

public class GMgr : MonoBehaviourSingleton<GMgr>
{
    // coś ciut przed startem
    // coś na start
    // coś ciut przed końcem
    // coś na koniec
    // coś tuż po końcu 
    
    // może eventy, ale ciężko się je śledzi

    public Transform objectToMove;
    
    private void Update() // ok, moje animacje działajaą xD fajnie xD
    {
        if(Input.GetKeyDown(KeyCode.A))
            Tester();
    }

    public void Tester()
    {
        objectToMove.AnimMove(new Vector3(0, 0, 0), .2f)
            .OnComplete(() =>
            {
                objectToMove.AnimRotate(new Vector3(0, 190, 0), .4f)
                    .OnComplete(() =>
                    {
                        Debug.Log("rotationDone");
                    });
            })
            .OnStart(() => Debug.Log("Starting"))
            .SetEase(Ease.InOutCubic)
            .Run();
    }
}
