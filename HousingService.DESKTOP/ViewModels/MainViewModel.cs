
using HousingService.DESKTOP.Commands;
using HousingService.DESKTOP.Models;
using HousingService.DESKTOP.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HousingService.DESKTOP.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;

        private ObservableCollection<CitiesDTO> _cities = new ObservableCollection<CitiesDTO>();
        public ObservableCollection<CitiesDTO> Cities
        {
            get => _cities;
            set => Set(ref _cities, value);
        }

        private ObservableCollection<StreetsDTO> _streets = new ObservableCollection<StreetsDTO>();
        public ObservableCollection<StreetsDTO> Streets
        {
            get => _streets;
            set => Set(ref _streets, value);
        }

        private ObservableCollection<HousesDTO> _houses = new ObservableCollection<HousesDTO>();
        public ObservableCollection<HousesDTO> Houses
        {
            get => _houses;
            set => Set(ref _houses, value);
        }

        private ObservableCollection<ApartmentsDTO> _apartments = new ObservableCollection<ApartmentsDTO>();
        public ObservableCollection<ApartmentsDTO> Apartments
        {
            get => _apartments;
            set => Set(ref _apartments, value);
        }

        private CitiesDTO? _selectedCity;
        public CitiesDTO? SelectedCity
        {
            get => _selectedCity;
            set
            {
                CitiesDTO? oldValue = _selectedCity;
                if (Set(ref _selectedCity, value))
                {
                    if (value != null)
                    {
                        SelectedStreet = null;
                        SelectedHouse = null;
                    }
                    else if (oldValue != null)
                    {
                        Streets.Clear();
                        Houses.Clear();
                        Apartments.Clear();
                    }
                }
            }
        }

        private StreetsDTO? _selectedStreet;
        public StreetsDTO? SelectedStreet
        {
            get => _selectedStreet;
            set
            {
                StreetsDTO? oldValue = _selectedStreet;
                if (Set(ref _selectedStreet, value))
                {
                    if (value != null)
                    {
                        SelectedHouse = null;
                    }
                    else if (oldValue != null)
                    {
                        Houses.Clear();
                        Apartments.Clear();
                    }
                }
            }
        }

        private HousesDTO? _selectedHouse;
        public HousesDTO? SelectedHouse
        {
            get => _selectedHouse;
            set
            {
                HousesDTO? oldValue = _selectedHouse;
                if (Set(ref _selectedHouse, value))
                {
                    if (value != null)
                    {
                    }
                    else if (oldValue != null)
                    {
                        Apartments.Clear();
                        MinAreaFilter = null;
                        MaxAreaFilter = null;
                    }
                }
            }
        }

        private string _currentLevel = "Города";
        public string CurrentLevel
        {
            get => _currentLevel;
            set
            {
                Set(ref _currentLevel, value);
                ((RelayCommand)GoBackCommand).RaiseCanExecuteChanged();
            }
        }

        private double? _minAreaFilter;
        public double? MinAreaFilter
        {
            get => _minAreaFilter;
            set
            {
                if (Set(ref _minAreaFilter, value))
                {
                    ApplyApartmentFilter();
                }
            }
        }

        private double? _maxAreaFilter;
        public double? MaxAreaFilter
        {
            get => _maxAreaFilter;
            set
            {
                if (Set(ref _maxAreaFilter, value))
                {
                    ApplyApartmentFilter();
                }
            }
        }

        public ICommand LoadCitiesCommand { get; }
        public ICommand LoadStreetsCommand { get; }
        public ICommand LoadHousesCommand { get; }
        public ICommand LoadApartmentsByHouseCommand { get; }
        public ICommand GoBackCommand { get; }
        public ICommand FilterApartmentsCommand { get; }

        public MainViewModel()
        {
            IConfiguration configuration = App.Configuration;
            _apiService = new ApiService(configuration);

            LoadCitiesCommand = new RelayCommand(async _ => await LoadCities());

            LoadStreetsCommand = new RelayCommand(async parameter => await ExecuteLoadStreets((CitiesDTO)parameter!), parameter => parameter is CitiesDTO);
            LoadHousesCommand = new RelayCommand(async parameter => await ExecuteLoadHouses((StreetsDTO)parameter!), parameter => parameter is StreetsDTO);
            LoadApartmentsByHouseCommand = new RelayCommand(parameter => ExecuteLoadApartments((HousesDTO)parameter!), parameter => parameter is HousesDTO);


            GoBackCommand = new RelayCommand(_ => GoBack(), _ => CanGoBack());
            FilterApartmentsCommand = new RelayCommand(_ => ApplyApartmentFilter());

            LoadCitiesCommand.Execute(null);
        }


        private async Task LoadCities()
        {
            try
            {
                var cities = await _apiService.GetCitiesAsync();
                Cities = new ObservableCollection<CitiesDTO>(cities ?? new List<CitiesDTO>());

                CurrentLevel = "Города";
                SelectedCity = null;
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"Ошибка при загрузке городов: {httpEx.Message}", "Ошибка API", MessageBoxButton.OK, MessageBoxImage.Error);
                CurrentLevel = "Error";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при загрузке городов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                CurrentLevel = "Error";
            }
        }

        private async Task ExecuteLoadStreets(CitiesDTO city)
        {
            SelectedCity = city;
            try
            {
                var streets = await _apiService.GetStreetsByCityAsync(city.Id);
                Streets = new ObservableCollection<StreetsDTO>(streets ?? new List<StreetsDTO>());
                CurrentLevel = "Улицы";
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"Ошибка при загрузке улиц: {httpEx.Message}", "Ошибка API", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при загрузке улиц: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task ExecuteLoadHouses(StreetsDTO street)
        {
            SelectedStreet = street;
            try
            {
                List<HousesDTO>? houses = await _apiService.GetHousesByStreetAsync(street.Id);
                Houses = new ObservableCollection<HousesDTO>(houses ?? new List<HousesDTO>());
                CurrentLevel = "Дома";
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"Ошибка при загрузке домов: {httpEx.Message}", "Ошибка API", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при загрузке домов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExecuteLoadApartments(HousesDTO house)
        {
            SelectedHouse = house;

            Apartments = new ObservableCollection<ApartmentsDTO>(SelectedHouse.Apartments ?? new List<ApartmentsDTO>());
            CurrentLevel = "Квартиры";

            MinAreaFilter = null;
            MaxAreaFilter = null;
        }


        private void ApplyApartmentFilter()
        {
            if (SelectedHouse == null || SelectedHouse.Apartments == null) return;

            IEnumerable<ApartmentsDTO> filteredApartments = SelectedHouse.Apartments;

            if (MinAreaFilter.HasValue)
            {
                filteredApartments = filteredApartments.Where(a => a.Area >= MinAreaFilter.Value);
            }
            if (MaxAreaFilter.HasValue)
            {
                filteredApartments = filteredApartments.Where(a => a.Area <= MaxAreaFilter.Value);
            }

            Apartments = new ObservableCollection<ApartmentsDTO>(filteredApartments);
        }

        private void GoBack()
        {
            switch (CurrentLevel)
            {
                case "Улицы":
                    SelectedCity = null;
                    CurrentLevel = "Города";
                    break;
                case "Дома":
                    SelectedStreet = null;
                    CurrentLevel = "Улицы";
                    break;
                case "Квартиры":
                    SelectedHouse = null;
                    CurrentLevel = "Дома";
                    break;
                default:
                    break;
            }
        }

        private bool CanGoBack()
        {
            return CurrentLevel != "Города";
        }
    }
}
