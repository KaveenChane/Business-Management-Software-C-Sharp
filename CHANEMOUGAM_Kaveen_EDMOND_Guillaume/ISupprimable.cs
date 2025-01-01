using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHANEMOUGAM_Kaveen_EDMOND_Guillaume
{
    internal interface ISupprimable
    {
        void SupprimerParId(int id);
        void SupprimerParNom(string nom, string prenom);
    }

}
