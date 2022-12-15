using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public SafeInt money = 80000;
    public Pools lastCar = Pools.MainCarStellar;
    public List<Pools> boughtCars = new List<Pools>() { Pools.MainCarStellar };
}
