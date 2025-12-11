// ============================================
// JavaScript para Hijos Expandibles en Apoderado
// ============================================

// Hacer la función accesible globalmente para que pueda ser llamada desde el servidor
window.verificarHijoSeleccionado = function() {
    // Buscar el botón activo (marcado por el servidor después del postback)
    const botonActivo = document.querySelector('.hijo-item .btn-hijo.activo');
    
    if (botonActivo) {
        const hijoItem = botonActivo.closest('.hijo-item');
        const opciones = hijoItem.querySelector('.hijo-opciones');
        
        if (opciones) {
            // Expandir las opciones del hijo seleccionado
            opciones.style.display = 'flex';
            console.log('Hijo expandido correctamente');
        } else {
            console.log('No se encontraron opciones para el hijo activo');
        }
    } else {
        console.log('No se encontró ningún hijo activo');
    }
};

document.addEventListener('DOMContentLoaded', function() {
    // Verificar si hay un hijo seleccionado y expandirlo después del postback
    verificarHijoSeleccionado();
    
    // Inicializar eventos para expansión visual (sin interferir con postback)
    inicializarHijosExpandibles();
});

/**
 * Inicializa la funcionalidad de hijos expandibles
 */
function inicializarHijosExpandibles() {
    // Obtener todos los botones de hijos
    const botonesHijos = document.querySelectorAll('.hijo-item .btn-hijo');
    
    botonesHijos.forEach(function(boton) {
        // Agregar evento para expansión visual inmediata ANTES del postback
        boton.addEventListener('click', function(e) {
            // No prevenir el postback, solo expandir visualmente
            const hijoItem = boton.closest('.hijo-item');
            const opciones = hijoItem.querySelector('.hijo-opciones');
            
            // Colapsar todos los demás hijos
            colapsarTodosLosHijos();
            
            // Expandir el hijo clickeado inmediatamente
            if (opciones) {
                opciones.style.display = 'flex';
                boton.classList.add('activo');
            }
        }, false); // false = no captura, permite que el postback continúe
    });
}


/**
 * Expande un hijo y carga sus datos
 */
function expandirHijo(hijoItem, idHijo) {
    const opciones = hijoItem.querySelector('.hijo-opciones');
    const boton = hijoItem.querySelector('.btn-hijo');
    
    if (opciones) {
        opciones.style.display = 'flex';
        boton.classList.add('activo');
        
        // El postback se maneja desde el servidor (btnHijo_Click)
        // Solo expandimos visualmente aquí
    }
}

/**
 * Colapsa un hijo específico
 */
function colapsarHijo(hijoItem) {
    const opciones = hijoItem.querySelector('.hijo-opciones');
    const boton = hijoItem.querySelector('.btn-hijo');
    
    if (opciones) {
        opciones.style.display = 'none';
        boton.classList.remove('activo');
    }
}

/**
 * Colapsa todos los hijos
 */
function colapsarTodosLosHijos() {
    const todosLosHijos = document.querySelectorAll('.hijo-item');
    
    todosLosHijos.forEach(function(hijoItem) {
        const opciones = hijoItem.querySelector('.hijo-opciones');
        const boton = hijoItem.querySelector('.btn-hijo');
        
        if (opciones && opciones.style.display !== 'none') {
            opciones.style.display = 'none';
            boton.classList.remove('activo');
        }
    });
}

/**
 * Verifica si hay un hijo seleccionado en la URL o sesión
 */
function verificarHijoSeleccionado() {
    const urlParams = new URLSearchParams(window.location.search);
    const idHijo = urlParams.get('idHijo');
    
    if (idHijo) {
        const hijoItem = document.querySelector(`.hijo-item[data-id-hijo="${idHijo}"]`);
        if (hijoItem) {
            expandirHijo(hijoItem, idHijo);
        }
    }
}

