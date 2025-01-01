using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHANEMOUGAM_Kaveen_EDMOND_Guillaume
{
    public class Camion_Frigo : Poids_Lourd
    {
        private double temperatureMin;  // Température minimale que le camion peut maintenir

        public Camion_Frigo(string numeroImmatriculation, double capaciteCharge, string typeCargaison, double temperatureMin)
        : base(numeroImmatriculation, capaciteCharge, typeCargaison)
        {
            this.temperatureMin = temperatureMin;
        }

        public override void AfficherDetails()
        {
            base.AfficherDetails();
            Console.WriteLine($"Température minimale maintenue: {temperatureMin}°C\n");
        }

        public static List<Camion_Frigo> InitListeCamionFrigos()
        {
            return new List<Camion_Frigo>
            {
                new Camion_Frigo("FRG001", 8000, "Alimentaire", -20),
                new Camion_Frigo("FRG002", 7500, "Pharmaceutique", -25),
                new Camion_Frigo("FRG003", 8200, "Alimentaire", -18),
                new Camion_Frigo("FRG004", 7700, "Alimentaire", -15),
                new Camion_Frigo("FRG005", 8500, "Chimique", -30)
            };
        }


    }
}
