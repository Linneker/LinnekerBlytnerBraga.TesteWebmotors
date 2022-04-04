using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteWebmotors.Dominio.DTO.ServicosExternos
{
    public class VehiclesDTO : BaseService
    {
        
        public VehiclesDTO(int id, string make, string model, string version,
            string image, int kM, string price, int yearModel, int yearFab, 
            string color) : base(id)
        {
            Make = make;
            Model = model;
            Version = version;
            Image = image;
            KM = kM;
            Price = price;
            YearModel = yearModel;
            YearFab = yearFab;
            Color = color;
        }

        public string Make { get; private set; }
        public string Model { get; private set; }
        public string Version { get; private set; }
        public string Image { get; private set; }
        public int KM { get; private set; }
        public string Price { get; private set; }
        public int YearModel { get; private set; }
        public int YearFab { get; private set; }
        public string Color { get; private set; }
    }
}
