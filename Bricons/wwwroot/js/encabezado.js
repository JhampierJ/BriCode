const menuOpciones = document.querySelector(".menu-opciones");
const btnSignUp = document.getElementById("btn-sign-up");
/*const btnSign = document.getElementById("btn-sign");*/
const header = document.querySelector("header");
const controlesUsuario = document.querySelector(".controles-usuario");
const contenedor = document.querySelector(".contenedor");
const btnMenu = document.getElementById("btn-menu");


/* Bsucador*/
const equisInput = document.getElementById("equis-input");
const inputBuscador = document.getElementById("input-buscador");
const btnCerrar = document.getElementById("btn-cerrar");
const buscador = document.querySelector(".contenedor-buscador");
const btnAbrir = document.querySelector(".btn-abrir");
const background = document.querySelector(".background");

inputBuscador.addEventListener("keyup", () => {
    if (inputBuscador.value.length > 0) {
        equisInput.style.display = "block";
    }
    else {
        equisInput.style.display = "none";
    }
});

equisInput.addEventListener("click", () => {
    inputBuscador.value = "";
    equisInput.style.display = "none";
});

btnCerrar.addEventListener("click", () => {
    buscador.style.display = "none";
    background.style.display = "none";
});

btnAbrir.addEventListener("click", () => {
    buscador.style.display = "flex";
    background.style.display = "block";
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

/*Carrito */

