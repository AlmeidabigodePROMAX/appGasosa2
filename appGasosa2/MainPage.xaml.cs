namespace appGasosa2
{
    public partial class MainPage : ContentPage
    {
       

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCalcularClicked(object sender, EventArgs e)
        {
            // Verifica se todos os campos foram preenchidos
            if (string.IsNullOrWhiteSpace(txtAlcool.Text) ||
                string.IsNullOrWhiteSpace(txtGasolina.Text) ||
                string.IsNullOrWhiteSpace(txtKmInicial.Text) ||
                string.IsNullOrWhiteSpace(txtKmFinal.Text) ||
                string.IsNullOrWhiteSpace(txtLitros.Text) ||
                string.IsNullOrWhiteSpace(txtConsumoAlcool.Text) ||
                string.IsNullOrWhiteSpace(txtConsumoGasolina.Text))
            {
                await DisplayAlert(
                    "Atenção",
                    "Preencha todos os campos.",
                    "OK");

                return;
            }

            // Tenta converter os valores informados pelo usuário
            bool alcoolValido = double.TryParse(
                txtAlcool.Text,
                out double precoAlcool);

            bool gasolinaValida = double.TryParse(
                txtGasolina.Text,
                out double precoGasolina);

            bool kmInicialValido = double.TryParse(
                txtKmInicial.Text,
                out double kmInicial);

            bool kmFinalValido = double.TryParse(
                txtKmFinal.Text,
                out double kmFinal);

            bool litrosValido = double.TryParse(
                txtLitros.Text,
                out double litros);

            bool consumoAlcoolValido = double.TryParse(
                txtConsumoAlcool.Text,
                out double consumoAlcool);

            bool consumoGasolinaValido = double.TryParse(
                txtConsumoGasolina.Text,
                out double consumoGasolina);


            // Verifica se os valores são números válidos
            if (!alcoolValido ||
                !gasolinaValida ||
                !kmInicialValido ||
                !kmFinalValido ||
                !litrosValido ||
                !consumoAlcoolValido ||
                !consumoGasolinaValido)
            {
                await DisplayAlert(
                    "Erro",
                    "Digite apenas valores numéricos válidos.",
                    "OK");

                return;
            }


            // Impede valores inválidos
            if (precoAlcool <= 0 ||
                precoGasolina <= 0 ||
                litros <= 0 ||
                consumoAlcool <= 0 ||
                consumoGasolina <= 0)
            {
                await DisplayAlert(
                    "Erro",
                    "Preços, litros e consumos devem ser maiores que zero.",
                    "OK");

                return;
            }


            // Verifica se a quilometragem final é maior que a inicial
            if (kmFinal <= kmInicial)
            {
                await DisplayAlert(
                    "Erro",
                    "A quilometragem final deve ser maior que a inicial.",
                    "OK");

                return;
            }


            // ========================================
            // 1. CÁLCULO DA DISTÂNCIA PERCORRIDA
            // Fórmula:
            // Distância = Km final - Km inicial
            // =====================================================

            double distancia = kmFinal - kmInicial;


            // =========================================
            // 2. CÁLCULO DO CONSUMO MÉDIO
            // Fórmula:
            // Consumo (km/L) = Distância percorrida / Litros
            // =====================================================

            double consumoMedio = distancia / litros;


            // ========================================
            // 3. CÁLCULO DO CUSTO POR KM
            //
            // Fórmula:
            // Custo por km = Preço do litro / Consumo (km/L)
            // =====================================================

            double custoKmAlcool =
                precoAlcool / consumoAlcool;

            double custoKmGasolina =
                precoGasolina / consumoGasolina;


            
            // 4. REGRA DOS 70%
            
            // Fórmula:
            //Índice = Preço do álcool / Preço da gasolina
            
            // Se índice <= 0,70:
            // Álcool tende a compensar
            
            // Se índice > 0,70:
            // Gasolina tende a compensar
            
            double indice70 =
                precoAlcool / precoGasolina;


            string resultado70;

            if (indice70 <= 0.70)
            {
                resultado70 =
                    $"Álcool tende a compensar. Índice: {indice70:P1}";
            }
            else
            {
                resultado70 =
                    $"Gasolina tende a compensar. Índice: {indice70:P1}";
            }


            // =====================================================
            // 5. COMPARAÇÃO REAL DO CUSTO POR KM
            // =====================================================

            string recomendacao;

            if (custoKmAlcool < custoKmGasolina)
            {
                recomendacao =
                    "Neste cenário compensa abastecer com ÁLCOOL.";
            }
            else if (custoKmGasolina < custoKmAlcool)
            {
                recomendacao =
                    "Neste cenário compensa abastecer com GASOLINA.";
            }
            else
            {
                recomendacao =
                    "Neste cenário os dois combustíveis têm o mesmo custo por km.";
            }


            // =====================================================
            // 6. EXIBIÇÃO DOS RESULTADOS
            // =====================================================

            lblDistancia.Text =
                $"Distância percorrida: {distancia:F2} km";

            lblConsumo.Text =
                $"Consumo calculado: {consumoMedio:F2} km/L";



            lblRegra70.Text =
                $"Regra dos 70%: {resultado70}";

            lblRecomendacao.Text =
                recomendacao;
        }


        // Botão limpar
        private void OnLimparClicked(object sender, EventArgs e)
        {
            txtAlcool.Text = "";
            txtGasolina.Text = "";
            txtKmInicial.Text = "";
            txtKmFinal.Text = "";
            txtLitros.Text = "";
            txtConsumoAlcool.Text = "";
            txtConsumoGasolina.Text = "";

            lblDistancia.Text = "Distância percorrida: -";
            lblConsumo.Text = "Consumo calculado: -";
            lblRegra70.Text = "Regra dos 70%: -";
            lblRecomendacao.Text = "Recomendação: -";



        }
    }
}
