using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Infrastructure.Contracts.Entities
{
    public class Packages
    {
        public enum pkg
        {
            [Description("g_catalogos.")]
            g_catalogos,
            [Description("esq_usuarios.")]
            esq_usuarios,
            //[Description("PKG_APORTANTE.")]
            //PKG_APORTANTE,
            //[Description("PKG_PRUEBA_CA.")]
            //PKG_PRUEBA_CA,
            //[Description("PKG_CRCA.")]
            //PKG_CRCA,
            //[Description("PKG_GENERAL.")]
            //PKG_GENERAL,
            //[Description("PKG_INGRESOS.")]
            //PKG_INGRESOS,
            //[Description("PKG_PERSONA.")]
            //PKG_PERSONA,
            //[Description("PKG_ACTIVOS_FIJOS.")]
            //PKG_ACTIVOS_FIJOS,
        };
    }
}
