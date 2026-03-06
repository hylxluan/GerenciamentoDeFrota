using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace GerenciamentoDeFrota.Helpers
{
    public static class WindowHandler
    {
        public static void Maximizar(Window window)
        {
            if (window == null) return;

            window.WindowState = window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        public static void Minimmizar(Window window)
        {
            if (window == null) return;

            window.WindowState = window.WindowState == WindowState.Minimized ? WindowState.Normal : WindowState.Minimized;

        }


        public static void Fechar(Window window)
        {
            if (window == null) return;
            window.Close();
        }
    }
}
