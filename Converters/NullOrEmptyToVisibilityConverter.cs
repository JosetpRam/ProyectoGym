using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ProyectoGym.Converters
{
    public class NullOrEmptyToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => string.IsNullOrWhiteSpace(value as string) ? Visibility.Collapsed : Visibility.Visible;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    } //Aqui lo que hago es que este pedazo de codigo es un convertidor que se utiliza en para convertir una cadena de texto a una propiedad de visibilidad y si la cadena es nula o esta vacia el convertidor devuelve Visibility.Collapsed, lo que significa que el elemento no será visible en la interfaz de usuario. Si la cadena tiene contenido, el convertidor devuelve esavisibilidad y ya conierte los datos antes de mostrarlos a la interfaz
}
