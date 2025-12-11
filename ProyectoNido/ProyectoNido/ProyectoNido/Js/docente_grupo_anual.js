// Verificar que las funciones estén disponibles globalmente
console.log('Script docente_grupo_anual.js cargado');

// Ejecutar después de cargar la página
window.addEventListener('load', function () {
    console.log('Página cargada completamente');
    
    // Cambiar el estilo activo de los enlaces del menú lateral
    var menuButtons = document.querySelectorAll('.docente-menu a');

    // Marcar activo basado en la URL actual si es necesario, o dejar que el HTML lo defina
    // En este caso, el HTML ya tendrá la clase 'activo' en el enlace correcto

    menuButtons.forEach(function (btn) {
        btn.addEventListener('click', function () {
            menuButtons.forEach(function (b) { b.classList.remove('activo'); });
            btn.classList.add('activo');
        });
    });
    
    // Verificar que Bootstrap esté cargado
    if (typeof bootstrap === 'undefined') {
        console.warn('Bootstrap no está cargado. El modal puede no funcionar correctamente.');
    } else {
        console.log('Bootstrap cargado correctamente');
    }
    
    // Verificar que PageMethods esté disponible
    if (typeof PageMethods === 'undefined') {
        console.warn('PageMethods no está disponible. Asegúrate de que ScriptManager tenga EnablePageMethods="true"');
    } else {
        console.log('PageMethods disponible');
    }
});

// Función para abrir el modal desde el botón (lee los data attributes)
function abrirModalAlumnosDesdeBoton(button) {
    console.log('abrirModalAlumnosDesdeBoton llamado', button);
    var idGrupo = button.getAttribute('data-id-grupo');
    var nivel = button.getAttribute('data-nivel');
    var salon = button.getAttribute('data-salon');
    console.log('Datos:', { idGrupo: idGrupo, nivel: nivel, salon: salon });
    abrirModalAlumnos(parseInt(idGrupo), nivel, salon);
}

// Función para abrir el modal de alumnos
function abrirModalAlumnos(idGrupo, nivel, salon) {
    console.log('abrirModalAlumnos llamado', { idGrupo: idGrupo, nivel: nivel, salon: salon });
    
    // Verificar que los elementos existan
    if (!document.getElementById('lblGrupoInfo') || !document.getElementById('lblSalonInfo')) {
        console.error('Elementos del modal no encontrados');
        alert('Error: No se encontraron los elementos del modal. Por favor, recarga la página.');
        return;
    }
    
    // Actualizar información del grupo en el modal
    document.getElementById('lblGrupoInfo').textContent = nivel;
    document.getElementById('lblSalonInfo').textContent = salon;
    
    // Mostrar loading
    document.getElementById('loadingAlumnos').style.display = 'block';
    document.getElementById('contenidoAlumnos').style.display = 'none';
    document.getElementById('sinAlumnos').style.display = 'none';
    
    // Limpiar tabla anterior
    var tbody = document.getElementById('tbodyAlumnos');
    tbody.innerHTML = '';
    
    // Abrir modal
    var modal = new bootstrap.Modal(document.getElementById('modalAlumnos'));
    modal.show();
    
    // Llamar al PageMethod para obtener alumnos
    PageMethods.ObtenerAlumnosPorGrupo(idGrupo, 
        function (result) {
            // Ocultar loading
            document.getElementById('loadingAlumnos').style.display = 'none';
            
            if (result && result.length > 0) {
                // Mostrar tabla con alumnos
                document.getElementById('contenidoAlumnos').style.display = 'block';
                
                var tbody = document.getElementById('tbodyAlumnos');
                result.forEach(function (alumno) {
                    var row = tbody.insertRow();
                    
                    // Nombres
                    var cellNombres = row.insertCell(0);
                    cellNombres.textContent = alumno.Nombres || '';
                    
                    // Apellidos
                    var cellApellidos = row.insertCell(1);
                    cellApellidos.textContent = (alumno.ApellidoPaterno || '') + ' ' + (alumno.ApellidoMaterno || '').trim();
                });
            } else {
                // Mostrar mensaje de sin alumnos
                document.getElementById('sinAlumnos').style.display = 'block';
            }
        },
        function (error) {
            // Ocultar loading
            document.getElementById('loadingAlumnos').style.display = 'none';
            
            // Mostrar error
            var errorMsg = 'Error al cargar alumnos';
            if (error) {
                if (error.get_message) {
                    errorMsg += ': ' + error.get_message();
                } else if (error.message) {
                    errorMsg += ': ' + error.message;
                } else if (typeof error === 'string') {
                    errorMsg += ': ' + error;
                }
            }
            alert(errorMsg);
            console.error('Error:', error);
        }
    );
}

