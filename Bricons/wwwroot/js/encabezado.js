const menuOpciones = document.querySelector(".menu-opciones");
const btnSignUp = document.getElementById("btn-sign-up");
/*const btnSign = document.getElementById("btn-sign");*/
const header = document.querySelector("header");
const controlesUsuario = document.querySelector(".controles-usuario");
const contenedor = document.querySelector(".contenedor");
const btnMenu = document.getElementById("btn-menu");
const resultadoBusqueda = document.getElementById("miRes");
const carretillaContainer = document.querySelector(".carretilla-container");


/* Bsucador*/
const equisInput = document.getElementById("equis-input");

const btnCerrar = document.getElementById("btn-cerrar");
const buscador = document.querySelector(".contenedor-buscador");
const btnAbrir = document.querySelector(".btn-abrir");
const background = document.querySelector(".background");
const inputBuscador = document.getElementById("input-buscador");

carretillaContainer.addEventListener("click", () => {
    let obejtos = JSON.parse(localStorage.getItem("ids"));
    let numeros = [];
    obejtos.forEach((obj) => {
        numeros.push(obj.id);
    });


    window.location.href = "/Cotizacions/Index?numeros=" + numeros.join(',');
    //alert(numeros);

    //fetch("/Cotizaciones/Index?numeros=" + numeros.join(','), {
    //    method: "GET"
    //})
    //   .then(response => response.json())
    //    .then(data => {
    //        // Maneja la respuesta del AJAX si es necesario
    //        console.log("Solicitud AJAX completada");

    //        // Después de que la solicitud AJAX se haya completado, redirige a una URL que llame a tu método del controlador
    //        window.location.href = "/Cotizaciones/IndexOr";
    //    })
    //    .catch(error => {
    //        console.error("Error al realizar la solicitud AJAX: " + error);
    //    });
});






inputBuscador.addEventListener("keyup", () => {
    if (inputBuscador.value.length > 0) {
        equisInput.style.display = "block";
    }
    else {
        equisInput.style.display = "none";
    }
    let palabra = inputBuscador.value;
    let palb = palabra.toUpperCase();
    resultadoBusqueda.innerHTML = "";
        $.ajax({
            url: '/Productos/BuscarProductos',
            method: 'GET',
            data: { palb: palb },
            success: function (data) {
               
                let productosHTM = "";
                if (data.length == 0) {
                    resultadoBusqueda.style.justifyContent = "center";
                    productosHTM = `<span class="respuesta">No hay resultados</span>`;
                    console.log(data);
                } else {
                    resultadoBusqueda.style.justifyContent = "";
                    data.forEach((p) => {

                        let catego = "";
                        if (p.categoriaID == 1) {
                            catego = "Ladrillo para Muros";
                        }
                        else if (p.categoriaID == 2) {
                            catego = "Ladrillo para Techos";
                        }
                        productosHTM +=
                            `
                        <div class="result">
                            <p class="categoriaP">  1${catego}</p>
                              <a class="prdo" href="/Productos/Details/${p.id}">
                              <img src="${p.imagen}" alt="hola" />
                                <span>${p.nombreProducto}</span>
                              </a>

                        </div>

                        `
                    });
                    
                }
               
                resultadoBusqueda.innerHTML = productosHTM;
            },
        });


});

equisInput.addEventListener("click", () => {
    inputBuscador.value = "";
    equisInput.style.display = "none";
    resultadoBusqueda.style.justifyContent = "center";
    resultadoBusqueda.innerHTML = `<span class="respuesta">No hay resultados</span>`;
});

btnCerrar.addEventListener("click", () => {
    buscador.style.display = "none";
    background.style.display = "none";
});

btnAbrir.addEventListener("click", () => {
    buscador.style.display = "flex";
    background.style.display = "block";
    inputBuscador.value = "";
});
background.addEventListener("click", () => {
    background.style.display = "none";
    buscador.style.display = "none";
});

/* */



const responsiveY = () => {
    if (window.innerHeight <= 362) {
        if (menuOpciones.classList.contains("mostrar"))
            menuOpciones.classList.add("min");
        else
            menuOpciones.classList.remove("min");
    }
    else {
        menuOpciones.classList.remove("min");
    }
};
const responsive = () => {
    if (window.innerWidth <= 865) {
        menuOpciones.children[0].appendChild(btnSignUp);
/*        menuOpciones.children[0].appendChild(btnSign);*/
        header.appendChild(menuOpciones);
    } else {
        controlesUsuario.appendChild(btnSignUp);
/*        controlesUsuario.appendChild(btnSign);*/
        contenedor.appendChild(menuOpciones);
    }

    responsiveY();
}

btnMenu.addEventListener("click", () => {
    menuOpciones.classList.toggle("mostrar");
    responsiveY();
});
responsive();

window.addEventListener("resize", responsive);
resultadoBusqueda.style.justifyContent = "center";


/**User */

const userIcon = document.getElementById("user-icon");
const userInfoContenedor = document.querySelector(".user-info-contenedor");
const fondoUser = document.getElementById("fondo");
const equisUser = document.getElementById("equisUser");

equisUser.addEventListener("click", () => {
    userInfoContenedor.classList.remove("activo");
    fondoUser.classList.remove("activo");
});

fondoUser.addEventListener("click", () => {
    userInfoContenedor.classList.remove("activo");
    fondoUser.classList.remove("activo");
});

userIcon.addEventListener("click", () => {
    userInfoContenedor.classList.toggle("activo");
    if (userInfoContenedor.classList.contains("activo")) {
        fondoUser.classList.add("activo");
    } else {
        fondoUser.classList.remove("activo");
    }
});



//const idsProductosCarrito = [];
//localStorage.setItem("ids", JSON.stringify(idsProductosCarrito));
if (!localStorage.getItem("ids")) {
    const idsProductosCarrito = [];
    localStorage.setItem("ids", JSON.stringify(idsProductosCarrito));
}

let nPROD = document.querySelector(".numero-productos");

let valorLocalStorague = JSON.parse(localStorage.getItem("ids"));
nPROD.textContent = valorLocalStorague.length;


