# WINDBRANDS MX

# Documento de Arquitectura y Asignación de Módulos: Windbrands MX

Este documento define el estado actual del proyecto, la estructura de la base de datos y las misiones individuales para conectar el diseño visual con los controladores autogenerados. El núcleo del sistema e-commerce Windbrands está construido, conectado a la nube y operando con las reglas de negocio estrictas.

## 1. Infraestructura y Estado Actual (Núcleo Terminado)

* El proyecto utiliza ASP.NET Core MVC (.NET 8).


* La base de datos es PostgreSQL y se encuentra alojada en Neon.tech.


* El traductor (ORM) configurado es Entity Framework Core.


* Todos los Modelos de C# están creados y mapeados con etiquetas `[Table]` y `[Column]` en minúsculas absolutas para evitar colisiones con PostgreSQL.


* Existen controladores y vistas base funcionales (CRUD) generadas mediante *Scaffolding* para todas las tablas.


* El catálogo principal (`Index.cshtml`) y la barra de navegación (`_Layout.cshtml`) son dinámicos.


* El catálogo extrae las categorías e imágenes directamente de la base de datos usando `ViewBag` e `Include()`.



### Reglas de Negocio Matemáticas (Ya Programadas)

* El Total de un pedido no existe en la base de datos.


* El Total se calcula iterando los `DetallesPedido` (`Cantidad * PrecioMomentoCompra`) en la memoria RAM dentro de `PedidosController`.


* Al registrar un `DetallePedido`, el sistema verifica `StockBodegas`.


* Si no hay inventario suficiente en la bodega, el sistema bloquea la venta.


* Si hay inventario, lo descuenta automáticamente.


* En `CuotasPagoController`, es imposible registrar un pago simulado si la suma de los abonos supera el Total del pedido calculado al vuelo.


* Al marcar una `DireccionEnvio` como `EsPredeterminada = true`, el sistema apaga automáticamente el resto de direcciones de ese cliente.



---

## 2. Mapa de la Base de Datos

| Tabla | Columnas |
| --- | --- |
| **Bodegas** | `id`, `ubicacion`, `capacidad`<br> |
| **Categorias** | `id`, `nombre`<br> |
| **Productos** | `id`, `nombre`, `descripcion`, `precioventa`, `categoriaid`, `imagenurl`<br> |
| **StockBodegas** (Llave Compuesta) | `bodegaid`, `productoid`, `cantidad`<br> |
| **Proveedores** | `id`, `nombre`<br> |
| **ProveedoresProductos** (Llave Compuesta) | `proveedorid`, `productoid`, `preciocompra`, `tiempoentregadias`<br> |
| **Clientes** | `id`, `nombrecompleto`, `correo`, `passwordhash`, `passwordresettokenhash`, `passwordresetexpiresat`<br> |
| **DireccionesEnvio** | `id`, `clienteid`, `direccionfisica`, `espredeterminada`<br> |
| **Pedidos** | `id`, `clienteid`, `bodegaorigenid`, `fechahora`, `estado`<br> |
| **DetallesPedido** | `id`, `pedidoid`, `productoid`, `cantidad`, `preciomomentocompra`<br> |
| **CuotasPago** | `id`, `pedidoid`, `monto`, `fecha`, `metodopago`, `estado`<br> |
| **IncidenciasDevoluciones** | `id`, `bodegaid`, `productoid`, `fecha`, `motivo`, `costo`, `creditogenerado`<br> |

---

## 3. Protocolo de Trabajo (Git)

Nadie programa sobre la rama principal directamente. El flujo obligatorio es:

1. Ejecutar `git checkout main`.


2. Ejecutar `git pull origin main` para tener el núcleo más reciente.


3. Ejecutar `git checkout -b tu-nombre-modulo` para crear un entorno de trabajo aislado.


4. Al terminar la vista, ejecutar `git add .`, `git commit -m "Descripción"`, y `git push origin tu-nombre-modulo`.



