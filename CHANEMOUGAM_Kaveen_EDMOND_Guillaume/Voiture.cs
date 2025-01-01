using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHANEMOUGAM_Kaveen_EDMOND_Guillaume
{
    public class Voiture : Vehicule
    {
        private int nombreDePassagers;  // Nombre maximal de passagers que la voiture peut transporter

        public Voiture(string numeroImmatriculation, double capaciteCharge, int nombreDePassagers)
        : base(numeroImmatriculation, capaciteCharge)
        {
            this.nombreDePassagers = nombreDePassagers;
        }

        public override void AfficherDetails()
        {   
            base.AfficherDetails();
            Console.WriteLine($"Nombre de passagers: {nombreDePassagers}\n");
        }


        public static List<Voiture> InitListeVoitures()
        {
            List<Voiture> voitures = new List<Voiture>
            {
                new Voiture("ABC-123", 500, 4),
                new Voiture("XYZ-789", 450, 5),
                new Voiture("DEF-456", 550, 4)
            };
            return voitures;
        }

    }

}
