using Acr.UserDialogs;
using SmartTrade.Entities;
using SmartTrade.Logica.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;


//using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

//using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using static System.Net.WebRequestMethods;

namespace SmartTrade.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PedidoPage : ContentPage
    {

        STService service;
        List<ItemCarrito> Carrito;
        public Producto_vendedor productoVendedor_seleccionado;
        List<int> ProductosVendedor;
        public double precioTotalPedido;
        public int puntosTotalesPedido;
        public Entry numTarjeta;
        public Entry codSeguridad;
        public DatePicker fechaCad;
        public CheckBox guardarTarjetaCheck;
        public Button selectTarjetaButton;
        public Entry correoPaypal;
        public Entry passwordPaypal;
        public CheckBox verContraseña;
        Boolean pagoTarjeta;


    public PedidoPage(List <ItemCarrito> carrito)
        {
            InitializeComponent();
            
            this.service = STService.Instance;
            this.Carrito = carrito;
            InicializarElementos();
        } 

        public async void InicializarElementos()
        {
            try
            {
                Usuario user = service.GetUsuarioLogueado();
                Console.WriteLine(user.ToString());
                double precioT = 0;
                int puntosT = 0;
                ProductosVendedor = new List<int>();
                foreach (ItemCarrito item in Carrito)
                {
                    Producto_vendedor pv = await service.GetProductoVendedorById(item.idProductoVendedor);
                    ProductosVendedor.Add(pv.Id);
                    Producto p = await service.GetProductoById(pv.IdProducto);
                    puntosT += p.Puntos;
                    precioT += pv.Precio;
                }
                puntosTotalesPedido = puntosT;
                precioTotalPedido = precioT;
                PrecioTotal.Text = precioT.ToString() + "€";
                PuntosTotal.Text = puntosT.ToString();
                string[] direccion = user.Direccion.Split(',');
                calleEntry.Text = direccion[0];
                numeroEntry.Text = direccion[1];
                ciudadEntry.Text = direccion[2];
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al calcular el precio total: {e.Message}");
            }
            numTarjeta = new Entry
            {
                Placeholder = "Nº tarjeta",
                FontSize = 15,
                TextColor = Color.Gray,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Margin = new Thickness(70, 10, 0, 0),
                WidthRequest = 250,
                MaxLength = 10
            };
            numTarjeta.Focused += NumTarjeta_Focused;
            numTarjeta.TextChanged += NumTarjeta_TextChanged;
            codSeguridad = new Entry
            {
                Placeholder = "Cod seguridad",
                FontSize = 15,
                TextColor = Color.Gray,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Margin = new Thickness(15, 0, 0, 0),
                MaxLength = 3
            };
            codSeguridad.Focused += CodSeguridad_Focused;
            codSeguridad.TextChanged += CodSeguridad_TextChanged;
            fechaCad = new DatePicker
            {
                HorizontalOptions = LayoutOptions.Start,
                MinimumDate = DateTime.Today
            };
            guardarTarjetaCheck = new CheckBox
            {
                Margin = new Thickness(50, 0, 0, 0),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                IsChecked = false,

            };
            selectTarjetaButton = new Button
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "Seleccionar tarjeta",
                Margin = new Thickness(90, 10, 40, 0),
                HeightRequest = 39,
                CornerRadius = 25,
                BackgroundColor = Color.DarkBlue,
                FontSize = 13
            };
            selectTarjetaButton.Clicked += SelectTarjetaButton_Clicked;

            correoPaypal = new Entry
            {
                Placeholder = "Correo electrónico",
                FontSize = 15,
                TextColor = Color.Gray,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Margin = new Thickness(70, 0, 0, 0),
                MaxLength = 35,
                WidthRequest = 200

            };
            correoPaypal.Focused += correoPaypal_Focused;
            correoPaypal.TextChanged += correoPaypal_TextChanged;

            passwordPaypal = new Entry
            {
                Placeholder = "Contraseña",
                IsPassword = true,
                FontSize = 15,
                TextColor = Color.Gray,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Margin = new Thickness(70, 0, 0, 0),
                MaxLength = 35
            };
            passwordPaypal.Focused += passwordPaypal_Focused;
            passwordPaypal.TextChanged += passwordPaypal_TextChanged;

            verContraseña = new CheckBox
            {
                IsChecked = false,
                Margin = new Thickness(75, 0, 0, 0)
            };
            verContraseña.CheckedChanged += verContraseña_Changed;
        }
        override protected void OnAppearing()
        {
            base.OnAppearing();
            UserDialogs.Instance.ShowLoading("Cargando...");
            CargarProductosLista();
            UserDialogs.Instance.HideLoading();
        }


        private async Task CargarProductosLista()
        {
            try
            {
                StackLayout listaProd = this.FindByName<StackLayout>("listaItems");

                listaProd.Children.Clear();
                await MostrarProductos(Carrito);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al cargar los productos de la lista: {e.Message}");
            }
        }

        private async Task MostrarProductos(List<ItemCarrito> carrito)
        {

            StackLayout stackLayout = this.FindByName<StackLayout>("listaItems");

            foreach (ItemCarrito item in carrito)
            {
                Console.WriteLine("EL ITEM " + item.Id);
                await crearTarjeta(item, stackLayout);
            }

        }

        private async Task crearTarjeta(ItemCarrito item, StackLayout stackLayout)
        {
            try
            {
                int cantidad = item.Cantidad;
                //De product_vendedor saco el precio 
                Producto_vendedor producto_vendedor =  await service.GetProductoVendedorById(item.idProductoVendedor);
                //De producto saco los datos de él, nombre, imagen ...
                int idProducto = producto_vendedor.IdProducto;
                Producto producto = await service.GetProductoById(idProducto);
                Console.WriteLine("[+] PRODUCTO:" + producto.Id);
                Console.WriteLine("[+] PRODUCTO_VENDEDOR:" + producto_vendedor.Id);
                Console.WriteLine("[+] CANTIDAD:" + cantidad);
                Console.WriteLine("[+] PRECIO:" + producto_vendedor.Precio);
                Console.WriteLine("[+] NOMBRE:" + producto.Nombre);


                string caracteristicas = "";
                double precio_total = cantidad * producto_vendedor.Precio;

 
                var productCard = new Frame
                {
                    BackgroundColor = Color.White,
                    CornerRadius = 10,
                    Margin = new Thickness(10),
                    Padding = new Thickness(10),
                    HasShadow = true,
                    Content = new Grid
                    {
                        Padding = 15,
                        RowDefinitions =
                        {
                            new RowDefinition { Height = GridLength.Auto },
                            new RowDefinition { Height = GridLength.Auto },
                            new RowDefinition { Height = GridLength.Auto },
                            new RowDefinition { Height = GridLength.Auto },
                            new RowDefinition { Height = GridLength.Auto }
                        },
                        ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = GridLength.Auto },
                            new ColumnDefinition { Width = GridLength.Star },
                            new ColumnDefinition { Width = GridLength.Auto },
                            new ColumnDefinition { Width = GridLength.Auto }
                        }
                    }
                };
                var grid = (Grid)productCard.Content;
                
                grid.Children.Add(
                                    new Image
                                    {
                                        Source = producto.Imagen,
                                        HeightRequest = 80,
                                        WidthRequest = 80,
                                        Aspect = Aspect.AspectFit,
                                    },
                                   0, 1
                                );
                grid.Children.Add(
                                new Label
                                {
                                    Text = producto.Nombre,
                                    FontAttributes = FontAttributes.Bold
                                },
                               1, 1
                            );
                grid.Children.Add(
                                new Label
                                {
                                    Text ="Cantidad: " + cantidad,
                                    FontAttributes = FontAttributes.Italic,
                                    VerticalOptions = LayoutOptions.End,
                                    MaxLines = 2,
                                    LineBreakMode = LineBreakMode.TailTruncation
                                },
                               1, 2
                            );
                grid.Children.Add(
                               new Label
                               {
                                   Text = "Total: " + precio_total  + "€",
                                   FontAttributes = FontAttributes.Italic,
                                   VerticalOptions = LayoutOptions.End,
                                   MaxLines = 2,
                                   LineBreakMode = LineBreakMode.TailTruncation
                               },
                              2, 2
                           );
                stackLayout.Children.Add(productCard);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al obtener el productoO: {e.Message}");
            }
        }


        private void BtnAtras_click(object sender, EventArgs e)
        {
            Console.WriteLine("Atras");
            Navigation.PopAsync();
        }

        private void BtnPerfil_click(object sender, EventArgs e)
        {
            //TODO 
            Console.WriteLine("Perfil");
        }

        private void BtnFinalizarCompra_click(object sender, EventArgs e)
        {
            //TODO
            Console.WriteLine("Finalizar Compra");
        }

        private bool IsValidoCorreo(string correo)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(correo);
                return addr.Address == correo;
            }
            catch
            {
                return false;
            }
        }


        //Pone los elementos de la tarjeta o del paypal segun la eleccion
    private void pickerPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            StackLayout stackLayout = this.FindByName<StackLayout>("PagoStacklayout");
            if (pickerPago.SelectedItem.ToString() == "Tarjeta de crédito")
            {
                stackLayout.Children.Clear();
                var sL = new StackLayout {
                 
                    Children = {
                   selectTarjetaButton,
                    numTarjeta,
                    new Label
                    {
                        Text = "Fecha caducidad",
                        FontSize= 13,
                        Margin= new Thickness(70,20,0,-20),
                        HorizontalOptions= LayoutOptions.StartAndExpand
                    },
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Margin = new Thickness(70,0,0,20),
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        WidthRequest = 250,
                        Children =
                        {
                           fechaCad,
                           codSeguridad,
                        }
                    },
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Margin = new Thickness(50,0,0,20),
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        WidthRequest = 250,
                        Children =
                        {
                            guardarTarjetaCheck,
                               new Label
                               {
                                   Text = "Guardar tarjeta para futuras compras",
                                   FontSize= 13,
                                   TextColor= Color.Black,
                               }
                        }
                    }
                  }
               };
                stackLayout.Children.Add(sL);
            }
            else if(pickerPago.SelectedItem.ToString() == "Paypal")
            {

                stackLayout.Children.Clear();
                var sL = new StackLayout
                {
                    Children =
                    {
                        correoPaypal,
                        passwordPaypal,
                        new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            Margin = new Thickness(50,0,0,20),
                            HorizontalOptions = LayoutOptions.StartAndExpand,
                            WidthRequest = 250,
                            Children =
                            {
                                verContraseña,
                                new Label
                                {
                                    Text= "Mostrar contraseña",
                                    FontSize = 13,
                                    TextColor= Color.Black,

                                }
                            }
                       }
                        
                    }
                };
                stackLayout.Children.Add(sL);
            }
            
        }
        public Boolean isNumerico(string s)
        {
                foreach (char c in s)
                {
                    if (!char.IsDigit(c))
                    {
                        return false;
                    }
                }
                return true;
            
        }
        
        private void NumTarjeta_Focused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(numTarjeta.Text))
            {
                numTarjeta.TextColor = Color.Gray;
            }
        }
        

        private void NumTarjeta_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(numTarjeta.Text)) { numTarjeta.TextColor = Color.Black; }
            
        }
        private void CodSeguridad_Focused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(codSeguridad.Text))
            {
                codSeguridad.TextColor = Color.Gray;
            }
        }


        private void CodSeguridad_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(codSeguridad.Text)) { codSeguridad.TextColor = Color.Black; }
        }
        private void correoPaypal_Focused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(correoPaypal.Text))
            {
                correoPaypal.TextColor = Color.Gray;
            }
        }
        private void correoPaypal_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(correoPaypal.Text)) { correoPaypal.TextColor = Color.Black; }
        }
        private void passwordPaypal_Focused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(passwordPaypal.Text))
            {
                passwordPaypal.TextColor = Color.Gray;
            }
        }

        public Boolean IsValidaContraseña(string contraseña)
        {

            if (string.IsNullOrEmpty(contraseña) || contraseña.Length < 8) return false;
            return contraseña.Any(char.IsDigit);
        }

        private void passwordPaypal_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordPaypal.Text)) { passwordPaypal.TextColor = Color.Black; }
            string password = e.NewTextValue;
            if (IsValidaContraseña(password) == false)
            { passwordPaypal.TextColor = Color.Red; }
            else passwordPaypal.TextColor = Color.Black;

        }

        private void verContraseña_Changed(object sender, CheckedChangedEventArgs e)
        {
            if (verContraseña.IsChecked == true) passwordPaypal.IsPassword = false;
            else passwordPaypal.IsPassword = true; ;
        }
        private async void SelectTarjetaButton_Clicked(object sender, EventArgs e) 
        {
            List <Tarjeta> tarjetas = await service.getTarjetas();
            List <string> options = new List<string>();
            if (tarjetas.Count != 0)
            {
                foreach (Tarjeta tarjeta in tarjetas)
                {
                   
                    options.Add($"{tarjeta.Numero},{tarjeta.Fecha_cad.ToShortDateString()},{tarjeta.Num_seguridad}");
                    
                }
            }
            else { options.Add("No tienes tarjetas guardadas");
            }
            string action = await DisplayActionSheet("Selecciona una tarjeta", "Cancel", null, options.ToArray());

            if (action != null && action != "Cancel" && action != "No tienes tarjetas guardadas")
            {
                Tarjeta tarjetaSeleccionada = await service.getTarjetaById(action);
                string [] tarjetaArray = action.ToString().Split(',');
                foreach(string t in tarjetaArray)
                {
                    Console.WriteLine(t);
                }
                
                numTarjeta.Text = tarjetaArray[0];
                
                if (DateTime.TryParse(tarjetaArray[1], out DateTime dateValue))
                {
                    // Establecer la fecha en el DatePicker
                    fechaCad.Date = dateValue;
                }
                codSeguridad.Text = tarjetaArray[2];
            }
        }

        private async void RealizarPedidoButton_Clicked(object sender, EventArgs e)
        {
            
           if (pickerPago.SelectedIndex == -1)
            {
                    await DisplayAlert("Error", "Por favor, elija un método de pago", "Aceptar");
                    return;
                
            }
            if (string.IsNullOrWhiteSpace(calleEntry.Text) || string.IsNullOrWhiteSpace(numeroEntry.Text) || string.IsNullOrWhiteSpace(ciudadEntry.Text))
            {
                await DisplayAlert("Error", "Por favor, complete todos los campos.", "Aceptar");
                return;
            }
          
           

            else
            {
                if (pickerPago.SelectedItem.ToString() == "Tarjeta de crédito")
                {
                    pagoTarjeta = true;
                    if (string.IsNullOrWhiteSpace(numTarjeta.Text) || string.IsNullOrWhiteSpace(codSeguridad.Text))
                    {
                        await DisplayAlert("Error", "Por favor, complete todos los campos.", "Aceptar");
                        return;
                    }
                    
                    if ( !isNumerico(numTarjeta.Text))
                    {
                        await DisplayAlert("Error", "Por favor, introduzca una tarjeta válida", "Aceptar");
                        numTarjeta.TextColor = Color.Red;
                        return;
                    }
                    if (!isNumerico(codSeguridad.Text))
                    {
                        await DisplayAlert("Error", "Por favor, introduzca una tarjeta válida", "Aceptar");
                        codSeguridad.TextColor = Color.Red;
                        return;
                    }

                }
                if (pickerPago.SelectedItem.ToString() == "Paypal")
                {
                    pagoTarjeta = false;
                    if (string.IsNullOrWhiteSpace(correoPaypal.Text) || string.IsNullOrWhiteSpace(passwordPaypal.Text))
                    {
                        await DisplayAlert("Error", "Por favor, complete todos los campos.", "Aceptar");
                        return;
                    }
                    if (!IsValidoCorreo(correoPaypal.Text))
                    {
                        await DisplayAlert("Error", "Por favor, ponga un correo válido.", "Aceptar");
                        correoPaypal.TextColor = Color.Red;
                        return;
                    }
                }

                int numeroTarjeta = 0;
                if (pagoTarjeta)
                {
                    if (guardarTarjetaCheck.IsChecked)
                    {
                        List<Tarjeta> tarjetasUser = await service.getTarjetas();
                        int.TryParse(numTarjeta.Text, out int numTarjetaInt);
                        int.TryParse(codSeguridad.Text, out int codSeguridadInt);
                        numeroTarjeta = numTarjetaInt;
                        Tarjeta nuevaTarjeta = new Tarjeta(numTarjetaInt, fechaCad.Date, codSeguridadInt, service.GetLoggedNickname());
                        await service.AddTarjeta(nuevaTarjeta);
                    }
                }
                foreach(ItemCarrito item in Carrito)
                {
                   await service.EliminarItemCarrito(item);
                }

            string Direccion = $"{calleEntry.Text}, {numeroEntry.Text}, {ciudadEntry.Text}";
              
            Pedido pedidoNuevo = new Pedido(DateTime.Now, precioTotalPedido, ProductosVendedor, service.GetLoggedNickname(), Direccion, numeroTarjeta, puntosTotalesPedido, "En preparación");

            await service.AddPedido(pedidoNuevo);
            MisPedidos misPedidos = new MisPedidos(pedidoNuevo);
            await Navigation.PushAsync(misPedidos);
                
            }
        }
    }
}
