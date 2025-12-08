window.addEventListener('load', function () {
    var menuButtons = document.querySelectorAll('.docente-menu a');
    menuButtons.forEach(function (btn) {
        btn.addEventListener('click', function () {
            menuButtons.forEach(function (b) { b.classList.remove('activo'); });
            btn.classList.add('activo');
        });
    });
});

function toggleDetalle(element) {
    var detalle = element.nextElementSibling;
    var estado = element.querySelector('.comunicado-estado');
    var id = element.getAttribute('data-id');

    if (detalle.style.display === "block") {
        detalle.style.display = "none";
        element.classList.remove("expanded");
        // No revertir el estado a 'abrir', se queda en 'visto'
    } else {
        detalle.style.display = "block";
        element.classList.add("expanded");

        // Cambiar a estado 'visto' si aún no lo está
        if (estado.textContent.trim() === "abrir") {
            estado.textContent = "visto";
            estado.classList.remove("estado-abrir");
            estado.classList.add("estado-visto");

            // Llamada AJAX al WebMethod para marcar como visto en la BD
            if (typeof PageMethods !== 'undefined') {
                PageMethods.MarcarComoVisto(parseInt(id), 
                    function (resultado) {
                        if (resultado === "OK") {
                            console.log("Comunicado " + id + " marcado como visto");
                        } else {
                            console.warn("Respuesta del servidor: " + resultado);
                        }
                    }, 
                    function (err) {
                        console.error("Error al marcar como visto: " + err.get_message());
                    }
                );
            } else {
                console.warn("PageMethods no disponible");
            }
        }
    }
}
