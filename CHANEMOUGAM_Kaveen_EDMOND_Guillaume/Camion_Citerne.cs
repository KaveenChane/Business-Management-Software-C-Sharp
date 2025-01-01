using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHANEMOUGAM_Kaveen_EDMOND_Guillaume
{
    public class Camion_Citerne : Poids_Lourd
    {
        private string typeLiquide;  // Exemple: "Carburant", "Produits chimiques"

        public Camion_Citerne(string numeroImmatriculation, double capaciteCharge, string typeCargaison ,string typeLiquide)
       : base(numeroImmatriculation, capaciteCharge, typeCargaison)
        {
            this.typeLiquide = typeLiquide;
        }

        public override void AfficherDetails()
        {
            base.AfficherDetails();
            Console.WriteLine($"Type de liquide transporté: {typeLiquide}\n");
        }


        public static List<Camion_Citerne> InitListeCamionCiternes()
        {
            return new List<Camion_Citerne>
            {
                new Camion_Citerne("CIT001", 10000, "Carburant", "Essence"),
                new Camion_Citerne("CIT002", 12000, "Carburant", "Diesel"),
                new Camion_Citerne("CIT003", 9000, "Chimique", "Acide"),
                new Camion_Citerne("CIT004", 11000, "Alimentaire", "Huile"),
                new Camion_Citerne("CIT005", 9500, "Carburant", "Kérosène")
            };
        }


    }
}
