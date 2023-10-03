

const clickInputAgregar = (elemento, idP) => {
  
    let carretilla = document.querySelector(".carretilla");
    let car = document.querySelectorAll(".btn-aa");
    let nPROD = document.querySelector(".numero-productos");
    car.forEach((btn) => {
        btn.addEventListener(("click"), () => {
            carretilla.style.animation = "rotateAnimation 0.3s infinite";
            setTimeout(() => {
                carretilla.style.animation = "none";
            }, 1000);
        });


    });    
    let obejtos = JSON.parse(localStorage.getItem("ids"));
    let numTotal = obejtos.length;

    let encontrado = false;
    let index = 0;
    obejtos.forEach((e) => {
        if (e.id == idP) {
            encontrado = true;
            elemento.value = "A\u00F1adir a carretilla";
            elemento.style.backgroundColor = "#E28762";
            obejtos.splice(index, 1);
            localStorage.setItem("ids", JSON.stringify(obejtos));

            nPROD.textContent = numTotal-1;
            return;
        }
        index++;
    });
    if (!encontrado) {
        let obj = {
            id: idP,
            estado: true
        }
        obejtos.push(obj);
        localStorage.setItem("ids", JSON.stringify(obejtos));
        elemento.value = "A\u00F1adido";
        elemento.style.backgroundColor = "#FF4A4D";


        nPROD.textContent = numTotal+1;
    }
}
