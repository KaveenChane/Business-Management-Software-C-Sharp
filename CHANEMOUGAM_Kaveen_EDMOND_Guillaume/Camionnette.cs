using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHANEMOUGAM_Kaveen_EDMOND_Guillaume
{
    public class Camionnette : Vehicule
    {
        private string usage;  // Usage spécifique de la camionnette, ex: "Artisanat", "Livraison de proximité"

        public Camionnette(string numeroImmatriculation, double capaciteCharge, string usage)
        : base(numeroImmatriculation, capaciteCharge)
        {
            this.usage = usage;
        }

        public override void AfficherDetails()
        {
            base.AfficherDetails();
            Console.WriteLine($"Usage spécifique: {usage}\n");
        }


        public static List<Camionnette> InitListeCamionnettes()
        {
            List<Camionnette> camionnettes = new List<Camionnette>
            {
                new Camionnette("VAN001", 1200, "Artisanat"),
                new Camionnette("VAN002", 1500, "Livraison de proximité"),
                new Camionnette("VAN003", 1300, "Transport d'équipement médical"),
                new Camionnette("VAN004", 1400, "Services de nettoyage"),
                new Camionnette("VAN005", 1100, "Livraison de fleurs")
            };
            return camionnettes;
        }
    }
}
