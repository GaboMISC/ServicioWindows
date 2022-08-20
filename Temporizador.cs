using System;
using System.IO;
using System.Timers;

namespace WindowsService
{
    public class Temporizador
    {
        private readonly Timer disparador;
        public Temporizador(int tiempo)
        {
            disparador = new Timer(2000);
            disparador.Elapsed += EventoElapsed;
        }

        public void IniciarMonitoreo()
        {
            disparador.Start();
        }

        public void DetenerMonitoreo()
        {
            disparador.Stop();
        }

        private void EventoElapsed(object sender, ElapsedEventArgs e)
        {
            EscribirLog($"El Servicio Windows esta funcionando: {DateTime.Now}");
        }

        private void EscribirLog(string Message)
        {
            object objeto = new object();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Logs";
            string filepath = Path.Combine(path, @"ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            else
            {
                lock (objeto)
                {
                    using (StreamWriter sw = File.AppendText(filepath))
                    {
                        sw.WriteLine(Message);
                    }
                }                
            }
        }
    }
}
