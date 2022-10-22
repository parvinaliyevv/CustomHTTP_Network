namespace CustomHTTP.Server;

public static class Program
{
    public static UdpClient Udp { get; set; }

    public static List<Car> Cars { get; set; }

    public static string FilePath { get; set; }


    static Program()
    {
        try
        {
            Udp = new UdpClient(int.Parse(ConfigurationManager.AppSettings["PortNumber"]));
        }
        catch
        {
            Console.WriteLine("Server already started!");
        }

        FilePath = string.Format(@"{0}Assets\Data\Cars.json", DirectoryService.GetProjectParentFolder());

        if (File.Exists(FilePath)) Task.Factory.StartNew(DeserializeCars);
        else
        {
            var stream = File.Create(FilePath);

            try
            {
                stream.Close();
                stream.Dispose();
            }
            catch { }

            Cars = new Faker<Car>()
                .RuleFor(car => car.Vin, faker => faker.Vehicle.Vin())
                .RuleFor(car => car.Vendor, faker => faker.Vehicle.Manufacturer())
                .RuleFor(car => car.Model, faker => faker.Vehicle.Model())
                .RuleFor(car => car.Year, faker => faker.Vehicle.Random.UShort(2000, 2022))
                .Generate(20);

            Task.Factory.StartNew(SerializeCars);
        }
    }


    private static void Main(string[] args)
    {
        try
        {
            HandleRequestsAsync().Wait();
        }
        catch { }
    }

    private static async Task HandleRequestsAsync()
    {
        restart:

        var receiveResult = await Udp.ReceiveAsync();
        var endPoint = receiveResult.RemoteEndPoint;

        var jsonBytes = receiveResult.Buffer;
        var jsonString = Encoding.Default.GetString(jsonBytes);

        var request = JsonConvert.DeserializeObject<HttpRequest>(jsonString);
        var response = new HttpResponse() { Method = request.Method };

        if (request?.Method == HTTP.HttpMethod.GET)
        {
            var cars = JsonConvert.SerializeObject(Cars);

            response.StatusCode = HttpStatusCode.OK;
            response.Content = cars;
        }
        else
        {
            var car = JsonConvert.DeserializeObject<Car>(request.Content);

            if (car is null) response.StatusCode = HttpStatusCode.BAD_REQUEST;
            else
            {
                if (request?.Method == HTTP.HttpMethod.POST)
                {
                    if (Cars.Contains(car))
                    {
                        response.StatusCode = HttpStatusCode.CONFLICT;
                    }
                    else
                    {
                        Cars.Add(car);
                        response.StatusCode = HttpStatusCode.CREATED;
                    }
                }
                else if (request?.Method == HTTP.HttpMethod.PUT)
                {
                    if (Cars.Contains(car))
                    {
                        var index = Cars.IndexOf(car);
                        Cars.Remove(car);
                        Cars.Insert(index, new Car(ref car));

                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.NOT_FOUND;
                    }
                }
                else if (request?.Method == HTTP.HttpMethod.DELETE)
                {
                    if (Cars.Remove(car)) response.StatusCode = HttpStatusCode.OK;
                    else response.StatusCode = HttpStatusCode.NOT_FOUND;
                }

                Task.Factory.StartNew(SerializeCars);
            }
        }

        jsonString = JsonConvert.SerializeObject(response);
        jsonBytes = Encoding.Default.GetBytes(jsonString);

        Udp.SendAsync(jsonBytes, jsonBytes.Length, endPoint);

        goto restart;
    }

    private static void SerializeCars()
    {
        if (File.Exists(FilePath))
        {
            var jsonString = JsonConvert.SerializeObject(Cars, Formatting.Indented);
            File.WriteAllText(FilePath, jsonString);
        }
    }

    private static void DeserializeCars()
    {
        if (File.Exists(FilePath))
        {
            var jsonString = File.ReadAllText(FilePath);
            Cars = JsonConvert.DeserializeObject<List<Car>>(jsonString);
        }
    }
}
