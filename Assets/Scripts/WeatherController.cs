using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class WeatherController : MonoBehaviour
{
    private const string API_KEY = "7829f870382962b76a182d9b8bf58f55"; // substitua pela sua chave da API OpenWeatherMap

    private const string API_URL = "https://api.openweathermap.org/data/2.5/weather?";

    public float latitude, longitude;

    public Text tempText;
    public Text climaText;
    private OpenWeatherResponse weatherData;
    

    private void Start()
    {
        // // Verifica se o serviço de localização está habilitado no dispositivo
        // if (!Input.location.isEnabledByUser)
        // {
        //     Debug.Log("Serviço de localização desabilitado pelo usuário.");
        //     return;
        // }
        //
        // // Inicia o serviço de localização com precisão de 5 metros
        // Input.location.Start(5f);
        //
        // // Aguarda até que o serviço de localização esteja inicializado
        // int maxWait = 20;
        // while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        // {
        //     maxWait--;
        //     Debug.Log("Aguardando serviço de localização inicializar...");
        //     System.Threading.Thread.Sleep(1000);
        // }
        //
        // // Verifica se o serviço de localização foi inicializado com sucesso
        // if (Input.location.status == LocationServiceStatus.Failed)
        // {
        //     Debug.Log("Falha ao inicializar serviço de localização.");
        //     return;
        // }
        //
        // // Obtem a latitude e a longitude atual do dispositivo
        // latitude = Input.location.lastData.latitude;
        // longitude = Input.location.lastData.longitude;

        Debug.Log($"Latitude: {latitude}, Longitude: {longitude}");
        
        StartCoroutine(GetWeatherData());
    }

    private IEnumerator GetWeatherData()
    {
        string url = $"{API_URL}lat={latitude}&lon={longitude}&appid={API_KEY}&units=metric&lang=pt_br";

        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Erro ao obter dados climáticos: " + www.error);
        }
        else
        {
            weatherData = JsonConvert.DeserializeObject<OpenWeatherResponse>(www.downloadHandler.text);
            if (weatherData != null)
            {
                Debug.Log($"Temperature: {weatherData.KeyInfo.Temperature}");
                Debug.Log($"Humidity: {weatherData.KeyInfo.Humidity}");
                Debug.Log($"Pressure: {weatherData.KeyInfo.Pressure}");
                tempText.text = $"Temperature: {weatherData.KeyInfo.Temperature} ºC";
                climaText.text = $"Clima: {weatherData.WeatherConditions[0].Group}";
            }

            //ProcessWeatherData(weatherData);
        }
    }

    // private void ProcessWeatherData(string data)
    // {
    //     WeatherInfo weatherInfo = JsonUtility.FromJson<WeatherInfo>(data);
    //     
    //     // Use as informações climáticas obtidas para ajustar o ambiente do jogo
    //     // Exemplo:
    //     float temperature = weatherInfo.main.temp;
    //     Debug.Log("Temperatura atual: " + temperature + " °C");
    //     climaText.text = "Temperatura atual: " + temperature + " °C";
    // }
    
    private void OnDestroy()
    {
        // Para o serviço de localização
        Input.location.Stop();
    }
    
    public class OpenWeather_Coordinates
    {
        [JsonProperty("lon")] public double Longitude { get; set; }
        [JsonProperty("lat")] public double Latitude { get; set; }
    }

    // Condition Info: https://openweathermap.org/weather-conditions
    public class OpenWeather_Condition
    {
        [JsonProperty("id")] public int ConditionID { get; set; }
        [JsonProperty("main")] public string Group { get; set; }
        [JsonProperty("description")] public string Description { get; set; }
        [JsonProperty("icon")] public string Icon { get; set; }
    }

    public class OpenWeather_KeyInfo
    {
        [JsonProperty("temp")] public double Temperature { get; set; }
        [JsonProperty("feels_like")] public double TemperatureFeelsLike { get; set; }
        [JsonProperty("temp_min")] public double TemperatureMinimum { get; set; }
        [JsonProperty("temp_max")] public double TemperatureMaximum { get; set; }
        [JsonProperty("pressure")] public int Pressure { get; set; }
        [JsonProperty("sea_level")] public int PressureAtSeaLevel { get; set; }
        [JsonProperty("grnd_level")] public int PressureAtGroundLevel { get; set; }
        [JsonProperty("humidity")] public int Humidity { get; set; }
    }

    class OpenWeatherResponse
    {
        [JsonProperty("weather")] public List<OpenWeather_Condition> WeatherConditions { get; set; }
        [JsonProperty("main")] public OpenWeather_KeyInfo KeyInfo { get; set; }
    }
}

[System.Serializable]
public class WeatherInfo
{
    public WeatherMain main;
}

[System.Serializable]
public class WeatherMain
{
    public float temp;
}

