using CHANEMOUGAM_Kaveen_EDMOND_Guillaume;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHANEMOUGAM_Kaveen_EDMOND_Guillaume
{
    public class Noeud
    {
        private Salarie salarie;

        private Noeud successeur;

        private Noeud frere;

        public Noeud(Salarie salarie, Noeud successeur = null, Noeud frere = null)
        {
            this.salarie = salarie;

            this.successeur = successeur;

            this.frere = frere;
        }

        public Salarie Salarie
        {
            get{ return salarie; }

            set { salarie = value; }
        }

        public Noeud Successeur
        {
            get { return successeur; }

            set { successeur = value; }
        }

        public Noeud Frere
        {
            get { return frere; }

            set { frere = value; }
        }

        public override string ToString()
        {
            return salarie.ToString();
        }
    }
}
