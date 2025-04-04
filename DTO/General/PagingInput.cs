using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.General
{
    public class PagingInput<TType>
    {
        /// <summary>
        /// Etkinleştir
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// Kaçıncı sayfa
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Sayfa boyutu 
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Veri
        /// </summary>
        public TType Data { get; set; }
    }
}
