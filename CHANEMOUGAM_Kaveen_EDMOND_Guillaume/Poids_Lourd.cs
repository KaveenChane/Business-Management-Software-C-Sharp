using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHANEMOUGAM_Kaveen_EDMOND_Guillaume
{
    public class Poids_Lourd : Vehicule
    {
        private string typeCargaison; // Exemple: "Produits alimentaires", "Matériaux de construction"

        public Poids_Lourd(string numeroImmatriculation, double capaciteCharge, string typeCargaison)
        : base(numeroImmatriculation, capaciteCharge)
        {
            this.typeCargaison = typeCargaison;
        }

        public override void AfficherDetails()
        {
            base.AfficherDetails();
            Console.WriteLine($"Type de cargaison: {typeCargaison}\n");
        }
    }

}
