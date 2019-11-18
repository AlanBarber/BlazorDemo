using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorDemo.Shared;
using Microsoft.AspNetCore.Components;
using Cloudcrate.AspNetCore.Blazor.Browser.Storage;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorDemo.Client.Services
{
    public class AppState
    {
        #region App State Setup

        // Lets components receive change notifications
        // Could have whatever granularity you want (more events, hierarchy...)
        public event Action OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

        // Receive 'http' instance from DI
        private readonly HttpClient _httpClient;
        // Browser session storage instance from DI (data stored per tab not shared, no persisted)
        private readonly SessionStorage _sessionStorage;
        // Browser local storage instance from DI (data stored per url, shared between tabs, is persisted)
        private readonly LocalStorage _localStorage;
        // Authentication State
        private readonly Task<AuthenticationState> _authenticationStateTask;

        // Constructor
        public AppState(HttpClient httpClient, LocalStorage localStorage, SessionStorage sessionStorage, Task<AuthenticationState> authenticationStateTask)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _sessionStorage = sessionStorage;
            _authenticationStateTask = authenticationStateTask;
        }

        #endregion

        #region App State Properties

        public int SessionCounter
        {
            get { return int.Parse(_sessionStorage.GetItem("Counter") ?? "0"); }
            set { 
                _sessionStorage.SetItem("Counter", value);
                NotifyStateChanged();
            }
        }

        public int LocalCounter
        {
            get { return int.Parse(_localStorage.GetItem("Counter") ?? "0"); }
            set
            {
                _localStorage.SetItem("Counter", value);
                NotifyStateChanged();
            }
        }

        #endregion

        #region App State Functions

        public async Task<WeatherForecast[]> GetWeatherForecast()
        {
            return await _httpClient.GetJsonAsync<WeatherForecast[]>("WeatherForecast");
        }

        #endregion
    }
}
