const menu = document.getElementById("menu-des");
const iconMenu = document.getElementById("icon-menu");

iconMenu.addEventListener("click",(e)=>{
    
    menu.classList.toggle('activo-menu');
   
}); 
/*
iconMenu.addEventListener("click",(e)=>{
    
    menu.classList.toggle('activo-menu');
    const menuActivo = document.querySelector(".activo-menu");
    if (menuActivo == null) {
        menu.style.height = "0px";
     
    } else {
        const lis = document.querySelectorAll('li');
        menuActivo.style.height = (lis.length*40)+"px";
    }

    
}); 
*/