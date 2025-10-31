using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidades;

namespace Capa_Datos
{
    public class ClassProducto
    {
        public List<string> ErroresValidacion { get; private set; } = new List<string>();
        //obtener producto_presentacion por cod_producto
        public producto_presentacion ObtenerProductoPresentacionPorCodigo(int cod_producto)
        {
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    return context.producto_presentacion.FirstOrDefault(p => p.cod_producto == cod_producto);
                }
            }
            catch (Exception ex)
            {
                ErroresValidacion.Clear();
                ErroresValidacion.Add(ex.Message);
                return null;
            }
        }
        //obtener producto por id_producto
        public PRODUCTO ObtenerProductoPorId(int id_producto)
        {
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    return context.PRODUCTO.FirstOrDefault(p => p.id_producto == id_producto);
                }
            }
            catch (Exception ex)
            {
                ErroresValidacion.Clear();
                ErroresValidacion.Add(ex.Message);
                return null;
            }
        }
        //obtener  PRESENTACION por id_presentacion
        public PRESENTACION ObtenerPresentacionPorId(int id_presentacion)
        {
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    return context.PRESENTACION.FirstOrDefault(p => p.ID_presentacion == id_presentacion);
                }
            }
            catch (Exception ex)
            {
                ErroresValidacion.Clear();
                ErroresValidacion.Add(ex.Message);
                return null;
            }
        }
        //obtener objeto stock por id_producto y id_presentacion
        public stock ObtenerStockPorProductoYPresentacion(int id_producto, int id_presentacion)
        {
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    return context.stock.FirstOrDefault(s => s.id_producto == id_producto && s.ID_presentacion == id_presentacion);
                }
            }
            catch (Exception ex)
            {
                ErroresValidacion.Clear();
                ErroresValidacion.Add(ex.Message);
                return null;
            }
        }
        //obtener lista de producto_presentacion
        public List<producto_presentacion> ListarProductoPresentacion()
        {
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    return context.producto_presentacion.ToList();
                }
            }
            catch (Exception ex)
            {
                ErroresValidacion.Clear();
                ErroresValidacion.Add(ex.Message);
                return new List<producto_presentacion>();
            }
        }
        //listar productos
        public List<PRODUCTO> ListarProductos()
        {
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    return context.PRODUCTO.ToList();
                }
            }
            catch (Exception ex)
            {
                ErroresValidacion.Clear();
                ErroresValidacion.Add(ex.Message);
                return new List<PRODUCTO>();
            }
        }
        //listar presentaciones
        public List<PRESENTACION> ListarPresentaciones()
        {
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    return context.PRESENTACION.ToList();
                }
            }
            catch (Exception ex)
            {
                ErroresValidacion.Clear();
                ErroresValidacion.Add(ex.Message);
                return new List<PRESENTACION>();
            }
        }
        //obtener lista de producto_presentacion activo y stock > 0
        public List<producto_presentacion> ListarProductoPresentacionActivosConStock()
        {
            //lista para usar en combo de productos en formulario de pedidos
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    var lista = (from pp in context.producto_presentacion
                                 join p in context.PRODUCTO on pp.id_producto equals p.id_producto
                                 join s in context.stock on new { pp.id_producto, pp.ID_presentacion } equals new { s.id_producto, s.ID_presentacion }
                                 where pp.activo == true && s.stock_actual > 0
                                 select pp).ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                ErroresValidacion.Clear();
                ErroresValidacion.Add(ex.Message);
                return new List<producto_presentacion>();
            }
        }
        //obtener producto_presentacion por id_producto_presentacion
        public producto_presentacion ObtenerProductoPresentacionPorId_productoId_ID_presentacion(int id_producto, int ID_presentacion)
        {
            try
            {
                using (var context = new ArimaERPEntities())
                {
                    return context.producto_presentacion.FirstOrDefault(p => p.id_producto == id_producto && p.ID_presentacion == ID_presentacion);
                }
            }
            catch (Exception ex)
            {
                ErroresValidacion.Clear();
                ErroresValidacion.Add(ex.Message);
                return null;
            }
        }
        //obtener producto por familia
        public List<PRODUCTO> ObtenerProductosPorFamilia(int familia)
        {


            using (var context = new ArimaERPEntities())
            {
                return context.PRODUCTO.Where(p => p.id_familia == familia).ToList();
            }
        }
        //obtener productos por marca
        public List<PRODUCTO> ObtenerProductoPorMarca(int marca)
        {
            using (var context = new ArimaERPEntities())
            {
                return context.PRODUCTO.Where(p => p.id_marca == marca).ToList();
            }
        }
        
    }

     }