// Función para abrir el modal de alumnos de servicio desde el botón
function abrirModalAlumnosServicioDesdeBoton(button) {
    console.log('abrirModalAlumnosServicioDesdeBoton llamado', button);
    var idGrupo = button.getAttribute('data-id-grupo');
    var servicio = button.getAttribute('data-servicio');
    var salon = button.getAttribute('data-salon');
    console.log('Datos:', { idGrupo: idGrupo, servicio: servicio, salon: salon });
    abrirModalAlumnosServicio(parseInt(idGrupo), servicio, salon);
}

// Función para abrir el modal de alumnos de servicio
function abrirModalAlumnosServicio(idGrupo, servicio, salon) {
    console.log('abrirModalAlumnosServicio llamado', { idGrupo: idGrupo, servicio: servicio, salon: salon });
    
    // Verificar que los elementos existan
    if (!document.getElementById('lblServicioInfo') || !document.getElementById('lblSalonServicioInfo')) {
        console.error('Elementos del modal no encontrados');
        alert('Error: No se encontraron los elementos del modal. Por favor, recarga la página.');
        return;
    }
    
    // Actualizar información del grupo en el modal
    document.getElementById('lblServicioInfo').textContent = servicio;
    document.getElementById('lblSalonServicioInfo').textContent = salon;
    
    // Mostrar loading
    document.getElementById('loadingAlumnosServicio').style.display = 'block';
    document.getElementById('contenidoAlumnosServicio').style.display = 'none';
    document.getElementById('sinAlumnosServicio').style.display = 'none';
    
    // Limpiar tabla anterior
    var tbody = document.getElementById('tbodyAlumnosServicio');
    tbody.innerHTML = '';
    
    // Abrir modal
    var modal = new bootstrap.Modal(document.getElementById('modalAlumnosServicio'));
    modal.show();
    
    // Llamar al PageMethod para obtener alumnos
    PageMethods.ObtenerAlumnosPorGrupoServicio(idGrupo, 
        function (result) {
            // Ocultar loading
            document.getElementById('loadingAlumnosServicio').style.display = 'none';
            
            if (result && result.length > 0) {
                // Mostrar tabla con alumnos
                document.getElementById('contenidoAlumnosServicio').style.display = 'block';
                
                var tbody = document.getElementById('tbodyAlumnosServicio');
                result.forEach(function (alumno) {
                    var row = tbody.insertRow();
                    
                    // Nombres
                    var cellNombres = row.insertCell(0);
                    cellNombres.textContent = alumno.Nombres || '';
                    
                    // Apellidos
                    var cellApellidos = row.insertCell(1);
                    cellApellidos.textContent = (alumno.ApellidoPaterno || '') + ' ' + (alumno.ApellidoMaterno || '').trim();
                });
            } else {
                // Mostrar mensaje de sin alumnos
                document.getElementById('sinAlumnosServicio').style.display = 'block';
            }
        },
        function (error) {
            // Ocultar loading
            document.getElementById('loadingAlumnosServicio').style.display = 'none';
            
            // Mostrar error
            var errorMsg = 'Error al cargar alumnos';
            if (error) {
                if (error.get_message) {
                    errorMsg += ': ' + error.get_message();
                } else if (error.message) {
                    errorMsg += ': ' + error.message;
                } else if (typeof error === 'string') {
                    errorMsg += ': ' + error;
                }
            }
            alert(errorMsg);
            console.error('Error:', error);
        }
    );
}