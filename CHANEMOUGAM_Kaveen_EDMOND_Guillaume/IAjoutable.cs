using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHANEMOUGAM_Kaveen_EDMOND_Guillaume
{
    internal interface IAjoutable<T>
    {
        void Ajouter(T item);
        void Ajouter(T item, int? superviseur);
    }

}
