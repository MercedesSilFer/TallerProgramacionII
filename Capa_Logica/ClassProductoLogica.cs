using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidades;
using Capa_Datos;

namespace Capa_Logica
{
    public class ClassProductoLogica
    {
        public List<string> ErroresValidacion { get; private set; } = new List<string>();
        //instanciar ClassProducto
        ClassProducto classProducto = new Capa_Datos.ClassProducto();
        //obtener producto_presentacion por cod_producto
        public producto_presentacion ObtenerProductoPresentacionPorCodigo(int cod_producto)
        {
            var productoPresentacion = classProducto.ObtenerProductoPresentacionPorCodigo(cod_producto);
            if (productoPresentacion == null)
            {
                ErroresValidacion = classProducto.ErroresValidacion;
            }
            return productoPresentacion;
        }
        //obtener producto por id_producto
        public PRODUCTO ObtenerProductoPorId(int id_producto)
        {
            var producto = classProducto.ObtenerProductoPorId(id_producto);
            if (producto == null)
            {
                ErroresValidacion = classProducto.ErroresValidacion;
            }
            return producto;
        }
        //obtener  PRESENTACION por id_presentacion
        public PRESENTACION ObtenerPresentacionPorId(int id_presentacion)
        {
            var presentacion = classProducto.ObtenerPresentacionPorId(id_presentacion);
            if (presentacion == null)
            {
                ErroresValidacion = classProducto.ErroresValidacion;
            }
            return presentacion;
        }
        //obtener objeto stock por id_producto y id_presentacion
        public stock ObtenerStockPorProductoYPresentacion(int id_producto, int id_presentacion)
        {
            var stock = classProducto.ObtenerStockPorProductoYPresentacion(id_producto, id_presentacion);
            if (stock == null)
            {
                ErroresValidacion = classProducto.ErroresValidacion;
            }
            return stock;
        }
        //obtener lista de producto_presentacion
        public List<producto_presentacion> ObtenerListaProductoPresentacion()
        {
            var lista = classProducto.ListarProductoPresentacion();
            if (lista == null)
            {
                ErroresValidacion = classProducto.ErroresValidacion;
            }
            return lista;
        }
        //obtener lista de productos
        public List<PRODUCTO> ObtenerListaProductos()
        {
            var lista = classProducto.ListarProductos();
            if (lista == null)
            {
                ErroresValidacion = classProducto.ErroresValidacion;
            }
            return lista;
        }
        //obtener lista de presentaciones
        public List<PRESENTACION> ObtenerListaPresentaciones()
        {
            var lista = classProducto.ListarPresentaciones();
            if (lista == null)
            {
                ErroresValidacion = classProducto.ErroresValidacion;
            }
            return lista;
        }
        public List<producto_presentacion> ListarProductoPresentacionActivosConStock()
        {
            var lista = classProducto.ListarProductoPresentacionActivosConStock();
            if (lista == null)
            {
                ErroresValidacion = classProducto.ErroresValidacion;
            }
            return lista;
        }
        //obtener producto_presentacion por id_producto id_presentacion
        public producto_presentacion ObtenerProductoPresentacionPorProductoYPresentacion(int id_producto, int id_presentacion)
        {
            var productoPresentacion = classProducto.ObtenerProductoPresentacionPorId_productoId_ID_presentacion(id_producto, id_presentacion);
            if (productoPresentacion == null)
            {
                ErroresValidacion = classProducto.ErroresValidacion;
            }
            return productoPresentacion;
        }
        //Obtener producto por familia
        public List<PRODUCTO> ObtenerProductosPorFamilia(int familia)
        {
            return classProducto.ObtenerProductosPorFamilia(familia);
        }
        public List<PRODUCTO> ObtenerProductoPorMarca(int marca)
        {
            return classProducto.ObtenerProductoPorMarca(marca);
        }
    }
}

