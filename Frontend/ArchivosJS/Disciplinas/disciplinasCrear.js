var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
const endpoint = "https://localhost:7118";
import { verificarToken } from "../Cuentas.js";
import { mostrarClanes, realizarFiltro, tiposVampiros } from "../Vampiro.js";
let contador = 1;
//Evento para cerrar sesión
$("#cerrar-sesion").on("click", function () {
    localStorage.removeItem("token");
    window.location.href = "/Frontend/Vistas/index.html";
});
//Evento que toma el valor del input de búsqueda y llama a la función para filtrar los clanes
$(".input_clanes").on("input", function () {
    console.log("Input detectado");
    let nombreClan = String($(".input_clanes").val() || ""); //Tomar nombre del input de búsqueda
    realizarFiltro(tiposVampiros, nombreClan);
});
$(document).ready(function () {
    return __awaiter(this, void 0, void 0, function* () {
        mostrarClanes(); //Se llama al método para mostrar los clanes de vampiro en el menú desplegable
        let token = verificarToken(); //Se verifica si hay un token en el localStorage
        if (token == undefined || token == null || token == "") {
            window.location.href = "/Frontend/Vistas/index.html";
        }
    });
});
$("#añadir").on("click", function () {
    const original = document.querySelector(".elemento_copias");
    const clon = original.cloneNode(true);
    const index = contador++;
    // 🔹 ENARDECIMIENTO
    clon
        .querySelectorAll('input.enardecimiento')
        .forEach(radio => {
        radio.name = `enardecimiento_${index}`;
        radio.checked = false;
    });
    // 🔹 Input oculto de enfrentada
    const inputEnfrentado = clon.querySelector('.input_enfrentado');
    if (inputEnfrentado) {
        inputEnfrentado.hidden = true;
    }
    $(".contenedor_habilidades").append(clon);
});
$(document).on('change', 'input[name="tirada_enfrentada"]', function (e) {
    var _a;
    const target = e.target;
    const valor = target.value;
    const input_hidden = (_a = target.closest(".enfrentado")) === null || _a === void 0 ? void 0 : _a.querySelector(".input_enfrentado");
    if (valor == "true") {
        input_hidden === null || input_hidden === void 0 ? void 0 : input_hidden.removeAttribute("hidden");
    }
    else {
        input_hidden === null || input_hidden === void 0 ? void 0 : input_hidden.setAttribute("hidden", "");
    }
});
$("#crear").on("click", function () {
    return __awaiter(this, void 0, void 0, function* () {
        const habilidades = Array.from(document.querySelectorAll('.elemento_copias')).map(bloque => {
            var _a, _b, _c, _d, _e, _f, _g, _h, _j;
            const nombre = (_b = (_a = (bloque.querySelector('.nombre-habilidad'))) === null || _a === void 0 ? void 0 : _a.value) !== null && _b !== void 0 ? _b : '';
            const nivel = Number((_d = (_c = (bloque.querySelector('.nivel-habilidad'))) === null || _c === void 0 ? void 0 : _c.value) !== null && _d !== void 0 ? _d : 0);
            const dados = (_f = (_e = (bloque.querySelector('.dados-habilidades'))) === null || _e === void 0 ? void 0 : _e.value) !== null && _f !== void 0 ? _f : '';
            const enardecimiento = Boolean(((_g = bloque.querySelector('input[name^="enardecimiento_"]:checked')) === null || _g === void 0 ? void 0 : _g.value) === "true");
            const tiradaEnfrentadaDato = (_j = (_h = (bloque.querySelector('.tirada_enfrentada_dato'))) === null || _h === void 0 ? void 0 : _h.value) !== null && _j !== void 0 ? _j : '';
            return {
                "nombre_Habilidad": nombre,
                "nivel": nivel,
                "tirada": dados,
                "enardecimiento": enardecimiento == true,
                "tiradaEnfrentada": tiradaEnfrentadaDato
            };
        }).filter(hab => 
        // Filtra solo si hay algo en el bloque (puedes definir tu criterio)
        hab.nombre_Habilidad || hab.nivel > 0 || hab.tirada || hab.tiradaEnfrentada);
        ;
        yield enviarDatos(habilidades);
    });
});
function enviarDatos(habilidad_disciplina) {
    return __awaiter(this, void 0, void 0, function* () {
        let nombre_disciplina = String($("#nombre_disciplina").val());
        try {
            yield $.ajax({
                url: `${endpoint}/api/Disciplinas/all`,
                method: "POST",
                contentType: "application/json",
                headers: {
                    "Authorization": `Bearer ${localStorage.getItem("token")}`
                },
                data: JSON.stringify({
                    "nombre_Disciplina": nombre_disciplina,
                    "habilidades": habilidad_disciplina,
                }),
                dataType: "json"
            });
            window.location.href = "../../Vistas/Disciplinas/DisciplinasIndex.html";
            sessionStorage.setItem("mostrarAlerta", "true");
        }
        catch (error) {
            Swal.fire({
                icon: "warning",
                title: "!Algo mal ocurrió!",
                text: `${error.responseText}`,
                confirmButtonText: 'Aceptar',
                confirmButtonColor: '#ff0000ff',
                toast: true,
                position: "bottom-start",
                timer: 3000
            });
        }
    });
}
