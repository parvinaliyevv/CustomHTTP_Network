namespace CustomHTTP.Client.ViewModels;

public class MainViewModel : DependencyObject
{
    public UdpClient Udp { get; set; }
    public IPEndPoint ServerEndPoint { get; set; }

    public ObservableCollection<Car> Cars { get; set; }
    public Car Car
    {
        get { return (Car)GetValue(CarProperty); }
        set { SetValue(CarProperty, value); }
    }
    public static readonly DependencyProperty CarProperty =
        DependencyProperty.Register("Car", typeof(Car), typeof(MainViewModel));

    public RelayCommand ReloadCommand { get; set; }
    public RelayCommand InsertCommand { get; set; }
    public RelayCommand UpdateCommand { get; set; }
    public RelayCommand DeleteCommand { get; set; }


    public MainViewModel()
    {
        var address = IPAddress.Parse(ConfigurationManager.AppSettings["ServerIpAddress"]);
        var port = int.Parse(ConfigurationManager.AppSettings["ServerPortNumber"]);
        ServerEndPoint = new IPEndPoint(address, port);

        Udp = new UdpClient(int.Parse(ConfigurationManager.AppSettings["ClientPortNumber"]));
        
        Car = null;
        Cars = new ObservableCollection<Car>();
        
        ReloadCommand = new RelayCommand(_ => Reload() );
        InsertCommand = new RelayCommand(_ => Insert() );
        UpdateCommand = new RelayCommand(_ => Update(), _ => Car is not null);
        DeleteCommand = new RelayCommand(_ => Delete(), _ => Car is not null);

        var timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(100) };
        timer.Tick += (_, _) => CommandManager.InvalidateRequerySuggested();
        timer.Start();
    }


    private async Task Reload()
    {
        var request = new HttpRequest()
        {
            Method = HttpMethod.GET,
        };

        var response = await SendRequestAsync(request);

        if (response?.StatusCode == HTTP.HttpStatusCode.OK)
        {
            var cars = JsonConvert.DeserializeObject<List<Car>>(response.Content);

            Application.Current.Dispatcher.Invoke(() =>
            {
                Cars.Clear();

                foreach (var item in cars) Cars.Add(item);
            });
        }
    }

    private async Task Insert()
    {
        var request = new HttpRequest()
        {
            Content = JsonConvert.SerializeObject(Car),
            Method = HttpMethod.POST,
        };

        var response = await SendRequestAsync(request);

        if (response?.StatusCode == HTTP.HttpStatusCode.CREATED) Application.Current.Dispatcher.Invoke(() => Cars.Add(Car));
        
        Application.Current.Dispatcher.Invoke(() => Car = null);
    }

    private async Task Update()
    {
        var request = new HttpRequest()
        {
            Content = JsonConvert.SerializeObject(Car),
            Method = HttpMethod.PUT,
        };

        var response = await SendRequestAsync(request);

        if (response?.StatusCode == HTTP.HttpStatusCode.OK)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var index = Cars.IndexOf(Car);

                try
                {
                    Cars.Remove(Car);
                }
                catch { }

                Cars.Insert(index, Car);
                Car = null;
            });
        }
    }

    private async Task Delete()
    {
        var request = new HttpRequest()
        {
            Content = JsonConvert.SerializeObject(Car),
            Method = HttpMethod.DELETE,
        };

        var response = await SendRequestAsync(request);

        if (response?.StatusCode == HTTP.HttpStatusCode.OK)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                try
                {
                    Cars.Remove(Car);
                }
                catch { }

                Car = null;
            });
        }
    }

    private async Task<HttpResponse?> SendRequestAsync(HttpRequest request)
    {
        var jsonString = JsonConvert.SerializeObject(request);
        var jsonBytes = Encoding.Default.GetBytes(jsonString);

        Udp.SendAsync(jsonBytes, jsonBytes.Length, ServerEndPoint);

        var receiveResult =  await Udp.ReceiveAsync();

        jsonBytes = receiveResult.Buffer;
        jsonString = Encoding.Default.GetString(jsonBytes);
        
        var response = JsonConvert.DeserializeObject<HttpResponse>(jsonString);

        return response;
    }
}
