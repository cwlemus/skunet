using Core.Entidades;
using Microsoft.AspNetCore.Mvc;
using Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using API.DTO;

namespace netsku.Controllers
{
    [ApiController]

    public class SkuController : ControllerBase
    {
        private readonly SkuContext _context;
        Status status = null;
        public SkuController(SkuContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/allskus/{filtro?}")]
        public ActionResult<List<Sku>> GetSkus(string filtro)
        {
            List<Sku> skus;
            if (filtro == null)
            {
                skus = _context.Skus.ToList();
            }
            else
            {
                skus = (from s in _context.Skus
                        where s.SkuNumber.ToString().Contains(filtro) || s.Descripcion.Contains(filtro) ||
                              s.Stock.ToString().Contains(filtro)
                        select new Sku
                        {
                            SkuNumber = s.SkuNumber,
                            Descripcion = s.Descripcion,
                            Cantidad = s.Cantidad,
                            Stock = s.Stock
                        }).ToList();
            }
            return Ok(skus);
        }

        [HttpGet]
        [Route("api/sku/{id}")]
        public ActionResult<Sku> GetSku(int id)
        {
            var sku = _context.Skus.Find(id);
            return Ok(sku);
        }


        //guardar transacciones   
        [HttpPost]
        [Route("api/addOrder")]
        public ActionResult<int> Guardar([FromBody] OrdenDto orden)
        {
            using (TransactionScope transacion = new TransactionScope())
            {
                bool pass = false;
                status = null;
                Sku skuSelected = _context.Skus.Where(x => x.SkuNumber == orden.SkuNumber).FirstOrDefault();
                Orden ordenSelected;
                //Preparo log
                LogSku log = new LogSku
                {
                    Fecha = DateTime.Now,
                    Cantidad = orden.Cantidad,
                    IdSkuNumber = skuSelected,
                };

                //1. Verificamos existencia
                var disponible = _context.Skus.Where(s => s.Stock > 1 && orden.Cantidad>1 && s.Stock>=orden.Cantidad && s == skuSelected).FirstOrDefault();
                if (disponible != null)
                {
                    pass = (disponible.Stock -= orden.Cantidad) >= 0;
                }
                if (pass)
                {
                    //si con lo que hay llega a cero o aun queda se procede sino no
                    _context.SaveChanges();
                    _context.Entry(disponible).State = EntityState.Detached;
                    //marco 1-Procesado
                    status = _context.Status.Where(e => e.IdStatus.Equals(1)).First();

                    //Creo orden
                    ordenSelected = new Orden
                    {
                        Cantidad = orden.Cantidad,
                        Fecha = DateTime.Now,
                        IdSkuNumber = skuSelected,
                        IdStatus = status
                    };
                    /*orden.IdSkuNumber = new Sku
                     {
                         SkuNumber = disponible.SkuNumber,
                         Descripcion = disponible.Descripcion,
                         Cantidad = disponible.Cantidad,
                         Stock = disponible.Stock
                     };*/
                    _context.Entry(skuSelected).State = EntityState.Detached;
                    skuSelected.Stock = disponible.Stock;
                    _context.Skus.Attach(skuSelected);
                    _context.Ordens.Add(ordenSelected);
                    _context.SaveChanges();
                }
                else
                {
                    //marco 2 -in existencia
                    status = _context.Status.Where(e => e.IdStatus.Equals(2)).First();
                }

                //2. Agregamos status al log
                log.IdStatus = status;
                _context.LogSkus.Add(log);
                _context.SaveChanges();


                transacion.Complete();


                return Ok(status.IdStatus);
            }


        }


        [HttpGet]
        [Route("api/deleteOrder/{id}")]
        public ActionResult<int> Eliminar(int Id)
        {
            using (TransactionScope transacion = new TransactionScope())
            {
                status = _context.Status.Where(s => s.IdStatus == 1).FirstOrDefault();
                var ord = _context.Ordens.Include(o => o.IdSkuNumber).Where(x => x.IdOrden == Id).FirstOrDefault();
                if (ord != null)
                {
                    var sku = ord.IdSkuNumber;

                    //se agrega al stock
                    sku.Stock += ord.Cantidad;
                    _context.SaveChanges();
                    _context.Entry(sku).State = EntityState.Detached;

                    //se agrega log
                    LogSku log = new LogSku
                    {
                        Fecha = DateTime.Now,
                        Cantidad = ord.Cantidad,
                        IdSkuNumber = ord.IdSkuNumber,
                        IdStatus = status
                    };

                    _context.Attach(log.IdSkuNumber);
                    _context.LogSkus.Add(log);
                    _context.SaveChanges();

                    //Quitamos la orden
                    _context.Ordens.Attach(ord);
                    _context.Ordens.Remove(ord);
                    _context.SaveChanges();

                }

                transacion.Complete();
                return Ok(status.IdStatus);
            }

        }

        [HttpGet]
        [Route("api/displayOrder/{filtro?}")]
        public ActionResult<List<Existencia>> MostrarOrdenes(String filtro)
        {
            List<Orden> lstOrdenes = _context.Ordens.Include(o=>o.IdSkuNumber).ToList();
            Existencia existencia;
            List<Existencia> lstExistencias = new List<Existencia>();
            foreach (var item in lstOrdenes)
            {
                existencia = new Existencia
                {
                    IdOrden = item.IdOrden,
                    Descripcion = item.IdSkuNumber.Descripcion,
                    Cantidad = item.Cantidad,
                    Stock = item.IdSkuNumber.Stock
                };
                lstExistencias.Add(existencia);
            }
            if (filtro != null)
            {
                lstExistencias = lstExistencias.Where(e => e.IdOrden.ToString().Contains(filtro) ||
                                e.Descripcion.Contains(filtro) || e.Cantidad.ToString().Contains(filtro) ||
                                e.Stock.ToString().Contains(filtro)
                                ).ToList();
            }

            return Ok(lstExistencias);
        }
    }
}
