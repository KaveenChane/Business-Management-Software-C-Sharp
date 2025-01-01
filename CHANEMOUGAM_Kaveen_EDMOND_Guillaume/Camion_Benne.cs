using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHANEMOUGAM_Kaveen_EDMOND_Guillaume
{
    public class Camion_Benne : Poids_Lourd
    {
        private int nombreDeBennes;  // Nombre de bennes du camion
        private bool possedeGrue;  // Indique si le camion est équipé d'une grue

        public Camion_Benne(string numeroImmatriculation, double capaciteCharge, string typeCargaison, int nombreDeBennes, bool possedeGrue)
        : base(numeroImmatriculation, capaciteCharge, typeCargaison)
        {
            this.nombreDeBennes = nombreDeBennes;
            this.possedeGrue = possedeGrue;
        }

        public override void AfficherDetails()
        {
            base.AfficherDetails();
            Console.WriteLine($"Nombre de bennes: {nombreDeBennes}, Possède grue: {possedeGrue}\n");
        }


        public static List<Camion_Benne> InitListeCamionBennes()
        {
            return new List<Camion_Benne>
            {
                new Camion_Benne("BEN001", 5000, "Construction", 3, true),
                new Camion_Benne("BEN002", 4500, "Débris", 2, false),
                new Camion_Benne("BEN003", 5500, "Minier", 4, true),
                new Camion_Benne("BEN004", 6000, "Gravier", 3, false),
                new Camion_Benne("BEN005", 6200, "Terre", 2, true)
            };
        }


    }

}
