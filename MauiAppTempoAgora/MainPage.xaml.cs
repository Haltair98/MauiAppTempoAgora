using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = "";

                        dados_previsao = $"Latitude: {t.lat} \n" +
                                         $"Longitude: {t.lon} \n" +
                                         $"Nascer do Sol: {t.sunrise} \n" +
                                         $"Por do Sol: {t.sunset} \n" +
                                         $"Temp Máx : {t.temp_max} \n" +
                                         $"Temp Min: {t.temp_min}  \n" +
                                         $"Visiblidade: {t.visibility} \n" +
                                         $"Velocidade do vento: {t.speed} \n" +
                                         $"Descrição do clima: {t.description} \n";

                        lbl_res.Text = dados_previsao;

                    }
                    else
                    {
                        await DisplayAlert("Cidade não existe", "Aprenda a escrever e tente mais uma vez.", "vou melhorar T_T");
                    }

                }
                else
                {
                    lbl_res.Text = "Preencha a cidade.";
                }
            }
            catch (HttpRequestException ex) // Captura erros de rede
            {
                await DisplayAlert("Sem Conexão", "Você está sem conexão com a internet. Pague seus boletos e tente novamente.", "OK");
            }
   
            catch (Exception ex)
            {
                await DisplayAlert("ops", ex.Message, "Ok");
            }
        }
    }

}
