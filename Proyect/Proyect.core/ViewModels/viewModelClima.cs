﻿using MvvmCross.Core.ViewModels;
using System;

using System.Threading.Tasks;
using Newtonsoft.Json;
using Proyect.core.Services;
using System.Net.Http;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Proyect.core.Services.Models;


namespace Proyect.core.ViewModels
{
    public class viewModelClima : MvxViewModel
    {
        
        public override void Start()
        {
           
            base.Start();
        }
     
        public viewModelClima()
        {
          ListaDeClima = new ObservableCollection<DatosClima>();
       
        }

        public string Ciudad
        {
            get { return datos.Ciudad; }
            set { datos.Ciudad = value; RaisePropertyChanged(() => Ciudad); }
        }


        public string Pais
        {
            get { return datos.Pais; }
            set { datos.Pais = value; RaisePropertyChanged(() => Pais); }
        }


        public string Temperatura
        {
            get { return datos.Temperatura; }
            set { datos.Temperatura = value; RaisePropertyChanged(() => Temperatura); }
        }


        public string Humedad
        {

            get { return datos.Humedad; }
            set { datos.Humedad = value; RaisePropertyChanged(() => Humedad); }
        }


        public string HoraConsulta
        {
            get { return datos.HoraConsulta; }
            set { datos.HoraConsulta = value; RaisePropertyChanged(() => HoraConsulta); }
        }
        private ICommand go;
        public ICommand Go
        {
            get { return this.go = this.go ?? new MvxAsyncCommand(this.ActualizarClima); }
        }



      
              private ObservableCollection<DatosClima> listaDeClima;

                public ObservableCollection<DatosClima> ListaDeClima
                {
                    get
                    {
                        return listaDeClima;
                    }
                    set
                    {
                        listaDeClima = value;
                this.RaisePropertyChanged(() => this.ListaDeClima);
                    }
                }

        //  ObservableCollection<DatosClima> ListaDeClima = new ObservableCollection<DatosClima>();
        int numeroConsulta = 1;

        private async Task ActualizarClima()
        {  
            var url = new Uri("http://api.apixu.com/v1/current.json?key=43dad43a410f428d86e133125171804&q=" + Ciudad);
            var httpClient = new HttpClient();
            var result = await httpClient.GetStringAsync(url);
            httpClient.Dispose();
            RespuestaTiempo json = JsonConvert.DeserializeObject<RespuestaTiempo>(result);
            this.Pais = json.location.country;
            this.HoraConsulta = Convert.ToString(json.location.localtime);
            this.Humedad = Convert.ToString(json.current.humidity) + "%";
            this.Temperatura = Convert.ToString(json.current.temp_c) + "°C";
            this.ListaDeClima.Add(new DatosClima{ Ciudad=Ciudad,Pais= json.location.country,HoraConsulta=Convert.ToString(json.location.localtime),Humedad = Convert.ToString(json.current.humidity) + "%" ,Temperatura= Convert.ToString(json.current.temp_c) + "°C" ,Id=numeroConsulta});
            this.numeroConsulta++;
        }




        private ICommand guardarClima;
        public ICommand GuardarClima
        {
            get { return this.guardarClima = this.guardarClima ?? new MvxCommand(this.cargarDatos); }
        }


        DatosClima datos = new DatosClima();

    
        public void cargarDatos()
        {      
            ListaDeClima.Add(datos);    
        }


        private ICommand getRecord;
        public ICommand GetRecord
        {
            get { return this.getRecord = this.getRecord ?? new MvxCommand(this.goToViewModel); }
        }

        void goToViewModel()
        {
            string datosSerialized = JsonConvert.SerializeObject(this.ListaDeClima);
            ShowViewModel<viewModelHistorial>(new { datos = datosSerialized });

        }
    }
}


    
