namespace CustomHTTP.Client.Models;

public class Car: IEquatable<Car>
{
    public string Vin { get; set; }
    public string Vendor { get; set; }
    public string Model { get; set; }
    public ushort? Year { get; set; }


    public Car()
    {
        Vin = new Faker().Vehicle.Vin();
        Vendor = string.Empty;
        Model = string.Empty;
        Year = null;
    }
    public Car(ref Car car)
    {
        Vin = car.Vin;
        Vendor = car.Vendor;
        Model = car.Model;
        Year = car.Year;
    }


    public bool Equals(Car? other) => Vin == other?.Vin;
}
