
const seccion1 = document.querySelector(".section-1");
const img1= document.querySelector(".img-1");
const imagenes = document.querySelectorAll(".section-1 img");
const init = ()=>{
    seccion1.style.height = img1.height+"px";
    zoom();
}

/*Imagenes */


let imgAnterior = 0;
let imgActual = 0;
let imgSiguiente = 1;
const zoom = ()=>{
    imagenes[imgAnterior].style.transition="none";
    imagenes[imgAnterior].style.transform = "scale(1)";
    imagenes[imgAnterior].style.zIndex = "0";
    imagenes[imgAnterior].style.opacity = "1";
    imagenes[imgActual].style.zIndex = "10";
    imagenes[imgSiguiente].style.zIndex = "9";

    if(imgActual==0){
        let num = 10;
        for(let i=0;i<imagenes.length;i++){
            imagenes[i].style.zIndex = num;
            num--;
        }
    }
   



    imagenes[imgActual].style.transition="transform 10s ease, opacity 2s ease";
    imagenes[imgActual].style.transform = "scale(1.5)";
    setTimeout(() => {
      
        desaparecer();
    }, 6000);
};
const desaparecer = ()=>{
    imagenes[imgActual].style.opacity = "0";


    setTimeout(() => {
        imgAnterior = imgActual;
        imgActual++;
        imgSiguiente = imgActual+1;
        if(imgActual>imagenes.length-1){
            imgActual = 0; 
            
        }
        if(imgSiguiente>imagenes.length-1){
            imgSiguiente = 0; 
            
        }
        
        zoom();
    }, 1700);   
};

/*Flecha abajo*/
const flechaAb = document.querySelector(".flecha-abajo");

window.addEventListener("resize",()=>{
    seccion1.style.height = img1.height+"px";
});

document.addEventListener("DOMContentLoaded", function() {
 
    init();
});


