using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHANEMOUGAM_Kaveen_EDMOND_Guillaume
{
    public abstract class Vehicule
    {
        protected string numeroImmatriculation;
        protected double capaciteCharge ;  // Capacité en kilogrammes ou en litres
        private HashSet<DateTime> datesIndisponibles; // HashSet permet d'eviter les doublons

        protected Vehicule(string numeroImmatriculation, double capaciteCharge)
        {
            this.numeroImmatriculation = numeroImmatriculation;
            this.capaciteCharge = capaciteCharge;
            this.datesIndisponibles = new HashSet<DateTime>();
        }

        /// <summary>
        /// Ajoute une date d'indisponibilité pour le véhicule.
        /// </summary>
        /// <param name="date">Date d'indisponibilité.</param>
        public void AjouterIndisponibilite(DateTime date)
        {
            datesIndisponibles.Add(date);
        }


        /// <summary>
        /// Vérifie si le véhicule est disponible à une date donnée.
        /// </summary>
        /// <param name="date">Date à vérifier.</param>
        /// <returns>True si le véhicule est disponible, false sinon.</returns>
        public bool EstDisponible(DateTime date)
        {
            return !datesIndisponibles.Contains(date);
        }


        /// <summary>
        /// Réserve le véhicule pour une date donnée.
        /// </summary>
        /// <param name="date">Date de réservation.</param>
        public void Reserver(DateTime date)
        {
            datesIndisponibles.Add(date);
        }

        public virtual void AfficherDetails()
        {
            Console.WriteLine($"Numéro d'immatriculation: {numeroImmatriculation}, Capacité de charge: {capaciteCharge} kg");
        }

        public string NumeroImmatriculation
        {
            get { return numeroImmatriculation; }
            set { numeroImmatriculation = value; }
        }

        public double CapaciteCharge
        {
            get { return capaciteCharge; }
            set { capaciteCharge = value; }
        }

        public HashSet<DateTime> DatesIndisponibles
        {
            get { return datesIndisponibles; }
            set { datesIndisponibles = value; }
        }
    }



}
