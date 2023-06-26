using OdontoBackend.Aplicacion.Mappers.Contracts;
using OdontoBackend.Aplicacion.ViewModels;
using OdontoBackend.Aplication.Entities.Queries;
using OdontoBackend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Aplicacion.Mappers
{
    public class CatalogoMapper : ICatalogoMapper
    {
        public IQueryable<Catalogo> CatalogoQueryToCatalogoCodUni(Task<CatalogoByCodUniQuery> source)
        {
            return new List<Catalogo>
            {
                new Catalogo
                {
                   // COD_UNI_CATALOGO = source.Result.codigoUnico
                    cod_uni_catalogo = source.Result.codigoUnico
                }
            }.AsQueryable();
        }
        public IQueryable<CatalogoAllViewModel> CatalogoToCatalogoVmByCodUni(Task<Catalogo> source)
        {
            return new List<CatalogoAllViewModel>
            {
                new CatalogoAllViewModel
                {
                    codigoCatalogo = source.Result.cod_catalogo//source.Result.COD_CATALOGO,
                    //codigoUnico = source.Result.COD_UNI_CATALOGO,
                    //codigoPadre = source.Result.COD_PAD_CATALOGO,
                    //descripcion = source.Result.DES_CATALOGO,
                    //estado = source.Result.EST_CATALOGO,
                    //fechaCreacion = source.Result.FEC_CREACION,
                    //fechaActualización = source.Result.FEC_ACTUALIZACION

                }
            }.AsQueryable();
        }
    }
}
