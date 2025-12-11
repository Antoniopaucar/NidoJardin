// ============================================
// JavaScript para manejo de selección de hijos con AJAX
// ============================================

document.addEventListener('DOMContentLoaded', function() {
    // Inicializar eventos para los botones de hijos
    inicializarBotonesHijos();
    
    // Verificar si hay un hijo seleccionado en sesión y mostrar su información
    verificarHijoSeleccionado();
});

/**
 * Inicializa los eventos de click para los botones de hijos
 */
function inicializarBotonesHijos() {
    // Obtener todos los botones de hijos
    const botonesHijos = document.querySelectorAll('.btn-hijo[data-id-hijo]');
    
    botonesHijos.forEach(function(boton) {
        // Remover el evento onclick del servidor y agregar el nuestro
        boton.addEventListener('click', function(e) {
            e.preventDefault();
            e.stopPropagation();
            
            const idHijo = parseInt(boton.getAttribute('data-id-hijo'));
            if (idHijo && !isNaN(idHijo)) {
                seleccionarHijo(idHijo);
            }
        });
    });
    
    // Inicializar botón "Ver Todos"
    const btnVerTodos = document.querySelector('.btn-ver-todos');
    if (btnVerTodos) {
        btnVerTodos.style.display = 'block';
        btnVerTodos.addEventListener('click', function(e) {
            e.preventDefault();
            e.stopPropagation();
            deseleccionarHijo();
        });
    }
}

/**
 * Selecciona un hijo y actualiza la UI sin recargar la página
 */
function seleccionarHijo(idHijo) {
    // Mostrar loading en el botón
    const boton = document.querySelector(`.btn-hijo[data-id-hijo="${idHijo}"]`);
    if (!boton) return;
    
    const textoOriginal = boton.textContent;
    boton.disabled = true;
    boton.textContent = 'Cargando...';
    
    // Llamar al WebMethod para guardar en sesión
    PageMethods.SeleccionarHijo(idHijo, function(resultado) {
        boton.disabled = false;
        boton.textContent = textoOriginal;
        
        if (resultado === 'OK') {
            // Actualizar estado visual de los botones
            actualizarEstadoBotones(idHijo);
            
            // Obtener y mostrar información del hijo
            obtenerInfoHijo(idHijo);
        } else if (resultado.startsWith('ERROR:')) {
            const mensaje = resultado.replace('ERROR:', '');
            alert('Error al seleccionar hijo: ' + mensaje);
        }
    }, function(error) {
        boton.disabled = false;
        boton.textContent = textoOriginal;
        console.error('Error al seleccionar hijo:', error);
        alert('Error al seleccionar hijo. Por favor, intente nuevamente.');
    });
}

/**
 * Deselecciona el hijo actual (Ver Todos)
 */
function deseleccionarHijo() {
    // Llamar al WebMethod para limpiar la sesión
    PageMethods.DeseleccionarHijo(function(resultado) {
        if (resultado === 'OK') {
            // Actualizar estado visual de los botones (ninguno activo)
            actualizarEstadoBotones(null);
            
            // Ocultar panel de información
            cerrarPanelHijo();
        } else if (resultado.startsWith('ERROR:')) {
            const mensaje = resultado.replace('ERROR:', '');
            alert('Error al deseleccionar hijo: ' + mensaje);
        }
    }, function(error) {
        console.error('Error al deseleccionar hijo:', error);
        alert('Error al deseleccionar hijo. Por favor, intente nuevamente.');
    });
}

/**
 * Actualiza el estado visual de los botones (activo/inactivo)
 */
function actualizarEstadoBotones(idHijoActivo) {
    const botonesHijos = document.querySelectorAll('.btn-hijo[data-id-hijo]');
    const btnVerTodos = document.querySelector('.btn-ver-todos');
    
    botonesHijos.forEach(function(boton) {
        const idHijo = parseInt(boton.getAttribute('data-id-hijo'));
        
        if (idHijoActivo && idHijo === idHijoActivo) {
            boton.classList.add('activo');
        } else {
            boton.classList.remove('activo');
        }
    });
    
    // Actualizar estado del botón "Ver Todos"
    if (btnVerTodos) {
        if (idHijoActivo === null) {
            btnVerTodos.classList.add('activo');
        } else {
            btnVerTodos.classList.remove('activo');
        }
    }
}

/**
 * Obtiene la información del hijo seleccionado y la muestra en el panel
 */
function obtenerInfoHijo(idHijo) {
    PageMethods.ObtenerInfoHijo(idHijo, function(resultado) {
        if (resultado.startsWith('ERROR:')) {
            const mensaje = resultado.replace('ERROR:', '');
            console.error('Error al obtener información del hijo:', mensaje);
            return;
        }
        
        try {
            // Parsear JSON
            const info = JSON.parse(resultado);
            
            // Actualizar el panel con la información
            document.getElementById('infoNombreHijo').textContent = 
                `${info.Nombres} ${info.ApellidoPaterno} ${info.ApellidoMaterno}`.trim();
            document.getElementById('infoDocumentoHijo').textContent = info.Documento || '-';
            document.getElementById('infoFechaNacHijo').textContent = info.FechaNacimiento || '-';
            document.getElementById('infoEdadHijo').textContent = info.Edad || '-';
            document.getElementById('infoSexoHijo').textContent = info.Sexo || '-';
            
            // Mostrar el panel
            mostrarPanelHijo();
        } catch (e) {
            console.error('Error al parsear información del hijo:', e);
        }
    }, function(error) {
        console.error('Error al obtener información del hijo:', error);
    });
}

/**
 * Muestra el panel de información del hijo
 */
function mostrarPanelHijo() {
    const panel = document.getElementById('pnlInfoHijo');
    if (panel) {
        panel.style.display = 'block';
        // Scroll suave hasta el panel
        panel.scrollIntoView({ behavior: 'smooth', block: 'nearest' });
    }
}

/**
 * Cierra/oculta el panel de información del hijo
 */
function cerrarPanelHijo() {
    const panel = document.getElementById('pnlInfoHijo');
    if (panel) {
        panel.style.display = 'none';
    }
}

/**
 * Verifica si hay un hijo seleccionado en sesión al cargar la página
 */
function verificarHijoSeleccionado() {
    // Buscar el botón activo
    const botonActivo = document.querySelector('.btn-hijo.activo[data-id-hijo]');
    
    if (botonActivo) {
        const idHijo = parseInt(botonActivo.getAttribute('data-id-hijo'));
        if (idHijo && !isNaN(idHijo)) {
            // Obtener y mostrar información del hijo
            obtenerInfoHijo(idHijo);
        }
    }
}

