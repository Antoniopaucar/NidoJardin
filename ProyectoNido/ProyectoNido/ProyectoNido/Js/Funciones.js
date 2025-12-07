function soloDecimales(evt, input) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;

    // Permitir: backspace, tab, enter, delete, arrows
    if (
        charCode === 8 || // backspace
        charCode === 9 || // tab
        charCode === 13 || // enter
        charCode === 46 && !evt.shiftKey && !evt.ctrlKey && !evt.altKey || // delete
        (charCode >= 37 && charCode <= 40) // arrow keys
    ) {
        return true;
    }

    // Obtener lo que se va a escribir (nuevo valor con el carácter actual)
    var char = String.fromCharCode(charCode);
    var nuevoValor = input.value + char;

    // Permitir solo un punto decimal
    if (char === '.') {
        if (input.value.includes('.') || input.selectionStart === 0) {
            evt.preventDefault();
            return false;
        }
        return true;
    }

    // Permitir solo números
    if (!/[0-9]/.test(char)) {
        evt.preventDefault();
        return false;
    }

    return true;
}

function validarCamposTabla(tablaId, excluidosCSV = '') {
    var tabla = document.getElementById(tablaId);
    if (!tabla) {
        console.warn("Tabla no encontrada: " + tablaId);
        return false;
    }

    var controles = tabla.querySelectorAll("input, select, textarea");
    var camposVacios = [];

    var excluidos = (excluidosCSV || '')
        .split(',')
        .map(id => id.trim().toLowerCase())
        .filter(Boolean);

    controles.forEach(function (control) {
        var idControl = (control.id || '').toLowerCase();

        var excluido = excluidos.some(function (ex) {
            return idControl.includes(ex);
        });

        if (
            excluido ||
            control.disabled ||
            control.readOnly ||
            control.type === "hidden" ||
            control.type === "button" ||
            control.type === "submit"
        ) {
            return;
        }

        var esSelect = control.tagName.toLowerCase() === "select";

        var esVacio =
            (esSelect && (control.value === "" || control.value === "0")) ||
            (!esSelect && control.value.trim() === "");

        if (esVacio) {
            var label = document.querySelector("label[for='" + control.id + "']");
            var nombreCampo;

            if (label) {
                nombreCampo = label.innerText;
            } else {
                var raw = control.name || control.id;

                raw = raw.replace(/^ctl00\$contentplaceholder2\$/i, "")
                    .replace(/^txt_/i, "")
                    .replace(/^ddl_/i, "")
                    .replace(/^hdn_/i, "")
                    .replace(/^chk_/i, "")
                    .replace(/^rbt_/i, "");

                nombreCampo = raw;
            }

            camposVacios.push(nombreCampo);
        }
    });

    if (camposVacios.length > 0) {
        alert("Complete los siguientes campos:\n- " + camposVacios.join("\n- "));
        return false;
    }

    return true;
}
function SinEspacios(e) {
    var tecla = e.key; // la tecla presionada
    if (tecla === " ") { // si es espacio
        return false; // evita que se ingrese
    }
    return true; // permite otras teclas
}

function SoloTexto(e) {
    var tecla = e.key;
    var regex = /^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]$/; // permite letras y espacios
    if (!regex.test(tecla)) {
        return false; // bloquea la tecla
    }
    return true; // permite la tecla
}

function SoloNumeros(e) {
    var tecla = e.key;
    var regex = /^[0-9]$/; // solo dígitos
    if (!regex.test(tecla)) {
        return false; // bloquea la tecla
    }
    return true; // permite la tecla
}

function ValidarCorreo(input) {
    var regex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    var lblError = document.getElementById('<%= lblError.ClientID %>');

    if (!regex.test(input.value)) {
        lblError.innerText = "Correo electrónico no válido";
        input.focus(); // opcional: regresa el foco al campo
    } else {
        lblError.innerText = "";
    }
}


