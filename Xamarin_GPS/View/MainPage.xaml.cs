using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading;
using Xamarin.Forms.Xaml;
using Android.Widget;

namespace Xamarin_GPS
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
                        
        }

        private async void BtnLanternaOn_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Flashlight.TurnOnAsync();  //Ligar lanterna
                               
                await Task.Delay(60000);         //Delay 60 segundos   
                await Flashlight.TurnOffAsync(); //Desliga automaticamente a lanterna
            }
            catch (PermissionException ex)
            {
                await DisplayAlert("Erro", ex.ToString(), "OK");
            }
        }

        private async void BtnLanternaOff_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Flashlight.TurnOffAsync();  //Desligar lanterna
            }
            catch (PermissionException ex)
            {
                await DisplayAlert("Erro", ex.ToString(), "OK");
            }
        }



        private async void BtnGeolocation_Clicked(object sender, EventArgs e)
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(60));
                var location = await Geolocation.GetLocationAsync(request);
                //var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    await DisplayAlert("Alerta:", "Latitude : " + location.Latitude + "\n" +
                                                  "Longitude: " + location.Longitude + "\n" +
                                                  "Altura   : " + location.Altitude + "\n" +
                                                  "Acuracia : " + location.Accuracy, "OK");                                     
                }
                else
                {
                    await DisplayAlert("GPS:", "Não é possivel localizar !", "OK");
                }
                
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Erro 1", fnsEx.ToString(), "OK");
            }
            catch (FeatureNotEnabledException)
            {
                await DisplayAlert("Erro 2", "Ative o GPS !", "OK");
            }
            catch (PermissionException ex)
            {
                await DisplayAlert("Erro 3", ex.ToString(), "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro 4", ex.ToString(), "OK");
            }

        }

        private void btnVibrator_Clicked(object sender, EventArgs e)
        {
            try
            {
                for (int n=1; n<6; n++)
                {
                    Vibration.Vibrate(TimeSpan.FromMilliseconds(150));
                    Thread.Sleep(300);
                }

                Toast toast = Toast.MakeText(Android.App.Application.Context, "Vibrador Funcionou !", ToastLength.Short);
                toast.Show();
            }
            catch (Exception ex)
            {
                Toast toast = Toast.MakeText(Android.App.Application.Context, ex.ToString(), ToastLength.Short);
                toast.Show();
            }

        }
    }
}