---

## 4. Distribución de Módulos (Misiones del Equipo)

El backend y los controladores ya hacen el trabajo duro. La tarea de cada integrante es tomar las Vistas Razor (`.cshtml`) genéricas que escupió el autogenerador, aplicarles las clases CSS oscuras del Front-End, y asegurar que los flujos se sientan como una tienda real.

**Tirado: Catálogo e Inventario (`Productos`, `Bodegas`, `StockBodegas`)**

* Modificar la vista `Views/Productos/Create.cshtml`.


* Asegurar que el formulario permita escribir la URL de la imagen y seleccione la categoría de un menú desplegable.


* Aplicar el diseño CSS de Windbrands.


* Modificar `Views/StockBodegas/Create.cshtml` para crear la interfaz donde un empleado asigna cuántos tenis llegaron a cuál bodega.



**Montijo: Abastecimiento (`Proveedores`, `ProveedorProductos`)**

* Estilizar la lista de proveedores en `Views/Proveedores/Index.cshtml`.


* Diseñar el formulario de vinculación en `Views/ProveedorProductos/Create.cshtml`.


* El usuario debe poder seleccionar un proveedor, un producto, y teclear el precio de compra interno y los días de entrega.



**Romero: Perfil de Usuario (`Clientes`, `DireccionesEnvio`)**

* Crear la pantalla de "Mi Cuenta" estilizada.


* En la vista de direcciones (`Views/DireccionesEnvio/Create.cshtml`), asegurar que el checkbox de `EsPredeterminada` sea claro y funcione visualmente, ya que la lógica de apagado automático ya está en el controlador.



**Ojeda: Carrito y Compras (`Pedidos`, `DetallesPedido`)**

* Interceptar el botón "Comprar" del catálogo principal para enviar el `ProductoId` a un carrito temporal o directamente a crear un `DetallePedido`.


* Estilizar la vista `Views/Pedidos/Details.cshtml` para que funcione como un recibo de compra elegante.


* Esta pantalla ya recibe el Total Calculado al vuelo por el Arquitecto.



**Román: Pagos y Panel de Control (`CuotasPago`, `IncidenciasDevolucion`, `Admin`)**

* Conectar el panel estático `Admin.cshtml` modificando el `HomeController` para que cuente cuántas bodegas y proveedores reales existen en la base de datos y pintar esos números en las tarjetas.


* Estilizar el formulario de pagos en `Views/CuotasPago/Create.cshtml`.


* Asegurarse de que el contenedor de errores (`asp-validation-summary`) esté visible en rojo si la tarjeta simulada es rechazada.



---

## 5. Instrucciones Heurísticas para la Inteligencia Artificial

No le pidan a la IA que "haga la página". Proporcionen el archivo de contexto `.txt` y apliquen instrucciones quirúrgicas basadas en el patrón MVC.

**Estructura del Prompt Perfecto para Copilot/Gemini:**

> "Actúa como desarrollador Senior en ASP.NET Core MVC. Estoy trabajando en el proyecto Windbrands. Te adjunto el archivo de contexto con la estructura de la base de datos y la lógica actual.
> 
> 
> Mi misión es el Módulo de [TU MÓDULO AQUI]. El controlador y el CRUD ya existen. Necesito que reescribas la vista `[RutaDeTuVista.cshtml]` para que cumpla estos requisitos:
> 
> 
> 1. Adapta el HTML genérico para que use las clases CSS del tema oscuro de Windbrands (por ejemplo, `products-grid`, `product-card`).
> 
> 
> 2. Asegúrate de que el formulario POST envíe correctamente los datos de [Variable 1] y [Variable 2] hacia el controlador.
> 
> 
> 3. Mantén intacta la etiqueta `asp-validation-summary="ModelOnly"` para que los errores de negocio que programó el arquitecto sigan apareciendo en pantalla."
