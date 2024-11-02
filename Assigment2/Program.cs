using FordonApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assigment2
{
    class Program
    {
        static void Main(string[] args) { var program = new Program(); program.Run(); }
        public Program()
        {
            var parkingGarage = new ParkingGarage(100);
            menu = new Menu(parkingGarage);
        }

        private Menu menu;
        private void Run() { menu.MainMenu(); }
    }
}
